using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.IO;

namespace GameOfLife
{
    public partial class GameOfLifeBoardControl : UserControl
    {
        // Dead, Dying, Alive, ComingBack
        private Brush[] _brushes = new Brush[] { Brushes.Black, Brushes.DarkGray, Brushes.White, Brushes.LightGray };
        private bool _wasRunningOnDrag;
        private bool _mouseDown;
        private int _brushSize = 1;
        private Point _patternInsertLocation;

        private MenuItem _start;
        private MenuItem _stop;
        private MenuItem _restart;
        private ContextMenu _patternMenu;
        private GameOfLifeBoard _board = new GameOfLifeBoard();

        public bool Running { get; private set; }

        public GameOfLifeBoardControl()
        {
            InitializeComponent();

            _board.Rows = 200;
            _board.Columns = 200;
            _board.BlockWidth = 5;
            _board.BlockHeight = 5;
            _board.Interval = 50;
            _board.SeedOnStart = true;
            _board.ShowTransitions = true;
            _board.BoardUpdated += new EventHandler( ( s, e ) => Invalidate() );

            ContextMenu contextMenu = new ContextMenu();
            _start = new MenuItem( "Start", ( s, e ) => _board.Start() );
            contextMenu.MenuItems.Add( _start );
            _stop = new MenuItem( "Stop", ( s, e ) => _board.Stop() );
            contextMenu.MenuItems.Add( _stop );
            _restart = new MenuItem( "Restart", ( s, e ) => _board.Restart() );
            contextMenu.MenuItems.Add( _restart );
            contextMenu.MenuItems.Add( "-" );
            contextMenu.MenuItems.Add( new MenuItem( "Clear Board", ( s, e ) => _board.ClearBoard() ) );
            contextMenu.MenuItems.Add( "-" );
            contextMenu.MenuItems.Add( new MenuItem( "Save Board", ( s, e ) => SaveBoard() ) );
            contextMenu.MenuItems.Add( new MenuItem( "Open Board", ( s, e ) => OpenBoard() ) );
            contextMenu.MenuItems.Add( "-" );
            contextMenu.MenuItems.Add( new MenuItem( "Show Options", ( s, e ) => _board.ChangeOptions() ) );
            this.ContextMenu = contextMenu;
            UpdateContextMenu();

            _patternMenu = new ContextMenu();
            foreach( Pattern p in Pattern.Patterns )
            {
                _patternMenu.MenuItems.Add( new MenuItem( p.Name, ( s, e ) => InsertPattern( p ) ) );
            }
        }

        private void UpdateContextMenu()
        {
            if( _board.Running )
            {
                _start.Visible = false;
                _stop.Visible = true;
            }
            else
            {
                _start.Visible = true;
                _stop.Visible = false;
            }
        }

        public void SetStateColor( GameOfLifeBoard.State state, Color color )
        {
            _brushes[ (int)state ] = new SolidBrush( color );
        }

        protected override void OnPaint( PaintEventArgs e )
        {
            Graphics g = e.Graphics;
            int x = this.Location.X;
            int y = this.Location.Y;
            for( int row = 0; row < _board.Rows; row++ )
            {
                for( int col = 0; col < _board.Columns; col++ )
                {
                    GameOfLifeBoard.State state = _board[ row, col ];
                    int thisX = x + col * _board.BlockWidth;
                    int thisY = y + row * _board.BlockHeight;
                    g.FillRectangle( _brushes[ (int)state ], thisX, thisY, _board.BlockWidth, _board.BlockHeight );
                }
            }
        }

        public void Start()
        {
            _board.Start();
        }

        public void Stop()
        {
            _board.Stop();
        }

        public void Restart()
        {
            _board.Restart();
        }

        private Point GetBlockFromCoords( int x, int y )
        {
            int row = y / _board.BlockHeight;
            int column = x / _board.BlockWidth;
            if( row < 0 || row >= _board.Rows || column < 0 || column >= _board.Columns )
            {
                return Point.Empty;
            }
            else
            {
                return new Point( row, column );
            }
        }

        private void InsertPattern( Pattern pattern )
        {
            pattern.InsertPatternInto( _board, _patternInsertLocation.X, _patternInsertLocation.Y );
        }

        private void ManualActivate( int row, int col )
        {
            for( int r = row - _brushSize / 2; r <= row + _brushSize / 2; r++ )
            {
                for( int c = col - _brushSize / 2; c <= col + _brushSize / 2; c++ )
                {
                    _board[ r.Mod( _board.Rows ), c.Mod( _board.Columns ) ] = GameOfLifeBoard.State.Alive;
                }
            }
        }

        private void SaveBoard()
        {
            if( uxSaveFileDialog.ShowDialog() == DialogResult.OK )
            {
                _board.WriteBoardToFile( uxSaveFileDialog.FileName );
            }
        }

        private void OpenBoard()
        {
            if( uxOpenFileDialog.ShowDialog() == DialogResult.OK )
            {
                _board.CreateBoardFromFile( uxOpenFileDialog.FileName );
            }
        }

        private void GameOfLifeBoard_KeyPress( object sender, KeyPressEventArgs e )
        {
            if( !_mouseDown )
            {
                e.Handled = true;
                switch( e.KeyChar )
                {
                    case ' ':
                        _board.Running = !_board.Running;
                        break;

                    case 'r':
                        Restart();
                        break;

                    case 's':
                        SaveBoard();
                        break;

                    case 'o':
                        OpenBoard();
                        break;

                    case 'c':
                        _board.ClearBoard();
                        break;

                    case '+':
                        _brushSize++;
                        break;

                    case '-':
                        _brushSize = Math.Max( 1, _brushSize - 1 );
                        break;

                    case '1':
                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                    case '9':
                    case '0':
                        _brushSize = Convert.ToInt32( e.KeyChar.ToString() + "0" );
                        break;

                    case 'm':
                        _board.ChangeOptions();
                        break;

                    default:
                        e.Handled = false;
                        break;
                }
            }
        }

        private void GameOfLifeBoard_MouseClick( object sender, MouseEventArgs e )
        {
            Point block = GetBlockFromCoords( e.X, e.Y );
            if( block == Point.Empty )
            {
                return;
            }

            if( e.Button == MouseButtons.Left )
            {
                if( ModifierKeys.HasFlag( Keys.Control ) )
                {
                    _patternInsertLocation = block;
                    _patternMenu.Show( this, new Point( e.X, e.Y ) );
                }
                else
                {
                    ManualActivate( block.X, block.Y );
                }
            }
        }

        private void GameOfLifeBoard_MouseDown( object sender, MouseEventArgs e )
        {
            if( e.Button == MouseButtons.Left
             && !ModifierKeys.HasFlag( Keys.Control ) )
            {
                _mouseDown = true;
                _wasRunningOnDrag = _board.Running;
                Stop();
            }
        }

        private void GameOfLifeBoard_MouseUp( object sender, MouseEventArgs e )
        {
            if( e.Button == MouseButtons.Left
             && _mouseDown )
            {
                _mouseDown = false;
                if( _wasRunningOnDrag )
                {
                    Start();
                }
            }
        }

        private void GameOfLifeBoard_MouseMove( object sender, MouseEventArgs e )
        {
            if( _mouseDown )
            {
                Point block = GetBlockFromCoords( e.X, e.Y );
                if( block == Point.Empty )
                {
                    return;
                }
                int row = block.X;
                int column = block.Y;
                ManualActivate( block.X, block.Y );
            }
        }

        private void GameOfLifeBoard_Resize( object sender, EventArgs e )
        {
            _board.ResizeBoard( this.Width, this.Height );
        }
    }
}
