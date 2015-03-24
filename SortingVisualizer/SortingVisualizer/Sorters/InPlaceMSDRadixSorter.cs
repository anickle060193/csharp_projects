using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingVisualizer.Sorters
{
    class InPlaceMSDRadixSorter : Sorter
    {
        public override bool IsWorking { get { return true; } }

        public override void Sort( SortingArray array )
        {
            InPlaceMSDRadixSort( array, 0, array.Length, 64 );
        }

        private void InPlaceMSDRadixSort( SortingArray array, int start, int end, int bit )
        {
            if( bit == 0
             || ( end - start ) == 0 )
            {
                return;
            }

            int bin0 = start - 1;
            int bin1 = end;
            while( bin0 + 1 < bin1 )
            {
                int bin = GetBit( array[ bin0 + 1 ], bit );
                if( bin == 1 )
                {
                    array.Swap( bin0 + 1, bin1 - 1 );
                    bin1--;
                }
                else
                {
                    bin0++;
                }
            }
            InPlaceMSDRadixSort( array, start, bin0 + 1, bit - 1 );
            InPlaceMSDRadixSort( array, bin1, end, bit - 1 );
        }

        private int GetBit( int number, int bit )
        {
            return ( number & ( 1L << ( bit - 1 ) ) ) != 0 ? 1 : 0;
        }
    }
}
