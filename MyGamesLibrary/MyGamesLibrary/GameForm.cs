using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyGamesLibrary
{
    public abstract class GameForm : Form
    {
        public virtual string GameName { get { return "Un-named Game"; } }

        public abstract void StartGame();
        public abstract void EndGame();

        protected override void OnFormClosing( FormClosingEventArgs e )
        {
            if( e.CloseReason == CloseReason.UserClosing )
            {
                e.Cancel = true;
                this.Hide();
            }
            else
            {
                base.OnFormClosing( e );
            }
        }
    }
}
