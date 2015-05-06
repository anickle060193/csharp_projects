using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LineThingy
{
    public class Line
    {
        private const int LineWidth = 2;
        private readonly Random R = new Random();

        private IList<PointF> _points = new List<PointF>();
        private Color _color;
        private Line _child;
        private Line _parent;

        public int LineCount
        {
            get
            {
                return 1 + ( _child == null ? 0 : _child.LineCount );
            }
        }

        public int PointCount
        {
            get
            {
                return _points.Count + ( _child == null ? 0 : _child.PointCount );
            }
        }

        public Line()
        {
            _color = Utilities.RandomColor();
        }

        public Line( Color color )
        {
            _color = color;
        }

        private Line( Color color, Line parent ) : this( color )
        {
            _parent = parent;
        }

        public void AddPoint( PointF p )
        {
            _points.Add( p );
            if( _points.Count == 2 )
            {
                _child = new Line( Utilities.RandomColor(), this );
            }
            if( _child != null )
            {
                _child.ParentPointAdded();
            }
        }

        private void ParentPointAdded()
        {
            IList<PointF> parentPoints = _parent._points;
            if( parentPoints.Count >= 2 )
            {
                AddPoint( Utilities.MidPoint( parentPoints[ parentPoints.Count - 1 ], parentPoints[ parentPoints.Count - 2 ] ) );
            }
        }

        public void Paint( Graphics g, int canvasWidth, int canvasHeight )
        {
            if( _points.Count > 1 )
            {
                using( Pen p = new Pen(  Color.FromArgb( Utilities.R.Next( 100, 185 ), Utilities.RandomColor() ) ) { Width = LineWidth } )
                {
                    PointF[] points = Utilities.ConvertToCanvas( _points, canvasWidth, canvasHeight ).ToArray();
                    g.DrawLines( p, points );
                }
                if( _child != null )
                {
                    _child.Paint( g, canvasWidth, canvasHeight );
                }
            }
        }
    }
}
