using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LineThingy
{
    public static class Utilities
    {
        public static Random R { get; private set; }

        public static PointF InvalidPoint
        {
            get { return new PointF( -1, -1 ); }
        }

        static Utilities()
        {
            R = new Random();
        }

        public static Color RandomColor()
        {
            return Color.FromArgb( R.Next( 256 ), R.Next( 256 ), R.Next( 256 ) );
        }

        public static PointF MidPoint( PointF p1, PointF p2 )
        {
            return new PointF( ( p1.X + p2.X ) / 2, ( p1.Y + p2.Y ) / 2 );
        }

        public static PointF ConvertFromCanvas( float x, float y, int canvasWidth, int canvasHeight )
        {
            return new PointF( x / canvasWidth, y / canvasHeight );
        }

        public static IList<PointF> ConvertFromCanvas( IList<PointF> points, int canvasWidth, int canvasHeight )
        {
            return points.ToList().ConvertAll( pF => Utilities.ConvertFromCanvas( pF.X, pF.Y, canvasWidth, canvasHeight ) );
        }

        public static PointF ConvertToCanvas( PointF point, int canvasWidth, int canvasHeight )
        {
            return new PointF( point.X * canvasWidth, point.Y * canvasHeight );
        }

        public static IList<PointF> ConvertToCanvas( IList<PointF> points, int canvasWidth, int canvasHeight )
        {
            return points.ToList().ConvertAll( pF => Utilities.ConvertToCanvas( pF, canvasWidth, canvasHeight ) );
        }

        public static float Distance( PointF p1, PointF p2 )
        {
            float xDiff = p1.X - p2.X;
            float yDiff = p1.Y - p2.Y;
            return (float)Math.Sqrt( xDiff * xDiff + yDiff * yDiff );
        }
    }
}
