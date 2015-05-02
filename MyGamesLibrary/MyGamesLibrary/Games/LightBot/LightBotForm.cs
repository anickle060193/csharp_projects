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

        public LightBotForm()
        {
            InitializeComponent();
        }
    }
}
