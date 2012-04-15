using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SystemEx.Windows.Forms
{
    public class Label : System.Windows.Forms.Label
    {
        public Label()
        {
            Etched = false;
        }

        [DefaultValue(false)]
        public bool Etched { get; set; }

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            if (Etched)
            {
                TextRenderer.DrawText(e.Graphics, Text, Font, new Point(1, 1), SystemColors.Window);
                TextRenderer.DrawText(e.Graphics, Text, Font, new Point(0, 0), SystemColors.ControlDark);
            }
            else
            {
                base.OnPaint(e);
            }
        }
    }
}
