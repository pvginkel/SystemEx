using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;
using SystemEx.Win32;
using SystemEx.Windows.Forms.Internal;

namespace SystemEx.Windows.Forms
{
    public class DockContent : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        private FormHelper _fixer;
        private bool _disposed;

        public DockContent()
        {
            _fixer = new FormHelper(this);
            _fixer.EnableBoundsTracking = false;
        }

        public event BrowseButtonEventHandler BrowseButtonClick;

        protected virtual void OnBrowseButtonClick(BrowseButtonEventArgs e)
        {
            var ev = BrowseButtonClick;

            if (ev != null)
                ev(this, e);
        }

        protected string UserSettingsKey
        {
            get { return _fixer.KeyAddition; }
            set { _fixer.KeyAddition = value; }
        }

        protected override void SetVisibleCore(bool value)
        {
            _fixer.InitializeForm();

            base.SetVisibleCore(value);
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

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case NativeMethods.WM_PARENTNOTIFY:
                    switch (NativeMethods.Util.LOWORD(m.WParam))
                    {
                        case NativeMethods.WM_XBUTTONDOWN:
                            switch (NativeMethods.Util.HIWORD(m.WParam))
                            {
                                case NativeMethods.XBUTTON1:
                                    OnBrowseButtonClick(new BrowseButtonEventArgs(BrowseButton.Back));
                                    break;

                                case NativeMethods.XBUTTON2:
                                    OnBrowseButtonClick(new BrowseButtonEventArgs(BrowseButton.Forward));
                                    break;
                            }
                            break;
                    }
                    break;

                case NativeMethods.WM_APPCOMMAND:
                    int cmd = NativeMethods.Util.HIWORD(m.LParam) & ~0xf000;

                    switch (cmd)
                    {
                        case NativeMethods.APPCOMMAND_BROWSER_BACKWARD:
                            OnBrowseButtonClick(new BrowseButtonEventArgs(BrowseButton.Back));
                            m.Result = (IntPtr)1;
                            return;

                        case NativeMethods.APPCOMMAND_BROWSER_FORWARD:
                            OnBrowseButtonClick(new BrowseButtonEventArgs(BrowseButton.Forward));
                            m.Result = (IntPtr)1;
                            return;
                    }
                    break;
            }

            base.WndProc(ref m);
        }
    }
}
