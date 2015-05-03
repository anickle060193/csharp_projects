using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyGamesLibrary.Games.LightBot
{
    public partial class PossibleMoves : UserControl
    {
        private LightBotGame _game;

        public LightBotGame Game
        {
            get { return _game; }
            set
            {
                if( _game != null )
                {
                    _game.GameUpdated -= LightBotGame_GameUpdated;
                }
                _game = value;
                if( _game != null )
                {
                    _game.GameUpdated += LightBotGame_GameUpdated;
                }
            }
        }

        public PossibleMoves()
        {
            InitializeComponent();
        }

        #region Event Handlers

        private void LightBotGame_GameUpdated( object sender, EventArgs e )
        {
            SuspendLayout();

            Controls.Clear();
            foreach( LightBotGame.MoveType possibleMove in _game.PossibleMoves )
            {
                Controls.Add( new MoveControl( possibleMove ) );
            }

            ResumeLayout();
        }

        #endregion
    }
}
