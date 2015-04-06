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
    public partial class PickUpSticks : GameForm
    {
        private const int Sticks = 20;
        private const float ClickThreshold = 2;
        private const int RandomPlacementRange = 400;
        private const int StickThickness = 5;
        private const int MaximumAllowedSticksToPickUp = 3;
        private const int MinimumAllowedSticksToPickup = 1;

        private static readonly SolidBrush PlayerTurnBrush = new SolidBrush( Color.FromArgb( 50, Color.Black ) );
        private static readonly Font PlayerTurnFont = new Font( FontFamily.GenericSansSerif, 36 );
        private static readonly String[] PlayerTurnStrings = { "Player One's Turn", "Player Two's Turn" };
        private static readonly String[] GameOverStrings = { "Player One Wins!", "Player Two Wins" };
        private static readonly String PlayAgainString = " Play again?";

        private List<Stick> _sticks = new List<Stick>();
        private int _turn = 0;
        private int _sticksPickedUp = 0;
        private bool _gameOver = false;

        public override string GameName { get { return "Pick-Up Sticks"; } }

        public PickUpSticks()
        {
            InitializeComponent();
        }

        protected override void OnGameStarted( EventArgs e )
        {
            InitializeBoard();
        }

        private void InitializeBoard()
        {
            _sticks.Clear();
            _turn = 0;
            _sticksPickedUp = 0;
            _gameOver = false;

            for( int i = 0; i < Sticks; i++ )
            {
                double x = Utilities.R.NextDouble() * RandomPlacementRange - RandomPlacementRange / 2.0f + this.ClientSize.Width / 2.0f;
                double y = Utilities.R.NextDouble() * RandomPlacementRange - RandomPlacementRange / 2.0f + this.ClientSize.Height / 2.0f;
                _sticks.Add( Stick.GenerateRandomStick( (float)x, (float)y, 300, StickThickness ) );
            }
            Invalidate();
        }

        protected override void OnPaint( PaintEventArgs e )
        {
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            float width = this.ClientSize.Width;
            float height = this.ClientSize.Height;
            String s = PlayerTurnStrings[ _turn];
            SizeF textSize = e.Graphics.MeasureString( s, PlayerTurnFont );
            float x = ( width - textSize.Width ) / 2.0f;
            float y = ( height - textSize.Height ) / 2.0f;
            e.Graphics.DrawString( s, PlayerTurnFont, PlayerTurnBrush, x, y );

            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            for( int i = _sticks.Count - 1; i >= 0; i-- )
            {
                _sticks[ i ].DrawStick( e.Graphics );
            }
        }

        private void PickUpSticks_MouseClick( object sender, MouseEventArgs e )
        {
            if( !_gameOver )
            {
                for( int i = 0; i < _sticks.Count; i++ )
                {
                    if( _sticks[ i ].Intersects( e.X, e.Y, ClickThreshold ) )
                    {
                        _sticks.RemoveAt( i );
                        _sticksPickedUp++;
                        Invalidate();
                        break;
                    }
                }
                if( _sticks.Count == 0 )
                {
                    DisplayGameEnd( 1 - _turn );
                }
                else if( _sticksPickedUp == MaximumAllowedSticksToPickUp )
                {
                    NextTurn();
                }
            }
        }

        private void PickUpSticks_KeyPress( object sender, KeyPressEventArgs e )
        {
            if( !_gameOver && e.KeyChar == ' ' )
            {
                if( _sticksPickedUp >= MinimumAllowedSticksToPickup )
                {
                    NextTurn();
                }
            }
        }

        private void NextTurn()
        {
            _turn = 1 - _turn;
            _sticksPickedUp = 0;
            Invalidate();
        }

        private void DisplayGameEnd( int winner )
        {
            _gameOver = true;
            if( MessageBox.Show( GameOverStrings[ winner ] + PlayAgainString, GameOverStrings[ winner ], MessageBoxButtons.YesNo ) == DialogResult.Yes )
            {
                StartGame();
            }
            else
            {
                EndGame();
            }
        }
    }

    class Stick
    {
        private const float ShadowOffset = 1.25f;

        private Pen _pen = new Pen( Color.White );
        private Pen _shadowPen = new Pen( Color.White );

        public PointF Start { get; set; }
        public PointF End { get; set; }
        public float Thickness
        {
            get { return _pen.Width; }
            set
            {
                _pen.Width = value;
                _shadowPen.Width = value;
            }
        }
        public Color Color
        {
            get { return _pen.Color; }
            set
            {
                _pen.Color = value;
                _shadowPen.Color = Color.FromArgb( 100, _pen.Color );
            }
        }

        public Stick()
        {
            _pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
            _pen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            _shadowPen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
            _shadowPen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
        }

        public void DrawStick( Graphics g )
        {
            g.DrawLine( _shadowPen, Start.X + ShadowOffset, Start.Y + ShadowOffset, End.X + ShadowOffset, End.Y + ShadowOffset );
            g.DrawLine( _pen, Start, End );
        }

        public bool Intersects( float x, float y, float threshold )
        {
            PointF p = new PointF(x, y);
            float distance = Utilities.DistancePointToSegment( p, Start, End );
            return distance <= ( Thickness / 2.0f + threshold );
        }

        public static Stick GenerateRandomStick( float x, float y, float length, float thickness )
        {
            Stick s = new Stick();
            s.Color = Color.FromArgb( Utilities.R.Next( 256 ), Utilities.R.Next( 256 ), Utilities.R.Next( 256 ) );
            s.Thickness = thickness;
            double angle = Utilities.R.NextDouble() * 2.0 * Math.PI;
            double width = length * Math.Cos( angle );
            double height = length * Math.Sin( angle );
            s.Start = new PointF( (float)( x - width / 2.0 ), (float)( y - height / 2.0f ) );
            s.End = new PointF( (float)( x + width / 2.0f ), (float)( y + height / 2.0f ) );
            return s;
        }
    }
}
