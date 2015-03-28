using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameOfLife
{
    public partial class GameOfLifeForm : Form
    {
        private bool _fullscreen;

        public GameOfLifeForm()
        {
            InitializeComponent();
            uxBoard.Start();
        }

        private void GameOfLifeForm_KeyUp( object sender, KeyEventArgs e )
        {
            switch( e.KeyCode )
            {
                case Keys.F11:
                    if( _fullscreen )
                    {
                        UnFullScreen();
                    }
                    else
                    {
                        FullScreen();
                    }
                    e.Handled = true;
                    break;

                case Keys.Escape:
                    if( _fullscreen )
                    {
                        UnFullScreen();
                    }
                    e.Handled = true;
                    break;
            }
        }

        private void FullScreen()
        {
            this.TopMost = true;
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            _fullscreen = true;
        }

        private void UnFullScreen()
        {
            this.TopMost = false;
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.WindowState = FormWindowState.Maximized;
            _fullscreen = false;
        }
    }
}
