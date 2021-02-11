using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SystemEx.Windows.Forms
{
    public class ButtonEx : Button
    {
        [DefaultValue(true)]
        public override bool AutoSize { get; set; }

        public ButtonEx()
        {
            AutoSize = true;
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);

            if (AutoSize)
                SetBounds(Left, Top, 0, 0, BoundsSpecified.None);
        }

        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);

            if (AutoSize)
                SetBounds(Left, Top, 0, 0, BoundsSpecified.None);
        }

        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        {
            if (AutoSize)
            {
                var size = GetPreferredSize(new Size(0, 0));

                base.SetBoundsCore(x, y, size.Width, size.Height, specified);
            }
            else
            {
                base.SetBoundsCore(x, y, width, height, specified);
            }
        }

        public override Size GetPreferredSize(Size proposedSize)
        {
            if (!AutoSize)
                return base.GetPreferredSize(proposedSize);

            int minWidth;
            int minHeight;

            using (var g = CreateGraphics())
            {
                minWidth = (int)((g.DpiX / 96) * 75);
                minHeight = (int)((g.DpiY / 96) * 23);
            }

            var size = base.GetPreferredSize(new Size(0, 0));

            return new Size(
                Math.Max(minWidth, size.Width),
                Math.Max(minHeight, size.Height)
            );
        }
    }
}
