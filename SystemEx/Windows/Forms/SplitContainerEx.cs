using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SystemEx.Windows.Forms
{
    public class SplitContainerEx : SplitContainer
    {
        protected override void ScaleControl(SizeF factor, BoundsSpecified specified)
        {
            base.ScaleControl(factor, specified);

            SplitterDistance = (int)(SplitterDistance * (Orientation == Orientation.Horizontal ? factor.Height : factor.Width));
        }
    }
}
