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
    public partial class ReversiForm : GameForm
    {
        private const int Rows = 8;
        private const int Columns = 8;
        private const int Cells = Rows * Columns;
        private const int CellSize = 60;
        private const int PieceSize = 45;
        private const int BlackTurn = 0;
        private const int WhiteTurn = 1;

        enum Direction
        {
            None  = 0x00000b,
            Valid = 0x00001b,
            Left  = 0x00010b,
            Right = 0x00100b,
            Up    = 0x01000b,
            Down  = 0x10000b,
            All   = Valid | Left | Right | Up | Down,
        }

        struct DirectionHelper
        {
            public readonly Direction Direction;
            public readonly int VerticalIncrement;
            public readonly int HorizontalIncrement;

            public DirectionHelper( Direction d, int vInc, int hInc )
            {
                Direction = d;
                VerticalIncrement = vInc;
                HorizontalIncrement = hInc;
            }
        }

        private static readonly Color BackgroundColor = Color.Green;
        private static readonly Color BorderColor = Color.Black;
        private static readonly SolidBrush BlackPieceBrush = (SolidBrush)Brushes.Black;
        private static readonly SolidBrush WhitePieceBrush = (SolidBrush)Brushes.White;
        private static readonly Point InvalidPoint = new Point( -1, -1 );
        private static readonly DirectionHelper[] Directions = new DirectionHelper[]
        {
            new DirectionHelper( Direction.Up, -1, 0 ),
            new DirectionHelper( Direction.Down, 1, 0 ),
            new DirectionHelper( Direction.Left, 0, -1 ),
            new DirectionHelper( Direction.Right, 0, 1 ),
        };

        enum Piece { Unplayed, Black, White }

        private Piece[ , ] _board;
        private Panel[ , ] _panels;
        private int _turn;
        private int _plays;

        public override string GameName { get { return "Reversi"; } }

        public ReversiForm()
        {
            InitializeComponent();
        }

        protected override void OnGameStarted( EventArgs e )
        {
            InitializeBoard();
        }

        private void InitializeBoard()
        {
            _panels = new Panel[ Rows, Columns ];

            TableLayoutPanel t = new TableLayoutPanel();
            t.AutoSize = true;
            t.Margin = new Padding();
            t.RowStyles.Clear();
            t.ColumnStyles.Clear();
            t.BackColor = BorderColor;
            for( int i = 0; i < Rows; i++ )
            {
                t.RowStyles.Add( new RowStyle( SizeType.AutoSize ) );
            }
            t.RowCount = Rows;
            for( int i = 0; i < Columns; i++ )
            {
                t.ColumnStyles.Add( new ColumnStyle( SizeType.AutoSize ) );
            }
            t.ColumnCount = Columns;
            for( int r = 0; r < Rows; r++ )
            {
                for( int c = 0; c < Columns; c++ )
                {
                    Panel p = new Panel()
                    {
                        BackColor = Color.Green,
                        Tag = new Point( r, c ),
                        Dock = DockStyle.Fill,
                        Margin = new Padding( 1 ),
                        ClientSize = new Size( CellSize, CellSize )
                    };
                    p.Click += Cell_Click;
                    p.Paint += Cell_Paint;
                    t.Controls.Add( p, c, r );
                    _panels[ r, c ] = p;
                }
            }
            this.Controls.Clear();
            this.Controls.Add( t );
            _board = new Piece[ Rows, Columns ];

            _plays = 0;
            _turn = BlackTurn;
        }

        private void Cell_Paint( object sender, PaintEventArgs e )
        {
            Panel p = sender as Panel;
            Point point = (Point)p.Tag;
            Piece piece = _board[ point.X, point.Y ];
            if( piece != Piece.Unplayed )
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                SolidBrush b = piece == Piece.Black ? BlackPieceBrush : WhitePieceBrush;
                float offset = ( CellSize - PieceSize ) / 2.0f;
                e.Graphics.FillEllipse( b, offset, offset, PieceSize, PieceSize );
            }
        }

        private void Cell_Click( object sender, EventArgs e )
        {
            Panel p = sender as Panel;
            Point point = (Point)p.Tag;
            Play( point.X, point.Y );
        }

        private bool HasPieceInDirection( int row, int col, int vertIncrement, int horzIncrement, Piece piece )
        {
            while( 0 <= row && row < Rows
                && 0 <= col && col < Columns )
            {
                if( _board[ row, col] == piece )
                {
                    return true;
                }
                else if( _board[ row, col] == Piece.Unplayed )
                {
                    return false;
                }
                row += vertIncrement;
                col += horzIncrement;
            }
            return false;
        }

        private Direction GetDirections( int row, int col, Piece piece )
        {
            Direction direction = Direction.None;
            foreach( DirectionHelper d in Directions )
            {
                int r = row + d.VerticalIncrement;
                int c = col + d.HorizontalIncrement;
                if( 0 <= r && r < Rows
                 && 0 <= c && c < Columns )
                {
                    if( _board[ r, c ] == Piece.Unplayed
                     || _board[ r, c ] == piece )
                    {
                        continue;
                    }
                    if( HasPieceInDirection( r, c, d.VerticalIncrement, d.HorizontalIncrement, piece ) )
                    {
                        direction |= d.Direction;
                    }
                }
            }
            return direction;
        }

        private Direction ValidatePlay( int row, int col, Piece piece )
        {
            if( _board[ row, col ] == Piece.Unplayed )
            {
                if( _plays < 4 )
                {
                    if( ( row == 3 || row == 4 )
                     && ( col == 3 || col == 4 ) )
                    {
                        return Direction.Valid;
                    }
                }
                else
                {
                    return GetDirections( row, col, piece );
                }
            }
            return Direction.None;
        }

        private void Play( int row, int col )
        {
            Piece piece = _turn == BlackTurn ? Piece.Black : Piece.White;
            Direction direction = ValidatePlay( row, col, piece );
            if( direction != Direction.None )
            {
                _board[ row, col ] = piece;
                _panels[ row, col ].Invalidate();

                foreach( DirectionHelper d in Directions )
                {
                    if( ( direction & d.Direction ) == d.Direction )
                    {
                        int r = row + d.VerticalIncrement;
                        int c = col + d.HorizontalIncrement;
                        while( 0 <= r && r < Rows
                            && 0 <= c && c < Columns
                            && _board[ r, c ] != piece )
                        {
                            _board[ r, c ] = piece;
                            _panels[ r, c ].Invalidate();
                            r += d.VerticalIncrement;
                            c += d.HorizontalIncrement;
                        }
                    }
                }
                _turn = 1 - _turn;
                _plays++;
                if( GameOver())
                {
                    MessageBox.Show( "Game Over!" );
                }
            }
        }

        private bool GameOver()
        {
            if( _plays == Cells )
            {
                return true;
            }
            for( int row = 0; row < Rows; row++ )
            {
                for( int col = 0; col < Columns; col++ )
                {
                    Direction d = ValidatePlay( row, col, _turn == BlackTurn ? Piece.Black : Piece.White );
                    if( d != Direction.None )
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
