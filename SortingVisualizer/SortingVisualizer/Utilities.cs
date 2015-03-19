using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingVisualizer
{
    public static class Utilities
    {
        public static Color ColorFromHSL( double hue, double saturation, double luminance )
        {
            double v;
            double r, g, b;

            r = luminance;   // default to gray
            g = luminance;
            b = luminance;
            v = ( luminance <= 0.5 ) ? ( luminance * ( 1.0 + saturation ) ) : ( luminance + saturation - luminance * saturation );
            if( v > 0 )
            {
                double m;
                double sv;
                int sextant;
                double fract, vsf, mid1, mid2;

                m = luminance + luminance - v;
                sv = ( v - m ) / v;
                hue *= 6.0;
                sextant = (int)hue;
                fract = hue - sextant;
                vsf = v * sv * fract;
                mid1 = m + vsf;
                mid2 = v - vsf;
                switch( sextant )
                {
                    case 0:
                        r = v;
                        g = mid1;
                        b = m;
                        break;
                    case 1:
                        r = mid2;
                        g = v;
                        b = m;
                        break;
                    case 2:
                        r = m;
                        g = v;
                        b = mid1;
                        break;
                    case 3:
                        r = m;
                        g = mid2;
                        b = v;
                        break;
                    case 4:
                        r = mid1;
                        g = m;
                        b = v;
                        break;
                    case 5:
                        r = v;
                        g = m;
                        b = mid2;
                        break;
                }
            }
            return Color.FromArgb( (int)( r * 255 ), (int)( g * 255 ), (int)( b * 255 ) );
        }
    }
}
