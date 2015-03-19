using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingVisualizer.Sorters
{
    class InPlaceMSDRadixSorter : Sorter
    {
        public override void Sort( IList<int> collection )
        {
            InPlaceMSDRadixSort( collection, 0, collection.Count, collection.Max().ToString().Length * 8 );
        }

        private void InPlaceMSDRadixSort( IList<int> collection, int start, int end, int bit )
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
                int bin = GetBit( collection[ bin0 + 1 ], bit );
                if( bin == 1 )
                {
                    Swap( bin0 + 1, bin1 - 1, collection );
                    bin1--;
                }
                else
                {
                    bin0++;
                }
            }
            InPlaceMSDRadixSort( collection, start, bin0 + 1, bit - 1 );
            InPlaceMSDRadixSort( collection, bin1, end, bit - 1 );
        }

        private int GetBit( int number, int bit )
        {
            return ( number & ( 1 << bit - 1 ) ) != 0 ? 1 : 0;
        }
    }
}
