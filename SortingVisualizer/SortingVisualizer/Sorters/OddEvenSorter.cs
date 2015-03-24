using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingVisualizer.Sorters
{
    class OddEvenSorter : Sorter
    {
        public override bool IsWorking { get { return true; } }

        public override void Sort( SortingArray array )
        {
            bool sorted = false;
            while( !sorted )
            {
                sorted = true;
                for( int i = 1; i < array.Length - 1; i += 2 )
                {
                    if( array.CompareValuesAt( i, i + 1 ) > 0 )
                    {
                        array.Swap( i, i + 1 );
                        sorted = false;
                    }
                }
                for( int i = 0; i < array.Length - 1; i += 2 )
                {
                    if( array.CompareValuesAt( i, i + 1 ) > 0 )
                    {
                        array.Swap( i, i + 1 );
                        sorted = false;
                    }
                }
            }
        }
    }
}
