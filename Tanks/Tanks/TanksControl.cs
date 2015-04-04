using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tanks
{
    public partial class TanksControl : UserControl
    {
        private static readonly SolidBrush[] TerrainBrushes = new SolidBrush[]
        {
            /* Air   */ new SolidBrush( Color.FromArgb( 190, 229, 234 ) ),
            /* Dirt  */ new SolidBrush( Color.FromArgb( 117, 98, 81 ) ),
            /* Grass */ new SolidBrush( Color.FromArgb( 92, 117, 81 ) )
        };

        TanksGame _game;

        public TanksControl()
        {
            InitializeComponent();

            _game = new TanksGame( 200, 100, 10 );
            _game.InitializeTerrainGeneration( 20, 30, 10 );
            _game.RunTerrainGeneration();
            uxUpdateTimer.Start();
        }

        protected override void OnMouseUp( MouseEventArgs e )
        {
            _game.RemoveTerrain( (int)( (float)e.X / _game.CellSize ), (int)( (float)e.Y / _game.CellSize ), 10 );
        }

        protected override void OnPaint( PaintEventArgs e )
        {
            _game.Paint( e.Graphics, TerrainBrushes );
        }

        private void uxUpdateTimer_Tick( object sender, EventArgs e )
        {
            Refresh();
        }
    }
}
