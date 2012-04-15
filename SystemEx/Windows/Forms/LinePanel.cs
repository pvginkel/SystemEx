using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;

namespace SystemEx.Windows.Forms
{
    public class LinePanel : Panel
    {
        private LineDirection _direction = LineDirection.Vertical;

        [Category("Layout")]
        [Browsable(true)]
        [DefaultValue(typeof(LineDirection), "Vertical")]
        public LineDirection Direction
        {
            get { return _direction; }
            set { _direction = value; }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.Clear(this.BackColor);

            base.OnPaint(e);

            if (_direction == LineDirection.Vertical)
            {
                int offset = this.Padding.Left + (int)Math.Floor((double)(this.Width - this.Padding.Horizontal - 2) / 2.0);

                e.Graphics.DrawLine(
                    SystemPens.ControlDark,
                    new Point(offset, this.Padding.Top),
                    new Point(offset, this.Height - this.Padding.Bottom)
                );

                e.Graphics.DrawLine(
                    SystemPens.ControlLightLight,
                    new Point(offset + 1, this.Padding.Top),
                    new Point(offset + 1, this.Height - this.Padding.Bottom)
                );
            }
            else
            {
                int offset = this.Padding.Top + (int)Math.Floor((double)(this.Height - this.Padding.Vertical - 2) / 2);

                e.Graphics.DrawLine(
                    SystemPens.ControlDark,
                    new Point(this.Padding.Left, offset),
                    new Point(this.Width - this.Padding.Right, offset)
                );

                e.Graphics.DrawLine(
                    SystemPens.Window,
                    new Point(this.Padding.Left, offset + 1),
                    new Point(this.Width - this.Padding.Right, offset + 1)
                );
            }
        }
    }

    public enum LineDirection
    {
        Horizontal,
        Vertical
    }
}
