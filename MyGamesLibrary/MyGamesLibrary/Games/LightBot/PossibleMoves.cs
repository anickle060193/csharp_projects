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
    public partial class PossibleMoves : FlowLayoutPanel
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

            foreach( Control c in Controls )
            {
                MoveControl moveControl = c as MoveControl;
                if( moveControl != null )
                {
                    moveControl.MoveClicked -= MoveControl_MoveClicked;
                }
            }
            Controls.Clear();
            foreach( MoveType possibleMove in _game.PossibleMoves )
            {
                MoveControl control = new MoveControl( possibleMove );
                control.MoveClicked += MoveControl_MoveClicked;
                Controls.Add( control );
            }

            ResumeLayout();
        }

        private void MoveControl_MoveClicked( object sender, MoveClickedEventArgs e )
        {
            _game.AddMove( e.MoveType );
        }

        #endregion
    }
}
