using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SystemEx.Windows.Forms
{
    [Obsolete("Use FormFlowFooter")]
    public partial class FormFooter : Panel
    {
        private Color _bumpLightColor = SystemColors.ControlDark;
        private Color _bumpDarkColor = SystemColors.ControlLightLight;

        public FormFooter()
        {
            this.Dock = DockStyle.Bottom;
            this.Padding = this.DefaultPadding;
            this.AutoSize = true;
        }

        protected override Padding DefaultPadding
        {
            get { return new Padding(8, 10, 8, 8); }
        }

        [Category("Layout")]
        [Browsable(true)]
        [DefaultValue(true)]
        public override bool AutoSize
        {
            get { return base.AutoSize; }
            set { base.AutoSize = value; }
        }

        [Category("Layout")]
        [Browsable(true)]
        [DefaultValue(typeof(DockStyle), "Bottom")]
        public override DockStyle Dock
        {
            get { return base.Dock; }
            set { base.Dock = value; }
        }

        [Category("Appearance")]
        [Browsable(true)]
        [DefaultValue(typeof(Color), "ControlDark")]
        public Color BumpLightColor
        {
            get { return _bumpLightColor; }
            set
            {
                _bumpLightColor = value;
                Refresh();
            }
        }

        [Category("Appearance")]
        [Browsable(true)]
        [DefaultValue(typeof(Color), "ControlLightLight")]
        public Color BumpDarkColor
        {
            get { return _bumpDarkColor; }
            set
            {
                _bumpDarkColor = value;
                Refresh();
            }
        }

        protected override void OnLayout(LayoutEventArgs levent)
        {
            base.OnLayout(levent);

            // Repaint when the layout has changed

            this.Refresh();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            try
            {
                var g = e.Graphics;

                g.Clear(this.BackColor);

                base.OnPaint(e);

                var lightPen = new Pen(_bumpLightColor);
                var darkPen = new Pen(_bumpDarkColor);

                g.DrawLine(lightPen, 0, 0, this.Width - 1, 0);
                g.DrawLine(darkPen, 0, 1, this.Width - 1, 1);
            }
            catch
            {
                // Suppress all exceptions coming from GDI actions.
            }
        }
    }
}
