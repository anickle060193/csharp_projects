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
        public enum UpdateMode { MaxUpdates, UpdatesPerTick }

        private static readonly Font _mainFont = new Font( "Courier", 72 );
        private static readonly SolidBrush _mainBrush = new SolidBrush( Color.FromArgb( 100, Color.Black ) );
        private static readonly Font _secondaryFont = new Font( "Courier", 20 );
        private static readonly SolidBrush _secondaryBrush = new SolidBrush( Color.FromArgb( 100, Color.Black ) );
        
        private readonly Random _random = new Random();
        private readonly SolidBrush _barBrush = new SolidBrush( Color.White );

        private readonly BackgroundWorker _worker;
        private MenuItem _currentMenuItem;
        private Sorter _sorter;
        private int[] _array;
        private SortingArray _sortingArray;
        private SortEdit _currentEdit;
        private IEnumerator<SortEdit> _historyEnumerator;
        private bool _cancelled;
        private bool _displayReads;
        private bool _displayWrites;
        private bool _displayCompares;
        private bool _displayEditCount;
        private bool _displaySortName;
        private bool _displayElapsedTime;

        public Sorter Sorter
        {
            get { return _sorter; }
            set
            {
                _sorter = value;
                StopSorting();
                if( _currentMenuItem != null )
                {
                    _currentMenuItem.Checked = false;
                }
                foreach( MenuItem item in ContextMenu.MenuItems )
                {
                    if( item.Tag.Equals( _sorter ) )
                    {
                        _currentMenuItem = item;
                        _currentMenuItem.Checked = true;
                    }
                }
                Invalidate();
            }
        }

        public int ArrayLength
        {
            get { return _array.Length; }
            set
            {
                StopSorting();
                _array = new int[ value ];
                FillArray();
                Invalidate();
            }
        }

        public bool DisplayReads
        {
            get { return _displayReads; }
            set
            {
                _displayReads = value;
                Invalidate();
            }
        }

        public bool DisplayWrites
        {
            get { return _displayWrites; }
            set
            {
                _displayWrites = value;
                Invalidate();
            }
        }

        public bool DisplayCompares
        {
            get { return _displayCompares; }
            set
            {
                _displayCompares = value;
                Invalidate();
            }
        }

        public bool DisplayEditCount
        {
            get { return _displayEditCount; }
            set
            {
                _displayEditCount = value;
                Invalidate();
            }
        }

        public bool DisplaySortName
        {
            get { return _displaySortName; }
            set
            {
                _displaySortName = value;
                Invalidate();
            }
        }

        public bool DisplayElapsedTime
        {
            get { return _displayElapsedTime; }
            set
            {
                _displayElapsedTime = value;
                Invalidate();
            }
        }

        public UpdateMode EditUpdateMode { get; set; }

        public int MaxUpdates { get; set; }

        public int EditsPerTick { get; set; }

        public int UpdateInterval
        {
            get { return uxUpdateTimer.Interval; }
            set { uxUpdateTimer.Interval = value; }
        }

        public SortingVisualizerControl()
        {
            InitializeComponent();

            this.ResizeRedraw = true;
            ArrayLength = 100;
            UpdateInterval = 1;
            MaxUpdates = 250;
            EditsPerTick = 10;
            EditUpdateMode = UpdateMode.MaxUpdates;

            _sortingArray = new SortingArray();

            IList<Sorter> sorters = Sorter.GetSorters();

            ContextMenu = new ContextMenu();
            foreach( Sorter sorter in sorters )
            {
                if( sorter.IsWorking )
                {
                    MenuItem item = new MenuItem();
                    item.Text = sorter.ToString();
                    item.Tag = sorter;
                    item.Click += ContextMenuItem_Click;
                    ContextMenu.MenuItems.Add( item );
                }
            }

            Sorter = sorters.First( s => s.IsWorking );

            _worker = new BackgroundWorker();
            _worker.DoWork += (DoWorkEventHandler)delegate( object sender, DoWorkEventArgs e )
            {
                _sortingArray.Sort( Sorter, _array );
                Debug.Assert( _sortingArray.IsSorted, Sorter.ToString() + " did not properly sort the array." );
                if( _historyEnumerator != null )
                {
                    _historyEnumerator.Dispose();
                }
                _historyEnumerator = _sortingArray.History.GetEnumerator();
            };
            _worker.RunWorkerCompleted += (RunWorkerCompletedEventHandler)delegate( object sender, RunWorkerCompletedEventArgs e )
            {
                if( !_cancelled )
                {
                    PlayHistory();
                }
                else
                {
                    _cancelled = false;
                }
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
                Sorter = (Sorter)item.Tag;
            }
        }

        private void FillArray()
        {
            for( int i = 0; i < _array.Length; i++ )
            {
                _array[ i ] = i + 1;
            }
            Invalidate();
        }

        private void FillArrayReverse()
        {
            for( int i = 0; i < _array.Length; i++ )
            {
                _array[ i ] = _array.Length - i;
            }
            Invalidate();
        }

        private void FillArrayDuplicates()
        {
            for( int i = 0; i < _array.Length; i++ )
            {
                _array[ i ] = _random.Next( (int)( _array.Length * 0.1f ) ) + _array.Length / 2;
            }
        }

        private void RandomizeArray()
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
                _cancelled = false;
                uxUpdateTimer.Stop();
                _currentEdit = null;
                FillArray();
                RandomizeArray();
                _worker.RunWorkerAsync();
            }
        }

        public void StopSorting()
        {
            _cancelled = true;
            uxUpdateTimer.Stop();
        }

        private void PlayHistory()
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
            OutputText( e.Graphics );
        }

        private void OutputText( Graphics g )
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
            String s;
            SizeF size;
            float x;
            float y;
            if( DisplayCompares )
            {
                // Comparisons Label
                s = "Comparisons";
                size = g.MeasureString( s, _secondaryFont );
                x = horizontalPadding;
                y = labelY;
                g.DrawString( s, _secondaryFont, _secondaryBrush, x, y );
                // Comparisons Text
                s = compares.ToString( "#,##0" );
                size = g.MeasureString( s, _secondaryFont );
                x = horizontalPadding;
                y = textY;
                g.DrawString( s, _secondaryFont, _secondaryBrush, x, y );
            }
            if( DisplayReads )
            {
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
            }
            if( DisplayWrites )
            {
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
            }
            if( DisplayElapsedTime )
            {
                // Elapsed Time
                s = "Elapsed Time: " + TimeSpan.FromTicks( elapsedTime ).ToString( @"ss\.ffff" );
                size = g.MeasureString( s, _secondaryFont );
                x = ( this.Width - size.Width ) / 2;
                y = this.Height - size.Height - verticalPadding;
                g.DrawString( s, _secondaryFont, _secondaryBrush, x, y );
            }
            if( DisplaySortName )
            {
                // Sorter Name
                s = Sorter.ToString();
                size = g.MeasureString( s, _secondaryFont );
                x = ( this.Width - size.Width ) / 2.0f;
                y = ( this.Height - size.Height ) / 2.0f;
                g.DrawString( s, _secondaryFont, _secondaryBrush, x, y );
            }
            if( !DisplaySortName && DisplayEditCount )
            {
                // Modifications
                s = modifications.ToString( "#,##0" );
                size = g.MeasureString( s, _mainFont );
                x = ( this.Width - size.Width ) / 2.0f;
                y = ( this.Height - size.Height ) / 2.0f;
                g.DrawString( s, _mainFont, _mainBrush, x, y );
            }
        }

        private void uxUpdateTimer_Tick( object sender, EventArgs e )
        {
            int updates;
            if( EditUpdateMode == UpdateMode.MaxUpdates )
            {
                updates = Math.Max( 1, (int)( (float)_sortingArray.History.Count / MaxUpdates ) );
            }
            else
            {
                updates = EditsPerTick;
            }
            for( int i = 0; i < updates; i++ )
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
    }
}
