using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingVisualizer.Sorters
{
    class BlockSorter : Sorter
    {
        public override void Sort( IList<int> collection )
        {
            int powerOfTwo = FloorPowerOfTwo( collection.Count );
            float scale = (float)collection.Count / powerOfTwo;

            for( int merge = 0; merge < powerOfTwo; merge += 16 )
            {
                int start = (int)( merge * scale );
                int end = (int)( ( merge + 16 ) * scale );
                InsertionSort( collection, start, end );
            }

            for( int length = 16; length < powerOfTwo; length *= 2 )
            {
                for( int merge = 0; merge < powerOfTwo; merge += length * 2 )
                {
                    int start = (int)( merge * scale );
                    int mid = (int)( ( merge + length ) * scale );
                    int end = (int)( ( merge + length * 2 ) * scale );
                    if( collection[ end - 1 ] < collection[ start ] )
                    {
                        Rotate( collection, mid - start, start, end );
                    }
                    else if( collection[mid - 1] > collection[mid] )
                    {
                        Merge( collection, start, mid - start, mid, end - mid );
                    }
                }
            }
        }

        private void Reverse( IList<int> collection, int start, int end )
        {
            int i = start;
            int j = end - 1;
            while( i < j )
            {
                Swap( i, j, collection );
                i++;
                j--;
            }
        }

        private void Rotate( IList<int> collection, int amount, int start, int end )
        {
            Reverse( collection, start, end );
            Reverse( collection, start, start + amount );
            Reverse( collection, start + amount, end );
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

        private void InsertionSort( IList<int> collection, int start, int end )
        {
            for( int i = start + 1; i < end; i++ )
            {
                int x = collection[ i ];
                int j = i;
                while( j > start && collection[j - 1] > x )
                {
                    collection[ j ] = collection[ j - 1 ];
                    j--;
                }
                collection[ j ] = x;
            }
        }

        private void BlockSwap( IList<int> collection, int start1, int start2, int length )
        {
            for( int i = 0; i < length; i++ )
            {
                Swap( start1 + i, start2 + i, collection );
            }
        }

        private void Merge( IList<int> collection, int leftStart, int leftLength, int rightStart, int rightLength )
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
