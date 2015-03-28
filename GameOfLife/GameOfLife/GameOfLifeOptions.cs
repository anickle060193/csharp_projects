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
    public partial class GameOfLifeOptions : Form
    {
        public int Rows
        {
            get
            {
                return (int)uxRows.Value;
            }

            set
            {
                uxRows.Value = value;
            }
        }

        public int Columns
        {
            get
            {
                return (int)uxColumns.Value;
            }

            set
            {
                uxColumns.Value = value;
            }
        }

        public int BlockWidth
        {
            get
            {
                return (int)uxBlockWidth.Value;
            }

            set
            {
                uxBlockWidth.Value = value;
            }
        }

        public int BlockHeight
        {
            get
            {
                return (int)uxBlockHeight.Value;
            }

            set
            {
                uxBlockHeight.Value = value;
            }
        }

        public int Interval
        {
            get
            {
                return (int)uxInterval.Value;
            }

            set
            {
                uxInterval.Value = value;
            }
        }

        public bool SeedOnStart
        {
            get
            {
                return uxSeedOnStart.Checked;
            }

            set
            {
                uxSeedOnStart.Checked = value;
            }
        }

        public bool ShowTransitions
        {
            get
            {
                return uxShowTransitions.Checked;
            }

            set
            {
                uxShowTransitions.Checked = value;
            }
        }

        public RuleSet RuleSet
        {
            get
            {
                return (RuleSet)uxRuleSet.SelectedItem;
            }

            set
            {
                uxRuleSet.SelectedItem = value;
            }
        }

        public GameOfLifeOptions()
        {
            InitializeComponent();

            uxRuleSet.Items.AddRange( GameOfLife.RuleSet.RuleSets );
        }

        public void InitializeValues( GameOfLifeBoard board )
        {
            uxAutosizeBoard.Checked = false;
            Rows = board.Rows;
            Columns = board.Columns;
            BlockWidth = board.BlockWidth;
            BlockHeight = board.BlockHeight;
            Interval = board.Interval;
            SeedOnStart = board.SeedOnStart;
            ShowTransitions = board.ShowTransitions;
            RuleSet = board.RuleSet;
        }

        private void uxAutosizeBoard_CheckedChanged( object sender, EventArgs e )
        {
            uxRows.Enabled = !uxAutosizeBoard.Checked;
            uxColumns.Enabled = !uxAutosizeBoard.Checked;
        }
    }
}
