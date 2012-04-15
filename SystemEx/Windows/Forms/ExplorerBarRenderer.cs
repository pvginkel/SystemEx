using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace SystemEx.Windows.Forms
{
    public abstract class ExplorerBarRenderer
    {
        private ExplorerBar _explorerBar;
        public event ExplorerBarRenderEventHandler RenderBackground;
        public event ExplorerBarRenderEventHandler RenderDockBackround;
        public event ExplorerBarRenderEventHandler RenderGrip;
        public event ExplorerBarItemRenderEventHandler RenderItemBackground;
        public event ExplorerBarItemRenderEventHandler RenderItemImage;
        public event ExplorerBarItemRenderEventHandler RenderItemText;

        public abstract int ButtonHeight { get; }
        public abstract int SmallButtonWidth { get; }
        public abstract int GripHeight { get; }
        public abstract Image DropDownImage { get; }

        internal void Initialize(ExplorerBar explorerBar)
        {
            _explorerBar = explorerBar;
        }

        protected virtual void OnRenderBackground(ExplorerBarRenderEventArgs e)
        {
            if (RenderBackground != null)
            {
                RenderBackground(this, e);
            }
        }

        protected virtual void OnRenderDockBackground(ExplorerBarRenderEventArgs e)
        {
            if (RenderDockBackround != null)
            {
                RenderDockBackround(this, e);
            }
        }

        protected virtual void OnRenderGrip(ExplorerBarRenderEventArgs e)
        {
            if (RenderGrip != null)
            {
                RenderGrip(this, e);
            }
        }

        protected virtual void OnRenderItemBackground(ExplorerBarItemRenderEventArgs e)
        {
            if (RenderItemBackground != null)
            {
                RenderItemBackground(this, e);
            }
        }

        protected virtual void OnRenderItemImage(ExplorerBarItemRenderEventArgs e)
        {
            if (RenderItemImage != null)
            {
                RenderItemImage(this, e);
            }
        }

        protected virtual void OnRenderItemText(ExplorerBarItemRenderEventArgs e)
        {
            if (RenderItemText != null)
            {
                RenderItemText(this, e);
            }
        }

        internal void PerformRenderBackground(Graphics g)
        {
            OnRenderBackground(new ExplorerBarRenderEventArgs(g, _explorerBar));
        }

        internal void PerformRenderDockBackground(Graphics g)
        {
            OnRenderDockBackground(new ExplorerBarRenderEventArgs(g, _explorerBar));
        }

        internal void PerformRenderGrip(Graphics g)
        {
            OnRenderGrip(new ExplorerBarRenderEventArgs(g, _explorerBar));
        }

        internal void PerformRenderItemBackground(Graphics g, ExplorerBarButton item)
        {
            OnRenderItemBackground(new ExplorerBarItemRenderEventArgs(g, _explorerBar, item));
        }

        internal void PerformRenderItemImage(Graphics g, ExplorerBarButton item)
        {
            OnRenderItemImage(new ExplorerBarItemRenderEventArgs(g, _explorerBar, item));
        }

        internal void PerformRenderItemText(Graphics g, ExplorerBarButton item)
        {
            OnRenderItemText(new ExplorerBarItemRenderEventArgs(g, _explorerBar, item));
        }
    }
}
