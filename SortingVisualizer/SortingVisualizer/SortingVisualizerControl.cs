using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using SortingVisualizer.Sorters;
using System.Drawing.Text;

namespace SortingVisualizer
{
    public partial class SortingVisualizerControl : UserControl
    {
        private readonly Random _random = new Random();
        private static readonly Font _modificationsFont = new Font( "Courier", 72 );
        private static readonly SolidBrush _modificationBrush = new SolidBrush( Color.FromArgb( 100, Color.Black ) );
        private MenuItem _currentMenuItem;
        private Sorter _sorter;
        private int[] _array;
        private SortRecord[] _history;
        private int _currentRecord;

        public int MaxUpdates { get; private set; }

        public int UpdateInterval
        {
            get { return uxUpdateTimer.Interval; }
            private set { uxUpdateTimer.Interval = value; }
        }

        public SortingVisualizerControl()
        {
            InitializeComponent();

            ResizeRedraw = true;
            _array = new int[ 1000 ];
            FillArray();
            UpdateInterval = 1;
            MaxUpdates = 250;

            Type[] sorterTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany( assembly => assembly.GetTypes() )
                .Where( type => type.IsSubclassOf( typeof( Sorter ) ) )
                .ToArray();
            List<Sorter> sorters = new List<Sorter>();
            foreach( Type sorterType in sorterTypes )
            {
                Sorter sorter = (Sorter)Activator.CreateInstance( sorterType );
                sorters.Add( sorter );
            }

            _sorter = sorters[ 0 ];

            ContextMenu = new ContextMenu();
            foreach( Sorter sorter in sorters )
            {
                MenuItem item = new MenuItem();
                item.Text = sorter.ToString();
                item.Tag = sorter;
                item.Click += ContextMenuItem_Click;
                if( sorter.Equals( _sorter ) )
                {
                    item.Checked = true;
                    _currentMenuItem = item;
                }
                ContextMenu.MenuItems.Add( item );
            }
        }

        private void ContextMenuItem_Click( object sender, EventArgs e )
        {
            MenuItem item = sender as MenuItem;
            if( item != null )
            {
                Sorter sorter = item.Tag as Sorter;
                if( sorter != null )
                {
                    _sorter = sorter;
                    item.Checked = true;
                    _currentMenuItem.Checked = false;
                    _currentMenuItem = item;
                }
            }
        }

        public void FillArray()
        {
            for( int i = 0; i < _array.Length; i++ )
            {
                _array[ i ] = i + 1;
            }
            Invalidate();
        }

        public void FillArrayReverse()
        {
            for( int i = 0; i < _array.Length; i++ )
            {
                _array[ i ] = _array.Length - i;
            }
            Invalidate();
        }

        public void RandomizeArray()
        {
            for( int i = 0; i < _array.Length; i++ )
            {
                int index = _random.Next( _array.Length );
                int temp = _array[ index ];
                _array[ index ] = _array[ i ];
                _array[ i ] = temp;
            }
            Invalidate();
        }

        public void DisplaySort()
        {
            uxUpdateTimer.Stop();
            FillArray();
            RandomizeArray();
            _history = SorterRecorder.RecordSort( _array, _sorter );
            PlayHistory();
        }

        public void PlayHistory()
        {
            _currentRecord = 0;
            uxUpdateTimer.Start();
        }

        protected override void OnPaint( PaintEventArgs e )
        {
            if( _array.Length > 0 )
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;
                float columnWidth = (float)this.Width / _array.Length;
                float rowHeight = (float)this.Height / ( _array.Length + 1 );
                SolidBrush b = new SolidBrush( Color.White );
                for( int i = 0; i < _array.Length; i++ )
                {
                    int value = _array[ i ];
                    float width = columnWidth;
                    float height = value * rowHeight;
                    float x = i * columnWidth;
                    float y = this.Height - height;
                    b.Color = Utilities.ColorFromHSL( (double)( value - 1 ) / _array.Length, 0.5, 0.5 );
                    e.Graphics.FillRectangle( b, x, y, width, height );
                }
            }
            OutputModifications( e.Graphics );
        }

        private void OutputModifications( Graphics g )
        {
            g.TextRenderingHint = TextRenderingHint.AntiAlias;
            String modifications = _currentRecord.ToString( "#,##0" );
            SizeF modSize = g.MeasureString( modifications, _modificationsFont );
            float modX = ( this.Width - modSize.Width ) / 2.0f;
            float modY = ( this.Height - modSize.Height ) / 2.0f;
            g.DrawString( modifications, _modificationsFont, _modificationBrush, modX, modY );
        }

        private void uxUpdateTimer_Tick( object sender, EventArgs e )
        {
            int updates = Math.Max( 1, (int)( (float)_history.Length / MaxUpdates ) );
            for( int i = 0; i < updates; i++ )
            {
                if( _currentRecord < _history.Length )
                {
                    _history[ _currentRecord ].ApplyRecord( _array );
                    _currentRecord++;
                }
                else
                {
                    uxUpdateTimer.Stop();
                    break;
                }
            }
            Invalidate();
        }

        private void SortingVisualizerControl_MouseUp( object sender, MouseEventArgs e )
        {
            if( e.Button == MouseButtons.Left )
            {
                DisplaySort();
            }
        }
    }
}
