// #define USE_TAB_RENDERER

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms.VisualStyles;
using System.Windows.Forms;
using System.Drawing;
using SystemEx.Win32;
using System.ComponentModel;

namespace SystemEx.Windows.Forms
{
    public class ThemedPanel : Panel
    {
#if USE_TAB_RENDERER
        private VisualStyleRenderer _tabRenderer;
#endif
        private Color _borderColor;
        private static bool _visualStyles;

        static ThemedPanel()
        {
            _visualStyles = Application.RenderWithVisualStyles;
        }

        public ThemedPanel()
        {
            SetStyle(ControlStyles.ResizeRedraw, true);

            this.BackColor = SystemColors.Window;

            UpdateThemeData();
        }

        private void UpdateThemeData()
        {
            if (!ControlUtil.GetIsInDesignMode(this) && _visualStyles)
            {
#if USE_TAB_RENDERER
                _tabRenderer = new VisualStyleRenderer(VisualStyleElement.Tab.Body.Normal);
#endif
                _borderColor = new VisualStyleRenderer("ListView", 0, 0).GetColor(ColorProperty.BorderColor);

                if (_borderColor == Color.Black)
                {
                    _borderColor = SystemColors.ControlDark;
                }
            }
            else
            {
#if USE_TAB_RENDERER
                _tabRenderer = null;
#endif
                _borderColor = SystemColors.ControlDark;
            }
        }

#if USE_TAB_RENDERER
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            if (_tabRenderer != null)
            {
                try
                {
                    _tabRenderer.DrawBackground(e.Graphics, this.ClientRectangle);
                }
                catch
                {
                    // Suppress all exceptions coming from GDI actions.
                }
                return;
            }
            
            base.OnPaintBackground(e);
        }
#endif

        protected override void WndProc(ref Message m)
        {
            if (
                m.Msg == NativeMethods.WM_NCPAINT &&
                this.BorderStyle == BorderStyle.FixedSingle &&
                _visualStyles
            )
                DrawNCBorder(ref m);
            else
                base.WndProc(ref m);
        }

        private void DrawNCBorder(ref Message m)
        {
            IntPtr hDC = IntPtr.Zero;
            Graphics g = null;

            try
            {
                hDC = NativeMethods.GetDCEx(
                    Handle,
                    m.WParam == (IntPtr)1 ? IntPtr.Zero : m.WParam,
                    NativeMethods.DeviceContextValues.Window | NativeMethods.DeviceContextValues.ParentClip
                );

                if (hDC != IntPtr.Zero)
                {
                    g = Graphics.FromHdc(hDC);

                    var bounds = new Rectangle(0, 0, this.Width, this.Height);

                    ControlPaint.DrawBorder(g, bounds, _borderColor, ButtonBorderStyle.Solid);
                }
            }
            catch
            {
                // Suppress all exceptions coming from GDI actions.
            }
            finally
            {
                if (g != null)
                    g.Dispose();

                if (hDC != IntPtr.Zero)
                    NativeMethods.ReleaseDC(Handle, hDC);
            }
        }

        [Category("Appearance")]
        [Browsable(true)]
        [DefaultValue(typeof(Color), "Window")]
        public override Color BackColor
        {
            get { return base.BackColor; }
            set
            {
                base.BackColor = value;

                Invalidate();
            }
        }
    }
}
