using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingVisualizer.Sorters
{
    class FlashSorter : Sorter
    {
        public override void Sort( SortingArray array )
        {
            throw new NotImplementedException();
        }

        private void InsertionSort( SortingArray array )
        {
            int hold;
            int j;
            for( int i = array.Length - 3; i >= 0; i-- )
            {
                if( array.CompareValuesAt( i + 1, i ) < 0 )
                {
                    hold = array[ i ];
                    j = i;
                    while( array.CompareValues( array[ j + 1 ], hold ) < 0 )
                    {
                        array[ j ] = array[ j + 1 ];
                        j++;
                    }
                    array[ j ] = hold;
                }
            }
        }

        private void PartialFlashSort( SortingArray array )
        {
            int n = array.Length;
            int m = n / 20;
        }
    }
}
