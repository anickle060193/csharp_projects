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
                    _game.PossibleMoveAdded += LightBotGame_PossibleMoveAdded;
                    _game.PossibleMoveRemoved += LightBotGame_PossibleMoveRemoved;
                }
                _game = value;
                if( _game != null )
                {
                    _game.PossibleMoveAdded -= LightBotGame_PossibleMoveAdded;
                    _game.PossibleMoveRemoved -= LightBotGame_PossibleMoveRemoved;
                }
                InitializeNewGame();
            }
        }

        public PossibleMoves()
        {
            InitializeComponent();
        }

        #region Event Handlers

        private void LightBotGame_PossibleMoveRemoved( object sender, MoveRemovedEventArgs e )
        {
            RemoveMove( e.RemovedIndex );
        }

        private void LightBotGame_PossibleMoveAdded( object sender, MoveAddedEventArgs e )
        {
            AddMove( e.MoveType, e.Index );
        }

        private void MoveControl_MoveClicked( object sender, MoveClickedEventArgs e )
        {
            _game.AddMove( e.MoveType );
        }

        #endregion

        private void AddMove( MoveType move, int index )
        {
            MoveControl c = new MoveControl( move );
            c.MoveClicked += MoveControl_MoveClicked;
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
                    c.MoveClicked += MoveControl_MoveClicked;
                    Controls.Add( c );
                }
            }

            ResumeLayout();
        }
    }
}
