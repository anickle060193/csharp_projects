using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    public static class MyMath
    {
        public static int Mod( this int x, int m )
        {
            int r = x % m;
            return r < 0 ? r + m : r;
        }
    }
}
