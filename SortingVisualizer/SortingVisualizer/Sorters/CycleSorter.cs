using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingVisualizer.Sorters
{
    class CycleSorter : Sorter
    {
        public override void Sort( SortingArray array )
        {
            for( int cycleStart = 0; cycleStart < array.Length; cycleStart++ )
            {
                int item = array[ cycleStart ];

                int pos = cycleStart;
                for( int i = cycleStart + 1; i < array.Length; i++ )
                {
                    if( array.CompareValues( array[ i ], item ) < 0 )
                    {
                        pos++;
                    }
                }

                if( pos != cycleStart )
                {
                    while( pos != cycleStart )
                    {
                        pos = cycleStart;
                        for( int i = cycleStart + 1; i < array.Length; i++ )
                        {
                            if( array.CompareValues( array[ i ], item ) < 0 )
                            {
                                pos++;
                            }
                        }

                        while( item == array[ pos ] )
                        {
                            pos++;
                        }
                        int temp = item;
                        item = array[ pos ];
                        array[ pos ] = temp;
                    }
                }
            }
        }
    }
}
