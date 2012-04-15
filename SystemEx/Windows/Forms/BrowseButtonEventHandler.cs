using System;
using System.Collections.Generic;
using System.Text;

namespace SystemEx.Windows.Forms
{
    public class BrowseButtonEventArgs : EventArgs
    {
        public BrowseButtonEventArgs(BrowseButton button)
        {
            Button = button;
        }

        public BrowseButton Button { get; private set; }
    }

    public delegate void BrowseButtonEventHandler(object sender, BrowseButtonEventArgs e);
}
