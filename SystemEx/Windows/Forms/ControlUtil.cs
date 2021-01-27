using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using SystemEx.Win32;

namespace SystemEx.Windows.Forms
{
    public static class ControlUtil
    {
        private static readonly DpiScaling scaling = new DpiScaling();

        public static int DpiX => scaling.DpiX;
        public static int DpiY => scaling.DpiY;
        public static float ScaleX => scaling.ScaleX;
        public static float ScaleY => scaling.ScaleY;

        public static bool IsDpiScaled { get; } = DpiX != 96 || DpiY != 96;

        public static bool GetIsInDesignMode(Control control)
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                return true;

            for (; control != null; control = control.Parent)
            {
                if (control.Site != null && control.Site.DesignMode)
                    return true;
            }

            return false;
        }

        public static Padding GetNonClientSize(Control control)
        {
            var memory = IntPtr.Zero;

            try
            {
                var rect = new NativeMethods.RECT
                {
                    left = 0,
                    top = 0,
                    right = control.Width,
                    bottom = control.Height
                };

                memory = Marshal.AllocHGlobal(Marshal.SizeOf(rect));

                Marshal.StructureToPtr(rect, memory, false);

                NativeMethods.SendMessage(control.Handle, NativeMethods.WM_NCCALCSIZE, IntPtr.Zero, memory);

                rect = (NativeMethods.RECT)Marshal.PtrToStructure(memory, typeof(NativeMethods.RECT));

                return new Padding(
                    rect.left,
                    rect.top,
                    control.Width - rect.right,
                    control.Height - rect.bottom
                );
            }
            finally
            {
                if (memory != IntPtr.Zero)
                    Marshal.FreeHGlobal(memory);
            }
        }

        public static void FixControlScaling(Control control)
        {
            if (!IsDpiScaled)
                return;

            switch (control)
            {
                case ButtonBase button:
                    if (button.Image != null)
                    {
                        button.Image = Scale(button.Image);
                        button.Padding = Scale(button.Padding);
                    }
                    break;

                case PictureBox pictureBox:
                    if (pictureBox.Image != null)
                        pictureBox.Image = Scale(pictureBox.Image);

                    break;

                case TabControl tabControl:
                    tabControl.Padding = Scale(tabControl.Padding);
                    break;

                case SplitContainer splitContainer:
                    splitContainer.SplitterWidth = Scale(splitContainer.SplitterWidth);
                    break;

                case TextBoxBase textBox when textBox.Margin == new Padding(12):
                    // Work around a bug in WinForms where the control's margin gets scaled beyond expectations
                    // see https://github.com/gitextensions/gitextensions/issues/5098
                    textBox.Margin = Scale(new Padding(3));
                    break;

                case UpDownBase upDown when upDown.Margin == new Padding(96):
                    // Work around a bug in WinForms where the control's margin gets scaled beyond expectations
                    // see https://github.com/gitextensions/gitextensions/issues/5098
                    upDown.Margin = Scale(new Padding(3));
                    break;

                case Panel panel:
                    panel.Padding = Scale(panel.Padding);
                    break;
            }
        }

        public static Size Scale(Size size)
        {
            return new Size(
                (int)(size.Width * ScaleX),
                (int)(size.Height * ScaleY)
            );
        }

        public static int Scale(int i)
        {
            return (int)Math.Round(i * ScaleX);
        }

        public static float Scale(float i)
        {
            return (float)Math.Round(i * ScaleX);
        }

        public static Point Scale(Point point)
        {
            return new Point(
                (int)(point.X * ScaleX),
                (int)(point.Y * ScaleY)
            );
        }

        public static Padding Scale(Padding padding)
        {
            return new Padding(
                (int)(padding.Left * ScaleX),
                (int)(padding.Top * ScaleY),
                (int)(padding.Right * ScaleX),
                (int)(padding.Bottom * ScaleY)
            );
        }

        public static Image Scale(Image image)
        {
            if (!IsDpiScaled)
                return image;

            var size = Scale(new Size(image.Width, image.Height));
            var bitmap = new Bitmap(size.Width, size.Height);

            using (var g = Graphics.FromImage(bitmap))
            {
                // NearestNeighbor is better for 200% and above
                // http://blogs.msdn.com/b/visualstudio/archive/2014/03/19/improving-high-dpi-support-for-visual-studio-2013.aspx

                g.InterpolationMode = ScaleX >= 2
                    ? InterpolationMode.NearestNeighbor
                    : InterpolationMode.HighQualityBicubic;

                g.DrawImage(image, new Rectangle(Point.Empty, size));
            }

            return bitmap;
        }

        private class DpiScaling
        {
            public int DpiX { get; }
            public int DpiY { get; }
            public float ScaleX { get; }
            public float ScaleY { get; }

            public DpiScaling()
            {
                var hdc = GetDC(IntPtr.Zero);
                try
                {
                    const int LOGPIXELSX = 88;
                    const int LOGPIXELSY = 90;

                    DpiX = GetDeviceCaps(hdc, LOGPIXELSX);
                    DpiY = GetDeviceCaps(hdc, LOGPIXELSY);

                    ScaleX = DpiX / 96.0f;
                    ScaleY = DpiY / 96.0f;
                }
                catch
                {
                    DpiX = 96;
                    DpiY = 96;

                    ScaleX = 1.0f;
                    ScaleY = 1.0f;
                }
                finally
                {
                    ReleaseDC(IntPtr.Zero, hdc);
                }
            }

            [DllImport("gdi32.dll")]
            private static extern int GetDeviceCaps(IntPtr hdc, int index);

            [DllImport("user32.dll")]
            private static extern IntPtr GetDC(IntPtr hwnd);

            [DllImport("user32.dll")]
            private static extern int ReleaseDC(IntPtr hwnd, IntPtr deviceContextHandle);
        }
    }
}
