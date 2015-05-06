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

        public bool PreventPainting { get; set; }

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
                AddPoint( e.X, e.Y );
            }
            else if( e.Button == MouseButtons.Right )
            {
                StartNewLine();
            }
            else if( e.Button == MouseButtons.Middle )
            {
                Reset();
            }
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
            if( PreventPainting )
            {
                return;
            }
            PaintLines( e.Graphics, Width, Height );
        }

        #endregion

        private void PaintLines( Graphics g, int width, int height )
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            foreach( Line l in _lines )
            {
                l.Paint( g, width, height );
            }
        }

        public Bitmap CreateBitmap()
        {
            return CreateBitmap( Width, Height );
        }

        public Bitmap CreateBitmap( int width, int height )
        {
            Bitmap b = new Bitmap( width, height );
            using( Graphics g = Graphics.FromImage( b ) )
            {
                g.Clear( BackColor );
                PaintLines( g, width, height );
            }
            return b;
        }

        public void AddPoint( float x, float y )
        {
            PointF p = Utilities.ConvertFromCanvas( x, y, Width, Height );
            _currentLine.AddPoint( p );
            _lastPoint = p;

            OnLinesUpdated( EventArgs.Empty );
            Invalidate();
        }

        public void StartNewLine()
        {
            _currentLine = new Line();
            _lines.Add( _currentLine );
            _lastPoint = Utilities.InvalidPoint;

            OnLinesUpdated( EventArgs.Empty );
            Invalidate();
        }

        public void Reset()
        {
            _lines.Clear();
            _currentLine = new Line();
            _lines.Add( _currentLine );
            _lastPoint = Utilities.InvalidPoint;

            OnLinesUpdated( EventArgs.Empty );
            Invalidate();
        }

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
