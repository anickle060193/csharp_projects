using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingVisualizer.Sorters
{
    class SeveralUniqueSorter : Sorter
    {
        public override void Sort( SortingArray array )
        {
            int endIndex = array.Length - 1;
            int pos;
            int highValue;
            int swapIndex = 0;
            int newValue;

            do
            {
                highValue = 0;
                pos = -1;
                while( pos < endIndex )
                {
                    pos++;
                    newValue = array[ pos ];

                    if( array.CompareValues( newValue, highValue ) < 0 )
                    {
                        array[ swapIndex ] = newValue;
                        swapIndex++;
                        array[ pos ] = highValue;
                    }
                    else if( array.CompareValues( newValue, highValue ) > 0 )
                    {
                        swapIndex = pos;
                        highValue = array[ pos ];
                    }
                }
                endIndex = swapIndex - 1;
            }
            while( pos >= swapIndex );
        }
    }
}
