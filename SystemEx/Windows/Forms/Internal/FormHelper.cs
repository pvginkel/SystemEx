using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Collections;

namespace SystemEx.Windows.Forms.Internal
{
    public class FormHelper
    {
        private const string RegistryBaseStr = "Software\\SystemEx\\Interface";

        public static string GlobalKeyAddition { get; set; }

        private readonly System.Windows.Forms.Form _form;
        private Point _normalLocation;
        private Size _normalSize;
        private List<PropertyTracker> _propertyTrackers;
        private bool _initializeFormCalled;
        private readonly string _defaultFontName;
        private readonly string _correctFontName;
        private readonly float _correctFontSize;
        private readonly float _defaultFontSize;
        private int? _mainMenuHeight;

        public bool InDesignMode { get; private set; }

        public bool EnableBoundsTracking { get; set; }

        public event ControlEventHandler FixControl;

        public FormHelper(Control control)
        {
            _form = control as System.Windows.Forms.Form;

            InDesignMode = ControlUtil.GetIsInDesignMode(control);

            if (!InDesignMode)
            {
                _defaultFontName = control.Font.Name;
                _defaultFontSize = control.Font.Size;

                if (_form != null)
                    _form.AutoScaleMode = AutoScaleMode.None;

                control.Font = SystemFonts.MessageBoxFont;

                _correctFontName = control.Font.Name;
                _correctFontSize = control.Font.Size;
            }
        }

        public void InitializeForm()
        {
            if (!InDesignMode && !_initializeFormCalled)
            {
                FixControls();

                RestoreUserSettings();

                _initializeFormCalled = true;
            }
        }

        private void FixControls()
        {
            FixControls(_form);
        }

        private void FixControls(Control control)
        {
            FixFont(control);

            OnFixControl(new ControlEventArgs(control));

            foreach (Control child in control.Controls)
            {
                FixControls(child);
            }
        }

        private void FixFont(Control control)
        {
            string fontName = control.Font.Name;

            if (fontName != _correctFontName)
            {
                float fontSize = control.Font.Size;

                if (fontName == _defaultFontName)
                    fontName = _correctFontName;
                if (fontSize == _defaultFontSize)
                    fontSize = _correctFontSize;

                control.Font = new Font(
                    fontName,
                    fontSize,
                    control.Font.Style,
                    control.Font.Unit,
                    control.Font.GdiCharSet);
            }
        }

        public string KeyAddition { get; set; }

        public void StoreUserSettings()
        {
            RegistryKey key = null;

            if (EnableBoundsTracking && _form.FormBorderStyle != FormBorderStyle.None)
            {
                key = FormKey;

                StoreDWord(key, "WindowState", (int)_form.WindowState);
                StorePoint(key, "Location", _normalLocation);

                var size = _normalSize;

                if (_form.Menu != null)
                {
                    size = new Size(
                        size.Width,
                        size.Height - SystemInformation.MenuHeight
                    );
                }

                StoreSize(key, "Size", size);
            }

            // Track all properties

            if (_propertyTrackers != null)
            {
                foreach (var item in _propertyTrackers)
                {
                    if (key == null)
                    {
                        key = FormKey;
                    }

                    item.Store(key);
                }
            }
        }

        public bool RestoreUserSettings()
        {
            RegistryKey key = null;
            bool restored = FormKeyExists;

            if (EnableBoundsTracking && _form.FormBorderStyle != FormBorderStyle.None)
            {
                key = FormKey;

                switch (_form.StartPosition)
                {
                    case FormStartPosition.CenterParent:
                    case FormStartPosition.CenterScreen:
                        // Do not restore the location
                        break;

                    default:
                        Point location = _form.Location;

                        if (RestorePoint(key, "Location", ref location))
                        {
                            _form.Location = location;
                            _form.StartPosition = FormStartPosition.Manual;
                        }
                        break;
                }

                switch (_form.FormBorderStyle)
                {
                    case FormBorderStyle.None:
                    case FormBorderStyle.FixedSingle:
                    case FormBorderStyle.Fixed3D:
                    case FormBorderStyle.FixedDialog:
                    case FormBorderStyle.FixedToolWindow:
                        // Do not restore the size
                        break;

                    default:
                        Size size = _form.Size;

                        if (RestoreSize(key, "Size", ref size))
                        {
                            if (_form.Menu != null)
                            {
                                // When we store the height, the MainMenu
                                // has already been removed, so this height
                                // won't include the height of the menu.
                                // We add it back here.

                                size = new Size(
                                    size.Width,
                                    size.Height + SystemInformation.MenuHeight
                                );
                            }

                            _form.Size = size;
                        }
                        break;
                }

                int windowState = (int)_form.WindowState;

                if (RestoreDWord(key, "WindowState", ref windowState))
                {
                    switch ((FormWindowState)windowState)
                    {
                        case FormWindowState.Minimized:
                            if (_form.MinimizeBox)
                                _form.WindowState = FormWindowState.Minimized;
                            break;

                        case FormWindowState.Maximized:
                            if (_form.MaximizeBox)
                                _form.WindowState = FormWindowState.Maximized;
                            break;

                        case FormWindowState.Normal:
                            _form.WindowState = FormWindowState.Normal;
                            break;
                    }
                }
            }

            // Restore all properties

            if (_propertyTrackers != null)
            {
                foreach (var item in _propertyTrackers)
                {
                    if (key == null)
                    {
                        key = FormKey;
                    }

                    item.Restore(key);
                }
            }

            if (key != null)
                ((IDisposable)key).Dispose();

            return restored;
        }

        private void StoreSize(RegistryKey key, string name, Size value)
        {
            key.SetValue(name, String.Format("{0}x{1}", value.Width, value.Height));
        }

        private bool RestoreSize(RegistryKey key, string name, ref Size value)
        {
            object data = key.GetValue(name);

            if (data is string)
            {
                string[] parts = (data as string).Split(new[] { 'x' });

                if (parts.Length == 2)
                {
                    value.Width = int.Parse(parts[0]);
                    value.Height = int.Parse(parts[1]);

                    return true;
                }
            }

            return false;
        }

        private void StorePoint(RegistryKey key, string name, Point value)
        {
            key.SetValue(name, String.Format("{0}x{1}", value.X, value.Y));
        }

        private bool RestorePoint(RegistryKey key, string name, ref Point value)
        {
            object data = key.GetValue(name);

            if (data is string)
            {
                string[] parts = (data as string).Split(new[] { 'x' });

                if (parts.Length == 2)
                {
                    value.X = int.Parse(parts[0]);
                    value.Y = int.Parse(parts[1]);

                    return true;
                }
            }

            return false;
        }

        private void StoreDWord(RegistryKey key, string name, int value)
        {
            key.SetValue(name, value);
        }

        private bool RestoreDWord(RegistryKey key, string name, ref int value)
        {
            object data = key.GetValue(name);

            if (data is int)
            {
                value = (int)data;

                return true;
            }

            return false;
        }

        private RegistryKey FormKey
        {
            get
            {
                return UserSettingsKey.CreateSubKey(FormKeyName);
            }
        }

        private string FormKeyName
        {
            get
            {
                string key = _form.GetType().FullName;

                if (KeyAddition != null)
                    key += "$" + KeyAddition;

                return key;
            }
        }

        private bool FormKeyExists
        {
            get
            {
                using (var key = UserSettingsKey.OpenSubKey(FormKeyName))
                {
                    return key != null;
                }
            }
        }

        private RegistryKey UserSettingsKey
        {
            get
            {
                string key = RegistryBaseStr;

                if (GlobalKeyAddition != null)
                    key += "\\" + GlobalKeyAddition;

                key += "\\" + GetScreenDimensionsKey();

                return Registry.CurrentUser.CreateSubKey(key);
            }
        }

        private string GetScreenDimensionsKey()
        {
            var sb = new StringBuilder();

            sb.Append(SerializeScreen(Screen.PrimaryScreen));

            foreach (var screen in Screen.AllScreens)
            {
                if (!screen.Primary)
                {
                    sb.Append("+");

                    sb.Append(SerializeScreen(screen));
                }
            }

            return sb.ToString();
        }

        private string SerializeScreen(Screen screen)
        {
            var bounds = screen.Bounds;

            return String.Format("{0}x{1}-{2}x{3}",
                    bounds.Left, bounds.Top, bounds.Width, bounds.Height);
        }

        public void OnSizeChanged(EventArgs e)
        {
            if (!InDesignMode)
            {
                if (_form.WindowState == FormWindowState.Normal)
                    _normalSize = GetNormalSize();
            }
        }

        private Size GetNormalSize()
        {
            var size = _form.Size;

            if (_form.Menu != null && !_mainMenuHeight.HasValue)
            {
                // We need to subtract the height of the main menu because
                // when the main menu is made visible after the size has been
                // restored.

                Win32.NativeMethods.RECT rect;

                Win32.NativeMethods.GetMenuItemRect(
                    _form.Handle,
                    _form.Menu.Handle,
                    0,
                    out rect
                );

                // We need to store the main menu height because when disposing
                // the form, the main menu is removed and becomes Null.
                // Once we've seen a main menu, we always include it in the
                // calculations.

                _mainMenuHeight = rect.bottom - rect.top + 1;
            }

            size = new Size(
                size.Width,
                size.Height - _mainMenuHeight.GetValueOrDefault()
            );

            return size;
        }

        public void OnLocationChanged(EventArgs e)
        {
            if (!InDesignMode)
            {
                if (_form.WindowState == FormWindowState.Normal)
                {
                    _normalLocation = _form.Location;
                }
            }
        }

        public void CenterOverParent(double relativeSize)
        {
            if (!InDesignMode)
            {
                if (_form.Owner != null)
                {
                    _form.WindowState = FormWindowState.Normal;

                    int width = _form.Owner.Width;
                    int height = _form.Owner.Height;
                    double relativeLocation = (1.0 - relativeSize) / 2.0;

                    _form.Location = new Point(
                        _form.Owner.Location.X + (int)(width * relativeLocation),
                        _form.Owner.Location.Y + (int)(height * relativeLocation)
                    );

                    _form.Size = new Size(
                        (int)(width * relativeSize),
                        (int)(height * relativeSize)
                    );
                }
            }
        }

        public void TrackProperty(Control control, string property)
        {
            if (_propertyTrackers == null)
            {
                _propertyTrackers = new List<PropertyTracker>();
            }

            _propertyTrackers.Add(new PropertyTracker(control, property));
        }

        private class PropertyTracker
        {
            private readonly Control _control;
            private readonly string _property;

            public PropertyTracker(Control control, string property)
            {
                _control = control;
                _property = property;
            }

            public void Store(RegistryKey key)
            {
                var controlKey = key.CreateSubKey("Controls\\" + _control.Name);

                var property = _control.GetType().GetProperty(_property);

                controlKey.SetValue(_property, property.GetValue(_control, null).ToString());
            }

            public void Restore(RegistryKey key)
            {
                var controlKey = key.OpenSubKey("Controls\\" + _control.Name);

                if (controlKey != null)
                {
                    object value = controlKey.GetValue(_property);

                    if (value != null)
                    {
                        var property = _control.GetType().GetProperty(_property);

                        try
                        {
                            property.SetValue(
                                _control,
                                Convert.ChangeType(value, property.PropertyType),
                                null);
                        }
                        catch
                        {
                            // The store value could be illegal. In this case,
                            // we just ignore the exception.
                        }
                    }
                }
            }
        }

        public void AssignMnenomics(Control[] controls)
        {
            AssignMnenomics(null, controls);
        }

        public void AssignMnenomics(char[] seed, Control[] controls)
        {
            var assigned = (seed == null ? new Dictionary<char, bool>() : CreateAssigned(seed));

            FindAssigned(assigned, _form.Controls);
            FindMainMenuAssigned(assigned, _form.Menu);

            PerformAssignMnenomics(assigned, controls);
        }

        private static Dictionary<char, bool> CreateAssigned(IEnumerable<char> seed)
        {
            var result = new Dictionary<char, bool>();

            foreach (char c in seed)
            {
                result[c] = true;
            }

            return result;
        }

        private void PerformAssignMnenomics(Dictionary<char, bool> assigned, IEnumerable controls)
        {
            foreach (Control control in controls)
            {
                // Do not recurse into DockContent forms.

                if (control is System.Windows.Forms.Form)
                    continue;

                var label = control as System.Windows.Forms.Label;

                if (label != null)
                {
                    string text = label.Text.TrimEnd().ToLowerInvariant();

                    if (text.EndsWith(":") && !text.Contains("&"))
                    {
                        bool hadSpace = true;
                        int pos = -1;
                        char c = (char)0;

                        // First assign by start of words.

                        for (int i = 0; i < text.Length; i++)
                        {
                            c = text[i];

                            if (Char.IsLetterOrDigit(c))
                            {
                                if (hadSpace && !assigned.ContainsKey(c))
                                {
                                    pos = i;
                                    break;
                                }

                                hadSpace = false;
                            }
                            else
                            {
                                hadSpace = true;
                            }
                        }

                        // Else, just the first that isn't assigned.

                        if (pos == -1)
                        {
                            for (int i = 0; i < text.Length; i++)
                            {
                                c = text[i];

                                if (Char.IsLetterOrDigit(c) && !assigned.ContainsKey(c))
                                {
                                    pos = i;
                                    break;
                                }
                            }
                        }

                        // Alter the label.

                        if (pos != -1)
                        {
                            assigned[c] = true;

                            label.Text = label.Text.Substring(0, pos) + "&" + label.Text.Substring(pos);
                        }
                    }
                }

                PerformAssignMnenomics(assigned, control.Controls);
            }
        }

        private void FindAssigned(Dictionary<char, bool> assigned, IEnumerable controls)
        {
            foreach (Control control in controls)
            {
                // Do not recurse into DockContent forms.

                if (control is System.Windows.Forms.Form)
                    continue;

                string text = null;

                if (control is System.Windows.Forms.Label)
                {
                    text = ((System.Windows.Forms.Label)control).Text;

                    if (!text.EndsWith(":"))
                        text = null;
                }
                else if (control is System.Windows.Forms.Button)
                {
                    text = ((System.Windows.Forms.Button)control).Text;
                }
                else if (control is System.Windows.Forms.ToolStrip)
                {
                    FindToolStripAssigned(assigned, (System.Windows.Forms.ToolStrip)control);
                }

                if (text != null)
                    ExtractAssignedMnenomic(assigned, text);

                FindAssigned(assigned, control.Controls);
            }
        }

        private void FindToolStripAssigned(Dictionary<char, bool> assigned, System.Windows.Forms.ToolStrip toolStrip)
        {
            foreach (ToolStripItem item in toolStrip.Items)
            {
                string text = null;

                if (item is System.Windows.Forms.ToolStripDropDownItem)
                {
                    text = ((System.Windows.Forms.ToolStripDropDownItem)item).Text;
                }
                else if (item is System.Windows.Forms.ToolStripButton)
                {
                    text = ((System.Windows.Forms.ToolStripButton)item).Text;
                }

                if (text != null)
                    ExtractAssignedMnenomic(assigned, text);
            }
        }

        private void FindMainMenuAssigned(Dictionary<char, bool> assigned, MainMenu mainMenu)
        {
            if (mainMenu == null)
                return;

            foreach (MenuItem item in mainMenu.MenuItems)
            {
                ExtractAssignedMnenomic(assigned, item.Text);
            }
        }

        private void ExtractAssignedMnenomic(Dictionary<char, bool> assigned, string text)
        {
            text = text.TrimEnd();

            int pos = text.IndexOf('&');

            if (pos >= 0 && pos < text.Length - 1)
            {
                char mnenomic = text.Substring(pos + 1, 1).ToLowerInvariant()[0];

                if (!assigned.ContainsKey(mnenomic))
                    assigned[mnenomic] = true;
            }
        }

        public char[] FindAssignedMnenomics()
        {
            var assigned = new Dictionary<char, bool>();

            FindAssigned(assigned, _form.Controls);
            FindMainMenuAssigned(assigned, _form.Menu);

            char[] result = new char[assigned.Count];
            int i = 0;

            foreach (char c in assigned.Keys)
            {
                result[i++] = c;
            }

            return result;
        }

        protected virtual void OnFixControl(ControlEventArgs e)
        {
            FixControl?.Invoke(this, e);
        }
    }
}
