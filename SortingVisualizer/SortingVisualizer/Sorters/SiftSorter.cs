using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingVisualizer.Sorters
{
    class SiftSorter : Sorter
    {
        public override void Sort( SortingArray array )
        {
            return;
            double mean = 0;
            for( int i = 0; i < array.Length; i++ )
            {
                mean += i;
            }
            Sift( array, 0, array.Length - 1, mean / array.Length );
        }

        private void Sift( SortingArray array, int left, int right, double sum )
        {
            if( right - left < 2 )
            {
                return;
            }
            double meanLeft = 0;
            double meanRight = 0;
            int count = 0;
            int i = left;
            int j = right;
            while( i < j )
            {
                if( array.CompareValues( array[ j ], (int)sum ) <= 0 )
                {
                    while( array.CompareValues( array[ i ], (int)sum ) < 0 )
                    {
                        i++;
                    }
                    int hold = array[ i ];
                    meanLeft += array[ i ] = array[ j ];
                    meanRight += array[ j-- ] = hold;
                    count++;
                }
                else
                {
                    meanRight += array[ j-- ];
                }
            }

            if( count > 0 )
            {
                Sift( array, left, left + i, meanLeft / i );
                Sift( array, left + i, right, meanRight / j );
            }
        }
    }
}
