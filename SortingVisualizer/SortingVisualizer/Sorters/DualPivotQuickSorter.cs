using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingVisualizer.Sorters
{
    class DualPivotQuickSorter : Sorter
    {
        public override bool IsWorking { get { return true; } }

        public override void Sort( SortingArray array )
        {
            DualPivotQuickSort( array, 0, array.Length - 1 );
        }

        private void DualPivotQuickSort( SortingArray array, int lo, int hi )
        {
            if( hi <= lo )
            {
                return;
            }

            if( array.CompareValuesAt( hi, lo ) < 0 )
            {
                array.Swap( lo, hi );
            }

            int lt = lo + 1;
            int gt = hi - 1;
            int i = lo + 1;
            while( i <= gt )
            {
                if( array.CompareValuesAt( i, lo ) < 0 )
                {
                    array.Swap( lt++, i++ );
                }
                else if( array.CompareValuesAt( hi, i ) < 0 )
                {
                    array.Swap( i, gt-- );
                }
                else
                {
                    i++;
                }
            }
            array.Swap( lo, --lt );
            array.Swap( hi, ++gt );

            DualPivotQuickSort( array, lo, lt - 1 );
            if( array.CompareValuesAt( lt, gt ) < 0 )
            {
                DualPivotQuickSort( array, lt + 1, gt - 1 );
            }
            DualPivotQuickSort( array, gt + 1, hi );
        }
    }
}
