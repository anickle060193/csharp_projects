using AForge;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MinesweeperSolver
{
    public partial class MinesweeperSolverForm : Form
    {
        private int _threshold = 40;
        private List<List<IntPoint>> _quads;
        private System.Drawing.Point _currentPoint;

        public MinesweeperSolverForm()
        {
            InitializeComponent();

            //String path = Path.Combine( Environment.GetFolderPath( Environment.SpecialFolder.Desktop ), "50_50.png" );
            //Bitmap image = new Bitmap( path );
            //SetImage( image );
        }

        private void MinesweeperSolverForm_KeyUp( object sender, KeyEventArgs e )
        {
        }

        private void SetImage( Bitmap bitmap )
        {
            if( bitmap != null )
            {
                if( uxImage.Image != null )
                {
                    uxImage.Image.Dispose();
                }
                _quads = MinesweeperImageDetector.FindQuads( (Bitmap)bitmap, _threshold );
                uxImage.Image = bitmap;
            }
        }

        private void uxImage_Paint( object sender, PaintEventArgs e )
        {
            Graphics g = e.Graphics;
            if( _quads != null )
            {
                foreach( List<IntPoint> quad in _quads )
                {
                    float scale = Utilities.GetImageScale( uxImage );
                    System.Drawing.Point[] points = Utilities.ConvertToPoint( quad );
                    points = Utilities.ConvertToControlPoints( uxImage, points );
                    g.DrawPolygon( Pens.LimeGreen, points );
                }
            }
            if( uxImage.Image != null 
             && !_currentPoint.Equals(System.Drawing.Point.Empty) )
            {
                float scale = Utilities.GetImageScale( uxImage );
                System.Drawing.Point scaledPoint = Utilities.ConvertToImagePoint( uxImage, _currentPoint );
                g.FillEllipse( Brushes.Blue, scaledPoint.X, scaledPoint.Y, 3, 3 );
            }
        }

        private void MinesweeperSolverForm_KeyPress( object sender, KeyPressEventArgs e )
        {
            if( ( ModifierKeys & Keys.Control ) == Keys.Control )
            {
                if( e.KeyChar == 'v' )
                {
                    //String path = Path.Combine( Environment.GetFolderPath( Environment.SpecialFolder.Desktop ), "50_50.png" );
                    //Bitmap image = new Bitmap( path );
                    SetImage( (Bitmap)Clipboard.GetImage() );
                }
                else if( e.KeyChar == '+' )
                {
                    _threshold += 5;
                    uxLabel.Text = _threshold.ToString();
                    _quads = MinesweeperImageDetector.FindQuads( (Bitmap)uxImage.Image, _threshold );
                    uxImage.Invalidate();
                }
                else if( e.KeyChar == '-' )
                {
                    _threshold -= 5;
                    uxLabel.Text = _threshold.ToString();
                    _quads = MinesweeperImageDetector.FindQuads( (Bitmap)uxImage.Image, _threshold );
                    uxImage.Invalidate();
                }
            }
        }
    }
}
