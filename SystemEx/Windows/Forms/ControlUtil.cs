using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using SystemEx.Win32;

namespace SystemEx.Windows.Forms
{
    public static class ControlUtil
    {
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
            if (!DpiScaling.IsDpiScaled)
                return;

            switch (control)
            {
                case ButtonBase button:
                    if (button.Image != null)
                        button.Image = DpiScaling.Scale(button.Image);
                    break;

                case PictureBox pictureBox:
                    if (pictureBox.Image != null)
                        pictureBox.Image = DpiScaling.Scale(pictureBox.Image);
                    break;

                case SplitContainer splitContainer:
                    if (splitContainer.FixedPanel == FixedPanel.Panel2)
                    {
                        if (splitContainer.Orientation == Orientation.Horizontal)
                            splitContainer.SplitterDistance = splitContainer.Height - DpiScaling.Scale(splitContainer.Height - splitContainer.SplitterDistance);
                        else
                            splitContainer.SplitterDistance = splitContainer.Width - DpiScaling.Scale(splitContainer.Width - splitContainer.SplitterDistance);
                    }
                    else
                    {
                        splitContainer.SplitterDistance = DpiScaling.Scale(splitContainer.SplitterDistance);
                    }

                    splitContainer.SplitterWidth = DpiScaling.Scale(splitContainer.SplitterWidth);
                    break;

                case System.Windows.Forms.ListView listView:
                    foreach (ColumnHeader column in listView.Columns)
                    {
                        column.Width = DpiScaling.Scale(column.Width);
                    }
                    break;
            }
        }
    }
}
