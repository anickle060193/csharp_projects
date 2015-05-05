using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdamNickle.Settings
{
    public partial class SettingsForm : Form
    {
        private const int LabelColumn = 0;
        private const int SettingEntryColumn = 1;

        private static readonly Color InvalidSettingValueColor = Color.FromArgb( 255, 77, 77 );
        private static readonly Color ValidSettingValueColor = default( Color );

        private Dictionary<String, AbstractSetting> _settings = new Dictionary<String, AbstractSetting>();

        public SettingsForm()
        {
            InitializeComponent();

            uxSettingsLayout.RowStyles.Clear();
        }

        private void UpdateSettingsLayout()
        {
            uxSettingsLayout.Controls.Clear();
            uxSettingsLayout.RowCount = _settings.Count;
            int row = 0;
            foreach( AbstractSetting setting in _settings.Values )
            {
                uxSettingsLayout.RowStyles.Add( new RowStyle( SizeType.AutoSize ) );
                uxSettingsLayout.Controls.Add( setting.CreateLabelControl(), LabelColumn, row );
                uxSettingsLayout.Controls.Add( setting.CreateEntryControl(), SettingEntryColumn, row );
                row++;
            }
        }

        private void ResetSettings()
        {
            foreach( AbstractSetting setting in _settings.Values )
            {
                switch( setting.Type )
                {
                    case SettingType.Integer:
                        IntSetting intSetting = (IntSetting)setting;
                        intSetting.EntryControl.Value = intSetting.InitialValue;
                        break;

                    case SettingType.String:
                        StringSetting stringSetting = (StringSetting)setting;
                        stringSetting.EntryControl.Text = stringSetting.InitialValue;
                        break;

                    case SettingType.Float:
                        FloatSetting floatSetting = (FloatSetting)setting;
                        floatSetting.EntryControl.Value = (decimal)floatSetting.InitialValue;
                        break;
                }
            }
        }

        private void AddNewSetting( AbstractSetting setting, String key )
        {
            _settings.Add( key, setting );
            UpdateSettingsLayout();
        }

        private Object GetSettingValue( String key )
        {
            if( _settings.ContainsKey( key ) )
            {
                return _settings[ key ].Value;
            }
            else
            {
                throw new ArgumentException( "Setting does not exist: " + key );
            }
        }

        public void AddStringSetting( String key, String label, String initialValue, Func<String, bool> validator )
        {
            StringSetting setting = new StringSetting();
            setting.Label = label;
            setting.Type = SettingType.String;
            setting.InitialValue = initialValue;
            setting.CurrentValue = initialValue;
            setting.Validator = validator;

            AddNewSetting( setting, key );
        }

        public String GetStringSetting( String key )
        {
            return (String)GetSettingValue( key );
        }

        public void AddIntegerSetting( String key, String label, int initialValue, Func<int, bool> validator )
        {
            IntSetting setting = new IntSetting();
            setting.Label = label;
            setting.Type = SettingType.String;
            setting.InitialValue = initialValue;
            setting.CurrentValue = initialValue;
            setting.Validator = validator;

            AddNewSetting( setting, key );
        }

        public int GetIntegerSetting( String key )
        {
            return (int)GetSettingValue( key );
        }

        public void AddFloatSetting( String key, String label, float initialValue, Func<float, bool> validator )
        {
            FloatSetting setting = new FloatSetting();
            setting.Label = label;
            setting.Type = SettingType.String;
            setting.InitialValue = initialValue;
            setting.CurrentValue = initialValue;
            setting.Validator = validator;

            AddNewSetting( setting, key );
        }

        public float GetFloatSetting( String key )
        {
            return (float)GetSettingValue( key );
        }

        private void uxOK_Click( object sender, EventArgs e )
        {
            foreach( AbstractSetting s in _settings.Values )
            {
                if( !s.IsValid )
                {
                    MessageBox.Show( "Invalid values." );
                    return;
                }
            }
            DialogResult = DialogResult.OK;
        }
    }
}
