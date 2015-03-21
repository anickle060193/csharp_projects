using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingVisualizer.Sorters
{
    class ThreeWayQuickSorter : Sorter
    {
        public override void Sort( SortingArray array )
        {
            ThreeWayQuickSort( array, 0, array.Length - 1 );
        }

        private void ThreeWayQuickSort( SortingArray array, int lo, int hi )
        {
            if( hi <= lo )
            {
                return;
            }

            int lt = lo;
            int gt = hi;
            int i = lo + 1;
            int pivotIndex = lo;
            int pivotValue = array[ pivotIndex ];

            while( i <= gt )
            {
                int comp = array.CompareValues( array[i], pivotValue );
                if( comp < 0 )
                {
                    array.Swap( i++, lt++ );
                }
                else if( comp > 0 )
                {
                    array.Swap( i, gt-- );
                }
                else
                {
                    i++;
                }
            }

            ThreeWayQuickSort( array, lo, lt - 1 );
            ThreeWayQuickSort( array, gt + 1, hi );
        }
    }
}
