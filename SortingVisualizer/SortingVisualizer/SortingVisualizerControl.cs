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
using System.Diagnostics;

namespace SortingVisualizer
{
    public partial class SortingVisualizerControl : UserControl
    {
        private static readonly Font _mainFont = new Font( "Courier", 72 );
        private static readonly SolidBrush _mainBrush = new SolidBrush( Color.FromArgb( 100, Color.Black ) );
        private static readonly Font _secondaryFont = new Font( "Courier", 20 );
        private static readonly SolidBrush _secondaryBrush = new SolidBrush( Color.FromArgb( 100, Color.Black ) );
        
        private readonly Random _random = new Random();
        private readonly SolidBrush _barBrush = new SolidBrush( Color.White );

        private BackgroundWorker _worker;
        private MenuItem _currentMenuItem;
        private Sorter _sorter;
        private int[] _array;
        private int _updatesPerTick;
        private SortEdit _currentEdit;
        private IEnumerator<SortEdit> _historyEnumerator;

        public int MaxUpdates { get; private set; }

        public int UpdateInterval
        {
            get { return uxUpdateTimer.Interval; }
            private set { uxUpdateTimer.Interval = value; }
        }

        public SortingVisualizerControl()
        {
            InitializeComponent();

            this.ResizeRedraw = true;
            _array = new int[ 1024 ];
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

            _worker = new BackgroundWorker();
            _worker.DoWork += (DoWorkEventHandler)delegate( object sender, DoWorkEventArgs e )
            {
                SortingArray a = new SortingArray( _array );
                _sorter.Sort( a );
                Debug.Assert( a.IsSorted, _sorter.ToString() + " did not properly sort the array." );
                _updatesPerTick = (int)( (float)a.History.Count / MaxUpdates );
                if( _historyEnumerator != null )
                {
                    _historyEnumerator.Dispose();
                }
                _historyEnumerator = a.History.GetEnumerator();
            };
            _worker.RunWorkerCompleted += (RunWorkerCompletedEventHandler)delegate( object sender, RunWorkerCompletedEventArgs e )
            {
                PlayHistory();
            };

            this.Disposed += (EventHandler)delegate( object sender, EventArgs e )
            {
                if( _historyEnumerator != null )
                {
                    _historyEnumerator.Dispose();
                }
                _barBrush.Dispose();
            };
        }

        private void ContextMenuItem_Click( object sender, EventArgs e )
        {
            MenuItem item = sender as MenuItem;
            if( item != null )
            {
                if( !item.Checked )
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

        public void FillArrayDuplicates()
        {
            for( int i = 0; i < _array.Length; i++ )
            {
                _array[ i ] = _random.Next( (int)( _array.Length * 0.1f ) ) + _array.Length / 2;
            }
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

        public void StartSort()
        {
            if( !_worker.IsBusy )
            {
                uxUpdateTimer.Stop();
                _currentEdit = null;
                FillArray();
                RandomizeArray();
                _worker.RunWorkerAsync();
            }
        }

        public void PlayHistory()
        {
            uxUpdateTimer.Start();
        }

        protected override void OnPaint( PaintEventArgs e )
        {
            if( _array.Length > 0 )
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;
                float columnWidth = (float)this.Width / _array.Length;
                float rowHeight = (float)this.Height / _array.Length;
                for( int i = 0; i < _array.Length; i++ )
                {
                    int value = _array[ i ];
                    float width = columnWidth;
                    float height = value * rowHeight;
                    float x = i * columnWidth;
                    float y = this.Height - height;
                    _barBrush.Color = Utilities.ColorFromHSL( (double)( value - 1 ) / _array.Length, 0.5, 0.5 );
                    e.Graphics.FillRectangle( _barBrush, x, y, width, height );
                }
            }
            OutputNumbers( e.Graphics );
        }

        private void OutputNumbers( Graphics g )
        {
            int modifications = 0;
            int compares = 0;
            int reads = 0;
            int writes = 0;
            long elapsedTime = 0;
            if( _currentEdit != null )
            {
                modifications = _currentEdit.EditNumber;
                compares = _currentEdit.Comparisons;
                reads = _currentEdit.Reads;
                writes = _currentEdit.Writes;
                elapsedTime = _currentEdit.ElapsedTime;
            }
            g.TextRenderingHint = TextRenderingHint.AntiAlias;

            float verticalPadding = this.Height * 0.03f;
            float horizontalPadding = this.Width * 0.02f;
            float labelY = verticalPadding;
            float labelHeight = g.MeasureString( "Test Label", _secondaryFont ).Height;
            float textY = labelY + labelHeight;

            // Comparisons Label
            String s = "Comparisons";
            SizeF size = g.MeasureString( s, _secondaryFont );
            float x = horizontalPadding;
            float y = labelY;
            g.DrawString( s, _secondaryFont, _secondaryBrush, x, y );
            // Comparisons Text
            s = compares.ToString( "#,##0" );
            size = g.MeasureString( s, _secondaryFont );
            x = horizontalPadding;
            y = textY;
            g.DrawString( s, _secondaryFont, _secondaryBrush, x, y );

            // Reads Label
            s = "Reads";
            size = g.MeasureString( s, _secondaryFont );
            x = ( this.Width - size.Width ) / 2;
            y = labelY;
            g.DrawString( s, _secondaryFont, _secondaryBrush, x, y );
            // Reads Text
            s = reads.ToString( "#,##0" );
            size = g.MeasureString( s, _secondaryFont );
            x = ( this.Width - size.Width ) / 2;
            y = textY;
            g.DrawString( s, _secondaryFont, _secondaryBrush, x, y );

            // Writes Label
            s = "Writes";
            size = g.MeasureString( s, _secondaryFont );
            x = this.Width - size.Width - horizontalPadding;
            y = labelY;
            g.DrawString( s, _secondaryFont, _secondaryBrush, x, y );
            // Writes Text
            s = writes.ToString( "#,##0" );
            size = g.MeasureString( s, _secondaryFont );
            x = this.Width - size.Width - horizontalPadding;
            y = textY;
            g.DrawString( s, _secondaryFont, _secondaryBrush, x, y );

            // Modifications
            s = modifications.ToString( "#,##0" );
            size = g.MeasureString( s, _mainFont );
            x = ( this.Width - size.Width ) / 2.0f;
            y = ( this.Height - size.Height ) / 2.0f;
            g.DrawString( s, _mainFont, _mainBrush, x, y );

            // Elapsed Time
            s = "Elapsed Time: " + TimeSpan.FromTicks( elapsedTime ).ToString( @"ss\.ffff" );
            size = g.MeasureString( s, _secondaryFont );
            x = ( this.Width - size.Width ) / 2;
            y = this.Height - size.Height - verticalPadding;
            g.DrawString( s, _secondaryFont, _secondaryBrush, x, y );
        }

        private void uxUpdateTimer_Tick( object sender, EventArgs e )
        {
            for( int i = 0; i < _updatesPerTick; i++ )
            {
                if( _historyEnumerator.MoveNext() )
                {
                    _currentEdit = _historyEnumerator.Current;
                    _currentEdit.ApplyRecord( _array );
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
                StartSort();
            }
        }
    }
}
