using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyGamesLibrary
{
    public partial class SettingsForm : Form
    {
        private const int LabelColumn = 0;
        private const int SettingEntryColumn = 1;

        private static readonly Color InvalidSettingValueColor = Color.FromArgb( 255, 77, 77 );
        private static readonly Color ValidSettingValueColor = default( Color );

        public enum SettingType { String, Number }

        class Setting
        {
            public SettingType Type;
            public String Label;
            public Object InitialValue;
            public Object CurrentValue;
            public Func<Object, bool> Validator;
            public Label LabelControl;
            public Control EntryControl;
        }

        private Dictionary<String, Setting> _settings = new Dictionary<String, Setting>();

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
            foreach( Setting setting in _settings.Values )
            {
                uxSettingsLayout.RowStyles.Add( new RowStyle( SizeType.AutoSize ) );
                uxSettingsLayout.Controls.Add( CreateLabel( setting ), LabelColumn, row );
                uxSettingsLayout.Controls.Add( CreateEntryControl( setting ), SettingEntryColumn, row );
                row++;
            }
        }

        private void ResetSettings()
        {
            foreach( Setting setting in _settings.Values )
            {
                switch( setting.Type )
                {
                    case SettingType.Number:
                        ( (NumericUpDown)setting.EntryControl ).Value = (int)setting.InitialValue;
                        break;

                    case SettingType.String:
                        ( (TextBox)setting.EntryControl ).Text = (String)setting.InitialValue;
                        break;
                }
            }
        }

        private Label CreateLabel( Setting setting )
        {
            if( setting.LabelControl == null )
            {
                Label label = new Label();
                label.Text = setting.Label;
                label.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Bottom;
                label.TextAlign = ContentAlignment.MiddleLeft;
                setting.LabelControl = label;
            }
            return setting.LabelControl;
        }

        private Control CreateEntryControl( Setting setting )
        {
            if( setting.EntryControl == null )
            {
                Control control;
                switch( setting.Type )
                {
                    case SettingType.String:
                        TextBox textbox = new TextBox();
                        textbox.TextChanged += (EventHandler)delegate( object sender, EventArgs e )
                        {
                            setting.CurrentValue = (String)textbox.Text;
                            UpdateEntryControl( setting );
                        };
                        textbox.Text = (String)setting.CurrentValue;
                        control = textbox;
                        break;

                    case SettingType.Number:
                        NumericUpDown numeric = new NumericUpDown();
                        numeric.ValueChanged += (EventHandler)delegate( object sender, EventArgs e )
                        {
                            setting.CurrentValue = (int)numeric.Value;
                            UpdateEntryControl( setting );
                        };
                        numeric.Value = (int)setting.CurrentValue;
                        control = numeric;
                        break;

                    default:
                        throw new ArgumentException( "Invalid setting type: " + setting.Type.ToString() );
                }
                control.Anchor = AnchorStyles.None;

                setting.EntryControl = control;
            }

            UpdateEntryControl( setting );
            return setting.EntryControl;
        }

        private void UpdateEntryControl( Setting setting )
        {
            if( setting.EntryControl != null )
            {
                if( setting.Validator( setting.CurrentValue ) )
                {
                    setting.EntryControl.BackColor = ValidSettingValueColor;
                }
                else
                {
                    setting.EntryControl.BackColor = InvalidSettingValueColor;
                }
            }
        }

        private Setting CreateSetting( SettingType type, String key, String label, Object initialValue, Func<Object, bool> validator )
        {
            Setting setting = new Setting();
            setting.Label = label;
            setting.Type = type;
            setting.InitialValue = initialValue;
            setting.CurrentValue = initialValue;
            setting.Validator = validator;
            _settings.Add( key, setting );
            UpdateSettingsLayout();
            return setting;
        }

        private Object GetSettingValue( String key )
        {
            if( _settings.ContainsKey( key ) )
            {
                Setting setting = _settings[ key ];
                if( setting.Validator( setting.CurrentValue ) )
                {
                    return setting.CurrentValue;
                }
                else
                {
                    return setting.InitialValue;
                }
            }
            else
            {
                throw new ArgumentException( "Setting does not exist: " + key );
            }
        }

        public void AddStringSetting( String key, String label, String initialValue, Func<Object, bool> validator )
        {
            CreateSetting( SettingType.String, key, label, initialValue, validator );
        }

        public String GetStringSetting( String key )
        {
            return (String)GetSettingValue( key );
        }

        public void AddIntegerSetting( String key, String label, int initialValue, Func<Object, bool> validator )
        {
            CreateSetting( SettingType.Number, key, label, initialValue, validator );
        }

        public int GetIntegerSetting( String key )
        {
            return (int)GetSettingValue( key );
        }

        private void uxOK_Click( object sender, EventArgs e )
        {
            foreach( Setting s in _settings.Values )
            {
                if( !s.Validator( s.CurrentValue ) )
                {
                    MessageBox.Show( "Invalid values." );
                    return;
                }
            }
            this.Close();
        }

        private void uxCancel_Click( object sender, EventArgs e )
        {
            this.Close();
        }
    }
}
