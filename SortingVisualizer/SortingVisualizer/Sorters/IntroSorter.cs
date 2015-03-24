using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingVisualizer.Sorters
{
    class IntroSorter : Sorter
    {
        public override bool IsWorking { get { return false; } }
        
        private static readonly int SIZE_THRESHOLD = 16;

        public override void Sort( SortingArray array )
        {

        }

        private void IntroSort( SortingArray array, int begin, int end )
        {
            if( begin < end )
            {
                IntroSort( array, 0, array.Length, 2 * FloorLg( array.Length ) );
            }
        }

        private void IntroSort( SortingArray array, int lo, int hi, int depth )
        {
            while( hi - lo > SIZE_THRESHOLD )
            {
                if( depth == 0 )
                {

                }
            }
        }

        private void InsertionSort( SortingArray array, int start, int end )
        {
            for( int i = start + 1; i < end; i++ )
            {
                int x = array[ i ];
                int j = i;
                while( j > start && array.CompareValues( array[ j - 1 ], x ) > 0 )
                {
                    array[ j ] = array[ j - 1 ];
                    j--;
                }
                array[ j ] = x;
            }
        }

        private static int FloorLg( int a )
        {
            return (int)Math.Floor( Math.Log( a, 2 ) );
        }
    }
}
