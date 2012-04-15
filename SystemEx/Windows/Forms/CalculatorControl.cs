using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

namespace SystemEx.Windows.Forms
{
    public partial class CalculatorControl : UserControl
    {
        private readonly Calculator _calculator = new Calculator();

        public CalculatorControl()
        {
            InitializeComponent();

            _addButton.Click += (s, e) => _calculator.PerformAction(CalculatorAction.Add);
            _backspaceButton.Click += (s, e) => _calculator.PerformAction(CalculatorAction.Backspace);
            _clearButton.Click += (s, e) => _calculator.PerformAction(CalculatorAction.Clear);
            _clearEntryButton.Click += (s, e) => _calculator.PerformAction(CalculatorAction.ClearEntry);
            _decimalButton.Click += (s, e) => _calculator.PerformAction(CalculatorAction.AddDecimal);
            _divideButton.Click += (s, e) => _calculator.PerformAction(CalculatorAction.Divide);
            _memoryAddButton.Click += (s, e) => _calculator.PerformAction(CalculatorAction.MemoryAdd);
            _memoryClearButton.Click += (s, e) => _calculator.PerformAction(CalculatorAction.MemoryClear);
            _memoryRecallButton.Click += (s, e) => _calculator.PerformAction(CalculatorAction.MemoryRecall);
            _memorySetButton.Click += (s, e) => _calculator.PerformAction(CalculatorAction.MemorySet);
            _memorySubtractButton.Click += (s, e) => _calculator.PerformAction(CalculatorAction.MemorySubtract);
            _multiplyButton.Click += (s, e) => _calculator.PerformAction(CalculatorAction.Multiply);
            _negativeButton.Click += (s, e) => _calculator.PerformAction(CalculatorAction.Negative);
            _number0Button.Click += (s, e) => _calculator.PerformAction(CalculatorAction.Add0);
            _number1Button.Click += (s, e) => _calculator.PerformAction(CalculatorAction.Add1);
            _number2Button.Click += (s, e) => _calculator.PerformAction(CalculatorAction.Add2);
            _number3Button.Click += (s, e) => _calculator.PerformAction(CalculatorAction.Add3);
            _number4Button.Click += (s, e) => _calculator.PerformAction(CalculatorAction.Add4);
            _number5Button.Click += (s, e) => _calculator.PerformAction(CalculatorAction.Add5);
            _number6Button.Click += (s, e) => _calculator.PerformAction(CalculatorAction.Add6);
            _number7Button.Click += (s, e) => _calculator.PerformAction(CalculatorAction.Add7);
            _number8Button.Click += (s, e) => _calculator.PerformAction(CalculatorAction.Add8);
            _number9Button.Click += (s, e) => _calculator.PerformAction(CalculatorAction.Add9);
            _oneDividedByButton.Click += (s, e) => _calculator.PerformAction(CalculatorAction.OneDividedBy);
            _percentageButton.Click += (s, e) => _calculator.PerformAction(CalculatorAction.Percentage);
            _resultButton.Click += (s, e) => _calculator.PerformAction(CalculatorAction.Perform);
            _rootButton.Click += (s, e) => _calculator.PerformAction(CalculatorAction.SquareRoot);
            _subtractButton.Click += (s, e) => _calculator.PerformAction(CalculatorAction.Subtract);

            _memoryStatusLabel.Text = "";
            _lastResultLabel.Text = "";
            _resultLabel.Text = "0";

            _calculator.MemoryChanged += new EventHandler(_calculator_MemoryChanged);
            _calculator.LastResultChanged += new EventHandler(_calculator_LastResultChanged);
            _calculator.LastActionChanged += new EventHandler(_calculator_LastActionChanged);
            _calculator.ResultChanged += new EventHandler(_calculator_ResultChanged);
        }

        void _calculator_LastActionChanged(object sender, EventArgs e)
        {
            UpdateLastResultLine();
        }

        private void UpdateLastResultLine()
        {
            if (_calculator.LastResult.HasValue && _calculator.LastAction != CalculatorAction.None)
            {
                _lastResultLabel.Text = String.Concat(
                    _calculator.LastResult.Value.ToString(CultureInfo.CurrentUICulture),
                    " ",
                    GetActionChar(_calculator.LastAction)
                );
            }
            else
            {
                _lastResultLabel.Text = "";
            }
        }

        private string GetActionChar(CalculatorAction calculatorAction)
        {
            switch (calculatorAction)
            {
                case CalculatorAction.Add: return "+";
                case CalculatorAction.Divide: return "/";
                case CalculatorAction.Multiply: return "*";
                case CalculatorAction.Negative: return "±";
                case CalculatorAction.OneDividedBy: return "1/x";
                case CalculatorAction.Percentage: return "%";
                case CalculatorAction.SquareRoot: return "√";
                case CalculatorAction.Subtract: return "-";

                default:
                    throw new NotSupportedException();
            }
        }

        void _calculator_ResultChanged(object sender, EventArgs e)
        {
            if (_calculator.HaveError)
            {
                _resultLabel.Text = "ERR";
            }
            else
            {
                _resultLabel.Text = _calculator.Result.ToString(CultureInfo.CurrentUICulture);
            }
        }

        void _calculator_LastResultChanged(object sender, EventArgs e)
        {
            UpdateLastResultLine();
        }

        void _calculator_MemoryChanged(object sender, EventArgs e)
        {
            _memoryStatusLabel.Text = _calculator.Memory.HasValue ? "M" : "";
        }

        public void Perform(CalculatorAction action)
        {
            _calculator.PerformAction(action);
        }

        public decimal Result
        {
            get { return _calculator.Result; }
        }
    }
}
