using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingVisualizer.Sorters
{
    class CombSorter : Sorter
    {
        public override void Sort( IList<int> collection )
        {
            int gap = collection.Count;
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
                for( int i = 0; i + gap < collection.Count; i++ )
                {
                    if( collection[ i ] > collection[ i + gap ] )
                    {
                        Swap( i, i + gap, collection );
                        swapped = true;
                    }
                }
            }
        }
    }
}
