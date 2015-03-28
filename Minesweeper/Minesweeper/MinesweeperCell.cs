using Minesweeper.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper
{
    public class CellChangedEventArgs : EventArgs
    {
        public enum ChangedProperty { Bomb, Flagged, Opened, Pressed, Row, Column }
        public ChangedProperty ChangedCellProperty { get; set; }
        public object OldValue { get; set; }
        public object NewValue { get; set; }

        public CellChangedEventArgs( ChangedProperty p, object oldValue, object newValue )
        {
            ChangedCellProperty = p;
            OldValue = oldValue;
            NewValue = newValue;
        }
    }

    public delegate void CellChangedEventHandler( object sender, CellChangedEventArgs e );

    public class MinesweeperCell
    {
        public event CellChangedEventHandler CellChanged;

        public const int MinesweeperCellSize = 30;
        private static readonly Font _bombFont = new Font( "Courier New", 16, FontStyle.Bold );
        private static readonly SolidBrush[] _brushes = new SolidBrush[]
        {
            /* 0 */ new SolidBrush( Color.Transparent ),
            /* 1 */ new SolidBrush( Color.Blue ),
            /* 2 */ new SolidBrush( Color.Green ),
            /* 3 */ new SolidBrush( Color.Red ),
            /* 4 */ new SolidBrush( Color.Purple ),
            /* 5 */ new SolidBrush( Color.Maroon ),
            /* 6 */ new SolidBrush( Color.Cyan ),
            /* 7 */ new SolidBrush( Color.Black ),
            /* 8 */ new SolidBrush( Color.Gray ),
        };

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
                int old = _row;
                _row = value;
                Y = _row * MinesweeperCellSize;
                OnCellChanged( new CellChangedEventArgs( CellChangedEventArgs.ChangedProperty.Row, old, _row ) );
            }
        }

        public int Column
        {
            get { return _column; }
            set
            {
                int old = _column;
                _column = value;
                X = _column * MinesweeperCellSize;
                OnCellChanged( new CellChangedEventArgs( CellChangedEventArgs.ChangedProperty.Column, old, _column ) );
            }
        }

        public bool Bomb
        {
            get { return _bomb; }
            set
            {
                bool old = _bomb;
                _bomb = value;
                OnCellChanged( new CellChangedEventArgs( CellChangedEventArgs.ChangedProperty.Bomb, old, _bomb ) );
            }
        }

        public bool Flagged
        {
            get { return _flagged; }
            set
            {
                bool old = _flagged;
                _flagged = value;
                OnCellChanged( new CellChangedEventArgs( CellChangedEventArgs.ChangedProperty.Flagged, old, _flagged ) );
            }
        }

        public bool Opened
        {
            get { return _opened; }
            set
            {
                bool old = _opened;
                _opened = value;
                OnCellChanged( new CellChangedEventArgs( CellChangedEventArgs.ChangedProperty.Opened, old, _opened ) );
            }
        }

        public bool Pressed
        {
            get { return _pressed; }
            set
            {
                bool old = _pressed;
                _pressed = value;
                OnCellChanged( new CellChangedEventArgs( CellChangedEventArgs.ChangedProperty.Pressed, old, _pressed ) );
            }
        }

        public int NeighborBombs { get; set; }

        public MinesweeperCell( int row, int col )
        {
            Row = row;
            Column = col;
        }

        private void OnCellChanged( CellChangedEventArgs e )
        {
            if( CellChanged != null )
            {
                CellChanged( this, e );
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
                        g.TextRenderingHint = TextRenderingHint.AntiAlias;
                        string s = NeighborBombs.ToString();
                        SizeF size = g.MeasureString( s, _bombFont, MinesweeperCellSize, StringFormat.GenericTypographic );
                        float x = X + ( MinesweeperCellSize - size.Width ) / 2;
                        float y = Y + ( MinesweeperCellSize - size.Height ) / 2;
                        g.DrawString( NeighborBombs.ToString(), _bombFont, _brushes[ NeighborBombs ], x, y, StringFormat.GenericTypographic );
                    }
                }
            }
            //if( Bomb )
            //{
            //    g.DrawImage( Resources.HiddenBomb, X, Y );
            //}
        }
    }
}
