using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper
{
    public static class Utilities
    {
        public static int Bound( int value, int lowerBound, int upperBound )
        {
            if( value < lowerBound )
            {
                return lowerBound;
            }
            else if( value > upperBound )
            {
                return upperBound;
            }
            else
            {
                return value;
            }
        }
    }
}
