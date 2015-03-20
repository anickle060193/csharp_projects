using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingVisualizer.Sorters
{
    class MergeSorter : Sorter
    {
        public override void Sort( SortingArray array )
        {
            MergeSort( 0, array.Length, array );
        }

        private void MergeSort( int start, int length, SortingArray array )
        {
            if( length <= 1 )
            {
                return;
            }
            int leftStart = start;
            int leftLength = length / 2;
            int rightStart = leftStart + leftLength;
            int rightLength = length - leftLength;
            MergeSort( leftStart, leftLength, array );
            MergeSort( rightStart, rightLength, array );
            Merge( leftStart, leftLength, rightStart, rightLength, array );
        }

        private void Merge( int leftStart, int leftLength, int rightStart, int rightLength, SortingArray array )
        {
            int leftIndex = leftStart;
            int rightIndex = rightStart;
            while( leftIndex < rightIndex && rightIndex < rightStart + rightLength )
            {
                if( array.CompareValuesAt( leftIndex, rightIndex ) < 0 )
                {
                    leftIndex++;
                }
                else
                {
                    int temp = array[ rightIndex ];

                    for( int i = rightIndex; i > leftIndex; i-- )
                    {
                        array.Swap( i, i - 1 );
                    }
                    array[ leftIndex ] = temp;
                    leftIndex++;
                    rightIndex++;
                }
            }
        }
    }
}
