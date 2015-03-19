using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingVisualizer.Sorters
{
    class OddEvenSorter : Sorter
    {
        public override void Sort( IList<int> collection )
        {
            bool sorted = false;
            while( !sorted )
            {
                sorted = true;
                for( int i = 1; i < collection.Count - 1; i += 2 )
                {
                    if( collection[ i ] > collection[ i + 1 ] )
                    {
                        Swap( i, i + 1, collection );
                        sorted = false;
                    }
                }
                for( int i = 0; i < collection.Count - 1; i += 2 )
                {
                    if( collection[ i ] > collection[ i + 1 ] )
                    {
                        Swap( i, i + 1, collection );
                        sorted = false;
                    }
                }
            }
        }
    }
}
