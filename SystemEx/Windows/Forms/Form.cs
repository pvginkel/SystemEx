using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using SystemEx.Win32;
using SystemEx.Windows.Forms.Internal;

namespace SystemEx.Windows.Forms
{
    public class Form : System.Windows.Forms.Form
    {
        private const int SizeGripSize = 16;

        private FormHelper _fixer;
        private bool _showingCalled = false;
        private bool _renderSizeGrip = false;
        private SizeGripStyle _lastSizeGripStyle;
        private bool _closeButtonEnabled = true;
        private bool _disposed;
        private bool _clientSizeScaled;

        public event ControlEventHandler FixControl;

        public Form()
        {
            _fixer = new FormHelper(this);
            _fixer.EnableBoundsTracking = true;
            _fixer.FixControl += (s, e) => OnFixControl(e);

            _lastSizeGripStyle = SizeGripStyle;
        }

        public event CancelPreviewEventHandler CancelPreview;

        public event EventHandler Showing;

        public event BrowseButtonEventHandler BrowseButtonClick;

        [DefaultValue(true)]
        public bool CloseButtonEnabled
        {
            get { return _closeButtonEnabled; }
            set
            {
                if (_closeButtonEnabled != value)
                {
                    _closeButtonEnabled = value;

                    NativeMethods.EnableMenuItem(
                        NativeMethods.GetSystemMenu(this.Handle, false),
                        NativeMethods.SC_CLOSE,
                        value ? NativeMethods.MF_ENABLED : NativeMethods.MF_GRAYED
                    );

                    InvalidateNonClient();
                }
            }
        }

        protected virtual void OnBrowseButtonClick(BrowseButtonEventArgs e)
        {
            var ev = BrowseButtonClick;

            if (ev != null)
                ev(this, e);
        }

        protected void StoreUserSettings()
        {
            _fixer.StoreUserSettings();
        }

        protected string UserSettingsKey
        {
            get { return _fixer.KeyAddition; }
            set { _fixer.KeyAddition = value; }
        }

        protected override void SetClientSizeCore(int x, int y)
        {
            if (!_clientSizeScaled)
            {
                _clientSizeScaled = true;

                if (ControlUtil.IsDpiScaled)
                {
                    var size = ControlUtil.Scale(new Size(x, y));

                    x = size.Width;
                    y = size.Height;
                }
            }

            base.SetClientSizeCore(x, y);
        }

        protected override void SetVisibleCore(bool value)
        {
            _fixer.InitializeForm();

            if (value && !_showingCalled)
            {
                _showingCalled = true;

                OnShowing(EventArgs.Empty);
            }

            base.SetVisibleCore(value);
        }

        protected bool RestoreUserSettings()
        {
            return _fixer.RestoreUserSettings();
        }

        protected virtual void OnShowing(EventArgs e)
        {
            if (Showing != null)
            {
                Showing(this, e);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                if (!_fixer.InDesignMode)
                    _fixer.StoreUserSettings();

                _disposed = true;
            }

            base.Dispose(disposing);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);

            _fixer.OnSizeChanged(e);
        }

        protected override void OnLocationChanged(EventArgs e)
        {
            base.OnLocationChanged(e);

            _fixer.OnLocationChanged(e);
        }

        public void CenterOverParent(double relativeSize)
        {
            _fixer.CenterOverParent(relativeSize);
        }

        public void TrackProperty(Control control, string property)
        {
            _fixer.TrackProperty(control, property);
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public new AutoScaleMode AutoScaleMode
        {
            get { return base.AutoScaleMode; }
            set
            {
                // This value is set by the designer. To not have to manually change the
                // defaults set by the designer, it's silently ignored here at runtime.

                if (_fixer.InDesignMode)
                    base.AutoScaleMode = value;
            }
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            //
            // Overrides the default handling of the Escape key. When a cancel
            // button is present, the Escape key is not published through
            // KeyPreview. Because of this, no alternative handling is possible.
            // This override catches the Escape key before it is processed by
            // the framework and cancels default handling when the CancelPreview
            // event signals it has handled the Escape key.
            //

            if ((keyData & (Keys.Alt | Keys.Control)) == Keys.None)
            {
                Keys keyCode = (Keys)keyData & Keys.KeyCode;

                if (keyCode == Keys.Escape)
                {
                    var e = new CancelPreviewEventArgs();

                    OnCancelPreview(e);

                    if (e.Handled)
                    {
                        return true;
                    }
                }
            }

            return base.ProcessDialogKey(keyData);
        }

        protected virtual void OnCancelPreview(CancelPreviewEventArgs e)
        {
            if (CancelPreview != null)
            {
                CancelPreview(this, e);
            }
        }

        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        {
            Screen screen;
            var location = this.Location;
            var size = this.Size;

            if ((specified & BoundsSpecified.X) != 0)
                location.X = x;
            if ((specified & BoundsSpecified.Y) != 0)
                location.Y = y;
            if ((specified & BoundsSpecified.Width) != 0)
                size.Width = width;
            if ((specified & BoundsSpecified.Height) != 0)
                size.Height = height;
            
            screen = FindScreen(location);

            if (screen == null)
            {
                location = new Point(location.X + size.Width, location.Y + size.Height);

                screen = FindScreen(location);
            }

            if (screen == null)
                screen = Screen.PrimaryScreen;

            if (screen.WorkingArea.X > x)
            {
                x = screen.WorkingArea.X;
                specified |= BoundsSpecified.X;
            }
            if (screen.WorkingArea.Y > y)
            {
                y = screen.WorkingArea.Y;
                specified |= BoundsSpecified.Y;
            }

            base.SetBoundsCore(x, y, width, height, specified);
        }

        private static Screen FindScreen(Point location)
        {
            foreach (var screen in Screen.AllScreens)
            {
                if (screen.WorkingArea.Contains(location))
                {
                    return screen;
                }
            }

            return null;
        }

        protected void AssignMnenomics(params Control[] controls)
        {
            _fixer.AssignMnenomics(controls);
        }

        protected void AssignMnenomics(char[] seed, params Control[] controls)
        {
            _fixer.AssignMnenomics(seed, controls);
        }

        public char[] FindAssignedMnenomics()
        {
            return _fixer.FindAssignedMnenomics();
        }

        private void UpdateRenderSizeGrip()
        {
            if (_lastSizeGripStyle != SizeGripStyle)
            {
                switch (FormBorderStyle)
                {
                    case FormBorderStyle.None:
                    case FormBorderStyle.FixedSingle:
                    case FormBorderStyle.Fixed3D:
                    case FormBorderStyle.FixedDialog:
                    case FormBorderStyle.FixedToolWindow:
                        _renderSizeGrip = false;
                        break;
                    case FormBorderStyle.Sizable:
                    case FormBorderStyle.SizableToolWindow:
                        switch (SizeGripStyle)
                        {
                            case SizeGripStyle.Show:
                                _renderSizeGrip = true;
                                break;
                            case SizeGripStyle.Hide:
                                _renderSizeGrip = false;
                                break;
                            case SizeGripStyle.Auto:
                                if (Modal)
                                {
                                    _renderSizeGrip = true;
                                }
                                else
                                {
                                    _renderSizeGrip = false;
                                }
                                break;
                        }
                        break;
                }
            }
        }

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case Win32.NativeMethods.WM_NCHITTEST:
                    WmNCHitTest(ref m);
                    return;

                case Win32.NativeMethods.WM_PARENTNOTIFY:
                    switch (Win32.NativeMethods.Util.LOWORD(m.WParam))
                    {
                        case Win32.NativeMethods.WM_XBUTTONDOWN:
                            switch (Win32.NativeMethods.Util.HIWORD(m.WParam))
                            {
                                case Win32.NativeMethods.XBUTTON1:
                                    OnBrowseButtonClick(new BrowseButtonEventArgs(BrowseButton.Back));
                                    break;

                                case Win32.NativeMethods.XBUTTON2:
                                    OnBrowseButtonClick(new BrowseButtonEventArgs(BrowseButton.Forward));
                                    break;
                            }
                            break;
                    }
                    break;

                case Win32.NativeMethods.WM_APPCOMMAND:
                    int cmd = Win32.NativeMethods.Util.HIWORD(m.LParam) & ~0xf000;

                    switch (cmd)
                    {
                        case Win32.NativeMethods.APPCOMMAND_BROWSER_BACKWARD:
                            OnBrowseButtonClick(new BrowseButtonEventArgs(BrowseButton.Back));
                            m.Result = (IntPtr)1;
                            return;

                        case Win32.NativeMethods.APPCOMMAND_BROWSER_FORWARD:
                            OnBrowseButtonClick(new BrowseButtonEventArgs(BrowseButton.Forward));
                            m.Result = (IntPtr)1;
                            return;
                    }
                    break;
            }

            base.WndProc(ref m);
        }

        private void WmNCHitTest(ref Message m)
        {
            UpdateRenderSizeGrip();

            if (_renderSizeGrip)
            {
                int x = Win32.NativeMethods.Util.SignedLOWORD(m.LParam);
                int y = Win32.NativeMethods.Util.SignedHIWORD(m.LParam);

                // Convert to client coordinates
                //
                var pt = PointToClient(new Point(x, y));

                Size clientSize = ClientSize;

                // If the grip is not fully visible the grip area could overlap with the system control box; we need to disable
                // the grip area in this case not to get in the way of the control box.  We only need to check for the client's
                // height since the window width will be at least the size of the control box which is always bigger than the
                // grip width.
                if (pt.X >= (clientSize.Width - SizeGripSize) &&
                    pt.Y >= (clientSize.Height - SizeGripSize) &&
                    clientSize.Height >= SizeGripSize)
                {
                    m.Result = IsMirrored ? (IntPtr)Win32.NativeMethods.HTBOTTOMLEFT : (IntPtr)Win32.NativeMethods.HTBOTTOMRIGHT;
                    return;
                }
            }

            DefWndProc(ref m);

            // If we got any of the "edge" hits (bottom, top, topleft, etc),
            // and we're AutoSizeMode.GrowAndShrink, return non-resizable border
            // The edge values are the 8 values from HTLEFT (10) to HTBOTTOMRIGHT (17).
            if (AutoSizeMode == AutoSizeMode.GrowAndShrink)
            {
                int result = (int)m.Result;
                if (result >= Win32.NativeMethods.HTLEFT &&
                    result <= Win32.NativeMethods.HTBOTTOMRIGHT)
                {
                    m.Result = (IntPtr)Win32.NativeMethods.HTBORDER;
                }
            }
        }

        public static IntPtr ForegroundWindow
        {
            get { return Win32.NativeMethods.GetForegroundWindow(); }
            set { Win32.NativeMethods.SetForegroundWindow(value); }
        }

        private void InvalidateNonClient()
        {
            NativeMethods.SendMessage(Handle, NativeMethods.WM_NCPAINT, (IntPtr)1, (IntPtr)0);
        }

        protected virtual void OnFixControl(ControlEventArgs e)
        {
            FixControl?.Invoke(this, e);
        }
    }
}
