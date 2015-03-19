using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingVisualizer.Sorters
{
    class InsertionSorter : Sorter
    {
        public override void Sort( IList<int> collection )
        {
            for( int i = 1; i < collection.Count; i++ )
            {
                int j = i;
                while( j > 0 && collection[ j - 1 ] > collection[ j ] )
                {
                    Swap( j, j - 1, collection );
                    j--;
                }
            }
        }
    }
}
