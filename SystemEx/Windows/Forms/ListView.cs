using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using SystemEx.Win32;

namespace SystemEx.Windows.Forms
{
    public class ListView : System.Windows.Forms.ListView
    {
        private bool _allowColumnResize = true;

        public ListView()
        {
            //Activate double buffering
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);

            //Enable the OnNotifyMessage event so we get a chance to filter out 
            // Windows messages before they get to the form's WndProc
            this.SetStyle(ControlStyles.EnableNotifyMessage, true);
        }

        [DefaultValue(true)]
        public bool AllowColumnResize
        {
            get { return _allowColumnResize; }
            set { _allowColumnResize = value; }
        }

        protected override void OnNotifyMessage(Message m)
        {
            //Filter out the WM_ERASEBKGND message
            if (m.Msg != NativeMethods.WM_ERASEBKGND)
            {
                base.OnNotifyMessage(m);
            }
        }
        protected override void WndProc(ref Message m)
        {
            if (!_allowColumnResize)
            {
                if (m.Msg == NativeMethods.WM_NOTIFY)
                {
                    NativeMethods.NMHDR nmhdr = (NativeMethods.NMHDR)m.GetLParam(typeof(NativeMethods.NMHDR));

                    switch (nmhdr.code)
                    {
                        case NativeMethods.HDN_BEGINTRACKA:
                        case NativeMethods.HDN_BEGINTRACKW:
                            m.Result = new IntPtr(1);
                            return;
                    }
                }
            }

            base.WndProc(ref m);
        }

        public void AutoResizeColumns()
        {
            if (this.Items.Count > 0)
            {
                AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            }
            else
            {
                AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            }
        }
    }
}
