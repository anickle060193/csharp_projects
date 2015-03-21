using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingVisualizer.Sorters
{
    class BitonicSorter : Sorter
    {
        public override void Sort( SortingArray array )
        {
            BitonicSort( array, 0, array.Length, true );
        }

        private void BitonicSort( SortingArray array, int lo, int n, bool ascending )
        {
            if( n > 1 )
            {
                int k = n / 2;
                BitonicSort( array, lo, k, true );
                BitonicSort( array, lo + k, k, false );
                BitonicMerge( array, lo, n, ascending );
            }
        }

        private void BitonicMerge( SortingArray array, int lo, int n, bool ascending )
        {
            if( n > 1 )
            {
                int k = n / 2;
                for( int i = lo; i < lo + k; i++ )
                {
                    Compare( array, i, i + k, ascending );
                }
                BitonicMerge( array, lo, k, ascending );
                BitonicMerge( array, lo + k, k, ascending );
            }
        }

        private void Compare( SortingArray array, int i, int j, bool ascending )
        {
            if( ascending == ( array.CompareValuesAt( i, j ) > 0 ) )
            {
                array.Swap( i, j );
            }
        }
    }
}
