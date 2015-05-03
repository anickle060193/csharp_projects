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
    public partial class MoveControl : UserControl
    {
        public LightBotGame.MoveType MoveType { get; private set; }

        public MoveControl( LightBotGame.MoveType moveType )
        {
            InitializeComponent();

            MoveType = moveType;
            switch( MoveType )
            {
                case LightBotGame.MoveType.Forward:
                    BackgroundImage = MyGamesLibrary.Properties.Resources.MoveForward;
                    break;
                case LightBotGame.MoveType.LightUp:
                    BackgroundImage = MyGamesLibrary.Properties.Resources.LightUp;
                    break;
                case LightBotGame.MoveType.TurnLeft:
                    BackgroundImage = MyGamesLibrary.Properties.Resources.TurnLeft;
                    break;
                case LightBotGame.MoveType.TurnRight:
                    BackgroundImage = MyGamesLibrary.Properties.Resources.TurnRight;
                    break;
            }
        }
    }
}
