using System;
using System.Collections.Generic;
using System.Text;

namespace SystemEx.Windows.Forms
{
    public class CancelPreviewEventArgs
    {
        private bool _handled = false;

        public CancelPreviewEventArgs()
        {
        }

        public bool Handled
        {
            get { return _handled; }
            set { _handled = value; }
        }
    }

    public delegate void CancelPreviewEventHandler(object sender, CancelPreviewEventArgs e);
}
