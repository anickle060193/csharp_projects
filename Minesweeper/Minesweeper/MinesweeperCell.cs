using Minesweeper.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper
{
    public class MinesweeperCell
    {
        public event EventHandler Updated;

        public const int MinesweeperCellSize = 30;

        private bool _bomb;
        private bool _flagged;
        private bool _opened;
        private bool _pressed;
        private int _row;
        private int _column;

        public int X { get; private set; }
        public int Y { get; private set; }

        public int Row
        {
            get { return _row; }
            set
            {
                _row = value;
                Y = _row * MinesweeperCellSize;
                OnUpdated( EventArgs.Empty );
            }
        }

        public int Column
        {
            get { return _column; }
            set
            {
                _column = value;
                X = _column * MinesweeperCellSize;
                OnUpdated( EventArgs.Empty );
            }
        }

        public bool Bomb
        {
            get { return _bomb; }
            set
            {
                _bomb = value;
                OnUpdated( EventArgs.Empty );
            }
        }

        public bool Flagged
        {
            get { return _flagged; }
            set
            {
                _flagged = value;
                OnUpdated( EventArgs.Empty );
            }
        }

        public bool Opened
        {
            get { return _opened; }
            set
            {
                _opened = value;
                OnUpdated( EventArgs.Empty );
            }
        }

        public bool Pressed
        {
            get { return _pressed; }
            set
            {
                _pressed = value;
                OnUpdated( EventArgs.Empty );
            }
        }

        public int NeighborBombs { get; set; }

        public MinesweeperCell( int row, int col )
        {
            Row = row;
            Column = col;
        }

        private void OnUpdated( EventArgs e )
        {
            if( Updated != null )
            {
                Updated( this, e );
            }
        }

        public void DrawCell( Graphics g, bool gameOver )
        {
            if( Pressed )
            {
                g.DrawImage( Resources.CellPressed, X, Y );
            }
            else if( !Opened )
            {
                g.DrawImage( Resources.CellUnpressed, X, Y );
                if( gameOver && Bomb )
                {
                    g.DrawImage( Resources.HiddenBomb, X, Y );
                }
                if( Flagged )
                {
                    g.DrawImage( Resources.Flag, X, Y );
                }
            }
            else if( Opened )
            {
                if( Bomb )
                {
                    g.DrawImage( Resources.CellExploded, X, Y );
                    g.DrawImage( Resources.Bomb, X, Y );
                }
                else
                {
                    g.DrawImage( Resources.CellOpened, X, Y );
                    if( NeighborBombs != 0 )
                    {
                        g.DrawString( NeighborBombs.ToString(), new Font( FontFamily.GenericSansSerif, 10 ), Brushes.Red, X + 5, Y + 5 );
                    }
                }
            }
        }
    }
}
