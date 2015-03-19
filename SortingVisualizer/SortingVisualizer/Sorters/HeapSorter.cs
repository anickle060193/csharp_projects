using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingVisualizer.Sorters
{
    class HeapSorter : Sorter
    {
        public override void Sort( IList<int> collection )
        {
            Heapify( collection );

            int end = collection.Count - 1;
            while( end > 0 )
            {
                Swap( end, 0, collection );
                end--;
                SiftDown( collection, 0, end );
            }
        }

        private void Heapify( IList<int> collection )
        {
            int start = ( ( collection.Count - 2 ) / 2 );
            while( start >= 0 )
            {
                SiftDown( collection, start, collection.Count - 1 );
                start--;
            }
        }

        private void SiftDown( IList<int> collection, int start, int end )
        {
            int root = start;

            while( root * 2 + 1 <= end )
            {
                int child = root * 2 + 1;
                int swap = root;
                if( collection[ swap ] < collection[ child ] )
                {
                    swap = child;
                }
                if( child + 1 <= end && collection[ swap ] < collection[ child + 1 ] )
                {
                    swap = child + 1;
                }
                if( swap == root )
                {
                    return;
                }
                else
                {
                    Swap( root, swap, collection );
                    root = swap;
                }
            }
        }
    }
}
