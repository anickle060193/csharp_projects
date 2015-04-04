using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGamesLibrary
{
    public struct Location
    {
        public static readonly Location InvalidLocation = new Location( -1, -1 );

        public int Row;
        public int Column;

        public Location( int row, int column )
        {
            Row = row;
            Column = column;
        }

        public Location( Location loc, Direction d )
        {
            Row = loc.Row;
            Column = loc.Column;
            if( d == Direction.Left )
            {
                Column--;
            }
            if( d == Direction.Right )
            {
                Column++;
            }
            if( d == Direction.Up )
            {
                Row--;
            }
            if( d == Direction.Down )
            {
                Row++;
            }
        }

        public static Location GenerateRandomLocation( int maxRows, int maxColumns )
        {
            return new Location( Utilities.R.Next( maxRows), Utilities.R.Next( maxColumns ) );
        }

        public bool WithinBounds( int minRow, int minCol, int maxRow, int maxCol )
        {
            return minRow <= Row && Row < maxRow && minCol <= Column && Column < maxCol;
        }

        public bool WithinBounds( int maxRow, int maxCol )
        {
            return this.WithinBounds( 0, 0, maxRow, maxCol );
        }

        public override string ToString()
        {
            return "{ Row: " + Row + ", Column: " + Column + " }";
        }

        public override bool Equals( object obj )
        {
            return obj != null && obj is Location && this.Equals( (Location)obj );
        }

        public bool Equals( Location loc )
        {
            return this.Row == loc.Row && this.Column == loc.Column;
        }

        public static bool operator ==( Location a, Location b )
        {
            if( Object.ReferenceEquals( a, b ) )
            {
                return true;
            }
            if( ( (object)a == null ) || ( (object)b == null ) )
            {
                return false;
            }
            return a.Equals( b );
        }

        public static bool operator !=( Location a, Location b )
        {
            return !( a == b );
        }
    }
}
