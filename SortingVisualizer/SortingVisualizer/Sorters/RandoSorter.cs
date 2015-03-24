using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingVisualizer.Sorters
{
    class RandoSorter : Sorter
    {
        private Random R = new Random();

        public override void Sort( SortingArray array )
        {
            while( !array.IsSorted )
            {
                int i = R.Next( array.Length );
                int j = R.Next( array.Length );
                int minIndex, maxIndex;
                if( i < j )
                {
                    minIndex = i;
                    maxIndex = j;
                }
                else
                {
                    minIndex = j;
                    maxIndex = i;
                }
                if( array.CompareValuesAt( minIndex, maxIndex ) > 0 )
                {
                    array.Swap( minIndex, maxIndex );
                }
            }
        }
    }
}
