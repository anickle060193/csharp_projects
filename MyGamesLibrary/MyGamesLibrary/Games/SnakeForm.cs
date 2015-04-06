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
    public partial class SnakeForm : GameForm
    {
        private const int Rows = 10;
        private const int Columns = 10;
        private const int TotalCells = Rows * Columns;
        private const int SnakeSpawnSafeBorderWidth = 2;
        private const int CellBlockSize = 30;
        private const int BoardWidth = CellBlockSize * Columns;
        private const int BoardHeight = CellBlockSize * Rows;

        private static readonly Color FoodColor = Color.Green;
        private static readonly Color SnakeColor = Color.Black;
        private static readonly Color PauseScreenColor = Color.FromArgb( 100, Color.White );
        private static readonly Color PauseScreenTextColor = Color.Black;
        private static readonly Font PauseScreenTextFont = new Font( FontFamily.GenericSansSerif, 72 );

        class SnakeBlock
        {
            public Location Location { get; set; }
            public SnakeBlock Previous { get; set; }
            public SnakeBlock Next { get; set; }

            public override string ToString()
            {
                return Location.ToString();
            }
        }

        private Direction _direction;
        private SnakeBlock _snakeHead;
        private SnakeBlock _snakeTail;
        private Location _food;
        private int _snakeLength;
        private bool _gameOver;
        private bool _paused;
        private bool _firstMove;

        public override string GameName { get { return "Snake"; } }

        public SnakeForm()
        {
            InitializeComponent();

            this.ClientSize = new Size( BoardWidth, BoardHeight );
        }

        protected override void OnGameStarted( EventArgs e )
        {
            uxSnakeTimer.Stop();
            _firstMove = true;
            _paused = false;
            _gameOver = false;
            _snakeLength = 1;
            _snakeHead = _snakeTail = new SnakeBlock();
            GenerateFood();

            Invalidate();
        }

        private void GenerateFood()
        {
            do
            {
                _food = MyGamesLibrary.Location.GenerateRandomLocation( Rows, Columns );
            }
            while( IsSnakeBody( _food ) );
            Invalidate();
        }

        private bool IsSnakeBody( Location loc )
        {
            SnakeBlock iter = _snakeHead;
            while( iter != null )
            {
                if( iter.Location == loc )
                {
                    return true;
                }
                iter = iter.Next;
            }
            return false;
        }

        private bool IsValidMove( Location newheadLocation )
        {
            return !IsSnakeBody( newheadLocation ) && newheadLocation.WithinBounds( Rows, Columns );
        }

        protected override bool IsInputKey( System.Windows.Forms.Keys keyData )
        {
            switch( keyData )
            {
                case Keys.Left:
                case Keys.Right:
                case Keys.Up:
                case Keys.Down:
                    return true;                    
                default:
                    return base.IsInputKey( keyData );
            }
        }

        protected override void OnKeyUp( KeyEventArgs e )
        {
            if( !_gameOver )
            {
                if( !_paused )
                {
                    switch( e.KeyCode )
                    {
                        case Keys.Left:
                            _direction = Direction.Left;
                            break;

                        case Keys.Right:
                            _direction = Direction.Right;
                            break;

                        case Keys.Down:
                            _direction = Direction.Down;
                            break;

                        case Keys.Up:
                            _direction = Direction.Up;
                            break;
                    }
                    UpdateSnake();
                    if( _firstMove )
                    {
                        _firstMove = false;
                        uxSnakeTimer.Start();
                    }
                }
                else
                {
                    if( e.KeyCode == Keys.Space )
                    {
                        _paused = !_paused;
                        uxSnakeTimer.Enabled = !_paused;
                        Invalidate();
                    }
                }
            }
        }

        protected override void OnPaint( PaintEventArgs e )
        {
            using( SolidBrush foodBrush = new SolidBrush( FoodColor ) )
            {
                int x = _food.Column * CellBlockSize;
                int y = _food.Row * CellBlockSize;
                e.Graphics.FillRectangle( foodBrush, x, y, CellBlockSize, CellBlockSize );
            }

            using( SolidBrush snakeBrush = new SolidBrush( SnakeColor ) )
            {
                SnakeBlock iter = _snakeHead;
                while( iter != null )
                {
                    int x = iter.Location.Column * CellBlockSize;
                    int y = iter.Location.Row * CellBlockSize;
                    e.Graphics.FillRectangle( snakeBrush, x, y, CellBlockSize, CellBlockSize );
                    iter = iter.Next;
                }
            }

            if( _paused )
            {
                using( SolidBrush brush = new SolidBrush( PauseScreenColor ) )
                {
                    e.Graphics.FillRectangle( brush, this.ClientRectangle );
                }
                using( SolidBrush brush = new SolidBrush( PauseScreenTextColor ) )
                {
                    String s = "Paused";
                    SizeF clientSize = this.ClientSize;
                    SizeF textSize = e.Graphics.MeasureString( s, PauseScreenTextFont );
                    e.Graphics.DrawString( s, PauseScreenTextFont, brush, Utilities.CenterText( clientSize, textSize ) );
                }
            }
        }

        private void uxSnakeTimer_Tick( object sender, EventArgs e )
        {
            UpdateSnake();
        }

        private void UpdateSnake()
        {
            if( !_gameOver && !_paused )
            {
                Location newHeadLocation = new Location( _snakeHead.Location, _direction );

                if( IsValidMove( newHeadLocation ) )
                {
                    SnakeBlock head = new SnakeBlock();
                    head.Location = newHeadLocation;
                    head.Next = _snakeHead;

                    _snakeHead.Previous = head;
                    _snakeHead = head;

                    if( _snakeHead.Location == _food )
                    {
                        _snakeLength++;
                        if( _snakeLength == TotalCells )
                        {
                            DisplayGameEnd( true );
                            return;
                        }
                        GenerateFood();
                    }
                    else
                    {
                        _snakeTail = _snakeTail.Previous;
                        _snakeTail.Next = null;
                    }

                    Invalidate();
                }
                else
                {
                    DisplayGameEnd( false );
                }
            }
        }

        private void DisplayGameEnd( bool win )
        {
            _gameOver = true;
            uxSnakeTimer.Stop();
            Invalidate();
            String message;
            if( win )
            {
                message = "You win!";
            }
            else
            {
                message = "You lost!";
            }
            if( MessageBox.Show( message + " Play again?", message, MessageBoxButtons.YesNo ) == DialogResult.Yes )
            {
                StartGame();
            }
            else
            {
                EndGame();
            }
        }
    }
}
