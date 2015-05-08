using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyGamesLibrary.Games.LightBot
{
    public partial class LightBotForm : GameForm
    {
        public override string GameName { get { return "Light Bot"; } }

        private LightBotGame _game;

        public LightBotForm()
        {
            InitializeComponent();
        }

        protected override void OnGameStarted( EventArgs e )
        {
            InitializeGame();
        }

        #region Event Handlers

        private void uxExecute_Click( object sender, EventArgs e )
        {
            _game.Execute();
        }

        private void uxReset_Click( object sender, EventArgs e )
        {
            _game.Reset();
        }

        #endregion

        private void InitializeGame()
        {
            _game = new LightBotGame();
            uxLightBotBoard.Game = _game;
            uxLightBotMoveQueue.Game = _game;
            uxPossibleMoves.Game = _game;
            _game.Reset();
        }
    }
}
