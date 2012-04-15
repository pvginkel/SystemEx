using System;
using System.Collections.Generic;
using System.Text;
using WeifenLuo.WinFormsUI.Docking;
using System.ComponentModel;
using System.Drawing;

namespace SystemEx.Windows.Forms
{
    public class DockPanel : WeifenLuo.WinFormsUI.Docking.DockPanel
    {
        public DockPanel()
        {
            this.DockBackColor = SystemColors.AppWorkspace;
/*
            this.Extender.AutoHideStripFactory = new VS2008AutoHideStrip.Factory();
            this.Extender.DockPaneCaptionFactory = new VS2008DockPaneCaption.Factory();
            this.Extender.DockPaneStripFactory = new VS2008DockPaneStrip.Factory();
*/
        }

        public bool DockContentTypeOpened(Type type)
        {
            foreach (var pane in this.Panes)
            {
                foreach (var content in pane.Contents)
                {
                    if (content.GetType() == type)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public void ActivateNextTab(bool forward)
        {
            var documents = GetDocuments();

            if (documents.Count == 0)
                return;

            int activeDocumentIndex = documents.IndexOf(this.ActiveDocument);

            if (activeDocumentIndex == -1)
                activeDocumentIndex = 0;
            else
            {
                activeDocumentIndex += (forward ? 1 : -1);

                if (activeDocumentIndex < 0)
                    activeDocumentIndex = documents.Count - 1;
                if (activeDocumentIndex >= documents.Count)
                    activeDocumentIndex = 0;
            }

            (documents[activeDocumentIndex] as WeifenLuo.WinFormsUI.Docking.DockContent).Show(this);
        }

        public List<IDockContent> GetDocuments()
        {
            var documents = new List<IDockContent>();

            foreach (var pane in this.Panes)
            {
                foreach (var document in pane.Contents)
                {
                    if (document.DockHandler.DockState == DockState.Document)
                    {
                        documents.Add(document);
                    }
                }
            }
            return documents;
        }

        [Category("Appearance")]
        [Browsable(true)]
        [DefaultValue(typeof(Color), "AppWorkspace")]
        public new Color DockBackColor
        {
            get { return base.DockBackColor; }
            set { base.DockBackColor = value; }
        }
    }
}
