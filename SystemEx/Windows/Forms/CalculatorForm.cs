using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SystemEx.Windows.Forms
{
    public partial class CalculatorForm : Form
    {
        public CalculatorForm()
        {
            InitializeComponent();
        }

        public event EventHandler ResultAccepted;

        protected virtual void OnResultAccepted(EventArgs e)
        {
            if (ResultAccepted != null)
                ResultAccepted(this, e);
        }

        public decimal Result
        {
            get { return _calculator.Result; }
        }

        private void _acceptButton_Click(object sender, EventArgs e)
        {
            _calculator.Perform(CalculatorAction.Perform);

            OnResultAccepted(EventArgs.Empty);

            Close();
        }

        private void _cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        public void Show(Control control)
        {
            Location = control.PointToScreen(new Point(0, control.Height));

            Show();
        }

        private void CalculatorForm_Shown(object sender, EventArgs e)
        {
            _acceptButton.Focus();

            Focus();
        }

        private void CalculatorForm_Deactivate(object sender, EventArgs e)
        {
            Close();
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            var keyCode = keyData & Keys.KeyCode;

            bool shift = (keyData & Keys.Shift) != 0;

            switch (keyCode)
            {
                case Keys.Add:
                    _calculator.Perform(CalculatorAction.Add);
                    return true;

                case Keys.Subtract:
                    _calculator.Perform(CalculatorAction.Subtract);
                    return true;

                case Keys.Multiply:
                    _calculator.Perform(CalculatorAction.Multiply);
                    return true;
                    
                case Keys.Divide:
                    _calculator.Perform(CalculatorAction.Divide);
                    return true;

                case Keys.Decimal:
                    _calculator.Perform(CalculatorAction.AddDecimal);
                    return true;

                case Keys.Return:
                    _acceptButton.PerformClick();
                    return true;

                case Keys.OemMinus:
                    if (!shift)
                    {
                        _calculator.Perform(CalculatorAction.Subtract);
                        return true;
                    }
                    return false;

                case Keys.Oemplus:
                    if (shift)
                        _calculator.Perform(CalculatorAction.Add);
                    else
                        _calculator.Perform(CalculatorAction.Perform);
                    return true;

                case Keys.OemQuestion:
                    if (!shift)
                    {
                        _calculator.Perform(CalculatorAction.Divide);
                        return true;
                    }
                    return false;

                case Keys.OemPeriod:
                case Keys.Oemcomma:
                    if (!shift)
                    {
                        _calculator.Perform(CalculatorAction.AddDecimal);
                        return true;
                    }
                    return false;

                case Keys.D0:
                case Keys.NumPad0:
                    if (!shift)
                    {
                        _calculator.Perform(CalculatorAction.Add0);
                        return true;
                    }
                    return false;

                case Keys.D1:
                case Keys.NumPad1:
                    if (!shift)
                    {
                        _calculator.Perform(CalculatorAction.Add1);
                        return true;
                    }
                    return false;

                case Keys.D2:
                case Keys.NumPad2:
                    if (!shift)
                    {
                        _calculator.Perform(CalculatorAction.Add2);
                        return true;
                    }
                    return false;

                case Keys.D3:
                case Keys.NumPad3:
                    if (!shift)
                    {
                        _calculator.Perform(CalculatorAction.Add3);
                        return true;
                    }
                    return false;

                case Keys.D4:
                case Keys.NumPad4:
                    if (!shift)
                    {
                        _calculator.Perform(CalculatorAction.Add4);
                        return true;
                    }
                    return false;

                case Keys.D5:
                case Keys.NumPad5:
                    if (!shift)
                    {
                        _calculator.Perform(CalculatorAction.Add5);
                        return true;
                    }
                    return false;

                case Keys.D6:
                case Keys.NumPad6:
                    if (!shift)
                    {
                        _calculator.Perform(CalculatorAction.Add6);
                        return true;
                    }
                    return false;

                case Keys.D7:
                case Keys.NumPad7:
                    if (!shift)
                    {
                        _calculator.Perform(CalculatorAction.Add7);
                        return true;
                    }
                    return false;

                case Keys.D8:
                    if (shift)
                        _calculator.Perform(CalculatorAction.Multiply);
                    else
                        _calculator.Perform(CalculatorAction.Add8);
                    return true;

                case Keys.NumPad8:
                    if (!shift)
                    {
                        _calculator.Perform(CalculatorAction.Add8);
                        return true;
                    }
                    return false;

                case Keys.D9:
                case Keys.NumPad9:
                    if (!shift)
                    {
                        _calculator.Perform(CalculatorAction.Add9);
                        return true;
                    }
                    return false;

                case Keys.Back:
                    _calculator.Perform(CalculatorAction.Backspace);
                    return true;

                default:
                    return base.ProcessDialogKey(keyData);
            }
        }
    }
}
