using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingVisualizer.Sorters
{
    class HeapSorter : Sorter
    {
        public override bool IsWorking { get { return true; } }

        public override void Sort( SortingArray array )
        {
            Heapify( array );

            int end = array.Length - 1;
            while( end > 0 )
            {
                array.Swap( end, 0 );
                end--;
                SiftDown( array, 0, end );
            }
        }

        private void Heapify( SortingArray array )
        {
            int start = ( ( array.Length - 2 ) / 2 );
            while( start >= 0 )
            {
                SiftDown( array, start, array.Length - 1 );
                start--;
            }
        }

        private void SiftDown( SortingArray array, int start, int end )
        {
            int root = start;

            while( root * 2 + 1 <= end )
            {
                int child = root * 2 + 1;
                int swap = root;
                if( array.CompareValuesAt( swap, child ) < 0 )
                {
                    swap = child;
                }
                if( child + 1 <= end && array.CompareValuesAt( swap, child + 1 ) < 0 )
                {
                    swap = child + 1;
                }
                if( swap == root )
                {
                    return;
                }
                else
                {
                    array.Swap( root, swap );
                    root = swap;
                }
            }
        }
    }
}
