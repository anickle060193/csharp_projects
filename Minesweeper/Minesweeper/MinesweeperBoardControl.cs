using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Minesweeper.Properties;

namespace Minesweeper
{
    public partial class MinesweeperBoardControl : UserControl
    {
        private readonly Random _random = new Random();
        private MinesweeperCell[ , ] _cells;
        private MinesweeperCell _onDownCell;

        private int _rows;
        private int _columns;
        private int _bombs;
        private bool _bombsPlaced;
        private bool _gameOver;
        private bool _firstClick = true;
        private int _flagsRemaining;
        private int _unflaggedBombsRemaining;

        public int Rows
        {
            get { return _rows; }
            set
            {
                _rows = value;
                this.Size = new Size( Columns * MinesweeperCell.MinesweeperCellSize, Rows * MinesweeperCell.MinesweeperCellSize );
            }
        }

        public int Columns
        {
            get { return _columns; }
            set
            {
                _columns = value;
                this.Size = new Size( Columns * MinesweeperCell.MinesweeperCellSize, Rows * MinesweeperCell.MinesweeperCellSize );
            }
        }

        public MinesweeperBoardControl()
        {
            InitializeComponent();

            this.MouseDown += MinesweeperBoardControl_MouseDown;
            this.MouseUp += MinesweeperBoardControl_MouseUp;

            Rows = 20;
            Columns = 20;

            _cells = new MinesweeperCell[ Columns, Rows ];

            for( int r = 0; r < Rows; r++ )
            {
                for( int c = 0; c < Columns; c++ )
                {
                    MinesweeperCell cell = new MinesweeperCell( r, c );
                    cell.CellChanged += MinesweeperCell_CellChanged;
                    _cells[ r, c ] = cell;
                }
            }

            SetBombs( 50 );
        }

        private void MinesweeperCell_CellChanged( object sender, CellChangedEventArgs e )
        {
            MinesweeperCell cell = sender as MinesweeperCell;
            if( cell != null && e.OldValue != e.NewValue )
            {
                if( e.ChangedCellProperty == CellChangedEventArgs.ChangedProperty.Bomb )
                {
                    if( cell.Bomb )
                    {
                        _bombs++;
                        if( !cell.Flagged )
                        {
                            _flagsRemaining++;
                            _unflaggedBombsRemaining++;
                        }
                        PerformActionOnNeighbors( delegate( MinesweeperCell c )
                        {
                            c.NeighborBombs++;
                        }, cell );
                    }
                    else
                    {
                        _bombs--;
                        if( cell.Flagged )
                        {
                            _flagsRemaining--;
                            _unflaggedBombsRemaining--;
                        }
                        PerformActionOnNeighbors( delegate( MinesweeperCell c )
                        {
                            c.NeighborBombs--;
                        }, cell );
                    }
                }
                else if( e.ChangedCellProperty == CellChangedEventArgs.ChangedProperty.Flagged )
                {
                    if( cell.Flagged )
                    {
                        _flagsRemaining--;
                        if( cell.Bomb )
                        {
                            _unflaggedBombsRemaining--;
                            if( _unflaggedBombsRemaining == 0 )
                            {
                                _gameOver = true;
                            }
                        }
                    }
                    else
                    {
                        _flagsRemaining++;
                        if( cell.Bomb )
                        {
                            _unflaggedBombsRemaining++;
                        }
                    }
                }

                Invalidate( new Rectangle( cell.X, cell.Y, MinesweeperCell.MinesweeperCellSize, MinesweeperCell.MinesweeperCellSize ) );
            }
        }

        public void SetBombs( int bombs )
        {
            bombs = Math.Min( bombs, Rows * Columns );
            for( int i = 0; i < bombs; i++ )
            {
                int row = _random.Next( Rows );
                int col = _random.Next( Columns );
                while( _cells[ row, col ].Bomb )
                {
                    row++;
                    if( row >= Rows )
                    {
                        row = 0;
                        col++;
                        if( col >= Columns )
                        {
                            col = 0;
                        }
                    }
                }
                _cells[ row, col ].Bomb = true;
            }
            _bombsPlaced = true;
        }

        private void PerformActionOnNeighbors( Action<MinesweeperCell> action, MinesweeperCell cell )
        {
            for( int r = Math.Max( 0, cell.Row - 1 ); r <= Math.Min( Rows - 1, cell.Row + 1 ); r++ )
            {
                for( int c = Math.Max( 0, cell.Column - 1 ); c <= Math.Min( Columns - 1, cell.Column + 1 ); c++ )
                {
                    if( r != cell.Row || c != cell.Column )
                    {
                        action( _cells[ r, c ] );
                    }
                }
            }
        }

        private MinesweeperCell GetCellFromPoint( int x, int y )
        {
            int row = (int)( y / (float)MinesweeperCell.MinesweeperCellSize );
            int col = (int)( x / (float)MinesweeperCell.MinesweeperCellSize );
            if( 0 <= row && row < Rows
             && 0 <= col && col < Columns )
            {
                return _cells[ row, col ];
            }
            else
            {
                return null;
            }
        }

        private void MinesweeperBoardControl_MouseDown( object sender, MouseEventArgs e )
        {
            if( !_gameOver )
            {
                if( _onDownCell != null )
                {
                    _onDownCell.Pressed = false;
                }

                MinesweeperCell cell = GetCellFromPoint( e.X, e.Y );
                if( cell != null )
                {
                    if( e.Button == MouseButtons.Left )
                    {
                        if( !cell.Flagged
                         && !cell.Opened )
                        {
                            cell.Pressed = true;
                            _onDownCell = cell;
                        }
                    }
                    else if( e.Button == MouseButtons.Right )
                    {
                        _onDownCell = cell;
                    }
                }
            }
        }

        private void MinesweeperBoardControl_MouseUp( object sender, MouseEventArgs e )
        {
            if( _onDownCell != null )
            {
                MinesweeperCell cell = GetCellFromPoint( e.X, e.Y );
                if( cell == _onDownCell )
                {
                    if( e.Button == MouseButtons.Left
                     || e.Button == MouseButtons.Right )
                    {
                        if( _firstClick )
                        {
                            _firstClick = false;
                            if( cell.Bomb )
                            {
                                cell.Bomb = false;
                                MinesweeperCell temp;
                                do
                                {
                                    temp = _cells[ _random.Next( Rows ), _random.Next( Columns ) ];
                                }
                                while( temp.Bomb );
                                temp.Bomb = true;
                            }
                        }
                    }
                    if( e.Button == MouseButtons.Left )
                    {
                        _onDownCell.Pressed = false;
                        PropagateOpen( _onDownCell );
                    }
                    else if( e.Button == MouseButtons.Right )
                    {
                        if( _onDownCell.Flagged )
                        {
                            _onDownCell.Flagged = false;
                        }
                        else if( _flagsRemaining > 0 )
                        {
                            _onDownCell.Flagged = true;
                        }
                    }
                }
                else
                {
                    _onDownCell.Pressed = false;
                }
                _onDownCell = null;
            }
        }

        private void PropagateOpen( MinesweeperCell cell )
        {
            if( !cell.Bomb
             && !cell.Opened )
            {
                cell.Flagged = false;
                cell.Opened = true;

                if( cell.NeighborBombs == 0 )
                {
                    PerformActionOnNeighbors( PropagateOpen, cell );
                }
            }
            else if( cell.Bomb )
            {
                cell.Opened = true;
                //_gameOver = true;
                Invalidate();
            }
        }

        protected override void OnPaint( PaintEventArgs e )
        {
            Graphics g = e.Graphics;
            int startRow = (int)( ( e.ClipRectangle.Top - 1 ) / (float)MinesweeperCell.MinesweeperCellSize );
            int startColumn = (int)( ( e.ClipRectangle.Left - 1 ) / (float)MinesweeperCell.MinesweeperCellSize );
            int endRow = (int)( ( e.ClipRectangle.Bottom - 1 ) / (float)MinesweeperCell.MinesweeperCellSize );
            int endColumn = (int)( ( e.ClipRectangle.Right - 1 ) / (float)MinesweeperCell.MinesweeperCellSize );
            for( int r = startRow; r <= endRow; r++ )
            {
                for( int c = startColumn; c <= endColumn; c++ )
                {
                    _cells[ r, c ].DrawCell( g, _gameOver );
                }
            }
        }
    }
}
