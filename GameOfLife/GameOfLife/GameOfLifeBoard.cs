using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameOfLife
{
    public class GameOfLifeBoard
    {
        public event EventHandler BoardUpdated;
        public event EventHandler RunningStateChanged;

        private const char DEAD_CHARACTER = ' ';
        private const char DYING_CHARACTER = '_';
        private const char ALIVE_CHARACTER = '@';
        private const char COMBING_BACK_CHARACTER = '*';

        private readonly Random _random = new Random();
        private readonly Timer _timer = new Timer();

        public enum State
        {
            Dead,
            Dying,
            Alive,
            ComingBack
        }

        private State[][] _boardState;
        private int _rows;
        private int _columns;
        private int _blockWidth;
        private int _blockHeight;
        private bool _transitioning;
        private bool _hasBeenSeeded;
        private bool _skipUpdate;

        public int BlockWidth
        {
            get
            {
                return _blockWidth;
            }

            set
            {
                _blockWidth = Math.Max( 1, value );
                OnBoardUpdated();
            }
        }

        public int BlockHeight
        {
            get
            {
                return _blockHeight;
            }

            set
            {
                _blockHeight = Math.Max( 1, value );
                OnBoardUpdated();
            }
        }

        public int Rows
        {
            get
            {
                return _rows;
            }

            set
            {
                _rows = Math.Max( 1, value );
                if( _boardState == null )
                {
                    _boardState = CreateBlankBoard();
                }
                else
                {
                    State[][] newState = new State[ _rows ][];
                    for( int i = 0; i < _rows && i < _boardState.Length; i++ )
                    {
                        newState[ i ] = _boardState[ i ];
                    }
                    for( int i = _boardState.Length; i < newState.Length; i++ )
                    {
                        newState[ i ] = new State[ _columns ];
                    }
                    _boardState = newState;
                }
                OnBoardUpdated();
            }
        }

        public int Columns
        {
            get
            {
                return _columns;
            }

            set
            {
                _columns = Math.Max( 1, value );
                if( _boardState == null )
                {
                    _boardState = CreateBlankBoard();
                }
                else
                {
                    for( int i = 0; i < _rows; i++ )
                    {
                        State[] oldRow = _boardState[ i ];
                        State[] newRow = new State[ _columns ];
                        for( int j = 0; j < oldRow.Length && j < newRow.Length; j++ )
                        {
                            newRow[ j ] = oldRow[ j ];
                        }
                        _boardState[ i ] = newRow;
                    }
                }
                OnBoardUpdated();
            }
        }

        public State this[ int row, int column ]
        {
            get
            {
                return _boardState[ row.Mod( _rows ) ][ column.Mod( _columns ) ];
            }

            set
            {
                _boardState[ row.Mod( _rows ) ][ column.Mod( _columns ) ] = value;
                OnBoardUpdated();
            }
        }

        public int Interval
        {
            get
            {
                return _timer.Interval;
            }

            set
            {
                _timer.Interval = value;
            }
        }

        public bool ShowTransitions { get; set; }
        public bool SeedOnStart { get; set; }
        public bool Running
        {
            get
            {
                return _timer.Enabled;
            }

            set
            {
                _timer.Enabled = value;
            }
        }
        public RuleSet RuleSet { get; set; }

        public GameOfLifeBoard()
        {
            _timer.Tick += Timer_Tick;
            RuleSet = RuleSet.Normal;
        }

        public void OnBoardUpdated()
        {
            if( !_skipUpdate )
            {
                EventHandler handler = BoardUpdated;
                if( handler != null )
                {
                    handler( this, EventArgs.Empty );
                }
            }
        }


        private void Timer_Tick( object sender, EventArgs e )
        {
            UpdateBoard();
        }

        public void Start()
        {
            if( !_hasBeenSeeded  )
            {
                SeedBoard();
            }
            _timer.Start();
        }

        public void Stop()
        {
            _timer.Stop();
        }

        public void Restart()
        {
            ClearBoard();
            if( SeedOnStart )
            {
                SeedBoard();
            }
            Start();
        }

        private State[][] CreateBlankBoard()
        {
            State[][] blankBoard = new State[ _rows ][];
            for( int i = 0; i < _rows; i++ )
            {
                blankBoard[ i ] = new State[ _columns ];
            }
            return blankBoard;
        }

        public void ClearBoard()
        {
            _boardState = CreateBlankBoard();
            OnBoardUpdated();
        }

        public void ResizeBoard( int boardWidth, int boardHeight )
        {
            _skipUpdate = true;

            int rows = (int)Math.Ceiling( (float)boardHeight / BlockHeight );
            int columns = (int)Math.Ceiling( (float)boardWidth / BlockWidth );
            Rows = rows;
            Columns = columns;

            _skipUpdate = false;
            OnBoardUpdated();
        }

        private void SeedBoard()
        {
            _skipUpdate = true;
            int total = _rows * _columns;
            for( int i = 0; i < _random.Next( (int)( total * .2 ), (int)( total * .5 ) ); i++ )
            {
                int row = _random.Next( 0, _rows );
                int col = _random.Next( 0, _columns );
                this[ row, col ] = State.Alive;
            }
            _hasBeenSeeded = true;
            _skipUpdate = false;
            OnBoardUpdated();
        }

        private State GetNextState( int row, int col, int neighbors, bool transitioning )
        {
            State currentState = this[ row, col ];
            if( transitioning )
            {
                switch( currentState )
                {
                    case GameOfLifeBoard.State.Dying:
                        return GameOfLifeBoard.State.Dead;

                    case GameOfLifeBoard.State.ComingBack:
                        return GameOfLifeBoard.State.Alive;

                    default:
                        return currentState;
                }
            }
            else
            {
                return RuleSet.GetNextState( neighbors, this[ row, col ], ShowTransitions );
            }
        }

        private void UpdateBoard()
        {
            _skipUpdate = true;
            int[ , ] neighbors = new int[ _rows, _columns ];
            for( int row = 0; row < _rows; row++ )
            {
                for( int col = 0; col < _columns; col++ )
                {
                    if( this[ row, col ] == State.Alive )
                    {
                        for( int r = row - 1; r <= row + 1; r++ )
                        {
                            for( int c = col - 1; c <= col + 1; c++ )
                            {
                                if( r != row || c != col )
                                {
                                    neighbors[ r.Mod( _rows ), c.Mod( _columns ) ]++;
                                }
                            }
                        }
                    }
                }
            }

            State[][] nextUpdate = CreateBlankBoard();
            for( int row = 0; row < _rows; row++ )
            {
                for( int col = 0; col < _columns; col++ )
                {
                    nextUpdate[ row ][ col ] = GetNextState( row, col, neighbors[ row, col ], _transitioning );
                }
            }
            _boardState = nextUpdate;
            if( ShowTransitions || _transitioning )
            {
                _transitioning = !_transitioning;
            }
            _skipUpdate = false;
            OnBoardUpdated();
        }

        public void WriteBoardToFile( string filename )
        {
            StringBuilder sb = new StringBuilder();
            for( int row = 0; row < _rows; row++ )
            {
                for( int col = 0; col < _columns; col++ )
                {
                    switch( this[ row, col ] )
                    {
                        case State.Alive:
                            sb.Append( ALIVE_CHARACTER );
                            break;

                        case State.ComingBack:
                            sb.Append( COMBING_BACK_CHARACTER );
                            break;

                        case State.Dead:
                            sb.Append( DEAD_CHARACTER );
                            break;

                        case State.Dying:
                            sb.Append( DYING_CHARACTER );
                            break;
                    }
                }
                sb.Append( '\n' );
            }

            try
            {
                using( StreamWriter output = new StreamWriter( filename ) )
                {
                    output.Write( sb.ToString() );
                }
            }
            catch( IOException e )
            {
                MessageBox.Show( "The following error occured while saving Game Of Life:\n" + e.ToString() );
            }
        }

        public void CreateBoardFromFile( string filename )
        {
            State[][] newState = CreateBlankBoard();
            try
            {
                using( StreamReader reader = new StreamReader( filename ) )
                {
                    int row = 0;
                    while( !reader.EndOfStream && row < _rows )
                    {
                        int column = 0;
                        string line = reader.ReadLine();
                        foreach( char c in line )
                        {
                            if( column >= _columns )
                            {
                                break;
                            }
                            switch( c )
                            {
                                case ALIVE_CHARACTER:
                                    newState[ row ][ column ] = State.Alive;
                                    break;

                                case DEAD_CHARACTER:
                                    newState[ row ][ column ] = State.Dead;
                                    break;

                                case COMBING_BACK_CHARACTER:
                                    newState[ row ][ column ] = State.ComingBack;
                                    break;

                                case DYING_CHARACTER:
                                    newState[ row ][ column ] = State.Dying;
                                    break;
                            }
                            column++;
                        }
                        row++;
                    }
                }
            }
            catch( IOException io )
            {
                MessageBox.Show( "The following error occured while open Game of Life:\n" + io.ToString() );
            }
            _boardState = newState;
            OnBoardUpdated();
        }

        public void ChangeOptions()
        {
            GameOfLifeOptions options = new GameOfLifeOptions();
            options.InitializeValues( this );
            if( options.ShowDialog() == DialogResult.OK )
            {
                _skipUpdate = true;

                int width = BlockWidth * Columns;
                int height = BlockHeight * Rows;
                //Rows = options.Rows;
                //Columns = options.Columns;
                BlockWidth = options.BlockWidth;
                BlockHeight = options.BlockHeight;

                ResizeBoard( width, height );

                Interval = options.Interval;
                ShowTransitions = options.ShowTransitions;
                SeedOnStart = options.SeedOnStart;
                RuleSet = options.RuleSet;

                _skipUpdate = false;
                OnBoardUpdated();
            }
        }
    }
}
