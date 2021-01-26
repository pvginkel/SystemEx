using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
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
                Size = Size.Empty;
        }

        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);

            if (AutoSize)
                Size = Size.Empty;
        }

        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        {
            if (AutoSize)
            {
                var size = GetPreferredSize(new Size(0, 0));

                base.SetBoundsCore(x, y, size.Width, size.Height, specified | BoundsSpecified.Width | BoundsSpecified.Height);
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

            using (var g = CreateGraphics())
            {
                minWidth = (int)((g.DpiX / 96) * 75);
            }

            var size = base.GetPreferredSize(new Size(0, 0));

            return new Size(
                Math.Max(minWidth, size.Width),
                size.Height
            );
        }
    }
}
