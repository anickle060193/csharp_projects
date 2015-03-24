using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingVisualizer.Sorters
{
    class BlockSorter : Sorter
    {
        public override bool IsWorking { get { return true; } }

        public override void Sort( SortingArray array )
        {
            int powerOfTwo = FloorPowerOfTwo( array.Length );
            float scale = (float)array.Length / powerOfTwo;

            for( int merge = 0; merge < powerOfTwo; merge += 16 )
            {
                int start = (int)( merge * scale );
                int end = (int)( ( merge + 16 ) * scale );
                InsertionSort( array, start, end );
            }

            for( int length = 16; length < powerOfTwo; length *= 2 )
            {
                for( int merge = 0; merge < powerOfTwo; merge += length * 2 )
                {
                    int start = (int)( merge * scale );
                    int mid = (int)( ( merge + length ) * scale );
                    int end = (int)( ( merge + length * 2 ) * scale );
                    if( array.CompareValuesAt( end - 1, start ) < 0 )
                    {
                        Rotate( array, mid - start, start, end );
                    }
                    else if( array.CompareValuesAt( mid - 1, mid ) > 0 )
                    {
                        Merge( start, mid, end, array );
                    }
                }
            }
        }

        private void Reverse( SortingArray array, int start, int end )
        {
            int i = start;
            int j = end - 1;
            while( i < j )
            {
                array.Swap( i, j );
                i++;
                j--;
            }
        }

        private void Rotate( SortingArray array, int amount, int start, int end )
        {
            Reverse( array, start, end );
            Reverse( array, start, start + amount );
            Reverse( array, start + amount, end );
        }

        private int FloorPowerOfTwo( Int32 x )
        {
            x |= x >> 1;
            x |= x >> 2;
            x |= x >> 4;
            x |= x >> 8;
            x |= x >> 16;
            return x - ( x >> 1 );
        }

        private void InsertionSort( SortingArray array, int start, int end )
        {
            for( int i = start + 1; i < end; i++ )
            {
                int x = array[ i ];
                int j = i;
                while( j > start && array.CompareValues( array[ j - 1 ], x ) > 0 )
                {
                    array[ j ] = array[ j - 1 ];
                    j--;
                }
                array[ j ] = x;
            }
        }

        private void BlockSwap( SortingArray array, int start1, int start2, int length )
        {
            for( int i = 0; i < length; i++ )
            {
                array.Swap( start1 + i, start2 + i );
            }
        }

        private void Merge( int leftStart, int rightStart, int end, SortingArray array )
        {
            int leftIndex = leftStart;
            int rightIndex = rightStart;
            while( leftIndex < rightIndex && rightIndex < end )
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
