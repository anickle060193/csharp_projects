﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGamesLibrary
{
    public static class Utilities
    {
        public static readonly Random R = new Random();

        public static double NextDouble( double min, double max )
        {
            return Utilities.R.NextDouble() * ( max - min ) + min;
        }

        public static Color RandomColor()
        {
            return Color.FromArgb( Utilities.R.Next( 256 ), Utilities.R.Next( 256 ), Utilities.R.Next( 256 ) );
        }

        public static PointF CenterText( SizeF clientSize, SizeF textSize )
        {
            float x = ( clientSize.Width - textSize.Width ) / 2.0f;
            float y = ( clientSize.Height - textSize.Height ) / 2.0f;
            return new PointF( x, y );
        }

        public static void Randomize( IList list )
        {
            for( int i = 0; i < list.Count; i++ )
            {
                int index = Utilities.R.Next( list.Count );
                Object temp = list[ index ];
                list[ index ] = list[ i ];
                list[ i ] = temp;
            }
        }

        public static double Sqr( double x )
        {
            return x * x;
        }

        public static float Distance( float x1, float y1, float x2, float y2 )
        {
            return (float)Math.Sqrt( Utilities.Sqr( x2 - x1 ) + Utilities.Sqr( y2 - y1 ) );
        }

        public static float Distance( PointF v, PointF w )
        {
            return Distance( v.X, v.Y, w.X, w.Y );
        }

        public static float Distance( Point v, Point w )
        {
            return Distance( v.X, v.Y, w.X, w.Y );
        }

        public static float DistancePointToSegment( PointF p, PointF v, PointF w )
        {
            float l2 = Distance( v, w );
            if( l2 == 0 )
            {
                return Distance( p, v );
            }
            float t = ( ( p.X - v.X ) * ( w.X - v.X ) + ( p.Y - v.Y ) * ( w.Y - v.Y ) ) / (float)Utilities.Sqr( l2 );
            if( t < 0 )
            {
                return Distance( p, v );
            }
            else if( t > 1 )
            {
                return Distance( p, w );
            }
            else
            {
                float x = v.X + t * ( w.X - v.X );
                float y = v.Y + t * ( w.Y - v.Y );
                return Distance( p, new PointF( x, y ) );
            }
        }
    }
}
