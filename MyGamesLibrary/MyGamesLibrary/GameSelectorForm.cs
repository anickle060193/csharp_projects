using MyGamesLibrary.Games;
using MyGamesLibrary.Games.LightBot;
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
            /* 0 */ new ChainReactionForm(),
            /* 1 */ new SlidingPuzzleForm(),
            /* 2 */ new SimonSaysForm(),
            /* 3 */ new TicTacToeForm(),
            /* 4 */ new FloodItForm(),
            /* 5 */ new ReversiForm(),
            /* 6 */ new PickUpSticks(),
            /* 7 */ new SnakeForm(),
            /* 8 */ new TextTwistForm(),
            /* 9 */ new LightBotForm(),
        };

        public GameSelectorForm()
        {
            InitializeComponent();

            foreach( GameForm game in GAMES )
            {
                game.GameEnded += Game_GameEnded;
                uxGamesList.Items.Add( game.GameName );
            }

            this.Shown += ( s, e ) => StartGame( 9 );
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
