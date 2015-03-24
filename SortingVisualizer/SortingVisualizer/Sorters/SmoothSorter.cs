using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingVisualizer.Sorters
{
    class SmoothSorter : Sorter
    {
        public override bool IsWorking { get { return false; } }

        private static readonly int[] LP =
        {
            1, 1, 3, 5, 9, 15, 25, 41, 67, 109, 177, 287,
            465, 753, 1219, 1973, 3193, 5167, 8361, 13529,
            21891, 35421, 57313, 92735, 150049, 242785, 392835,
            635621, 1028457, 1664079, 2692537, 4356617, 7049155,
            11405773, 18454929, 29860703, 48315633, 78176337,
            126491971, 204668309, 331160281, 535828591, 866988873
        };

        public override void Sort( SortingArray array )
        {
            //SmoothSort( array, 0, array.Count - 1 );
        }

        private void SmoothSort( SortingArray array, int lo, int hi )
        {
            int head = lo;

            int p = 1;
            int pShift = 1;

            while( head < hi )
            {
                if( ( p & 3 ) == 3 )
                {
                    Sift( array, pShift, head );
                    p = (int)( (uint)p >> 2 );
                    pShift += 2;
                }
                else
                {
                    if( LP[ pShift - 1 ] >= hi - head )
                    {
                        Trinkle( array, p, pShift, head, false );
                    }
                    else
                    {
                        Sift( array, pShift, head );
                    }

                    if( pShift == 1 )
                    {
                        p <<= 1;
                        pShift--;
                    }
                    else
                    {
                        p <<= pShift - 1;
                        pShift = 1;
                    }
                }
                p |= 1;
                head++;
            }

            Trinkle( array, p, pShift, head, false );

            while( pShift != 1 || p != 1 )
            {
                if( pShift <= 1 )
                {
                    int trail = TrailingZeros( p & ~1 );
                    p = (int)( (uint)p >> trail );
                    pShift += trail;
                }
                else
                {
                    p <<= 2;
                    p ^= 7;
                    pShift -= 2;

                    Trinkle( array, (int)( (uint)p >> 1 ), pShift + 1, head - LP[ pShift ] - 1, true );
                    Trinkle( array, p, pShift, head - 1, true );
                }

                head--;
            }
        }

        private void Sift( SortingArray array, int pShift, int head )
        {
            int val = array[ head ];

            while( pShift > 1 )
            {
                int rt = head - 1;
                int lf = head - 1 - LP[ pShift - 2 ];

                if( array.CompareValues( val, array[ lf ] ) >= 0
                 && array.CompareValues( val, array[ rt ] ) >= 0 )
                {
                    break;
                }
                if( array.CompareValuesAt( lf, rt ) >= 0 )
                {
                    array[ head ] = array[ lf ];
                    head = lf;
                    pShift -= 1;
                }
                else
                {
                    array[ head ] = array[ rt ];
                    head = rt;
                    pShift -= 2;
                }
            }

            array[ head ] = val;
        }

        private void Trinkle( SortingArray array, int p, int pShift, int head, bool isTrusty )
        {
            int val = array[ head ];
            while( p != 1 )
            {
                int stepson = head - LP[ pShift ];
                if( array.CompareValues( array[ stepson ], val ) <= 0 )
                {
                    break;
                }
                if( !isTrusty && pShift > 1 )
                {
                    int rt = head - 1;
                    int lf = head - 1 - LP[ pShift - 2 ];
                    if( array.CompareValuesAt( rt, stepson ) >= 0
                     || array.CompareValuesAt( lf, stepson ) >= 0 )
                    {
                        break;
                    }
                }

                array[ head ] = array[ stepson ];

                head = stepson;
                int trail = TrailingZeros( p & ~1 );
                p = (int)( (uint)p >> trail );
                pShift += trail;
                isTrusty = false;
            }

            if( !isTrusty )
            {
                array[ head ] = val;
                Sift( array, pShift, head );
            }
        }

        private int TrailingZeros( int number )
        {
            int i = 1;
            int result = 0;
            while( number >= i )
            {
                i *= 5;
                result += number / i;
            }
            return result;
        }
    }
}
