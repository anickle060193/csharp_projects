﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyGamesLibrary.Games
{
    public partial class TicTacToeForm : GameForm
    {
        private const int ROWS = 3;
        private const int COLUMNS = 3;
        private const int TOTAL_CELLS = ROWS * COLUMNS;
        private static readonly String[] PLAYER_REPRESENTATION = new String[] { "X", "O", "" };
        private const int HUMAN_PLAYER = 0;
        private const int COMPUTER_PLAYER = 1;
        private const int UNPLAYED = 2;
        private const int WINNING_SEQUENCE_LENGTH = 3;
        private const int VERTICAL_INCREMENT_INDEX = 0;
        private const int HORIZONTAL_INCREMENT_INDEX = 1;
        private static readonly int[][][] SEQUENCE_DIRECTIONS = new int[][][]
            {
                new int[][]
                {
                    new int[] { 1, -1 },
                    new int[] { -1, 1 }
                },
                new int[][]
                {
                    new int[] { 1, 0 },
                    new int[] { -1, 0 }
                },
                new int[][]
                {
                    new int[] { 1, 1 },
                    new int[] { -1, -1 }
                },
                new int[][]
                {
                    new int[] { 0, -1 },
                    new int[] { 0, 1 }
                }
            };

        private Button[ , ] _buttons;
        private int _playsMade;
        private int[ , ] _board;

        public override string GameName { get { return "Tic-Tac-Toe"; } }

        public TicTacToeForm()
        {
            InitializeComponent();

            InitializeBoard();
        }

        protected override void OnGameStarted( EventArgs e )
        {
            InitializeBoard();
        }

        private void InitializeBoard()
        {
            uxTableLayout.Controls.Clear();
            _buttons = new Button[ ROWS, COLUMNS ];
            _board = new int[ ROWS, COLUMNS ];
            _playsMade = 0;

            for( int r = 0; r < ROWS; r++ )
            {
                for( int c = 0; c < COLUMNS; c++ )
                {
                    Button b = new Button()
                    {
                        Dock = DockStyle.Fill,
                        Font = new Font( FontFamily.GenericSansSerif, 40.0f )
                    };
                    b.Click += Cell_Click;
                    b.Tag = new Point( r, c );
                    uxTableLayout.Controls.Add( b, c, r );
                    _board[ r, c ] = UNPLAYED;
                    _buttons[ r, c ] = b;
                }
            }
        }

        private void Cell_Click( object sender, EventArgs e )
        {
            Button b = sender as Button;
            Point p = (Point)b.Tag;
            ProcessPlay( p.X, p.Y );
        }

        private int LengthOfPiecesSegment( int row, int col, int vInc, int hInc, int player )
        {
            int sequence = 0;
            while( 0 <= row && row < ROWS
                && 0 <= col && col < COLUMNS
                && _board[ row, col ] == player )
            {
                sequence++;
                row += vInc;
                col += hInc;
            }
            return sequence;
        }

        private bool MakePlay( int row, int col, int player )
        {
            _board[ row, col ] = player;
            _playsMade++;

            foreach( int[][] check in SEQUENCE_DIRECTIONS )
            {
                int sequenceLength = 0;
                foreach( int[] increments in check )
                {
                    int hIncrement = increments[ HORIZONTAL_INCREMENT_INDEX ];
                    int vIncrement = increments[ VERTICAL_INCREMENT_INDEX ];
                    sequenceLength += LengthOfPiecesSegment( row + vIncrement, col + hIncrement, vIncrement, hIncrement, player );
                }
                if( sequenceLength + 1 >= WINNING_SEQUENCE_LENGTH )
                {
                    return true;
                }
            }
            return false;
        }

        private void UndoPlay( int row, int col )
        {
            _board[ row, col ] = UNPLAYED;
            _playsMade--;
        }

        private int EvaluatePlay( int row, int col, int player, out int bestRow, out int bestCol )
        {
            bestRow = -1;
            bestCol = -1;
            if( MakePlay( row, col, player ) )
            {
                UndoPlay( row, col );
                return 1;
            }
            else if( _playsMade == TOTAL_CELLS )
            {
                UndoPlay( row, col );
                return 0;
            }
            else
            {
                int bestValue = -2;
                for( int r = 0; r < ROWS; r++ )
                {
                    for( int c = 0; c < COLUMNS; c++ )
                    {
                        if( _board[ r, c] == UNPLAYED )
                        {
                            int tempRow, tempCol;
                            int nextPlayer = player == HUMAN_PLAYER ? COMPUTER_PLAYER : HUMAN_PLAYER;
                            int value = EvaluatePlay( r, c, nextPlayer, out tempRow, out tempCol );
                            if( value > bestValue )
                            {
                                bestValue = value;
                                bestRow = r;
                                bestCol = c;
                            }
                            if( bestValue == 1 )
                            {
                                break;
                            }
                        }
                    }
                }

                UndoPlay( row, col );
                return -bestValue;
            }
        }

        private void DisplayGameEnd( String message )
        {
            DialogResult result = MessageBox.Show( message + " Play again?", message, MessageBoxButtons.YesNo );
            if( result == DialogResult.Yes )
            {
                StartGame();
            }
            else
            {
                EndGame();
            }
        }

        private void ProcessPlay( int row, int col )
        {
            for( int r = 0; r < ROWS; r++ )
            {
                for( int c = 0; c < COLUMNS; c++ )
                {
                    _buttons[ r, c ].Enabled = false;
                }
            }

            _buttons[ row, col ].Text = PLAYER_REPRESENTATION[ HUMAN_PLAYER ];

            this.Update();

            int computerPlayRow, computerPlayCol;
            EvaluatePlay( row, col, HUMAN_PLAYER, out computerPlayRow, out computerPlayCol );

            if( MakePlay( row, col, HUMAN_PLAYER ) )
            {
                DisplayGameEnd( "You won!" );
                return;
            }
            
            if( _playsMade == TOTAL_CELLS )
            {
                DisplayGameEnd( "The game is a draw!" );
                return;
            }

            _buttons[ computerPlayRow, computerPlayCol ].Text = PLAYER_REPRESENTATION[ COMPUTER_PLAYER ];

            if( MakePlay( computerPlayRow, computerPlayCol, COMPUTER_PLAYER ) )
            {
                DisplayGameEnd( "I win!" );
                return;
            }

            if( _playsMade == TOTAL_CELLS )
            {
                DisplayGameEnd( "The game is a draw!" );
                return;
            }

            for( int r = 0; r < ROWS; r++ )
            {
                for( int c = 0; c < COLUMNS; c++ )
                {
                    if( _board[ r, c] == UNPLAYED )
                    {
                        _buttons[ r, c ].Enabled = true;
                    }
                }
            }
        }
    }
}
