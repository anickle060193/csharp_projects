﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingVisualizer.Sorters
{
    class LSDRadixSorter : Sorter
    {
        public override bool IsWorking { get { return true; } }

        public override void Sort( SortingArray array )
        {
            LinkedList<int>[] counter = new LinkedList<int>[]
            {
                new LinkedList<int>(),
                new LinkedList<int>(),
                new LinkedList<int>(),
                new LinkedList<int>(),
                new LinkedList<int>(),
                new LinkedList<int>(),
                new LinkedList<int>(),
                new LinkedList<int>(),
                new LinkedList<int>(),
                new LinkedList<int>()
            };

            int maxDigitSymbols = array.Max().ToString().Length;
            for( int i = 1; i <= maxDigitSymbols; i++ )
            {
                for( int j = 0; j < array.Length; j++ )
                {
                    int bucket = GetDigit( array[ j ], i );
                    counter[ bucket ].AddLast( array[ j ] );
                }
                int pos = 0;
                for( int j = 0; j < counter.Length; j++ )
                {
                    while( counter[ j ].First != null )
                    {
                        array[ pos++ ] = counter[ j ].First.Value;
                        counter[ j ].RemoveFirst();
                    }
                }
            }
        }

        private int GetDigit( int number, int place )
        {
            int dev, mod;
            switch( place )
            {
                case 1:
                    mod = 10;
                    dev = 1;
                    break;

                case 2:
                    mod = 100;
                    dev = 10;
                    break;

                case 3:
                    mod = 1000;
                    dev = 100;
                    break;

                case 4:
                    mod = 10000;
                    dev = 1000;
                    break;

                case 5:
                    mod = 100000;
                    dev = 10000;
                    break;

                case 6:
                    mod = 1000000;
                    dev = 100000;
                    break;

                case 7:
                    mod = 10000000;
                    dev = 1000000;
                    break;

                case 8:
                    mod = 100000000;
                    dev = 10000000;
                    break;

                case 9:
                    mod = 1000000000;
                    dev = 100000000;
                    break;

                default:
                    dev = 10 ^ ( place - 1 );
                    mod = dev * 10;
                    break;
            }

            return ( number % mod ) / dev;
        }
    }
}
