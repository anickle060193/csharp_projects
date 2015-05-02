using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SortingVisualizer.Sorters;

namespace SortingVisualizer
{
    public partial class SortingVisualizerForm : Form
    {
        private List<SortingVisualizerControl> _sortingVisualizers = new List<SortingVisualizerControl>();
        private IEnumerator<Sorter> _sorters = Sorter.GetSorters().GetEnumerator();

        public SortingVisualizerForm()
        {
            InitializeComponent();
            /*          
            int ROWS = 4;
            int COLUMNS = 4;
            TableLayoutPanel t = new TableLayoutPanel()
            {
                Dock = DockStyle.Fill
            };
            t.RowStyles.Clear();
            t.ColumnStyles.Clear();
            for( int i = 0; i < ROWS; i++ )
            {
                t.RowStyles.Add( new RowStyle( SizeType.Percent, 1.0f / ROWS ) );
            }
            t.RowCount = ROWS;
            for( int i = 0; i < COLUMNS; i++ )
            {
                t.ColumnStyles.Add( new ColumnStyle( SizeType.Percent, 1.0f / COLUMNS ) );
            }
            t.ColumnCount = COLUMNS;
            this.Controls.Add( t );
            for( int r = 0; r < ROWS; r++ )
            {
                for( int c = 0; c < COLUMNS; c++ )
                {
                    SortingVisualizerControl svc = CreateSortingVisualizerControl();
                    svc.DisplayElapsedTime = true;
                    _sortingVisualizers.Add( svc );
                    t.Controls.Add( svc, c, r );
                }
            }
            */
            
            this.Controls.Clear();
            SortingVisualizerControl svc = new SortingVisualizerControl()
            {
                Dock = DockStyle.Fill,
                ArrayLength = 100,
                DisplayReads = false,
                DisplayCompares = false,
                DisplayEditCount = false,
                DisplayElapsedTime = false,
                DisplaySortName = false,
                DisplayWrites = false,
                MaxUpdates = 250,
                UpdateInterval = 1,
                EditUpdateMode = SortingVisualizerControl.UpdateMode.MaxUpdates
            };
            svc.MouseUp += (MouseEventHandler)delegate( object sender, MouseEventArgs e )
            {
                svc.StartSort();
            };
            this.Controls.Add( svc );
        }

        private SortingVisualizerControl CreateSortingVisualizerControl()
        {
            SortingVisualizerControl svc = new SortingVisualizerControl()
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(),
                Margin = new Padding(),
                ArrayLength = 100,
                DisplayReads = false,
                DisplayCompares = false,
                DisplayEditCount = true,
                DisplayElapsedTime = false,
                DisplaySortName = false,
                DisplayWrites = false,
                EditsPerTick = 1,
                EditUpdateMode = SortingVisualizerControl.UpdateMode.UpdatesPerTick
            };
            Sorter sorter = null;
            while( sorter == null )
            {
                if( !_sorters.MoveNext() )
                {
                    _sorters = Sorter.GetSorters().GetEnumerator();
                    _sorters.MoveNext();
                }
                if( _sorters.Current.IsWorking )
                {
                    sorter = _sorters.Current;
                }
            }
            svc.Sorter = sorter;
            svc.MouseUp += SortingVisualizer_MouseUp;
            return svc;
        }

        private void SortingVisualizer_MouseUp( object sender, MouseEventArgs e )
        {
            if( e.Button == MouseButtons.Left )
            {
                foreach( SortingVisualizerControl svc in _sortingVisualizers )
                {
                    svc.StopSorting();
                }
                foreach( SortingVisualizerControl svc in _sortingVisualizers )
                {
                    svc.StartSort();
                }
            }
        }
    }
}
