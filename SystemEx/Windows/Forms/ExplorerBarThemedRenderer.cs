using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace SystemEx.Windows.Forms
{
    public class ExplorerBarThemedRenderer : ExplorerBarRenderer
    {
        private const int ImageDimensionLarge = 24;

        public override int ButtonHeight
        {
            get { return 31; }
        }

        public override int SmallButtonWidth
        {
            get { return 20; }
        }

        public override int GripHeight
        {
            get { return 7; }
        }

        public override Image DropDownImage
        {
            get { return null; }
        }

        protected override void OnRenderBackground(ExplorerBarRenderEventArgs e)
        {
            e.Graphics.FillRectangle(SystemBrushes.ControlDark, e.ExplorerBar.ClientRectangle);
        }

        protected override void OnRenderItemText(ExplorerBarItemRenderEventArgs e)
        {
            var font = new Font(e.ExplorerBar.Font, FontStyle.Bold);

            e.Graphics.DrawString(
                e.Item.Text,
                font,
                e.Item.Selected && e.Item.Checked ? SystemBrushes.Window : SystemBrushes.ControlText,
                10 + ImageDimensionLarge + 8,
                (float)(e.Item.Bounds.Y + ((ButtonHeight / 2.0F) - (font.Height / 2.0F)))
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
                Color.FromArgb(206, 199, 198),
                Color.FromArgb(156, 154, 156),
                LinearGradientMode.Vertical
            );

            e.Graphics.FillRectangle(brush, e.ExplorerBar.GripBounds);

            var image = Properties.Resources.themed_grip;

            var iconRect = new Rectangle(
                (e.ExplorerBar.Width / 2) - (image.Width / 2),
                ((e.ExplorerBar.GripBounds.Height / 2) - (image.Height / 2)) + 1,
                image.Width,
                image.Height
            );

            e.Graphics.DrawImage(image, iconRect);

            image.Dispose();
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
                return Color.FromArgb(132, 146, 181);
            }
            else if (selected)
            {
                return Color.FromArgb(181, 190, 214);
            }
            else if (checked_)
            {
                return Color.FromArgb(214, 215, 222);
            }
            else
            {
                return Color.FromArgb(255, 255, 255);
            }
        }

        private Color GetButtonColorTo(bool checked_, bool selected)
        {
            if (checked_ && selected)
            {
                return Color.FromArgb(132, 146, 181);
            }
            else if (selected)
            {
                return Color.FromArgb(181, 190, 214);
            }
            else if (checked_)
            {
                return Color.FromArgb(214, 215, 222);
            }
            else
            {
                return Color.FromArgb(222, 215, 206);
            }
        }
    }
}
