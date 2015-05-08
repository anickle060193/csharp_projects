using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGamesLibrary.Games.LightBot
{
    public enum MoveType { Forward, LightUp, TurnRight, TurnLeft }

    public class MoveAddedEventArgs : EventArgs
    {
        public MoveType MoveType { get; private set; }
        public int Index { get; private set; }

        public MoveAddedEventArgs( MoveType move, int index )
        {
            MoveType = move;
            Index = index;
        }
    }

    public class MoveRemovedEventArgs : EventArgs
    {
        public int RemovedIndex { get; private set; }

        public MoveRemovedEventArgs( int index )
        {
            RemovedIndex = index;
        }
    }

    public class LightBotGame
    {
        public event EventHandler GameUpdated;
        public event EventHandler<MoveAddedEventArgs> MoveAdded;
        public event EventHandler<MoveAddedEventArgs> PossibleMoveAdded;
        public event EventHandler<MoveRemovedEventArgs> MoveRemoved;
        public event EventHandler<MoveRemovedEventArgs> PossibleMoveRemoved;

        public const int ROWS = 10;
        public const int COLUMNS = 10;

        private List<MoveType> _moves = new List<MoveType>();

        public BoardCell[ , ] Board { get; private set; }
        public Location PlayerLocation { get; private set; }
        public Direction PlayerFacing { get; private set; }
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
        public bool Executing { get; private set; }

        public LightBotGame()
        {
            Board = new BoardCell[ ROWS, COLUMNS ];

            Initialize();
        }

        public void Initialize()
        {
            PlayerLocation = new Location( 0, 0 );
            PlayerFacing = Direction.Down;

            for( int r = 0; r < LightBotGame.ROWS; r++ )
            {
                for( int c = 0; c < LightBotGame.COLUMNS; c++ )
                {
                    Board[ r, c ] = new BoardCell( BoardCell.BoardTile.Empty );
                }
            }

            Location light = Location.GenerateRandomLocation( ROWS, COLUMNS );
            Board[ light.Row, light.Column ].Type = BoardCell.BoardTile.UnlitLight;

            SendUpdate();
        }

        #region Event Handler Callers

        protected void OnGameUpdated( EventArgs e )
        {
            EventHandler handler = GameUpdated;
            if( handler != null )
            {
                handler( this, e );
            }
        }

        protected void OnMoveAdded( MoveAddedEventArgs e )
        {
            EventHandler<MoveAddedEventArgs> handler = MoveAdded;
            if( handler != null )
            {
                handler( this, e );
            }
        }

        protected void OnPossibleMoveAdded( MoveAddedEventArgs e )
        {
            EventHandler<MoveAddedEventArgs> handler = PossibleMoveAdded;
            if( handler != null )
            {
                handler( this, e );
            }
        }

        protected void OnMoveRemoved( MoveRemovedEventArgs e )
        {
            EventHandler<MoveRemovedEventArgs> handler = MoveRemoved;
            if( handler != null )
            {
                handler( this, e );
            }
        }

        protected void OnPossibleMoveRemoved( MoveRemovedEventArgs e )
        {
            EventHandler<MoveRemovedEventArgs> handler = PossibleMoveRemoved;
            if( handler != null )
            {
                handler( this, e );
            }
        }

        #endregion

        private void SendUpdate()
        {
            OnGameUpdated( EventArgs.Empty );
        }

        public void Reset()
        {
            while( _moves.Count > 0 )
            {
                RemoveMove( 0 );
            }
            Initialize();
        }

        public void AddMove( MoveType move )
        {
            if( Executing )
            {
                return;
            }
            _moves.Add( move );
            OnMoveAdded( new MoveAddedEventArgs( move, _moves.Count - 1 ) );
        }

        public void RemoveMove( int index )
        {
            if( Executing )
            {
                return;
            }
            _moves.RemoveAt( index );
            OnMoveRemoved( new MoveRemovedEventArgs( index ) );
        }

        public async void Execute()
        {
            Executing = true;

            foreach( MoveType move in _moves )
            {
                await Task.Delay( 1000 );
                ExecuteMove( move );
            }

            Executing = false;
        }

        private void ExecuteMove( MoveType move )
        {
            switch( move )
            {
                case MoveType.Forward:
                    Location newLocation = new Location( PlayerLocation, PlayerFacing );
                    if( newLocation.WithinBounds( ROWS, COLUMNS ) )
                    {
                        PlayerLocation = newLocation;
                    }
                    SendUpdate();
                    break;

                case MoveType.LightUp:
                    BoardCell cell = Board[ PlayerLocation.Row, PlayerLocation.Column ];
                    if( cell.Type == BoardCell.BoardTile.UnlitLight )
                    {
                        cell.Type = BoardCell.BoardTile.LitLight;
                    }
                    SendUpdate();
                    break;

                case MoveType.TurnLeft:
                    switch( PlayerFacing )
                    {
                        case Direction.Down:
                            PlayerFacing = Direction.Right;
                            break;

                        case Direction.Left:
                            PlayerFacing = Direction.Down;
                            break;

                        case Direction.Right:
                            PlayerFacing = Direction.Up;
                            break;
                    
                        case Direction.Up:
                            PlayerFacing = Direction.Left;
                            break;
                    }
                    SendUpdate();
                    break;

                case MoveType.TurnRight:
                    switch( PlayerFacing )
                    {
                        case Direction.Down:
                            PlayerFacing = Direction.Left;
                            break;

                        case Direction.Left:
                            PlayerFacing = Direction.Up;
                            break;

                        case Direction.Right:
                            PlayerFacing = Direction.Down;
                            break;

                        case Direction.Up:
                            PlayerFacing = Direction.Right;
                            break;
                    }
                    SendUpdate();
                    break;
            }
        }
    }
}
