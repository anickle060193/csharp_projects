using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyGamesLibrary.Games
{
    public partial class SimonSaysForm : GameForm
    {
        enum SimonSaysPad { Green, Red, Yellow, Blue }
        enum GameMode { UserPlay, SimonPlay, GameOver }

        private const float PaddingPercent = 0.02f;

        private const int START_DELAY = 1200;
        private const int PAD_DISPLAY_TIME = 800;
        private const int PAD_DISPLAY_DELAY = 300;

        private const int PAD_COUNT = 4;
        private const int ACTIVE_COLOR = 0;
        private const int NON_ACTIVE_COLOR = 1;
        private const SimonSaysPad InvalidPad = (SimonSaysPad)( -1 );

        private static readonly Brush[][] PAD_BRUSHES = new Brush[PAD_COUNT][];
        static SimonSaysForm()
        {
            //            ACTIVE            NON-ACTIVE
            PAD_BRUSHES[ (int)SimonSaysPad.Red    ] = new Brush[] { Brushes.Red, Brushes.Pink };
            PAD_BRUSHES[ (int)SimonSaysPad.Blue   ] = new Brush[] { Brushes.Blue, Brushes.LightBlue };
            PAD_BRUSHES[ (int)SimonSaysPad.Yellow ] = new Brush[] { Brushes.Yellow, Brushes.PaleGoldenrod };
            PAD_BRUSHES[ (int)SimonSaysPad.Green  ] = new Brush[] { Brushes.Green, Brushes.LightGreen };
        }

        private readonly Random R = new Random();
        private readonly Rectangle[] _padRects = new Rectangle[ PAD_COUNT ];
        private SimonSaysPad _activePad = InvalidPad;
        private GameMode _currentMode = GameMode.SimonPlay;

        private readonly List<SimonSaysPad> _instructions = new List<SimonSaysPad>();
        private int _currentInstruction;

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

        protected override void OnGameStarted( EventArgs e )
        {
            InitializeBoard();
        }

        protected override void OnPaint( PaintEventArgs e )
        {
            foreach( SimonSaysPad pad in Enum.GetValues( typeof( SimonSaysPad ) ) )
            {
                int brushSelect = _activePad == pad ? ACTIVE_COLOR : NON_ACTIVE_COLOR;
                e.Graphics.FillRectangle( PAD_BRUSHES[ (int)pad ][ brushSelect ], _padRects[ (int)pad ] );
            }
        }

        private void InitializeBoard()
        {
            _activePad = InvalidPad;
            _currentMode = GameMode.SimonPlay;
            _instructions.Clear();
            uxStart.Show();
            Invalidate();
        }

        private bool IntersectsPad( Point p, Control pad )
        {
            return p.X >= pad.Location.X && p.X <= pad.Location.X + pad.Size.Width
                && p.Y >= pad.Location.Y && p.Y <= pad.Location.Y + pad.Size.Height;
        }

        private SimonSaysPad GetPadFromLocation( int x, int y )
        {
            foreach( SimonSaysPad pad in Enum.GetValues( typeof( SimonSaysPad ) ) )
            {
                if( _padRects[ (int)pad ].Contains( x, y ) )
                {
                    return pad;
                }
            }
            return InvalidPad;
        }

        private void SimonSaysForm_MouseDown( object sender, MouseEventArgs e )
        {
            if( _currentMode == GameMode.UserPlay )
            {
                SimonSaysPad padColor = GetPadFromLocation( e.X, e.Y );
                if( padColor != InvalidPad )
                {
                    _activePad = padColor;
                    Refresh();
                }
            }
        }

        private void SimonSaysForm_MouseUp( object sender, MouseEventArgs e )
        {
            if( _currentMode == GameMode.UserPlay )
            {
                SimonSaysPad pad = GetPadFromLocation( e.X, e.Y );
                if( pad != InvalidPad )
                {
                    UserPlay( pad );
                }
            }
        }

        private void UserPlay( SimonSaysPad pad )
        {
            _activePad = InvalidPad;
            Refresh();
            if( pad != _instructions[ _currentInstruction] )
            {
                DisplayEndGame();
            }
            else
            {
                _currentInstruction++;
                if( _currentInstruction == _instructions.Count)
                {
                    _currentMode = GameMode.SimonPlay;
                    StartNextRound();
                }
            }
        }

        private async void StartNextRound()
        {
            await Task.Delay( START_DELAY );
            SimonSaysPad nextPad = (SimonSaysPad)R.Next( PAD_COUNT );
            _instructions.Add( nextPad );
            DisplayInstructions();
        }

        private async void DisplayInstructions()
        {
            foreach( SimonSaysPad pad in _instructions )
            {
                _activePad = pad;
                Refresh();
                await Task.Delay( PAD_DISPLAY_TIME );
                _activePad = InvalidPad;
                Refresh();
                await Task.Delay( PAD_DISPLAY_DELAY );
            }
            _currentInstruction = 0;
            _currentMode = GameMode.UserPlay;
        }

        private void DisplayEndGame()
        {
            _currentMode = GameMode.GameOver;
            if( MessageBox.Show( "You lasted " + ( _instructions.Count - 1 ) + " steps. Play again?", "Game Over", MessageBoxButtons.YesNo ) == DialogResult.Yes )
            {
                this.StartGame();
            }
            else
            {
                this.EndGame();
            }
        }

        private void uxStart_Click( object sender, EventArgs e )
        {
            uxStart.Hide();

            StartNextRound();
        }
    }
}
