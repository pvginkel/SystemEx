using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SystemEx.Win32;

namespace SystemEx.Windows.Forms
{
    internal partial class AutoCompleteForm : Form
    {
        public const int ItemCount = 8;
        private const int DefaultWidth = 240;

        internal AutoCompleteForm()
        {
            InitializeComponent();

            SetStyle(ControlStyles.Opaque | ControlStyles.Selectable, false);
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);

            MouseActivated = false;
        }

        internal new Control Owner { get; set; }

        public event EventHandler<AutoCompleteItemEventArgs> ItemActivated;

        public bool MouseActivated { get; private set; }

        protected virtual void OnItemActivated(AutoCompleteItemEventArgs e)
        {
            if (ItemActivated != null)
            {
                ItemActivated(this, e);
            }
        }

        protected override CreateParams CreateParams
        {
            get
            {
                var createParams = base.CreateParams;

                createParams.Style = NativeMethods.WS_POPUP | NativeMethods.WS_CLIPSIBLINGS | NativeMethods.WS_CLIPCHILDREN | NativeMethods.WS_BORDER;
                createParams.ExStyle = NativeMethods.WS_EX_LEFT | NativeMethods.WS_EX_LTRREADING | NativeMethods.WS_EX_TOPMOST | NativeMethods.WS_EX_NOPARENTNOTIFY | NativeMethods.WS_EX_TOOLWINDOW;

                return createParams;
            }
        }

        internal new void Show()
        {
            var windowRect = new NativeMethods.RECT();
            var clientRect = new NativeMethods.RECT();

            NativeMethods.GetWindowRect(Handle, ref windowRect);
            NativeMethods.GetClientRect(Handle, ref clientRect);

            int extraHeight =
                ((windowRect.bottom - windowRect.top) - clientRect.bottom) +
                panel1.Height;

            int requiredHeight = ItemCount * listBox1.ItemHeight + extraHeight;

            var borderSize = new Size(
                (Owner.Width - Owner.ClientRectangle.Width) / 2,
                (Owner.Height - Owner.ClientRectangle.Height) / 2
            );

            var point = new Point(-borderSize.Width, -borderSize.Height);

            NativeMethods.ClientToScreen(Owner.Handle, ref point);

            var screen = Screen.FromHandle(Owner.Handle);

            int bottom = point.Y + Owner.Height + requiredHeight;

            bool atTop = bottom > screen.Bounds.Bottom;

            if (atTop)
                Bounds = new Rectangle(
                    point.X, point.Y - requiredHeight, DefaultWidth, requiredHeight);
            else
                Bounds = new Rectangle(
                    point.X, point.Y + Owner.Height, DefaultWidth, requiredHeight);

            NativeMethods.ShowWindow(Handle, NativeMethods.SW_SHOWNOACTIVATE);
        }

        private void panel1_Resize(object sender, EventArgs e)
        {
            pictureBox1.Left = (Width - pictureBox1.Width) / 2;
        }

        internal void SetResults(IEnumerable<AutoCompleteItem> results)
        {
            listBox1.BeginUpdate();

            int selectedIndex = listBox1.SelectedIndex;

            listBox1.Items.Clear();

            int count = 0;
            int listBoxCount = listBox1.ClientRectangle.Height / listBox1.ItemHeight;
            bool hadMore = false;

            foreach (var result in results)
            {
                count++;

                if (count > listBoxCount)
                {
                    hadMore = true;
                    break;
                }

                listBox1.Items.Add(result);
            }

            pictureBox1.Visible = hadMore;

            if (selectedIndex >= listBox1.Items.Count)
                listBox1.SelectedIndex = -1;
            else
                listBox1.SelectedIndex = selectedIndex;

            listBox1.EndUpdate();
        }

        private void listBox1_MouseMove(object sender, MouseEventArgs e)
        {
            int index = e.Y / listBox1.ItemHeight;

            listBox1.SelectedIndex = index >= listBox1.Items.Count ? -1 : index;
        }

        internal void MoveItem(bool down)
        {
            int selectedIndex = listBox1.SelectedIndex;

            if (selectedIndex == -1)
                selectedIndex = 0;
            else
                selectedIndex += down ? 1 : -1;

            if (listBox1.Items.Count == 0)
                selectedIndex = -1;
            else if (selectedIndex < 0)
                selectedIndex = 0;
            else if (selectedIndex >= listBox1.Items.Count)
                selectedIndex = listBox1.Items.Count - 1;

            listBox1.SelectedIndex = selectedIndex;
        }

        internal bool SelectCurrentItem()
        {
            if (listBox1.SelectedIndex != -1)
            {
                OnItemActivated(
                    new AutoCompleteItemEventArgs((AutoCompleteItem)listBox1.SelectedItem)
                );

                return true;
            }
            else
            {
                return false;
            }
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == NativeMethods.WM_MOUSEACTIVATE)
            {
                MouseActivated = true;
                m.Result = (IntPtr)NativeMethods.MA_NOACTIVATEANDEAT;
                return;
            }

            base.WndProc(ref m);
        }

        private void listBox1_MouseUp(object sender, MouseEventArgs e)
        {
            OnItemActivated(
                new AutoCompleteItemEventArgs((AutoCompleteItem)listBox1.SelectedItem)
            );
        }
    }
}
