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
            new SlidingPuzzleForm(),
            new SimonSaysForm()
        };
        static GameSelectorForm()
        {
            GAMES.ToList().Sort( ( x, y ) => x.GameName.CompareTo( y.GameName ) );
        }

        public GameSelectorForm()
        {
            InitializeComponent();

            foreach( GameForm game in GAMES )
            {
                uxGameList.Items.Add( game.GameName );
            }
        }

        private void uxPlayGame_Click( object sender, EventArgs e )
        {
            if( uxGameList.SelectedIndex >= 0 )
            {
                GAMES[ uxGameList.SelectedIndex ].StartGame();
            }
        }
    }
}
