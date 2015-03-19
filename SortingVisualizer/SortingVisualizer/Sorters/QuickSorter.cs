using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingVisualizer.Sorters
{
    class QuickSorter : Sorter
    {
        public override void Sort( IList<int> collection )
        {
            QuickSort( collection, 0, collection.Count - 1 );
        }

        private void QuickSort( IList<int> collection, int low, int high )
        {
            if( low < high )
            {
                int storeIndex = Partition( collection, low, high );
                QuickSort( collection, low, storeIndex - 1 );
                QuickSort( collection, storeIndex + 1, high );
            }
        }

        private int Partition( IList<int> collection, int low, int high )
        {
            int pivotIndex = ( high + low ) / 2;
            int pivotValue = collection[ pivotIndex ];
            Swap( pivotIndex, high, collection );
            int storeIndex = low;
            for( int i = low; i < high; i++ )
            {
                if( collection[i] < pivotValue )
                {
                    Swap( i, storeIndex, collection );
                    storeIndex++;
                }
            }
            Swap( storeIndex, high, collection );
            return storeIndex;
        }
    }
}
