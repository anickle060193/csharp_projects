using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingVisualizer.Sorters
{
    public class BubbleSorter : Sorter
    {
        public override void Sort( SortingArray array )
        {
            bool changed;
            do
            {
                changed = false;
                for( int i = 0; i < array.Length - 1; i++ )
                {
                    if( array.CompareValuesAt( i, i + 1 ) > 0 )
                    {
                        array.Swap( i + 1, i );
                        changed = true;
                    }
                }
            }
            while( changed );
        }
    }
}
