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
                    _game.MoveAdded -= LightBotGame_MoveAdded;
                    _game.MoveRemoved -= LightBotGame_MoveRemoved;
                }
                _game = value;
                if( _game != null )
                {
                    _game.MoveAdded += LightBotGame_MoveAdded;
                    _game.MoveRemoved += LightBotGame_MoveRemoved;
                }
            }
        }

        public MoveQueueControl()
        {
            InitializeComponent();
        }

        #region Event Handlers

        private void LightBotGame_MoveAdded( object sender, MoveAddedEventArgs e )
        {
            AddMove( e.MoveType, e.Index );
        }

        private void LightBotGame_MoveRemoved( object sender, MoveRemovedEventArgs e )
        {
            RemoveMove( e.RemovedIndex );
        }

        #endregion

        private void AddMove( MoveType move, int index )
        {
            MoveControl c = new MoveControl( move );
            Controls.Add( c );
            Controls.SetChildIndex( c, index );
        }

        private void RemoveMove( int index )
        {
            Controls.RemoveAt( index );
        }

        private void InitializeNewGame()
        {
            SuspendLayout();

            Controls.Clear();

            if( _game != null )
            {
                foreach( MoveType move in _game.PossibleMoves )
                {
                    MoveControl c = new MoveControl( move );
                    Controls.Add( c );
                }
            }

            ResumeLayout();
        }
    }
}
