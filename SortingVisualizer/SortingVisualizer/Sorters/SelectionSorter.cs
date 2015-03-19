using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingVisualizer.Sorters
{
    class SelectionSorter : Sorter
    {
        public override void Sort( IList<int> collection )
        {
            for( int i = 0; i < collection.Count; i++ )
            {
                int minIndex = i;
                for( int j = i + 1; j < collection.Count; j++ )
                {
                    if( collection[ j ] < collection[ minIndex ] )
                    {
                        minIndex = j;
                    }
                }
                if( minIndex != i )
                {
                    Swap( i, minIndex, collection );
                }
            }
        }
    }
}
