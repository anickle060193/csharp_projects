using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingVisualizer.Sorters
{
    class GnomeSorter : Sorter
    {
        public override void Sort( IList<int> collection )
        {
            int index = 1;
            while( index < collection.Count )
            {
                if( collection[ index ] > collection[ index - 1 ] )
                {
                    index++;
                }
                else
                {
                    Swap( index, index - 1, collection );
                    if( index > 1 )
                    {
                        index--;
                    }
                }
            }
        }
    }
}
