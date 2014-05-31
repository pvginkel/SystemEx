using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using SystemEx.Win32;

namespace SystemEx.Windows.Forms
{
    public static class ProgressBarUtil
    {
        public static void SetState(ProgressBar self, ProgressBarState state)
        {
            NativeMethods.SendMessage(self.Handle, NativeMethods.PBM_SETSTATE, (IntPtr)state, IntPtr.Zero);
        }
    }
}
