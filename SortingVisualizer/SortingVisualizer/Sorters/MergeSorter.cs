using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingVisualizer.Sorters
{
    class MergeSorter : Sorter
    {
        public override void Sort( IList<int> collection )
        {
            MergeSort( 0, collection.Count, collection );
        }

        private void MergeSort( int start, int length, IList<int> collection )
        {
            if( length <= 1 )
            {
                return;
            }
            int leftStart = start;
            int leftLength = length / 2;
            int rightStart = leftStart + leftLength;
            int rightLength = length - leftLength;
            MergeSort( leftStart, leftLength, collection );
            MergeSort( rightStart, rightLength, collection );
            Merge( leftStart, leftLength, rightStart, rightLength, collection );
        }

        private void Merge( int leftStart, int leftLength, int rightStart, int rightLength, IList<int> collection )
        {
            int leftIndex = leftStart;
            int rightIndex = rightStart;
            while( leftIndex < rightIndex && rightIndex < rightStart + rightLength )
            {
                if( collection[ leftIndex ] < collection[ rightIndex ] )
                {
                    leftIndex++;
                }
                else
                {
                    int temp = collection[ rightIndex ];

                    for( int i = rightIndex; i > leftIndex; i-- )
                    {
                        Swap( i, i - 1, collection );
                    }
                    collection[ leftIndex ] = temp;
                    leftIndex++;
                    rightIndex++;
                }
            }
        }
    }
}
