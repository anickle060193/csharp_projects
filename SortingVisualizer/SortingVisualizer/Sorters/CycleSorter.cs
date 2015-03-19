using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingVisualizer.Sorters
{
    class CycleSorter : Sorter
    {
        public override void Sort( IList<int> collection )
        {
            for( int cycleStart = 0; cycleStart < collection.Count; cycleStart++ )
            {
                int item = collection[ cycleStart ];

                int pos = cycleStart;
                for( int i = cycleStart + 1; i < collection.Count; i++ )
                {
                    if( collection[ i ] < item )
                    {
                        pos++;
                    }
                }

                if( pos != cycleStart )
                {
                    while( pos != cycleStart )
                    {
                        pos = cycleStart;
                        for( int i = cycleStart + 1; i < collection.Count; i++ )
                        {
                            if( collection[ i ] < item )
                            {
                                pos++;
                            }
                        }

                        while( item == collection[ pos ] )
                        {
                            pos++;
                        }
                        int temp = item;
                        item = collection[ pos ];
                        collection[ pos ] = temp;
                    }
                }
            }
        }
    }
}
