using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingVisualizer.Sorters
{
    class ShellSorter : Sorter
    {
        private static readonly int[] GAPS = new int[] { 701, 301, 132, 57, 23, 10, 4, 1 };

        public override void Sort( IList<int> collection )
        {
            foreach( int gap in GAPS )
            {
                for( int i = gap; i < collection.Count; i++ )
                {
                    int temp = collection[ i ];
                    int j;
                    for( j = i; j >= gap && collection[ j - gap] > temp; j -= gap )
                    {
                        collection[ j ] = collection[ j - gap ];
                    }
                    collection[ j ] = temp;
                }
            }
        }
    }
}
