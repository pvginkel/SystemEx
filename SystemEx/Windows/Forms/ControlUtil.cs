using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using SystemEx.Win32;

namespace SystemEx.Windows.Forms
{
    public class ControlUtil
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
    }
}
