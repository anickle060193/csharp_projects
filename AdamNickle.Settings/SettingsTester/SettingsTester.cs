using AdamNickle.Settings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SettingsTester
{
    public partial class SettingsTester : Form
    {
        private SettingsForm _settingsForm = new SettingsForm();

        public SettingsTester()
        {
            InitializeComponent();

            _settingsForm.AddFloatSetting( "TEST", "Testing: ", 1.0f, delegate( object value )
            {
                return (float)value != 1;
            } );
        }

        private void uxShowSettings_Click( object sender, EventArgs e )
        {
            if( _settingsForm.ShowDialog() == DialogResult.OK )
            {
                MessageBox.Show( "Settings closed: OK" );
            }
            else
            {
                MessageBox.Show( "Settings closed: Cancel" );
            }
        }
    }
}
