using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyGamesLibrary
{
    public partial class SimonSaysForm : GameForm
    {
        enum SimonSaysPad { Red, Blue, Yellow, Green }

        private const float PaddingPercent = 0.02f;

        private const int PAD_COUNT = 4;
        private const int BRIGHT_COLOR = 0;
        private const int DARK_COLOR = 1;
        private const SimonSaysPad InvalidPad = (SimonSaysPad)( -1 );

        private static readonly Color[][] PAD_COLORS = new Color[][]
        {
            new Color[] { Color.Red, Color.DarkRed },
            new Color[] { Color.Blue, Color.DarkBlue },
            new Color[] { Color.Yellow, Color.Orange },
            new Color[] { Color.Green, Color.DarkGreen }
        };

        private readonly bool[] _isDown = new bool[ PAD_COUNT ];
        private readonly Rectangle[] _padRects = new Rectangle[ PAD_COUNT ];
        private SimonSaysPad _downPad = InvalidPad;

        public SimonSaysForm()
        {
            InitializeComponent();

            int horizontalPadding = (int)( this.Size.Width * PaddingPercent );
            int verticalPadding = (int)( this.Size.Height * PaddingPercent );
            int padWidth = (int)( ( this.Size.Width - horizontalPadding * 3 ) / 2.0f );
            int padHeight = (int)( ( this.Size.Height - verticalPadding * 3 ) / 2.0f );

            int rowOneY = verticalPadding;
            int rowTwoY = verticalPadding + padHeight + verticalPadding;
            int colOneX = horizontalPadding;
            int colTwoX = horizontalPadding + padWidth + horizontalPadding;

            _padRects[ (int)SimonSaysPad.Red ] = new Rectangle( colOneX, rowOneY, padWidth, padHeight );
            _padRects[ (int)SimonSaysPad.Blue ] = new Rectangle( colTwoX, rowOneY, padWidth, padHeight );
            _padRects[ (int)SimonSaysPad.Yellow ] = new Rectangle( colOneX, rowTwoY, padWidth, padHeight );
            _padRects[ (int)SimonSaysPad.Blue ] = new Rectangle( colTwoX, rowTwoY, padWidth, padHeight );
        }

        public override string GameName { get { return "Simon Says"; } }

        private bool IntersectsPad( Point p, Control pad )
        {
            return p.X >= pad.Location.X && p.X <= pad.Location.X + pad.Size.Width
                && p.Y >= pad.Location.Y && p.Y <= pad.Location.Y + pad.Size.Height;
        }

        private SimonSaysPad GetColorFromPoint( Point p )
        {
            foreach( SimonSaysPad pad in Enum.GetValues( typeof( SimonSaysPad ) ) )
            {
                if( _padRects[ (int)pad ].Contains( p ) )
                {
                    return pad;
                }
            }
            return InvalidPad;
        }

        private void SimonSaysForm_MouseDown( object sender, MouseEventArgs e )
        {
            SimonSaysPad padColor = GetColorFromPoint( new Point( e.X, e.Y ) );
            if( padColor != InvalidPad )
            {
                _downPad = padColor;
                _pads[ (int)_downPad ].BackColor = PAD_COLORS[ (int)padColor ][ DARK_COLOR ];
            }
        }

        private void SimonSaysForm_MouseUp( object sender, MouseEventArgs e )
        {
            if( _downPad != InvalidPad )
            {
                _pads[ (int)_downPad ].BackColor = PAD_COLORS[ (int)_downPad ][ BRIGHT_COLOR ];
                _downPad = InvalidPad;
            }
        }
    }
}
