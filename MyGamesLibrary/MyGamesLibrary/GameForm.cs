using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyGamesLibrary
{
    public class GameForm : Form
    {
        public event EventHandler GameStarted;
        public event EventHandler GameEnded;

        public virtual string GameName { get { return "Un-named Game"; } }

        public void StartGame()
        {
            this.Show();

            if( GameStarted != null )
            {
                GameStarted( this, EventArgs.Empty );
            }
            OnGameStarted( EventArgs.Empty );
        }

        public void EndGame()
        {
            this.Hide();

            if( GameEnded != null )
            {
                GameEnded( this, EventArgs.Empty );
            }
            OnGameEnded( EventArgs.Empty );
        }

        protected virtual void OnGameStarted( EventArgs e )
        {
        }

        protected virtual void OnGameEnded( EventArgs e )
        {
        }

        protected override void OnFormClosing( FormClosingEventArgs e )
        {
            if( e.CloseReason == CloseReason.UserClosing )
            {
                e.Cancel = true;
                EndGame();
            }
            else
            {
                base.OnFormClosing( e );
            }
        }
    }
}
