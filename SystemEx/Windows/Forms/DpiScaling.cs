using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace SystemEx.Windows.Forms
{
    public static class DpiScaling
    {
        private static readonly DpiScalingData scaling = new DpiScalingData();

        public static int DpiX => scaling.DpiX;
        public static int DpiY => scaling.DpiY;
        public static float ScaleX => scaling.ScaleX;
        public static float ScaleY => scaling.ScaleY;

        public static bool IsDpiScaled { get; } = DpiX != 96 || DpiY != 96;

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
            var size = Scale(new Size(image.Width, image.Height));

            return Scale(image, size);
        }

        public static Image Scale(Image image, Size size)
        {
            if (!IsDpiScaled || image.Size == size || image.Tag as string == "__DPI__SCALED__")
                return image;

            var bitmap = new Bitmap(size.Width, size.Height)
            {
                Tag = "__DPI__SCALED__"
            };

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

        private class DpiScalingData
        {
            public int DpiX { get; }
            public int DpiY { get; }
            public float ScaleX { get; }
            public float ScaleY { get; }

            public DpiScalingData()
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
