using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingVisualizer.Sorters
{
    class SelectionSorter : Sorter
    {
        public override void Sort( SortingArray array )
        {
            for( int i = 0; i < array.Length; i++ )
            {
                int minIndex = i;
                for( int j = i + 1; j < array.Length; j++ )
                {
                    if( array.CompareValuesAt( j, minIndex ) < 0 )
                    {
                        minIndex = j;
                    }
                }
                if( minIndex != i )
                {
                    array.Swap( i, minIndex );
                }
            }
        }
    }
}
