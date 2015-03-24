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
        public SortingVisualizerForm()
        {
            InitializeComponent();
            /*
            for( int r = 0; r < uxTableLayout.RowCount; r++ )
            {
                for( int c = 0; c < uxTableLayout.ColumnCount; c++ )
                {
                    uxTableLayout.Controls.Add( new SortingVisualizerControl()
                    {
                        Dock = DockStyle.Fill,
                        DisplayReads = false,
                        DisplayCompares = false,
                        DisplayEditCount = false,
                        DisplayElapsedTime = false,
                        DisplaySortName = true,
                        DisplayWrites = false
                    }, c, r );
                }
            }
            */
            this.Controls.Clear();
            this.Controls.Add( new SortingVisualizerControl()
            {
                Dock = DockStyle.Fill,
                DisplayReads = true,
                DisplayCompares = true,
                DisplayEditCount = true,
                DisplayElapsedTime = true,
                DisplaySortName = false,
                DisplayWrites = true
            } );
        }
    }
}
