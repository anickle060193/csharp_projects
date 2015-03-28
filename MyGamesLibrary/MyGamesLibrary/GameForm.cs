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
        public virtual string GameName { get { return "Un-named Game"; } }

        public virtual void StartGame()
        {
            this.Show();
        }

        public virtual void EndGame()
        {
            this.Hide();
        }

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
