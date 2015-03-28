using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    public class Pattern
    {
        public static readonly Pattern Glider = new Pattern( "Glider", 3, 3, " X " +
                                                                             "  X" +
                                                                             "XXX" );

        public static readonly Pattern LWSS = new Pattern( "Light Weight Space Ship", 4, 5, "X  X " +
                                                                                            "    X" +
                                                                                            "X   X" +
                                                                                            " XXXX" );

        public static readonly Pattern Pulsar = new Pattern( "Pulsar", 13, 13, "  XXX   XXX  " +
                                                                               "             " +
                                                                               "X    X X    X" +
                                                                               "X    X X    X" +
                                                                               "X    X X    X" +
                                                                               "  XXX   XXX  " +
                                                                               "             " +
                                                                               "  XXX   XXX  " +
                                                                               "X    X X    X" +
                                                                               "X    X X    X" +
                                                                               "X    X X    X" +
                                                                               "             " +
                                                                               "  XXX   XXX  " );

        public static readonly Pattern GosperGliderGun = new Pattern( "Gosper Glider Gun", 9, 36, "                        X           " +
                                                                                                  "                      X X           " +
                                                                                                  "            XX      XX            XX" +
                                                                                                  "           X   X    XX            XX" +
                                                                                                  "XX        X     X   XX              " +
                                                                                                  "XX        X   X XX    X X           " +
                                                                                                  "          X     X       X           " +
                                                                                                  "           X   X                    " +
                                                                                                  "            XX                      " );

        public static readonly Pattern[] Patterns = new Pattern[] { Glider, LWSS, Pulsar, GosperGliderGun };

        private bool[,] _pattern;

        public String Name { get; private set; }
        public int Rows { get; private set; }
        public int Columns { get; private set; }

        public Pattern( String name, int rows, int columns, String pattern )
        {
            Name = name;
            Rows = rows;
            Columns = columns;
            _pattern = new bool[ rows, columns ];
            int row = 0;
            int col = 0;
            foreach( char c in pattern )
            {
                _pattern[ row, col ] = c != ' ';
                col++;
                if( col == Columns )
                {
                    col = 0;
                    row++;
                }
            }
        }

        public void InsertPatternInto( GameOfLifeBoard board, int row, int column )
        {
            int pRow = 0;
            row -= this.Rows / 2;
            column -= this.Columns / 2;
            for( int r = row; r < row + this.Rows; r++ )
            {
                int pCol = 0;
                for( int c = column; c < column + this.Columns; c++ )
                {
                    board[ r, c ] = _pattern[ pRow, pCol ] ? GameOfLifeBoard.State.Alive : GameOfLifeBoard.State.Dead;
                    pCol++;
                }
                pRow++;
            }
        }
    }
}
