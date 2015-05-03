using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGamesLibrary.Games.LightBot
{
    public class LightBotGame
    {
        public event EventHandler GameUpdated;
        public enum MoveType { Forward, LightUp, TurnRight, TurnLeft }

        public const int ROWS = 10;
        public const int COLUMNS = 10;

        private List<MoveType> _moves = new List<MoveType>();

        public BoardCell[ , ] Board { get; private set; }
        public Location PlayerLocation { get; private set; }
        public IEnumerable<MoveType> Moves
        {
            get { return _moves.AsEnumerable(); }
        }
        public IEnumerable<MoveType> PossibleMoves
        {
            get
            {
                foreach( MoveType moveType in Enum.GetValues( typeof( MoveType ) ) )
                {
                    yield return moveType;
                }
            }
        }

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

        protected void OnGameUpdated( EventArgs e )
        {
            EventHandler handler = GameUpdated;
            if( handler != null )
            {
                handler( this, e );
            }
        }

        private void SendUpdate()
        {
            OnGameUpdated( EventArgs.Empty );
        }

        public void AddMove( MoveType move )
        {
            _moves.Add( move );
            SendUpdate();
        }

        public void RemoveMove( int index )
        {
            _moves.RemoveAt( index );
            SendUpdate();
        }
    }
}
