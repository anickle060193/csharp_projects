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
        enum SimonSaysPad { Green, Red, Yellow, Blue }

        private const float PaddingPercent = 0.02f;

        private const int PAD_COUNT = 4;
        private const int BRIGHT_COLOR = 0;
        private const int DARK_COLOR = 1;
        private const SimonSaysPad InvalidPad = (SimonSaysPad)( -1 );

        private static readonly Brush[][] PAD_BRUSHES = new Brush[][]
        {
            new Brush[] { Brushes.Red, Brushes.DarkRed },
            new Brush[] { Brushes.Blue, Brushes.DarkBlue },
            new Brush[] { Brushes.Yellow, Brushes.Orange },
            new Brush[] { Brushes.Green, Brushes.DarkGreen }
        };

        private readonly bool[] _isDown = new bool[ PAD_COUNT ];
        private readonly Rectangle[] _padRects = new Rectangle[ PAD_COUNT ];
        private SimonSaysPad _downPad = InvalidPad;

        public override string GameName { get { return "Simon Says"; } }

        public SimonSaysForm()
        {
            InitializeComponent();

            SizeF size = this.ClientSize;

            int horizontalPadding = (int)( size.Width * PaddingPercent );
            int verticalPadding = (int)( size.Height * PaddingPercent );
            int padWidth = (int)( ( size.Width - horizontalPadding * 3 ) / 2.0f );
            int padHeight = (int)( ( size.Height - verticalPadding * 3 ) / 2.0f );

            int rowOneY = verticalPadding;
            int rowTwoY = verticalPadding + padHeight + verticalPadding;
            int colOneX = horizontalPadding;
            int colTwoX = horizontalPadding + padWidth + horizontalPadding;

            // G R
            // Y B
            _padRects[ (int)SimonSaysPad.Green ] = new Rectangle( colOneX, rowOneY, padWidth, padHeight );
            _padRects[ (int)SimonSaysPad.Red ] = new Rectangle( colTwoX, rowOneY, padWidth, padHeight );
            _padRects[ (int)SimonSaysPad.Yellow ] = new Rectangle( colOneX, rowTwoY, padWidth, padHeight );
            _padRects[ (int)SimonSaysPad.Blue ] = new Rectangle( colTwoX, rowTwoY, padWidth, padHeight );
        }

        public override void StartGame()
        {
            InitializeBoard();

            base.StartGame();
        }

        protected override void OnPaint( PaintEventArgs e )
        {
            foreach( SimonSaysPad pad in Enum.GetValues( typeof( SimonSaysPad ) ) )
            {
                e.Graphics.FillRectangle( PAD_BRUSHES[ (int)pad ][ _isDown[ (int)pad ] ? DARK_COLOR : BRIGHT_COLOR ], _padRects[ (int)pad ] );
            }
        }

        private void InitializeBoard()
        {
            for( int i = 0; i < PAD_COUNT; i++ )
            {
                _isDown[ i ] = false;
            }
            Invalidate();
        }

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
                _isDown[ (int)_downPad ] = true;
                Invalidate();
            }
        }

        private void SimonSaysForm_MouseUp( object sender, MouseEventArgs e )
        {
            if( _downPad != InvalidPad )
            {
                _isDown[ (int)_downPad ] = false;
                _downPad = InvalidPad;
                Invalidate();
            }
        }
    }
}
