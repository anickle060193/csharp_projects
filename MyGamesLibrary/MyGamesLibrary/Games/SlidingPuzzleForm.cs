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
    public partial class SlidingPuzzleForm : GameForm
    {
        enum Direction { Up, Down, Left, Right, Count }
        
        private const int ROWS = 4;
        private const int COLUMNS = 4;

        private readonly Random R = new Random();

        private Button[ , ] _buttons;
        private int[ , ] _board;
        private bool _gameOver;

        public override string GameName { get { return "Sliding Puzzle"; } }

        public SlidingPuzzleForm()
        {
            InitializeComponent();

            InitializeBoard();
        }

        public override void StartGame()
        {
            InitializeBoard();

            base.StartGame();
        }

        private void InitializeBoard()
        {
            uxFlowLayout.Controls.Clear();

            _buttons = new Button[ ROWS, COLUMNS ];
            _board = new int[ ROWS, COLUMNS ];

            float buttonWidth = (float)uxFlowLayout.ClientSize.Width / COLUMNS;
            float buttonHeight = (float)uxFlowLayout.ClientSize.Height / ROWS;
            for( int r = 0; r < ROWS; r++ )
            {
                for( int c = 0; c < COLUMNS; c++ )
                {
                    _board[ r, c ] = r * COLUMNS + c;
                    Button b = new Button()
                    {
                        Size = new Size( (int)buttonWidth, (int)buttonHeight ),
                        Font = new Font( FontFamily.GenericSansSerif, 40f ),
                        Margin = new Padding(),
                        Text = _board[ r, c ].ToString(),
                        Tag = new Point( r, c )
                    };
                    b.Click += Button_Click;
                    uxFlowLayout.Controls.Add( b );
                    _buttons[ r, c ] = b;
                }
            }

            RandomizeBoard();
        }

        private int GetNumberFromButton( Button b )
        {
            Point p = (Point)b.Tag;
            return _board[ p.X, p.Y ];
        }

        private void RandomizeBoard()
        {
            for( int r = 0; r < ROWS; r++ )
            {
                for( int c = 0; c < COLUMNS; c++ )
                {
                    SwapButtons( r, c, R.Next( ROWS ), R.Next( COLUMNS ) );
                }
            }
        }

        private void UpdateBoard()
        {
            for( int r = 0; r < ROWS; r++ )
            {
                for( int c = 0; c < COLUMNS; c++ )
                {
                    int number = GetNumberFromButton( _buttons[ r, c ] );
                    _buttons[ r, c ].Text = number != 0 ? number.ToString() : "";
                    _buttons[ r, c ].Enabled = number != 0;
                }
            }
            CheckIfGameOver();
        }

        private void CheckIfGameOver()
        {
            for( int r = 0; r < ROWS; r++ )
            {
                for( int c = 0; c < COLUMNS; c++ )
                {
                    if( _board[ r, c ] != r * COLUMNS + c + 1 && _board[ r, c ] != 0 )
                    {
                        _gameOver = false;
                        return;
                    }
                }
            }
            _gameOver = true;
            if( MessageBox.Show( "Do you want to play again?", "Finished", MessageBoxButtons.YesNo ) == DialogResult.Yes )
            {
                InitializeBoard();
            }
            else
            {
                Close();
            }
        }

        private void SwapButtons( int row1, int col1, int row2, int col2 )
        {
            int temp = _board[ row1, col1 ];
            _board[ row1, col1 ] = _board[ row2, col2 ];
            _board[ row2, col2 ] = temp;

            UpdateBoard();
        }

        private Point GetButtonPoint( int row, int col, Direction direction )
        {
            switch( direction )
            {
                case Direction.Left:
                    return new Point( row - 1, col );
                case Direction.Right:
                    return new Point( row + 1, col );
                case Direction.Up:
                    return new Point( row, col - 1 );
                case Direction.Down:
                    return new Point( row, col + 1 );
                default:
                    return new Point( -1, -1 );
            }
        }

        private bool IsValidPoint( Point p )
        {
            return 0 <= p.X && p.X < ROWS && 0 <= p.Y && p.Y < COLUMNS;
        }

        private int GetButtonNumber( int row, int col, Direction direction )
        {
            Point p = GetButtonPoint( row, col, direction );
            if( IsValidPoint( p ) )
            {
                return _board[ p.X, p.Y ];
            }
            else
            {
                return -1;
            }
        }

        private void Button_Click( object sender, EventArgs e )
        {
            if( _gameOver )
            {
                return;
            }
            Button b = sender as Button;
            int row = ( (Point)b.Tag ).X;
            int col = ( (Point)b.Tag ).Y;
            for( Direction direction = (Direction)0; direction < Direction.Count; direction++ )
            {
                if( GetButtonNumber( row, col, direction ) == 0 )
                {
                    Point p = GetButtonPoint( row, col, direction );
                    SwapButtons( row, col, p.X, p.Y );
                    break;
                }
            }
        }
    }
}
