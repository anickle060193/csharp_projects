using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingVisualizer.Sorters
{
    class InsertionSorter : Sorter
    {
        public override bool IsWorking { get { return true; } }

        public override void Sort( SortingArray array )
        {
            for( int i = 1; i < array.Length; i++ )
            {
                int temp = array[ i ];
                int j;
                for( j = i; j > 0; j-- )
                {
                    if( array.CompareValues( array[ j - 1 ], temp ) < 0 )
                    {
                        break;
                    }
                    array[ j ] = array[ j - 1 ];
                }
                array[ j ] = temp;
            }
        }
    }
}
