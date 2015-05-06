using AdamNickle.Settings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LineThingy
{
    public partial class LineThingyForm : Form
    {
        private const int FontPadding = 10;
        private static readonly Font CountFont = new Font( FontFamily.GenericSansSerif, 20.0f );
        private static readonly SolidBrush CountBrush = new SolidBrush( Color.FromArgb( 150, Color.Black ) );

        private static readonly String MinimumDistancePercentKey = "minimum_distance_percent";
        private SettingsForm _settingsForm = new SettingsForm();
        private Timer _timer = new Timer();

        public LineThingyForm()
        {
            InitializeComponent();

            _settingsForm.AddFloatSetting( MinimumDistancePercentKey, "Drag minimum percent distance: ", 0.1f, delegate( float value )
            {
                return value > 0 && value < Math.Sqrt( 2 );
            } );

            uxLineCanvas.Paint += uxLineCanvas_Paint;
            uxLineCanvas.KeyPress += uxLineCanvas_KeyPress;

            _timer.Tick += Timer_Tick;
        }

        private void Timer_Tick( object sender, EventArgs e )
        {
            double x = uxLineCanvas.Width * Utilities.R.NextDouble();
            double y = uxLineCanvas.Height * Utilities.R.NextDouble();
            uxLineCanvas.AddPoint( (float)x, (float)y );
        }

        private void uxLineCanvas_KeyPress( object sender, KeyPressEventArgs e )
        {
            if( e.KeyChar == ' ' )
            {
                if( _settingsForm.ShowDialog() == DialogResult.OK )
                {
                    uxLineCanvas.MinimumDistancePercent = _settingsForm.GetFloatSetting( MinimumDistancePercentKey );
                }
            }
            else if( e.KeyChar == 'c' )
            {
                uxLineCanvas.Reset();
            }
            else if( e.KeyChar == 't' )
            {
                _timer.Enabled = !_timer.Enabled;
            }
            else if( e.KeyChar == 'p' )
            {
                uxLineCanvas.PreventPainting = !uxLineCanvas.PreventPainting;
                if( uxLineCanvas.PreventPainting )
                {
                    _timer.Interval = 1;
                }
                else
                {
                    _timer.Interval = 500;
                }
            }
        }

        private void uxLineCanvas_Paint( object sender, PaintEventArgs e )
        {
            Graphics g = e.Graphics;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            String lineCount = "Lines: " + uxLineCanvas.LineCount;
            String pointCount = "Points: " + uxLineCanvas.PointCount;

            SizeF size = g.MeasureString( lineCount, CountFont );

            g.DrawString( lineCount, CountFont, CountBrush, FontPadding, FontPadding );
            g.DrawString( pointCount, CountFont, CountBrush, FontPadding, FontPadding + size.Height + FontPadding );
        }
    }
}
