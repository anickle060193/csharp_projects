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
        enum Direction { Left, Right, Up, Down }
        enum Deviation { Vertical, Horizontal }

        private const int BoardSize = 500;
        private const int Rows = 10;
        private const int Columns = 10;
        private const int SnakeSpawnSafeBorderWidth = 2;
        private const int CellBlockSize = 30;

        private static readonly int[][] DirectionDeviations = new int[][]
        {
            /* Direction            V   H */
            /* Left  */ new int[]{  0, -1 },
            /* Right */ new int[]{  0,  1 },
            /* Up    */ new int[]{ -1,  0 },
            /* Down  */ new int[]{  1,  0 }
        };

        class SnakeBlock
        {
            public int Row { get; set; }
            public int Column { get; set; }
            public SnakeBlock Previous { get; set; }
            public SnakeBlock Next { get; set; }
        }

        private int _row;
        private int _col;
        private Direction _direction;
        private SnakeBlock _snakeHead;
        private SnakeBlock _snakeTail;

        public override string GameName { get { return "Snake"; } }

        public SnakeForm()
        {
            InitializeComponent();

            this.ClientSize = new Size( BoardSize, BoardSize );
        }

        protected override void OnGameStarted( EventArgs e )
        {
            InitializeBoard();
        }

        private void InitializeBoard()
        {
            _row = Utilities.R.Next( SnakeSpawnSafeBorderWidth, Rows - SnakeSpawnSafeBorderWidth );
            _col = Utilities.R.Next( SnakeSpawnSafeBorderWidth, Columns - SnakeSpawnSafeBorderWidth );
            _snakeHead = _snakeTail = new SnakeBlock();
            Invalidate();
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
            switch( e.KeyCode )
            {
                case Keys.Left:
                    _direction = Direction.Left;
                    break;

                case Keys.Right:
                    _direction = Direction.Left;
                    break;

                case Keys.Down:
                    _direction = Direction.Left;
                    break;

                case Keys.Up:
                    _direction = Direction.Left;
                    break;
            }
        }

        protected override void OnPaint( PaintEventArgs e )
        {
            SnakeBlock block = _snakeHead;
            while( block != null )
            {
                e.Graphics.FillRectangle( Brushes.Black, block.Column * CellBlockSize, block.Row * CellBlockSize, CellBlockSize, CellBlockSize );
                block = block.Next;
            }
        }

        private void uxSnakeTimer_Tick( object sender, EventArgs e )
        {
            SnakeBlock head = new SnakeBlock();
            head.Next = _snakeHead;
            _snakeHead.Previous = head;

            head.Column = _snakeHead.Column + DirectionDeviations[ (int)_direction ][ (int)Deviation.Horizontal ];
            head.Row = _snakeHead.Row + DirectionDeviations[ (int)_direction ][ (int)Deviation.Vertical ];
            
            _snakeHead = head;
            _snakeTail = _snakeTail.Previous;
            
            Invalidate();
        }
    }
}
