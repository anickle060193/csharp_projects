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
        private static readonly Color NormalBackground = Color.SlateGray;
        private static readonly Color CurrentMoveBackground = Color.DarkSlateGray;

        public event EventHandler<MoveClickedEventArgs> MoveClicked;

        private bool _isCurrentMove;

        public MoveType MoveType { get; private set; }

        public bool IsCurrentMove
        {
            get { return _isCurrentMove; }
            set
            {
                _isCurrentMove = value;
                BackColor = _isCurrentMove ? CurrentMoveBackground : NormalBackground;
            }
        }

        public MoveControl( MoveType moveType )
        {
            InitializeComponent();

            IsCurrentMove = false;

            MoveType = moveType;
            switch( MoveType )
            {
                case MoveType.Forward:
                    BackgroundImage = MyGamesLibrary.Properties.Resources.MoveForward;
                    break;
                case MoveType.LightUp:
                    BackgroundImage = MyGamesLibrary.Properties.Resources.LightUp;
                    break;
                case MoveType.TurnLeft:
                    BackgroundImage = MyGamesLibrary.Properties.Resources.TurnLeft;
                    break;
                case MoveType.TurnRight:
                    BackgroundImage = MyGamesLibrary.Properties.Resources.TurnRight;
                    break;
            }
        }

        #region Event Handlers

        private void MoveControl_Click( object sender, EventArgs e )
        {
            OnMoveClicked( new MoveClickedEventArgs( MoveType ) );
        }

        #endregion

        protected void OnMoveClicked( MoveClickedEventArgs e )
        {
            EventHandler<MoveClickedEventArgs> handler = MoveClicked;
            if( handler != null )
            {
                handler( this, e );
            }
        }
    }

    public class MoveClickedEventArgs : EventArgs
    {
        public MoveType MoveType { get; set; }

        public MoveClickedEventArgs() { }

        public MoveClickedEventArgs( MoveType moveType ) : this()
        {
            MoveType = moveType;
        }
    }
}
