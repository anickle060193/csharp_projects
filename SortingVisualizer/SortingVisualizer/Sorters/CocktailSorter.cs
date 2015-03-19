using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingVisualizer.Sorters
{
    public class CocktailSorter : Sorter
    {
        public override void Sort( IList<int> collection )
        {
            bool changed;
            do
            {
                changed = false;
                for( int i = 0; i < collection.Count - 1; i++ )
                {
                    if( collection[ i ] > collection[ i + 1 ] )
                    {
                        Swap( i + 1, i, collection );
                        changed = true;
                    }
                }
                if( changed == false )
                {
                    break;
                }
                for( int i = collection.Count - 1; i >= 1; i-- )
                {
                    if( collection[ i ] < collection[ i - 1 ] )
                    {
                        Swap( i, i - 1, collection );
                    }
                }
            }
            while( changed );
        }
    }
}
