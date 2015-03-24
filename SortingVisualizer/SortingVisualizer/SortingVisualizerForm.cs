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
            for( int r = 0; r < uxTableLayout.RowCount; r++ )
            {
                for( int c = 0; c < uxTableLayout.ColumnCount; c++ )
                {
                    SortingVisualizerControl svc = CreateSortingVisualizerControl();
                    _sortingVisualizers.Add( svc );
                    uxTableLayout.Controls.Add( svc, c, r );
                }
            }
            */
            this.Controls.Clear();
            SortingVisualizerControl svc = new SortingVisualizerControl()
            {
                Dock = DockStyle.Fill,
                ArrayLength = 100,
                DisplayReads = true,
                DisplayCompares = true,
                DisplayEditCount = false,
                DisplayElapsedTime = true,
                DisplaySortName = true,
                DisplayWrites = true,
                MaxUpdates = 250,
                EditUpdateMode = SortingVisualizerControl.UpdateMode.MaxUpdates,
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
                DisplayEditCount = false,
                DisplayElapsedTime = false,
                DisplaySortName = true,
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
