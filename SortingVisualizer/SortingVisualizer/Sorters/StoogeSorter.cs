using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingVisualizer.Sorters
{
    class StoogeSorter : Sorter
    {
        public override bool IsWorking { get { return true; } }

        public override void Sort( SortingArray array )
        {
            StoogeSort( array, 0, array.Length - 1 );
        }

        private void StoogeSort( SortingArray array, int lo, int hi )
        {
            if( array.CompareValuesAt( hi, lo ) < 0 )
            {
                array.Swap( lo, hi );
            }

            if( hi - lo + 1 > 2 )
            {
                int third = ( hi - lo + 1 ) / 3;
                StoogeSort( array, lo, hi - third );
                StoogeSort( array, lo + third, hi );
                StoogeSort( array, lo, hi - third );
            }
        }
    }
}
