using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;

namespace SystemEx.Windows.Forms
{
    public class NumericTextBox : TextBox
    {
        // This class is kept here to stay compatible with the old, forced,
        // numeric text box. The SimpleNumericTextBox is the preferred one
        // for new applications.

        private bool _noChangeEvent = false;
        private int _scaleOnLostFocus = 0;
        private Decimal _internalValue = 0;
        private Decimal _numericValue = 0;
        private int _scaleOnFocus = 0;
        private int _precision = 1;
        private bool _allowNegative = true;
        public event EventHandler NumericValueChanged;

        public NumericTextBox()
        {
            LostFocus += new EventHandler(TextBox_LostFocus);
            GotFocus += new EventHandler(TextBox_GotFocus);
            TextChanged += new EventHandler(TextBox_TextChanged);
            KeyDown += new KeyEventHandler(TextBox_KeyDown);
            KeyPress += new KeyPressEventHandler(TextBox_KeyPress);
        }

        [Category("Behavior")]
        [Browsable(true)]
        [DefaultValue(1)]
        public int NumericPrecision
        {
            get { return _precision; }
            set
            {
                if (value < 0)
                {
                    throw new Exception("Precision cannot be less than 0");
                }

                _precision = value;

                FormatCurrentText();
            }
        }

        [RefreshProperties(RefreshProperties.All)]
        [Category("Behavior")]
        [DefaultValue(0)]
        public int NumericScaleOnFocus
        {
            get { return _scaleOnFocus; }
            set
            {
                if (value < 0)
                {
                    throw new Exception("Scale cannot be negative");
                }

                _scaleOnFocus = value;

                FormatCurrentText();
            }
        }

        [RefreshProperties(System.ComponentModel.RefreshProperties.All)]
        [Category("Behavior")]
        [Browsable(true)]
        [DefaultValue(0)]
        public int NumericScaleOnLostFocus
        {
            get { return _scaleOnLostFocus; }
            set
            {
                if (value < 0)
                {
                    throw new Exception("Scale cannot be negative");
                }

                _scaleOnLostFocus = value;

                FormatCurrentText();
            }
        }

        private string DecimalSeperator
        {
            get { return NumberFormatInfo.CurrentInfo.NumberDecimalSeparator; }
        }

        private string GroupSeperator
        {
            get { return NumberFormatInfo.CurrentInfo.NumberGroupSeparator; }
        }

        [Category("Behavior")]
        [Browsable(true)]
        [DefaultValue(true)]
        public bool AllowNegative
        {
            get { return _allowNegative; }
            set { _allowNegative = value; }
        }

        [Bindable(true)]
        [Browsable(true)]
        [DefaultValue(0)]
        [Category("Appearance")]
        public object NumericValue
        {
            get { return _numericValue; }
            set
            {
                if (value.Equals(DBNull.Value))
                {
                    if (value.Equals(0))
                    {
                        Text = Convert.ToString(0);
                        _numericValue = Convert.ToDecimal(0);
                        OnNumericValueChanged(new System.EventArgs());
                        return;
                    }
                }

                if (!value.Equals(_numericValue))
                {
                    Text = Convert.ToString(value);
                    _numericValue = Convert.ToDecimal(value);
                    OnNumericValueChanged(new System.EventArgs());
                }
            }
        }

        void TextBox_LostFocus(object sender, EventArgs e)
        {
            _noChangeEvent = true;

            _internalValue = Convert.ToDecimal(Text);

            if (!(_scaleOnLostFocus == 0))
            {
                Text = FormatNumber();
            }
            else
            {
                if (Text.IndexOf('-') < 0)
                {
                    Text = FormatNumber();
                }
                else
                {
                    if (Text == "-")
                    {
                        Text = "";
                    }
                    else
                    {
                        Text = FormatNumber();
                    }
                }
            }

            _noChangeEvent = false;
        }

        void TextBox_GotFocus(object sender, EventArgs e)
        {
            _noChangeEvent = true;

            Text = Convert.ToString(_internalValue);

            if (!(_scaleOnFocus == 0))
            {
                Text = FormatNumber();
            }
            else
            {
                if (Text.IndexOf('-') < 0)
                {
                    Text = FormatNumber();
                }
                else
                {
                    if (Text == "-")
                    {
                        Text = "";
                    }
                    else
                    {
                        Text = FormatNumber();
                    }
                }
            }

            _noChangeEvent = false;
        }

        void TextBox_TextChanged(object sender, EventArgs e)
        {
            FormatCurrentText();
        }

        private void FormatCurrentText()
        {
            //Indicates that no change event should happen
            //Prevent event from firing on changing the text in the change
            //event

            if (_noChangeEvent || (SelectionStart == -1))
            {
                return;
            }

            //No Change event

            _noChangeEvent = true;

            int li_SelStart = 0;
            bool lb_PositionCursorBeforeComma = false;

            if (string.Empty.Equals(Text.Trim()))
            {
                Text = "0";
            }

            if (Text.Substring(0, 1) == GroupSeperator)
            {
                Text = Text.Substring(1);
            }

            if (!(_scaleOnFocus == 0))
            {
                if (SelectionStart == (Text.IndexOf(DecimalSeperator)))
                {
                    lb_PositionCursorBeforeComma = true;
                }
                else
                {
                    li_SelStart = SelectionStart;
                }
            }
            else
            {
                li_SelStart = SelectionStart;
            }

            _internalValue = Convert.ToDecimal(Text);
            NumericValue = Convert.ToDecimal(Text);



            if (Focused)
            {
                if (!(_scaleOnFocus == 0))
                {
                    bool hadMinus = Text.IndexOf('-') >= 0;

                    Text = FormatNumber();

                    if (hadMinus && Text.IndexOf('-') < 0)
                    {
                        Text = "-" + Text;
                    }
                }
                else
                {
                    if (Text.IndexOf('-') < 0)
                    {
                        Text = FormatNumber();
                    }
                    else
                    {
                        if (Text.Equals('-'))
                        {
                            Text = "";
                        }
                        else
                        {
                            Text = FormatNumber();

                            if (Text.IndexOf('-') < 0)
                            {
                                Text = "-" + Text;
                            }
                        }
                    }
                }
            }
            else
            {
                if (!(_scaleOnLostFocus == 0))
                {
                    Text = FormatNumber();
                }
                else
                {
                    if (Text.IndexOf('-') < 0)
                    {
                        Text = FormatNumber();
                    }
                    else
                    {
                        if (Text.Equals('-'))
                        {
                            Text = "";
                        }
                        else
                        {
                            Text = FormatNumber();
                        }
                    }
                }

            }

            if (!(_scaleOnFocus == 0))
            {
                if (lb_PositionCursorBeforeComma)
                {
                    SelectionStart = (Text.IndexOf(DecimalSeperator));
                }
                else
                {
                    SelectionStart = li_SelStart;
                }
            }
            else
            {
                SelectionStart = li_SelStart;
            }

            //Change event may fire
            _noChangeEvent = false;
        }

        void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            bool lb_PositionCursorJustBeforeComma = false;

            if (!(_scaleOnFocus == 0))
            {
                //Is the position of the cursor just before the comma
                lb_PositionCursorJustBeforeComma = (SelectionStart == (Text.IndexOf(DecimalSeperator)));
            }

            switch (e.KeyCode)
            {
                case System.Windows.Forms.Keys.Delete:
                    //Otherwise strange effect
                    if (lb_PositionCursorJustBeforeComma)
                    {
                        SelectionStart = Text.IndexOf(DecimalSeperator) + 1;
                        e.Handled = true;
                        break;
                    }

                    if (Text.IndexOf('-') < 0)
                    {
                        if (SelectionLength == Text.Length)
                        {
                            Text = "0";
                            SelectionStart = 1;
                            e.Handled = true;
                            break;
                        }
                    }
                    else
                    {

                        if (SelectionLength == Text.Length)
                        {
                            Text = "0";
                            SelectionStart = 1;
                            e.Handled = true;
                            break;
                        }

                        if (SelectionLength > 0)
                        {
                            if (SelectedText != "-")
                            {
                                if (Convert.ToDouble(SelectedText) == System.Math.Abs(Convert.ToDouble(Text)))
                                {
                                    Text = "0";
                                    SelectionStart = 1;
                                    e.Handled = true;
                                    break;
                                }
                            }
                        }
                    }
                    break;

                default:
                    break;
            }
        }

        void TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            bool lb_PositionCursorBeforeComma = false;
            bool lb_InputBeforeCommaValid = false;
            bool lb_PositionCursorJustAfterComma = false;
            int li_SelStart = 0;

            lb_InputBeforeCommaValid = true;

            //Minus pressed
            if (e.KeyChar.Equals('-'))
            {
                if (AllowNegative)
                {
                    if (Text.IndexOf('-') < 0)
                    {
                        Text = "-" + Text;

                        SelectionStart = li_SelStart + 1;
                        SelectionLength = Text.Length - SelectionStart;
                    }
                    else
                    {
                        double value = Convert.ToDouble(Text);

                        string stringValue = Convert.ToString(value == 0.0 ? 0.0 : value * -1);
                        Text = stringValue;

                        SelectionStart = Text.IndexOf('-') < 0 ? 0 : 1;
                        SelectionLength = Text.Length - SelectionStart;
                    }

                    e.Handled = true;
                    return;
                }
            }

            //The + key
            if (e.KeyChar.Equals('+'))
            {
                if (!(Text.IndexOf('-') < 0))
                {
                    //Is everything selected
                    switch (SelectionLength)
                    {
                        case 0:
                            li_SelStart = SelectionStart;

                            Text = Convert.ToString(Convert.ToDouble(Text) * -1);

                            SelectionStart = li_SelStart - 1;
                            break;

                        default:
                            if (TextLength == SelectionLength)
                            {
                                Text = "0";
                            }
                            break;
                    }
                }

                e.Handled = true;
                return;
            }

            if (!(_scaleOnFocus == 0))
            {
                //Is the position of the cursor just after the comma
                lb_PositionCursorJustAfterComma = (SelectionStart == Text.IndexOf(DecimalSeperator) + 1);
            }

            if (e.KeyChar == '\b')
            {
                //Backspace
                if (lb_PositionCursorJustAfterComma)
                {
                    SelectionStart = Text.IndexOf(DecimalSeperator);
                    e.Handled = true;
                }

                //if ( all selected on delete pressed)
                if (SelectionLength == Text.Length)
                {
                    Text = "0";
                    SelectionStart = 1;
                    e.Handled = true;
                }

                if (e.KeyChar.Equals(null))
                {
                    e.Handled = true;
                }
                return;
            }

            //Prevent other keys than numeric and ,
            string ls_AllowedKeyChars = "1234567890" + DecimalSeperator;

            if (ls_AllowedKeyChars.IndexOf(e.KeyChar) < 0)
            {
                e.Handled = true;
                return;
            }

            if (!(_scaleOnFocus == 0))
            {
                //position of cursor is before comma?
                lb_PositionCursorBeforeComma = !(SelectionStart >= Text.IndexOf(DecimalSeperator) + 1);
            }

            //Comma pressed
            if (e.KeyChar.ToString() == DecimalSeperator)
            {
                if (lb_PositionCursorBeforeComma)
                {
                    SelectionStart = Text.IndexOf(DecimalSeperator) + 1;
                    SelectionLength = 0;
                }

                e.Handled = true;
                return;
            }

            //Prevent more than the precission numbers entered
            if (!(_scaleOnFocus == 0))
            {
                if (SelectionStart == Text.Length)
                {
                    e.Handled = true;
                    return;
                }
            }

            if (!(_scaleOnFocus == 0))
            {
                //if ( the character entered would violate the numbers before the comma
                if (Text.IndexOf('-') < 0)
                {
                    lb_InputBeforeCommaValid = !(Text.Substring(0, Text.IndexOf(DecimalSeperator)).Length >= (_precision - _scaleOnFocus));
                }
                else
                {
                    lb_InputBeforeCommaValid = !(Text.Substring(0, Text.IndexOf(DecimalSeperator)).Length >= (_precision - _scaleOnFocus + 1));
                }
            }
            else
            {
                if (Text.IndexOf('-') < 0)
                {
                    lb_InputBeforeCommaValid = !((Text.Length) >= _precision);
                }
                else
                {
                    lb_InputBeforeCommaValid = !((Text.Length) >= _precision + 1);
                }
            }

            //if first char is 0 another may be entered
            if (!(_scaleOnFocus == 0))
            {
                if ((Text.Substring(0, 1) == "0") && !(SelectionStart == 0))
                {
                    lb_InputBeforeCommaValid = true;
                }
                if (SelectionLength > 0)
                {
                    lb_InputBeforeCommaValid = true;
                }
            }
            else
            {
                if ((Text.Substring(0, 1) == "0") && ((SelectionStart == Text.Length) || (SelectionLength == 1)))
                {
                    lb_InputBeforeCommaValid = true;
                }
                if (SelectionLength > 0)
                {
                    lb_InputBeforeCommaValid = true;
                }
            }

            if (!(_scaleOnFocus == 0))
            {
                if (lb_PositionCursorBeforeComma && !(lb_InputBeforeCommaValid))
                {
                    e.Handled = true;
                    return;
                }
            }
            else
            {
                if (!(lb_InputBeforeCommaValid))
                {
                    e.Handled = true;
                    return;
                }
            }
        }

        protected virtual void OnNumericValueChanged(System.EventArgs e)
        {
            if (NumericValueChanged != null)
            {
                NumericValueChanged(this, e);
            }
        }

        protected string FormatNumber()
        {
            StringBuilder lsb_Format = new StringBuilder();
            int li_Counter = 1;
            long ll_Remainder = 0;

            if (Focused)
            {
                while (li_Counter <= _precision - _scaleOnFocus)
                {
                    if (li_Counter == 1)
                    {
                        lsb_Format.Insert(0, '0');
                    }
                    else
                    {
                        lsb_Format.Insert(0, '#');
                    }

                    System.Math.DivRem(li_Counter, 3, out ll_Remainder);
                    if ((ll_Remainder == 0) && (li_Counter + 1 <= _precision - _scaleOnFocus))
                    {
                        lsb_Format.Insert(0, ',');
                    }

                    li_Counter++;
                }

                li_Counter = 1;

                if (_scaleOnFocus > 0)
                {
                    lsb_Format.Append(".");

                    while (li_Counter <= _scaleOnFocus)
                    {
                        lsb_Format.Append('0');
                        li_Counter++;
                    }
                }

            }
            else
            {
                while (li_Counter <= _precision - _scaleOnLostFocus)
                {
                    if (li_Counter == 1)
                    {
                        lsb_Format.Insert(0, '0');
                    }
                    else
                    {
                        lsb_Format.Insert(0, '#');
                    }
                    System.Math.DivRem(li_Counter, 3, out ll_Remainder);
                    if ((ll_Remainder == 0) && (li_Counter + 1 <= _precision - _scaleOnLostFocus))
                    {
                        lsb_Format.Insert(0, ',');
                    }
                    li_Counter++;
                }

                li_Counter = 1;

                if (_scaleOnLostFocus > 0)
                {
                    lsb_Format.Append(".");

                    while (li_Counter <= _scaleOnLostFocus)
                    {
                        lsb_Format.Append('0');
                        li_Counter++;
                    }
                }
            }

            return Convert.ToDecimal(Text).ToString(lsb_Format.ToString());
        }
    }
}
