using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingVisualizer.Sorters
{
    class QuickSorter : Sorter
    {
        public override bool IsWorking { get { return true; } }

        public override void Sort( SortingArray array )
        {
            QuickSort( array, 0, array.Length - 1 );
        }

        private void QuickSort( SortingArray array, int low, int high )
        {
            if( low < high )
            {
                int storeIndex = Partition( array, low, high );
                QuickSort( array, low, storeIndex - 1 );
                QuickSort( array, storeIndex + 1, high );
            }
        }

        private int Partition( SortingArray array, int low, int high )
        {
            int pivotIndex = ( high + low ) / 2;
            int pivotValue = array[ pivotIndex ];
            array.Swap( pivotIndex, high );
            int storeIndex = low;
            for( int i = low; i < high; i++ )
            {
                if( array.CompareValues( array[ i ], pivotValue ) < 0 )
                {
                    array.Swap( i, storeIndex );
                    storeIndex++;
                }
            }
            array.Swap( storeIndex, high );
            return storeIndex;
        }
    }
}
