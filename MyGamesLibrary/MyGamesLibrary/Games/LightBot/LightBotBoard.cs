using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyGamesLibrary.Games.LightBot
{
    public partial class LightBotBoard : UserControl
    {
        private const int BORDER_WIDTH = 1;

        private static readonly SolidBrush[] BRUSHES = new SolidBrush[BoardCell.BoardTileTypeCount];
        static LightBotBoard()
        {
            BRUSHES[ (int)BoardCell.BoardTile.Empty ] = new SolidBrush( Color.LightGray );
            BRUSHES[ (int)BoardCell.BoardTile.LitLight ] = new SolidBrush( Color.LightYellow );
            BRUSHES[ (int)BoardCell.BoardTile.UnlitLight ] = new SolidBrush( Color.LightSkyBlue );
        }
        private static readonly Pen BORDER_PEN = new Pen( Color.Black ) { Width = BORDER_WIDTH };
        private static readonly Pen WALL_BORDER_PEN = new Pen( Color.Blue ) { Width = 3 };


        private LightBotGame _game = new LightBotGame();

        public LightBotBoard()
        {
            InitializeComponent();
        }

        private void LightBotBoard_Paint( object sender, PaintEventArgs e )
        {
            float width = this.Width;
            float height = this.Height;

            float cellWidth = width / LightBotGame.COLUMNS;
            float cellHeight = height / LightBotGame.ROWS;

            for( int r = 0; r < LightBotGame.ROWS; r++ )
            {
                for( int c = 0; c < LightBotGame.COLUMNS; c++ )
                {
                    Brush b = BRUSHES[ (int)_game.Board[ r, c ].Type ];
                    float x = c * cellWidth;
                    float y = r * cellHeight;
                    e.Graphics.FillRectangle( b, x, y, cellWidth, cellHeight );
                }
            }

            DrawBorder( e.Graphics );
        }

        private void DrawBorder( Graphics g )
        {
            float totalWidth = this.Width;
            float totalHeight = this.Height;

            float width = totalWidth - BORDER_WIDTH * ( LightBotGame.COLUMNS - 1 );
            float height = totalHeight - BORDER_WIDTH * ( LightBotGame.ROWS - 1 );

            float cellWidth = width / LightBotGame.COLUMNS;
            float cellHeight = height / LightBotGame.ROWS;

            float y = cellHeight;
            for( int r = 0; r < LightBotGame.ROWS; r++ )
            {
                g.DrawLine( BORDER_PEN, 0, y, totalWidth, y );
                y += cellHeight + BORDER_WIDTH;
            }

            float x = cellWidth;
            for( int c = 0; c < LightBotGame.COLUMNS; c++ )
            {
                g.DrawLine( BORDER_PEN, x, 0, x, totalHeight );
                x += cellWidth + BORDER_WIDTH;
            }
        }
    }
}
