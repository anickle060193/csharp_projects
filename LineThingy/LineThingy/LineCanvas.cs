using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LineThingy
{
    public partial class LineCanvas : UserControl
    {
        public event EventHandler LinesUpdated;

        private IList<Line> _lines = new List<Line>();
        private Line _currentLine = new Line();

        private PointF _lastPoint;
        private bool _mouseDown;

        public int LineCount
        {
            get { return _lines.Sum( l => l.LineCount ); }
        }

        public int PointCount
        {
            get { return _lines.Sum( l => l.PointCount ); }
        }

        public float MinimumDistancePercent { get; set; }

        public LineCanvas()
        {
            InitializeComponent();

            ResizeRedraw = true;
            DoubleBuffered = true;

            _lines.Add( _currentLine );
            MinimumDistancePercent = 0.1f;
        }

        #region Event Handlers

        private void LineCanvas_MouseClick( object sender, MouseEventArgs e )
        {
            if( e.Button == MouseButtons.Left )
            {
                PointF p = Utilities.ConvertFromCanvas( e.X, e.Y, Width, Height );
                _currentLine.AddPoint( p );
                _lastPoint = p;

                OnLinesUpdated( EventArgs.Empty );
            }
            else if( e.Button == MouseButtons.Right )
            {
                _currentLine = new Line();
                _lines.Add( _currentLine );
                _lastPoint = Utilities.InvalidPoint;

                OnLinesUpdated( EventArgs.Empty );
            }
            else if( e.Button == MouseButtons.Middle )
            {
                _lines.Clear();
                _currentLine = new Line();
                _lines.Add( _currentLine );
                _lastPoint = Utilities.InvalidPoint;

                OnLinesUpdated( EventArgs.Empty );
            }
            Invalidate();
        }

        private void LineCanvas_MouseDown( object sender, MouseEventArgs e )
        {
            _mouseDown = true;
            _lastPoint = Utilities.ConvertFromCanvas( e.X, e.Y, Width, Height );
        }

        private void LineCanvas_MouseMove( object sender, MouseEventArgs e )
        {
            if( _mouseDown )
            {
                PointF last = Utilities.ConvertToCanvas( _lastPoint, Width, Height );
                PointF currentCanvas = new PointF( e.X, e.Y );
                float dist = Utilities.Distance( last, currentCanvas );
                float percent = dist / (float)Math.Sqrt( Width * Width + Height * Height );
                if( percent > MinimumDistancePercent )
                {
                    PointF current = Utilities.ConvertFromCanvas( e.X, e.Y, Width, Height );
                    _currentLine.AddPoint( current );
                    _lastPoint = current;
                    Invalidate();

                    OnLinesUpdated( EventArgs.Empty );
                }
            }
        }

        private void LineCanvas_MouseUp( object sender, MouseEventArgs e )
        {
            _mouseDown = false;
        }

        private void LineCanvas_Paint( object sender, PaintEventArgs e )
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            foreach( Line l in _lines )
            {
                l.Paint( e.Graphics, Width, Height );
            }
        }

        #endregion

        protected void OnLinesUpdated( EventArgs e )
        {
            EventHandler handler = LinesUpdated;
            if( handler != null )
            {
                handler( this, e );
            }
        }
    }
}
