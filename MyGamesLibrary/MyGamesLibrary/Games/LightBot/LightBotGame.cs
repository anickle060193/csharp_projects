using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGamesLibrary.Games.LightBot
{
    public class LightBotGame
    {
        public const int ROWS = 10;
        public const int COLUMNS = 10;

        public BoardCell[ , ] Board { get; private set; }
        public Location PlayerLocation { get; private set; }

        public LightBotGame()
        {
            PlayerLocation = new Location( 0, 0 );

            Board = new BoardCell[ ROWS, COLUMNS ];

            for( int r = 0; r < LightBotGame.ROWS; r++ )
            {
                for( int c = 0; c < LightBotGame.COLUMNS; c++ )
                {
                    Board[ r, c ] = new BoardCell( BoardCell.BoardTile.Empty );
                }
            }
        }
    }
}
