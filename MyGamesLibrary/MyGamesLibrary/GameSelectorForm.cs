using MyGamesLibrary.Games;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyGamesLibrary
{
    public partial class GameSelectorForm : Form
    {
        private static readonly List<GameForm> GAMES = new List<GameForm>()
        {
            new ChainReactionForm(),
            new SlidingPuzzleForm(),
            new SimonSaysForm(),
            new TicTacToeForm(),
            new FloodItForm(),
            new ReversiForm(),
            new PickUpSticks(),
            new SnakeForm(),
            new TextTwistForm(),
        };

        public GameSelectorForm()
        {
            InitializeComponent();

            foreach( GameForm game in GAMES )
            {
                game.GameEnded += Game_GameEnded;
                uxGamesList.Items.Add( game.GameName );
            }

            this.Load += (EventHandler)delegate( object sender, EventArgs e )
            {
                GAMES[ 7 ].StartGame();
            };
        }

        private void Game_GameEnded( object sender, EventArgs e )
        {
            this.Show();
        }

        private void uxPlayGame_Click( object sender, EventArgs e )
        {
            StartGame( uxGamesList.SelectedIndex );
        }

        private void uxGamesList_DoubleClick( object sender, EventArgs e )
        {
            StartGame( uxGamesList.SelectedIndex );
        }

        private void StartGame( int i )
        {
            if( 0 <= i && i <= GAMES.Count )
            {
                this.Hide();
                GAMES[ i ].StartGame();
            }
        }
    }
}
