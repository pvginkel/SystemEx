using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SystemEx.Windows.Forms
{
    public partial class FormGridFooter : Panel
    {
        private const int CONTROL_SPACING = 10;
        private Color _bumpLightColor = SystemColors.ControlDark;
        private Color _bumpDarkColor = SystemColors.ControlLightLight;

        public FormGridFooter()
        {
            this.Dock = DockStyle.Bottom;
            this.Padding = this.DefaultPadding;
        }

        protected override Padding DefaultPadding
        {
            get { return new Padding(8, 8, 10, 8); }
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

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;

            g.Clear(this.BackColor);

            base.OnPaint(e);

            var lightPen = new Pen(_bumpLightColor);
            var darkPen = new Pen(_bumpDarkColor);

            g.DrawLine(lightPen, 0, 0, this.Width - 1, 0);
            g.DrawLine(darkPen, 0, 1, this.Width - 1, 1);
        }

        protected override void OnControlAdded(ControlEventArgs e)
        {
            e.Control.Anchor = AnchorStyles.None;
        }

        protected override void OnLayout(LayoutEventArgs e)
        {
            base.OnLayout(e);

            //
            // Calculate the max height
            //

            int height = 0;

            foreach (Control control in this.Controls)
            {
                if (control.Height > height)
                {
                    height = control.Height;
                }
            }

            //
            // Resize the panel to for its contents
            //

            if (height != this.Height)
            {
                this.Height = height + this.Padding.Top + this.Padding.Bottom + 2;
            }

            //
            // Calculate what x will become
            //

            int x = this.Width - this.Padding.Right;
            int y = this.Padding.Top + 2;

            bool hadOne = false;

            foreach (Control control in this.Controls)
            {
                if (hadOne)
                {
                    x -= CONTROL_SPACING;
                }
                else
                {
                    hadOne = true;
                }

                x -= control.Width;
            }

            //
            // Now reposition the controls in the correct order
            //

            foreach (Control control in this.Controls)
            {
                control.Location = new Point(x, y);

                x += control.Width + CONTROL_SPACING;
            }

            this.Refresh();
        }
    }
}
