using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;
using SystemEx.Win32;

namespace SystemEx.Windows.Forms
{
    public class ToolStrip : System.Windows.Forms.ToolStrip
    {
        [DefaultValue(false)]
        public bool ClickThrough { get; set; }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (
                ClickThrough &&
                m.Msg == NativeMethods.WM_MOUSEACTIVATE &&
                m.Result == (IntPtr)NativeMethods.MA_ACTIVATEANDEAT
            )
                m.Result = (IntPtr)NativeMethods.MA_ACTIVATE;
        }
    }
}
