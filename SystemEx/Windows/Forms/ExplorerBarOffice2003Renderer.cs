using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace SystemEx.Windows.Forms
{
    public class ExplorerBarOffice2003Renderer : ExplorerBarRenderer
    {
        private const int ImageDimensionLarge = 24;

        public override int ButtonHeight
        {
            get { return 32; }
        }

        public override int SmallButtonWidth
        {
            get { return 22; }
        }

        public override int GripHeight
        {
            get { return 5; }
        }

        public override Image DropDownImage
        {
            get { return Properties.Resources.DropDown2003; }
        }

        protected override void OnRenderBackground(ExplorerBarRenderEventArgs e)
        {
            var brush = new SolidBrush(Color.FromArgb(0, 45, 150));

            e.Graphics.FillRectangle(brush, e.ExplorerBar.ClientRectangle);

            brush.Dispose();
        }

        protected override void OnRenderItemText(ExplorerBarItemRenderEventArgs e)
        {
            var font = new Font("Tahoma", (float)8.25, FontStyle.Bold, GraphicsUnit.Point, 0);

            e.Graphics.DrawString(
                e.Item.Text,
                font,
                Brushes.Black,
                10 + ImageDimensionLarge + 8,
                (float)(e.Item.Bounds.Y + ((ButtonHeight / 2.0F) - (font.Height / 2.0F))) + 2.0F
            );

            font.Dispose();
        }

        protected override void OnRenderItemImage(ExplorerBarItemRenderEventArgs e)
        {
            var image = e.Item.GetImage(e.Item.IsLarge);

            if (image != null)
            {
                e.Graphics.DrawImage(image, e.Item.ImageBounds);
            }
        }

        protected override void OnRenderItemBackground(ExplorerBarItemRenderEventArgs e)
        {
            DrawContentBackground(e.Graphics, e.Item.Bounds, e.Item.Checked, e.Item.Selected);
        }

        protected override void OnRenderDockBackground(ExplorerBarRenderEventArgs e)
        {
            DrawContentBackground(e.Graphics, e.ExplorerBar.DockBounds, false, false);
        }

        protected override void OnRenderGrip(ExplorerBarRenderEventArgs e)
        {
            var brush = new LinearGradientBrush(
                e.ExplorerBar.GripBounds,
                Color.FromArgb(89, 135, 214),
                Color.FromArgb(0, 45, 150),
                LinearGradientMode.Vertical
            );

            e.Graphics.FillRectangle(brush, e.ExplorerBar.GripBounds);

            Icon icon = Properties.Resources.Grip2003;

            var iconRect = new Rectangle(
                (e.ExplorerBar.Width / 2) - (icon.Width / 2),
                ((e.ExplorerBar.GripBounds.Height / 2) - (icon.Height / 2)) + 1,
                icon.Width,
                icon.Height
            );

            e.Graphics.DrawIcon(icon, iconRect);

            icon.Dispose();
        }

        private void DrawContentBackground(Graphics g, Rectangle rect, bool checked_, bool selected)
        {
            var brush = new LinearGradientBrush(
                new Point(rect.Left, rect.Top - 1),
                new Point(rect.Left, rect.Bottom + 1),
                GetButtonColorFrom(checked_, selected),
                GetButtonColorTo(checked_, selected)
            );

            g.FillRectangle(brush, rect);

            brush.Dispose();
        }

        private Color GetButtonColorFrom(bool checked_, bool selected)
        {
            if (checked_ && selected)
            {
                return Color.FromArgb(232, 127, 8);
            }
            else if (selected)
            {
                return Color.FromArgb(255, 255, 220);
            }
            else if (checked_)
            {
                return Color.FromArgb(247, 218, 124);
            }
            else
            {
                return Color.FromArgb(203, 225, 252);
            }
        }

        private Color GetButtonColorTo(bool checked_, bool selected)
        {
            if (checked_ && selected)
            {
                return Color.FromArgb(247, 218, 124);
            }
            else if (selected)
            {
                return Color.FromArgb(247, 192, 91);
            }
            else if (checked_)
            {
                return Color.FromArgb(232, 127, 8);
            }
            else
            {
                return Color.FromArgb(125, 166, 223);
            }
        }
    }
}
