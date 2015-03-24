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

        public override bool IsWorking { get { return true; } }

        public override void Sort( SortingArray array )
        {
            foreach( int gap in GAPS )
            {
                for( int i = gap; i < array.Length; i++ )
                {
                    int temp = array[ i ];
                    int j;
                    for( j = i; j >= gap && array.CompareValues( array[ j - gap ], temp ) > 0; j -= gap )
                    {
                        array[ j ] = array[ j - gap ];
                    }
                    array[ j ] = temp;
                }
            }
        }
    }
}
