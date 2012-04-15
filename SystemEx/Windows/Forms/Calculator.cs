using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace SystemEx.Windows.Forms
{
    internal class Calculator
    {
        private decimal? _memory;
        private decimal? _lastResult;
        private decimal _result;
        private string _textResult = String.Empty;
        private CalculatorAction _lastAction;
        private bool _haveError;

        internal event EventHandler LastResultChanged;
        internal event EventHandler LastActionChanged;
        internal event EventHandler ResultChanged;
        internal event EventHandler MemoryChanged;

        internal Calculator()
        {
            _lastAction = CalculatorAction.None;
            _result = 0;
        }

        internal bool HaveError
        {
            get { return _haveError; }
            set
            {
                if (_haveError != value)
                {
                    _haveError = value;

                    OnResultChanged(EventArgs.Empty);
                }
            }
        }

        protected virtual void OnLastResultChanged(EventArgs e)
        {
            if (LastResultChanged != null)
                LastResultChanged(this, e);
        }

        protected virtual void OnLastActionChanged(EventArgs e)
        {
            if (LastActionChanged != null)
                LastActionChanged(this, e);
        }

        protected virtual void OnResultChanged(EventArgs e)
        {
            if (ResultChanged != null)
                ResultChanged(this, e);
        }

        protected virtual void OnMemoryChanged(EventArgs e)
        {
            if (MemoryChanged != null)
                MemoryChanged(this, e);
        }

        internal decimal? Memory
        {
            get { return _memory; }
            private set
            {
                if (!Equals(_memory, value))
                {
                    _memory = value;

                    OnMemoryChanged(EventArgs.Empty);
                }
            }
        }

        internal decimal? LastResult
        {
            get { return _lastResult; }
            private set
            {
                if (!Equals(_lastResult, value))
                {
                    _lastResult = value;

                    OnLastResultChanged(EventArgs.Empty);
                }
            }
        }

        internal CalculatorAction LastAction
        {
            get { return _lastAction; }
            private set
            {
                if (_lastAction != value)
                {
                    _lastAction = value;

                    OnLastActionChanged(EventArgs.Empty);
                }
            }
        }

        internal decimal Result
        {
            get { return _result; }
            private set
            {
                if (!Equals(_result, value))
                {
                    _result = value;
                    _textResult = value.ToString(CultureInfo.InvariantCulture);

                    OnResultChanged(EventArgs.Empty);
                }
            }
        }

        internal void PerformAction(CalculatorAction action)
        {
            HaveError = false;

            switch (action)
            {
                case CalculatorAction.Add:
                case CalculatorAction.Divide:
                case CalculatorAction.Multiply:
                case CalculatorAction.OneDividedBy:
                case CalculatorAction.SquareRoot:
                case CalculatorAction.Subtract:
                    AddAction(action);
                    break;

                case CalculatorAction.Negative: PerformNegative(); break;

                case CalculatorAction.Percentage:
                case CalculatorAction.Perform:
                    Calculate(action);
                    break;

                case CalculatorAction.Add0: AddCharacter("0"); break;
                case CalculatorAction.Add1: AddCharacter("1"); break;
                case CalculatorAction.Add2: AddCharacter("2"); break;
                case CalculatorAction.Add3: AddCharacter("3"); break;
                case CalculatorAction.Add4: AddCharacter("4"); break;
                case CalculatorAction.Add5: AddCharacter("5"); break;
                case CalculatorAction.Add6: AddCharacter("6"); break;
                case CalculatorAction.Add7: AddCharacter("7"); break;
                case CalculatorAction.Add8: AddCharacter("8"); break;
                case CalculatorAction.Add9: AddCharacter("9"); break;
                case CalculatorAction.AddDecimal: AddCharacter("."); break;
                case CalculatorAction.Backspace: RemoveCharacter(); break;

                case CalculatorAction.MemoryAdd: MemoryAdd(); break;
                case CalculatorAction.MemoryClear: MemoryClear(); break;
                case CalculatorAction.MemoryRecall: MemoryRecall(); break;
                case CalculatorAction.MemorySet: MemorySet(); break;
                case CalculatorAction.MemorySubtract: MemorySubtract(); break;

                case CalculatorAction.Clear: ClearAll(); break;
                case CalculatorAction.ClearEntry: ClearEntry(); break;

                default:
                    throw new NotSupportedException();
            }
        }

        private void ClearAll()
        {
            LastAction = CalculatorAction.None;
            LastResult = null;
            Result = 0m;
        }

        private void ClearEntry()
        {
            Result = 0m;
        }

        private void Calculate(CalculatorAction action)
        {
            if (!LastResult.HasValue)
                return;

            decimal result = Result;

            if (action == CalculatorAction.Percentage)
                result /= 100m;

            try
            {
                switch (LastAction)
                {
                    case CalculatorAction.Add:
                        Result = LastResult.Value + result;
                        break;

                    case CalculatorAction.Divide:
                        Result = LastResult.Value / result;
                        break;

                    case CalculatorAction.Multiply:
                        Result = LastResult.Value * result;
                        break;

                    case CalculatorAction.OneDividedBy:
                        Result = 1m / LastResult.Value;
                        break;

                    case CalculatorAction.SquareRoot:
                        Result = (decimal)Math.Sqrt((double)LastResult.Value);
                        break;

                    case CalculatorAction.Subtract:
                        Result = LastResult.Value - result;
                        break;

                    default:
                        throw new NotSupportedException();
                }
            }
            catch
            {
                HaveError = true;
                ClearAll();
            }

            LastAction = CalculatorAction.None;
            LastResult = null;

            _textResult = "0";
        }

        private void AddAction(CalculatorAction action)
        {
            if (LastAction != CalculatorAction.None)
                Calculate(CalculatorAction.Perform);

            LastAction = action;
            LastResult = Result;

            Result = 0m;

            switch (action)
            {
                case CalculatorAction.OneDividedBy:
                case CalculatorAction.SquareRoot:
                    Calculate(CalculatorAction.Perform);
                    break;
            }
        }

        private void PerformNegative()
        {
            if (Result != 0m)
            {
                Result = -Result;
            }
        }

        private void RemoveCharacter()
        {
            if (_textResult.Length == 1)
                _textResult = "0";
            else
                _textResult = _textResult.Substring(0, _textResult.Length - 1);

            Result = Decimal.Parse(_textResult, CultureInfo.InvariantCulture);
        }

        private void AddCharacter(string character)
        {
            if (_textResult == "0" && character != ".")
                _textResult = character;
            else
                _textResult += character;

            Result = Decimal.Parse(_textResult, CultureInfo.InvariantCulture);
        }

        private void MemoryAdd()
        {
            Memory = (Memory.HasValue ? Memory.Value : 0m) + Result;
        }

        private void MemoryClear()
        {
            Memory = null;
        }

        private void MemoryRecall()
        {
            Result = Memory.HasValue ? Memory.Value : 0m;
        }

        private void MemorySet()
        {
            Memory = Result;
        }

        private void MemorySubtract()
        {
            Memory = (Memory.HasValue ? Memory.Value : 0m) - Result;
        }
    }

    public enum CalculatorAction
    {
        None,
        Add,
        Add0,
        Add1,
        Add2,
        Add3,
        Add4,
        Add5,
        Add6,
        Add7,
        Add8,
        Add9,
        AddDecimal,
        Backspace,
        Clear,
        ClearEntry,
        Divide,
        MemoryAdd,
        MemoryClear,
        MemoryRecall,
        MemorySet,
        MemorySubtract,
        Multiply,
        Negative,
        OneDividedBy,
        Percentage,
        Perform,
        SquareRoot,
        Subtract
    }
}
