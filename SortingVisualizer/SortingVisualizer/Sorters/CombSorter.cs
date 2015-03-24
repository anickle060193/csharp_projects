using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingVisualizer.Sorters
{
    class CombSorter : Sorter
    {
        public override bool IsWorking { get { return true; } }

        public override void Sort( SortingArray array )
        {
            int gap = array.Length;
            double shrink = 1.3;
            bool swapped = false;
            while( gap != 1 || swapped )
            {
                gap = (int)( gap / shrink );
                if( gap < 1 )
                {
                    gap = 1;
                }
                swapped = false;
                for( int i = 0; i + gap < array.Length; i++ )
                {
                    if( array.CompareValuesAt( i, i + gap ) > 0 )
                    {
                        array.Swap( i, i + gap );
                        swapped = true;
                    }
                }
            }
        }
    }
}
