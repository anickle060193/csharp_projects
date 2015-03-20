using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingVisualizer.Sorters
{
    class GnomeSorter : Sorter
    {
        public override void Sort( SortingArray array )
        {
            int index = 1;
            while( index < array.Length )
            {
                if( array.CompareValuesAt( index, index - 1 ) > 0 )
                {
                    index++;
                }
                else
                {
                    array.Swap( index, index - 1 );
                    if( index > 1 )
                    {
                        index--;
                    }
                }
            }
        }
    }
}
