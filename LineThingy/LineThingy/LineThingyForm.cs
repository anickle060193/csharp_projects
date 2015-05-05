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

        public LineThingyForm()
        {
            InitializeComponent();

            _settingsForm.AddFloatSetting( MinimumDistancePercentKey, "Drag minimum percent distance: ", 0.1f, delegate( float value )
            {
                return value > 0 && value < Math.Sqrt( 2 );
            } );

            uxLineCanvas.Paint += uxLineCanvas_Paint;
            uxLineCanvas.KeyPress += uxLineCanvas_KeyPress;
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
