using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace SystemEx.Windows.Forms
{
    public class ExplorerBarRenderEventArgs : EventArgs
    {
        private Graphics _graphics;
        private ExplorerBar _explorerBar;

        internal ExplorerBarRenderEventArgs(Graphics graphics, ExplorerBar explorerBar)
        {
            _graphics = graphics;
            _explorerBar = explorerBar;
        }

        public Graphics Graphics
        {
            get { return _graphics; }
        }

        public ExplorerBar ExplorerBar
        {
            get { return _explorerBar; }
        }
    }

    public delegate void ExplorerBarRenderEventHandler(object sender, ExplorerBarRenderEventArgs e);
}
