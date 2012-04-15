using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using SystemEx.Win32;

namespace SystemEx.Windows.Forms
{
    public class MdiWindowManager
    {
        private MdiClient _client;
        private Dictionary<IMdiChildForm, FormWindowState> _states = new Dictionary<IMdiChildForm, FormWindowState>();
        private bool _suppressStateChange;
        private bool _suppressGotFocus;
        private List<System.Windows.Forms.Form> _activeChildren = new List<System.Windows.Forms.Form>();
        private Timer _flashTimer = new Timer();
        private System.Windows.Forms.Form _flashingForm;

        public System.Windows.Forms.Form Parent { get; private set; }

        public event EventHandler ActiveChildrenChanged;

        protected virtual void OnActiveChildrenChanged(EventArgs e)
        {
            if (ActiveChildrenChanged != null)
                ActiveChildrenChanged(this, e);
        }

        public System.Windows.Forms.Form[] ActiveChildren
        {
            get { return _activeChildren.ToArray(); }
        }

        public MdiWindowManager(System.Windows.Forms.Form parent)
        {
            if (parent == null)
                throw new ArgumentNullException("parent");

            Parent = parent;

            foreach (var control in parent.Controls)
            {
                _client = control as MdiClient;

                if (_client != null)
                    break;
            }

            if (_client == null)
                throw new ArgumentException("parent is not an MDI container");

            _client.MouseClick += _client_MouseClick;

            _client.ControlAdded += (s, e) => AddControl(e.Control);
            _client.ControlRemoved += (s, e) => RemoveControl(e.Control);

            foreach (Control control in _client.Controls)
            {
                AddControl(control);
            }
            
            Parent.Disposed += (s, e) => _flashTimer.Dispose();

            _flashTimer.Interval = 5 * SystemInformation.CaretBlinkTime;
            _flashTimer.Tick += _flashTimer_Tick;
        }

        void _flashTimer_Tick(object sender, EventArgs e)
        {
            StopFlashing();
        }

        private void StopFlashing()
        {
            _flashTimer.Stop();

            if (_flashingForm != null)
            {
                var info = new NativeMethods.FLASHWINFO();

                info.cbSize = (uint)Marshal.SizeOf(info);
                info.hWnd = _flashingForm.Handle;
                info.dwFlags = NativeMethods.FLASHW_STOP;
                
                NativeMethods.FlashWindowEx(ref info);

                if (_flashingForm.ContainsFocus)
                {
                    NativeMethods.SetWindowPos(
                        _flashingForm.Handle,
                        IntPtr.Zero,
                        0, 0, 0, 0,
                        NativeMethods.SWP_NOSIZE | NativeMethods.SWP_NOMOVE |
                        NativeMethods.SWP_NOZORDER | NativeMethods.SWP_FRAMECHANGED
                    );
                }

                _flashingForm = null;
            }
        }

        private void AddControl(Control control)
        {
            var mdiChild = control as IMdiChildForm;
            var form = control as System.Windows.Forms.Form;

            if (mdiChild == null || form == null)
                return;

            form.SizeChanged += form_SizeChanged;
            form.Activated += form_Activated;

            _states[mdiChild] = form.WindowState;

            if (mdiChild.MdiOwner != null)
            {
                _activeChildren[_activeChildren.IndexOf(mdiChild.MdiOwner)] = form;

                NativeMethods.EnableWindow(mdiChild.MdiOwner.Handle, false);
            }
            else
            {
                _activeChildren.Add(form);
            }

            OnActiveChildrenChanged(EventArgs.Empty);
        }

        private void RemoveControl(Control control)
        {
            var mdiChild = control as IMdiChildForm;
            var form = control as System.Windows.Forms.Form;

            if (mdiChild == null || form == null)
                return;

            _states.Remove(mdiChild);

            if (_flashingForm != null && _flashingForm == form)
                StopFlashing();

            form.SizeChanged -= form_SizeChanged;
            form.Activated -= form_Activated;

            var owner = mdiChild.MdiOwner;

            if (owner != null)
            {
                _activeChildren[_activeChildren.IndexOf(form)] = owner;

                NativeMethods.EnableWindow(owner.Handle, true);

                owner.Focus();
            }
            else
            {
                _activeChildren.Remove(form);
            }

            OnActiveChildrenChanged(EventArgs.Empty);
        }

        void form_Activated(object sender, EventArgs e)
        {
            if (_suppressGotFocus)
                return;

            var form = sender as System.Windows.Forms.Form;

            if (_flashingForm != null && _flashingForm != form)
                StopFlashing();

            PerformReorder(form);
        }

        private void PerformReorder(System.Windows.Forms.Form form)
        {
            var mdiChild = form as IMdiChildForm;

            try
            {
                _suppressGotFocus = true;

                var currentForm = FindTopForm(form);

                if (currentForm != form)
                    currentForm.Focus();

                var lastForm = currentForm;
                currentForm = GetParent(currentForm);

                // Ensure correct window ordering.

                while (currentForm != null)
                {
                    NativeMethods.SetWindowPos(
                        currentForm.Handle,
                        lastForm.Handle,
                        0, 0, 0, 0,
                        NativeMethods.SWP_NOSIZE | NativeMethods.SWP_NOMOVE |
                        NativeMethods.SWP_NOACTIVATE
                    );

                    lastForm = currentForm;
                    currentForm = GetParent(currentForm);
                }
            }
            finally
            {
                _suppressGotFocus = false;
            }
        }

        private System.Windows.Forms.Form GetParent(System.Windows.Forms.Form form)
        {
            var mdiForm = form as IMdiChildForm;

            return mdiForm != null ? mdiForm.MdiOwner : null;
        }

        void form_SizeChanged(object sender, EventArgs e)
        {
            if (_suppressStateChange)
                return;

            var mdiChild = sender as IMdiChildForm;
            var form = sender as System.Windows.Forms.Form;

            var lastState = _states[mdiChild];

            if (lastState == form.WindowState)
                return;

            _states[mdiChild] = form.WindowState;

            // Propagate the state change to all forms in the chain.

            try
            {
                _suppressStateChange = true;

                var topForm = FindTopForm(form);

                var currentForm = topForm;

                while (currentForm != null)
                {
                    if (currentForm != form)
                        currentForm.WindowState = form.WindowState;

                    currentForm = GetParent(currentForm);
                }

                if (topForm.WindowState != FormWindowState.Minimized)
                    PerformReorder(topForm);
            }
            finally
            {
                _suppressStateChange = false;
            }
        }

        void _client_MouseClick(object sender, MouseEventArgs e)
        {
            var handle = NativeMethods.ChildWindowFromPoint(_client.Handle, e.Location);

            if (handle == IntPtr.Zero || handle == _client.Handle)
                return;

            // Find the child that was clicked.

            System.Windows.Forms.Form clickedForm = null;

            foreach (var form in Parent.MdiChildren)
            {
                if (form.Handle == handle)
                {
                    clickedForm = form;
                    break;
                }
            }

            if (clickedForm == null)
                return;

            // Activate the correct form.

            var topForm = FindTopForm(clickedForm);

            if (topForm.ContainsFocus)
            {
                StopFlashing();

                var info = new NativeMethods.FLASHWINFO();

                info.cbSize = (uint)Marshal.SizeOf(info);
                info.hWnd = topForm.Handle;
                info.dwFlags = NativeMethods.FLASHW_TIMER | NativeMethods.FLASHW_ALL;

                NativeMethods.FlashWindowEx(ref info);

                _flashingForm = topForm;

                _flashTimer.Start();
            }
            else
            {
                topForm.Focus();
            }
        }

        public System.Windows.Forms.Form FindTopForm(System.Windows.Forms.Form form)
        {
            return FindTopFormInternal(form) ?? form;
        }

        private System.Windows.Forms.Form FindTopFormInternal(System.Windows.Forms.Form form)
        {
            foreach (var childForm in Parent.MdiChildren)
            {
                var mdiChild = childForm as IMdiChildForm;

                if (mdiChild != null && mdiChild.MdiOwner == form)
                    return FindTopFormInternal(childForm) ?? childForm;
            }

            return null;
        }

        public void SetThinBorderStyle()
        {
            // Replace the thick border with a thin border.

            int style = NativeMethods.GetWindowLong(_client.Handle, NativeMethods.GWL_STYLE);
            int exStyle = NativeMethods.GetWindowLong(_client.Handle, NativeMethods.GWL_EXSTYLE);

            style |= NativeMethods.WS_BORDER;
            exStyle &= ~NativeMethods.WS_EX_CLIENTEDGE;

            NativeMethods.SetWindowLong(_client.Handle, NativeMethods.GWL_STYLE, style);
            NativeMethods.SetWindowLong(_client.Handle, NativeMethods.GWL_EXSTYLE, exStyle);

            NativeMethods.SetWindowPos(
                _client.Handle, IntPtr.Zero, 0, 0, 0, 0,
                NativeMethods.SWP_FRAMECHANGED | NativeMethods.SWP_NOMOVE | NativeMethods.SWP_NOZORDER | NativeMethods.SWP_NOSIZE
            );
        }
    }
}
