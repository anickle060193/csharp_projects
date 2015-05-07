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
    public partial class MoveQueueControl : FlowLayoutPanel
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

        public MoveQueueControl()
        {
            InitializeComponent();
        }

        #region Event Handlers

        private void LightBotGame_GameUpdated( object sender, EventArgs e )
        {
            SuspendLayout();

            Controls.Clear();
            foreach( MoveType move in _game.Moves )
            {
                Controls.Add( new MoveControl( move ) );
            }

            ResumeLayout();
        }

        #endregion
    }
}
