using System;
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
    public partial class FloodItForm : GameForm
    {
        private const int ROWS = 10;
        private const int COLUMNS = 10;
        private const int CELLS = ROWS * COLUMNS;
        private const int COLORS = 6;

        private const int ProgressBarBorderThickness = 1;
        private const int ProgressBarSegments = 20;
        private static readonly Color ProgressBarBorderColor = Color.Black;
        private static readonly Brush ProgressBarBrushCompleted = Brushes.Red;
        private static readonly Brush ProgressBarBrushUncompleted = Brushes.Transparent;

        private static readonly String FloodsRemaining = "Floods Remaining: ";
        private static readonly Random R = new Random();
        private static readonly SolidBrush[] ColorBrushes = new SolidBrush[ COLORS ]
        {
            (SolidBrush)Brushes.Red,
            (SolidBrush)Brushes.Green,
            (SolidBrush)Brushes.Magenta,
            (SolidBrush)Brushes.Blue,
            (SolidBrush)Brushes.Yellow,
            (SolidBrush)Brushes.Orange
        };

        private float _progress;
        private int[ , ] _board;
        private Button[] _colorButtons;
        private bool _gameOver;
        private int _floodsRemaining;
        private bool _aiPlaying;

        public override string GameName { get { return "Flood It"; } }

        public FloodItForm()
        {
            InitializeComponent();

            _colorButtons = new Button[ COLORS ] { ux0, ux1, ux2, ux3, ux4, ux5 };

            for( int i = 0; i < COLORS; i++ )
            {
                _colorButtons[ i ].BackColor = ColorBrushes[ i ].Color;
                _colorButtons[ i ].Tag = i;
                _colorButtons[ i ].Click += ColorButton_Click;
            }
        }

        protected override void OnGameStarted( EventArgs e )
        {
            InitializeBoard();
        }

        private void InitializeBoard()
        {
            foreach( Button b in _colorButtons )
            {
                b.Enabled = true;
            }

            _board = new int[ ROWS, COLUMNS ];
            for( int r = 0; r < ROWS; r++ )
            {
                for( int c = 0; c < COLUMNS; c++ )
                {
                    _board[ r, c ] = R.Next( COLORS );
                }
            }
            uxPanel.Invalidate();

            UpdateGame();

            _floodsRemaining = 30;
            uxFloodsRemaining.Text = FloodsRemaining + _floodsRemaining;
        }

        private void ColorButton_Click( object sender, EventArgs e )
        {
            if( !_gameOver )
            {
                MakePlay( (int)( (Button)sender ).Tag );
                _floodsRemaining--;
                UpdateGame();
                if( !_gameOver && _floodsRemaining == 0 )
                {
                    DisplayGameEnd( "You ran out of Floods!" );
                }
            }
        }

        private void DisplayGameEnd( String message )
        {
            if( MessageBox.Show( message + "\nDo you want to play again?", message, MessageBoxButtons.YesNo ) == DialogResult.Yes )
            {
                StartGame();
            }
            else
            {
                EndGame();
            }
        }

        private void UpdateGame()
        {
            uxPanel.Invalidate();
            uxProgressBar.Invalidate();
            uxFloodsRemaining.Text = FloodsRemaining + _floodsRemaining;

            _progress = CalculateProgress();

            if( _progress == 1.0f )
            {
                _gameOver = true;
                DisplayGameEnd( "You won!" );
                foreach( Button b in _colorButtons )
                {
                    b.Enabled = false;
                }
            }
        }

        private float CalculateProgress()
        {
            int[] colorTotals = new int[ COLORS ];
            for( int r = 0; r < ROWS; r++ )
            {
                for( int c = 0; c < COLUMNS; c++ )
                {
                    colorTotals[ _board[ r, c ] ]++;
                }
            }

            int maxColor = 0;
            int max = 0;
            for( int i = 0; i < COLORS; i++ )
            {
                if( colorTotals[ i ] > max )
                {
                    max = colorTotals[ i ];
                    maxColor = i;
                }
            }
            return (float)colorTotals[ maxColor ] / CELLS;
        }

        private void MakePlay( int newColor )
        {
            Flood( 0, 0, _board[ 0, 0 ], newColor, new bool[ ROWS, COLUMNS ] );
        }

        private void Flood( int row, int col, int oldColor, int newColor, bool[ , ] flooded )
        {
            if( 0 <= row && row < ROWS
             && 0 <= col && col < COLUMNS )
            {
                if( !flooded[ row, col ]
                 && _board[ row, col ] == oldColor )
                {
                    _board[ row, col ] = newColor;
                    flooded[ row, col ] = true;
                    Flood( row - 1, col, oldColor, newColor, flooded );
                    Flood( row + 1, col, oldColor, newColor, flooded );
                    Flood( row, col - 1, oldColor, newColor, flooded );
                    Flood( row, col + 1, oldColor, newColor, flooded );
                }
            }
        }

        private void uxPanel_Paint( object sender, PaintEventArgs e )
        {
            float cellWidth = (float)uxPanel.ClientSize.Width / COLUMNS;
            float cellHeight = (float)uxPanel.ClientSize.Height / ROWS;
            for( int r = 0; r < ROWS; r++ )
            {
                for( int c = 0; c < COLUMNS; c++ )
                {
                    e.Graphics.FillRectangle( ColorBrushes[ _board[ r, c ] ], c * cellWidth, r * cellHeight, cellWidth, cellHeight );
                }
            }
        }

        private void uxProgressBar_Paint( object sender, PaintEventArgs e )
        {
            int totalHeight = uxProgressBar.ClientSize.Height;
            int totalWidth = uxProgressBar.ClientSize.Width;

            totalHeight -= ProgressBarBorderThickness;
            totalWidth -= 2 * ProgressBarBorderThickness;
            e.Graphics.Clear( ProgressBarBorderColor );

            int completedSegments = (int)Math.Floor( (float)_progress * ProgressBarSegments );

            float segmentWidth = totalWidth;
            float segmentHeight = (float)totalHeight / ProgressBarSegments - ProgressBarBorderThickness;
            for( int i = 0; i < ProgressBarSegments; i++ )
            {
                float x = ProgressBarBorderThickness;
                float y = ( ProgressBarSegments - i - 1 ) * segmentHeight + ( ProgressBarSegments - i ) * ProgressBarBorderThickness;
                e.Graphics.FillRectangle( completedSegments > 0 ? ProgressBarBrushCompleted : ProgressBarBrushUncompleted, x, y, segmentWidth, segmentHeight );
                completedSegments--;
            }
        }
    }
}
