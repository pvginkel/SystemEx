using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace SystemEx.Windows.Forms
{
    public class ExplorerBarItemRenderEventArgs : EventArgs
    {
        private Graphics _graphics;
        private ExplorerBar _explorerBar;
        private ExplorerBarButton _item;

        internal ExplorerBarItemRenderEventArgs(Graphics graphics, ExplorerBar explorerBar, ExplorerBarButton item)
        {
            _graphics = graphics;
            _explorerBar = explorerBar;
            _item = item;
        }

        public Graphics Graphics
        {
            get { return _graphics; }
        }

        public ExplorerBar ExplorerBar
        {
            get { return _explorerBar; }
        }

        public ExplorerBarButton Item
        {
            get { return _item; }
        }
    }

    public delegate void ExplorerBarItemRenderEventHandler(object sender, ExplorerBarItemRenderEventArgs e);
}
