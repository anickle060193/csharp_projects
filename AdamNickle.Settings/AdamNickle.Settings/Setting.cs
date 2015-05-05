using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdamNickle.Settings
{
    public enum SettingType { String, Integer, Float }

    abstract class AbstractSetting
    {
        internal SettingType Type { get; set; }
        public String Label { get; set; }
        internal Label LabelControl { get; set; }
        internal abstract Object Value { get; }
        internal abstract bool IsValid { get; }

        internal abstract Label CreateLabelControl();
        internal abstract Control CreateEntryControl();
        internal abstract void UpdateEntryControl();
    }

    abstract class Setting<TData, TControl> : AbstractSetting where TControl: Control
    {
        private static readonly Color InvalidSettingValueColor = Color.FromArgb( 255, 77, 77 );
        private static readonly Color ValidSettingValueColor = default( Color );

        public TData InitialValue;
        public TData CurrentValue;
        public TControl EntryControl;
        public Func<TData, bool> Validator;

        internal override object Value
        {
            get { return IsValid ? CurrentValue : InitialValue; }
        }

        internal override bool IsValid
        {
            get { return Validator( CurrentValue ); }
        }

        internal override void UpdateEntryControl()
        {
            if( EntryControl != null )
            {
                if( Validator( CurrentValue ) )
                {
                    EntryControl.BackColor = ValidSettingValueColor;
                }
                else
                {
                    EntryControl.BackColor = InvalidSettingValueColor;
                }
            }
        }

        internal override Label CreateLabelControl()
        {
            if( LabelControl == null )
            {
                LabelControl = new Label()
                {
                    Text = Label,
                    Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Bottom,
                    TextAlign = ContentAlignment.MiddleLeft
                };
            }
            return LabelControl;
        }
    }

    class StringSetting : Setting<String, TextBox>
    {
        public StringSetting()
        {
            Type = SettingType.String;
        }

        internal override Control CreateEntryControl()
        {
            TextBox textbox = new TextBox();
            textbox.TextChanged += (EventHandler)delegate( object sender, EventArgs e )
            {
                CurrentValue = textbox.Text;
                UpdateEntryControl();
            };
            textbox.Text = CurrentValue;
            EntryControl = textbox;
            return EntryControl;
        }
    }

    class IntSetting : Setting<int, NumericUpDown>
    {
        public IntSetting()
        {
            Type = SettingType.Integer;
        }

        internal override Control CreateEntryControl()
        {
            NumericUpDown numeric = new NumericUpDown();
            numeric.Maximum = Decimal.MaxValue;
            numeric.Minimum = Decimal.MinValue;
            numeric.Increment = 1;
            numeric.ValueChanged += (EventHandler)delegate( object sender, EventArgs e )
            {
                CurrentValue = (int)numeric.Value;
                UpdateEntryControl();
            };
            numeric.Value = CurrentValue;
            EntryControl = numeric;
            return EntryControl;
        }
    }

    class FloatSetting : Setting<float, NumericUpDown>
    {
        public FloatSetting()
        {
            Type = SettingType.Float;
        }

        internal override Control CreateEntryControl()
        {
            NumericUpDown numeric = new NumericUpDown();
            numeric.Maximum = Decimal.MaxValue;
            numeric.Minimum = Decimal.MinValue;
            numeric.Increment = 0.1M;
            numeric.DecimalPlaces = 2;
            numeric.ValueChanged += delegate( object sender, EventArgs e )
            {
                CurrentValue = (float)numeric.Value;
                UpdateEntryControl();
            };
            numeric.Value = (decimal)CurrentValue;
            EntryControl = numeric;
            return EntryControl;
        }
    }
}
