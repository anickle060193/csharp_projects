using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGamesLibrary.Games.LightBot
{
    public class BoardCell
    {
        public const int BoardTileTypeCount = 3;
        public enum BoardTile { Empty, UnlitLight, LitLight }

        public BoardTile Type { get; set; }

        public BoardCell( BoardTile type )
        {
            Type = type;
        }
    }
}
