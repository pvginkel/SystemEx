using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace SystemEx.Windows.Forms
{
    public class ExplorerBarOffice2007Renderer : ExplorerBarRenderer
    {
        private const int ImageDimensionLarge = 24;

        public override int ButtonHeight
        {
            get { return 32; }
        }

        public override int SmallButtonWidth
        {
            get { return 26; }
        }

        public override int GripHeight
        {
            get { return 7; }
        }

        public override Image DropDownImage
        {
            get { return Properties.Resources.DropDown2007; }
        }

        protected override void OnRenderBackground(ExplorerBarRenderEventArgs e)
        {
            var brush = new SolidBrush(Color.FromArgb(101, 147, 207));

            e.Graphics.FillRectangle(brush, e.ExplorerBar.ClientRectangle);

            brush.Dispose();
        }

        protected override void OnRenderItemText(ExplorerBarItemRenderEventArgs e)
        {
            var font = new Font("Tahoma", (float)8.25, FontStyle.Bold, GraphicsUnit.Point, 0);
            var brush = e.Item.Selected ? new SolidBrush(Color.FromArgb(37, 77, 137)) : Brushes.Black;

            e.Graphics.DrawString(
                e.Item.Text,
                font,
                brush,
                10 + ImageDimensionLarge + 8,
                (float)(e.Item.Bounds.Y + ((ButtonHeight / 2.0F) - (font.Height / 2.0F))) + 2.0F
            );

            font.Dispose();

            if (e.Item.Selected)
            {
                brush.Dispose();
            }
        }

        protected override void OnRenderItemImage(ExplorerBarItemRenderEventArgs e)
        {
            var image = e.Item.GetImage(e.Item.IsLarge);

            if (image != null)
            {
                e.Graphics.DrawImage(image, e.Item.ImageBounds);
            }
        }

        protected override void OnRenderDockBackground(ExplorerBarRenderEventArgs e)
        {
            DrawContentBackground(e.Graphics, e.ExplorerBar.DockBounds, false, false);
        }

        protected override void OnRenderItemBackground(ExplorerBarItemRenderEventArgs e)
        {
            DrawContentBackground(e.Graphics, e.Item.Bounds, e.Item.Checked, e.Item.Selected);
        }

        protected override void OnRenderGrip(ExplorerBarRenderEventArgs e)
        {
            var brush = new LinearGradientBrush(
                e.ExplorerBar.GripBounds,
                Color.FromArgb(227, 239, 255),
                Color.FromArgb(179, 212, 255),
                LinearGradientMode.Vertical
            );

            e.Graphics.FillRectangle(brush, e.ExplorerBar.GripBounds);

            Icon icon = Properties.Resources.Grip2007;

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
            var topRect = rect;

            var brush = new LinearGradientBrush(
                new Point(rect.Left, rect.Top - 1),
                new Point(rect.Left, rect.Bottom + 1),
                GetButtonColorTopFrom(checked_, selected),
                GetButtonColorTopTo(checked_, selected)
            );

            topRect.Height = (ButtonHeight * 15) / 32;

            g.FillRectangle(brush, topRect);

            brush.Dispose();

            var bottomRect = rect;

            brush = new LinearGradientBrush(
                new Point(rect.Left, rect.Top - 1),
                new Point(rect.Left, rect.Bottom + 1),
                GetButtonColorBottomFrom(checked_, selected),
                GetButtonColorBottomTo(checked_, selected)
            );

            bottomRect.Y += (ButtonHeight * 12) / 32;
            bottomRect.Height -= (ButtonHeight * 12) / 32;

            g.FillRectangle(brush, bottomRect);

            brush.Dispose();
        }

        private Color GetButtonColorTopFrom(bool checked_, bool selected)
        {
            if (checked_ && selected)
            {
                return Color.FromArgb(255, 189, 105);
            }
            else if (selected)
            {
                return Color.FromArgb(255, 254, 228);
            }
            else if (checked_)
            {
                return Color.FromArgb(255, 217, 170);
            }
            else
            {
                return Color.FromArgb(227, 239, 255);
            }
        }

        private Color GetButtonColorTopTo(bool checked_, bool selected)
        {
            if (checked_ && selected)
            {
                return Color.FromArgb(255, 172, 66);
            }
            else if (selected)
            {
                return Color.FromArgb(255, 232, 166);
            }
            else if (checked_)
            {
                return Color.FromArgb(255, 187, 109);
            }
            else
            {
                return Color.FromArgb(196, 221, 255);
            }
        }

        private Color GetButtonColorBottomFrom(bool checked_, bool selected)
        {
            if (checked_ && selected)
            {
                return Color.FromArgb(251, 140, 60);
            }
            else if (selected)
            {
                return Color.FromArgb(255, 215, 103);
            }
            else if (checked_)
            {
                return Color.FromArgb(255, 171, 63);
            }
            else
            {
                return Color.FromArgb(173, 209, 255);
            }
        }

        private Color GetButtonColorBottomTo(bool checked_, bool selected)
        {
            if (checked_ && selected)
            {
                return Color.FromArgb(254, 211, 101);
            }
            else if (selected)
            {
                return Color.FromArgb(255, 230, 159);
            }
            else if (checked_)
            {
                return Color.FromArgb(254, 225, 123);
            }
            else
            {
                return Color.FromArgb(193, 219, 255);
            }
        }
    }
}
