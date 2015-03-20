using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingVisualizer.Sorters
{
    class InsertionSorter : Sorter
    {
        public override void Sort( SortingArray array )
        {
            for( int i = 1; i < array.Length; i++ )
            {
                int j = i;
                while( j > 0 && array.CompareValuesAt( j - 1, j ) > 0 )
                {
                    array.Swap( j, j - 1 );
                    j--;
                }
            }
        }
    }
}
