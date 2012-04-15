using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace SystemEx.Windows.Forms
{
    public class AutoComplete : IDisposable
    {
        private AutoCompleteForm _form = null;
        private bool _suppressTextChanged = false;
        private Timer _timer = null;

        public const int ItemCount = AutoCompleteForm.ItemCount;

        public AutoComplete(System.Windows.Forms.TextBox textBox)
            : this(textBox, textBox)
        {
        }

        public AutoComplete(Control owner, System.Windows.Forms.TextBox textBox)
        {
            if (owner == null)
                throw new ArgumentNullException("owner");
            if (textBox == null)
                throw new ArgumentNullException("textBox");

            Owner = owner;
            TextBox = textBox;
            Delay = 0;

            textBox.TextChanged += new EventHandler(textBox_TextChanged);
            textBox.LostFocus += new EventHandler(textBox_LostFocus);
            textBox.PreviewKeyDown += new PreviewKeyDownEventHandler(textBox_PreviewKeyDown);
            textBox.KeyDown += new KeyEventHandler(textBox_KeyDown);
        }

        public event EventHandler<AutoCompleteEventArgs> GetResults;
        public event EventHandler<AutoCompleteItemEventArgs> ItemActivated;
        public event EventHandler Showing;
        public event EventHandler Hidden;

        public int Delay
        {
            get
            {
                return _timer == null ? 0 : _timer.Interval;
            }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("value");

                if (value == 0)
                {
                    if (_timer != null)
                    {
                        _timer.Dispose();
                        _timer = null;
                    }
                }
                else
                {
                    if (_timer == null)
                    {
                        _timer = new Timer();
                        _timer.Tick += new EventHandler(Timer_Tick);
                    }

                    _timer.Interval = value;
                }
            }
        }

        public bool IsVisible
        {
            get { return _form != null && !_form.IsDisposed; }
        }

        public void Hide()
        {
            ShowForm(false);
        }

        void Timer_Tick(object sender, EventArgs e)
        {
            _timer.Stop();

            if (TextBox.Focused)
                PerformTextChanged();
        }

        protected virtual void OnGetResults(AutoCompleteEventArgs e)
        {
            if (GetResults != null)
            {
                GetResults(this, e);
            }
        }

        protected virtual void OnItemActivated(AutoCompleteItemEventArgs e)
        {
            if (ItemActivated != null)
            {
                ItemActivated(this, e);
            }
        }

        protected virtual void OnShowing(EventArgs e)
        {
            if (Showing != null)
            {
                Showing(this, e);
            }
        }

        protected virtual void OnHidden(EventArgs e)
        {
            if (Hidden != null)
            {
                Hidden(this, e);
            }
        }

        void textBox_LostFocus(object sender, EventArgs e)
        {
            if (IsVisible && !_form.MouseActivated)
            {
                ShowForm(false);
            }
        }

        void textBox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && IsVisible)
            {
                bool result = _form.SelectCurrentItem();

                if (!e.IsInputKey && result)
                    e.IsInputKey = true;
            }
        }

        void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (!IsVisible)
                return;

            switch (e.KeyCode)
            {
                case Keys.Up:
                case Keys.Down:
                    MoveItem(e.KeyCode == Keys.Down);

                    e.SuppressKeyPress = e.Handled = true;
                    break;
            }
        }

        private void MoveItem(bool down)
        {
            _form.MoveItem(down);

            _suppressTextChanged = true;

            _form.SelectCurrentItem();

            _suppressTextChanged = false;
        }

        void textBox_TextChanged(object sender, EventArgs e)
        {
            if (_suppressTextChanged)
                return;

            if (_timer == null)
            {
                PerformTextChanged();
            }
            else
            {
                _timer.Stop();
                _timer.Start();
            }
        }

        private void PerformTextChanged()
        {
            if (TextBox.Text == "")
            {
                ShowForm(false);
            }
            else
            {
                var results = new AutoCompleteEventArgs();
                bool suppressTextChanged = _suppressTextChanged;

                _suppressTextChanged = true;

                OnGetResults(results);

                _suppressTextChanged = suppressTextChanged;

                if (results.Items == null || results.Items.Length == 0)
                {
                    ShowForm(false);
                }
                else
                {
                    ShowForm(true);

                    _form.SetResults(results.Items);
                }
            }
        }

        private void ShowForm(bool show)
        {
            if (show)
            {
                if (!IsVisible)
                {
                    _form = new AutoCompleteForm();

                    _form.Owner = Owner;
                    _form.ItemActivated += new EventHandler<AutoCompleteItemEventArgs>(Form_ItemActivated);
                }

                OnShowing(EventArgs.Empty);

                _form.Show();
            }
            else
            {
                if (IsVisible)
                {
                    _form.Dispose();
                    _form = null;

                    OnHidden(EventArgs.Empty);
                }

                if (_timer != null)
                    _timer.Stop();
            }
        }

        void Form_ItemActivated(object sender, AutoCompleteItemEventArgs e)
        {
            if (!_suppressTextChanged)
                ShowForm(false);

            OnItemActivated(e);
        }

        public Control Owner { get; private set; }
        public System.Windows.Forms.TextBox TextBox { get; private set; }

        public void Dispose()
        {
            TextBox.TextChanged -= new EventHandler(textBox_TextChanged);
            TextBox.LostFocus -= new EventHandler(textBox_LostFocus);
            TextBox.KeyDown -= new KeyEventHandler(textBox_KeyDown);
            TextBox.PreviewKeyDown -= new PreviewKeyDownEventHandler(textBox_PreviewKeyDown);

            if (_timer != null)
            {
                _timer.Dispose();
                _timer = null;
            }
        }
    }

    public class AutoCompleteEventArgs : EventArgs
    {
        public AutoCompleteEventArgs()
        {
        }

        public AutoCompleteItem[] Items { get; set; }
    }

    public class AutoCompleteItemEventArgs : EventArgs
    {
        public AutoCompleteItemEventArgs(AutoCompleteItem item)
        {
            Item = item;
        }

        public AutoCompleteItem Item { get; private set; }
    }

    public class AutoCompleteItem
    {
        public AutoCompleteItem()
            : this(null)
        {
        }

        public AutoCompleteItem(string text)
            : this(text, null)
        {
        }

        public AutoCompleteItem(string text, object tag)
        {
            Text = text;
            Tag = tag;
        }

        public string Text { get; set; }
        public object Tag { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }
}
