using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace SystemEx.Windows.Forms
{
    [DefaultEvent("ValueChanged")]
    [DefaultProperty("Value")]
    public class SimpleNumericTextBox : TextBox
    {
        private int? _precision;
        private int? _scale;
        private bool _isNullable = true;
        private decimal? _value;
        private decimal? _lastValue;
        private bool _isCalculatorEnabled;
        private Button _calculatorButton;
        private bool _suppressSetValue;

        public SimpleNumericTextBox()
        {
            TextAlign = HorizontalAlignment.Right;
        }

        [DefaultValue(null)]
        [Category("Behavior")]
        public int? NumberPrecision
        {
            get { return _precision; }
            set
            {
                if (value.HasValue)
                    value = Math.Max(value.Value, 1);

                if (_precision != value)
                {
                    _precision = value;

                    FormatText();
                }
            }
        }

        [DefaultValue(null)]
        [Category("Behavior")]
        public int? NumberScale
        {
            get { return _scale; }
            set
            {
                if (value.HasValue)
                    value = Math.Max(value.Value, 0);

                if (_scale != value)
                {
                    _scale = value;

                    FormatText();
                }
            }
        }

        [DefaultValue(true)]
        [Category("Behavior")]
        public bool IsNullable
        {
            get { return _isNullable; }
            set
            {
                if (_isNullable != value)
                {
                    _isNullable = value;

                    if (!_isNullable && !_value.HasValue)
                    {
                        _value = 0;
                        _lastValue = 0;
                    }

                    FormatText();
                }
            }
        }

        [DefaultValue(false)]
        [Category("Behavior")]
        public bool IsCalculatorEnabled
        {
            get { return _isCalculatorEnabled; }
            set
            {
                if (_isCalculatorEnabled != value)
                {
                    _isCalculatorEnabled = value;

                    UpdateCalculatorEnabled();
                }
            }
        }

        [DefaultValue(null)]
        [Category("Behavior")]
        public decimal? Value
        {
            get { return _value; }
            set { SetValue(value, true); }
        }

        [DefaultValue(HorizontalAlignment.Right)]
        [Localizable(true)]
        public new HorizontalAlignment TextAlign
        {
            get { return base.TextAlign; }
            set { base.TextAlign = value; }
        }

        [Category("Property Changed")]
        public event EventHandler ValueChanged;

        private void OnValueChanged(EventArgs e)
        {
            if (ValueChanged != null)
                ValueChanged(this, e);
        }

        private void SetValue(decimal? value, bool force)
        {
            if (_suppressSetValue)
                return;

            _suppressSetValue = true;

            try
            {
                if (!Equals(_value, value) || force)
                {
                    _value = value;
                    _lastValue = value;

                    if (force)
                        FormatText();

                    OnValueChanged(EventArgs.Empty);
                }
            }
            finally
            {
                _suppressSetValue = false;
            }
        }

        protected override void OnValidating(CancelEventArgs e)
        {
            SetValue(Parse(Text), true);

            base.OnValidating(e);
        }

        protected override void OnTextChanged(EventArgs e)
        {
            SetValue(Parse(Text), false);

            base.OnTextChanged(e);
        }

        private decimal? Parse(string text)
        {
            int? precision;

            if (_precision.HasValue && _scale.HasValue)
                precision = Math.Max(_precision.Value, _scale.Value + 1);
            else if (_precision.HasValue)
                precision = _precision.Value;
            else
                precision = null;

            decimal value;

            text = Regex.Replace(
                text, "[^\\d-" + CultureInfo.CurrentUICulture.NumberFormat.NumberDecimalSeparator + "]", ""
            );

            if (String.Empty.Equals(text))
            {
                return _isNullable ? (decimal?)null : 0;
            }
            else if (Decimal.TryParse(text, NumberStyles.Any, CultureInfo.CurrentUICulture, out value))
            {
                if (precision.HasValue)
                {
                    // When the number entered is too large, force it to the
                    // maximum value.

                    decimal maxValue = 1m;

                    for (int i = 0; i < precision - _scale.GetValueOrDefault(); i++)
                    {
                        maxValue *= 10m;
                    }

                    if (Math.Abs(value) >= maxValue)
                    {
                        // If we only have a precision, but not a scale, we don't
                        // know how many 9's we have to put after the comma.
                        // We choose a reasonable number here: 3.

                        decimal smallestIncrement = new Decimal(1, 0, 0, false, (byte)_scale.GetValueOrDefault(3));
                        bool negative = value < 0m;

                        value = maxValue - smallestIncrement;

                        if (negative)
                            value = -value;
                    }
                }

                // Truncate decimal digits out of range.

                if (_scale.HasValue)
                    value = Decimal.Round(value, _scale.Value);

                return value;
            }
            else
            {
                return _lastValue;
            }
        }

        private void FormatText()
        {
            if (_value.HasValue && _scale.HasValue)
            {
                string format = String.Concat(
                    "0",
                    (_scale.Value == 0 ? String.Empty : "."),
                    new String('0', _scale.Value)
                );

                Text = _value.Value.ToString(format);
            }
            else if (_value.HasValue)
            {
                Text = _value.Value.ToString();
            }
            else
            {
                Text = String.Empty;
            }
        }

        private void UpdateCalculatorEnabled()
        {
            if (_isCalculatorEnabled)
            {
                _calculatorButton = new Button
                {
                    Image = Properties.Resources.calculator,
                    Visible = Enabled
                };

                _calculatorButton.Click += _calculatorButton_Click;

                LeftButtons.Add(_calculatorButton);
            }
            else
            {
                LeftButtons.Remove(_calculatorButton);

                _calculatorButton.Dispose();
                _calculatorButton = null;
            }
        }

        void _calculatorButton_Click(object sender, EventArgs e)
        {
            ShowFlyout();
        }

        private void ShowFlyout()
        {
            var calculator = new CalculatorForm();

            calculator.FormClosed += (s, e) => calculator.Dispose();
            calculator.ResultAccepted += (s, e) => Value = calculator.Result;

            calculator.Show(this);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (
                _isCalculatorEnabled &&
                !(e.Control || e.Alt || e.Shift) &&
                e.KeyCode == Keys.F4
            )
            {
                ShowFlyout();
            }
            else if (e.KeyCode == Keys.Decimal)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;

                SelectedText = CultureInfo.CurrentUICulture.NumberFormat.NumberDecimalSeparator;
            }
            else
            {
                base.OnKeyDown(e);
            }
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);

            if (_calculatorButton != null)
                _calculatorButton.Visible = Enabled;
        }
    }
}
