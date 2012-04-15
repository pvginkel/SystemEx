using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace SystemEx.Windows.Forms
{
    public class BreadcrumbToolStripRenderer : ToolStripRenderer, IDisposable
    {
        private const float CORNER_SIZE = 3F;

        private bool _disposed = false;
        private bool _brushesLoaded = false;
        private Brush _backgroundBrushTop = null;
        private Brush _backgroundBrushBottom = null;
        private Brush _innerBorderBrush = null;
        private Pen _innerBorderPen = null;

        private Brush _itemOuterBorderBrush = null;
        private Pen _itemOuterBorderPen = null;
        private Brush _itemInnerBorderBrush = null;
        private Pen _itemInnerBorderPen = null;

        private Brush _itemPressedBorderBrush = null;
        private Pen _itemPressedBorderPen = null;
        private Brush _itemPressedBrushTop = null;
        private Brush _itemPressedBrushBottom = null;

        private Brush _itemButtonBrushTop = null;
        private Brush _itemButtonBrushBottom = null;
        private System.Windows.Forms.ToolStrip _toolStrip;

        private const int ItemMargin = 3;

        public BreadcrumbToolStripRenderer()
        {
        }

        protected override void Initialize(System.Windows.Forms.ToolStrip toolStrip)
        {
            base.Initialize(toolStrip);

            _toolStrip = toolStrip;

            _toolStrip.Padding = new Padding(8, 3, 8, 3);
            _toolStrip.GripStyle = ToolStripGripStyle.Hidden;
        }

        protected override void InitializeItem(ToolStripItem item)
        {
            base.InitializeItem(item);

            if (item is ToolStripButton && ((ToolStripButton)item).DisplayStyle == ToolStripItemDisplayStyle.Image)
            {
                item.Margin = new Padding(0);
                item.Padding = new Padding(0);

                if (item is ToolStripSpriteButton)
                {
                    item.AutoSize = false;
                    item.Size = item.Image.Size;
                }
            }
            else if (!(item is ToolStripSeparator))
            {
                item.Margin = new Padding(0, ItemMargin, 0, ItemMargin);
                item.Padding = new Padding(5, 2, 5, 2);
            }
        }

        protected override void OnRenderItemImage(ToolStripItemImageRenderEventArgs e)
        {
            var spriteButton = e.Item as ToolStripSpriteButton;

            if (spriteButton != null)
            {
                Image image = null;

                if (!spriteButton.Enabled)
                {
                    image = spriteButton.DisabledImage;
                }
                else if (spriteButton.Pressed)
                {
                    image = spriteButton.PressedImage;
                }
                else if (spriteButton.Selected)
                {
                    image = spriteButton.SelectedImage;
                }

                if (image == null)
                {
                    image = spriteButton.Image;
                }

                e.Graphics.DrawImage(image, new Point(0, 0));
            }
            else
            {
                base.OnRenderItemImage(e);
            }
        }

        protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
        {
            base.OnRenderToolStripBackground(e);

            var g = e.Graphics;

            LoadBrushes();

            g.FillRectangle(
                _backgroundBrushTop,
                new Rectangle(0, 0, _toolStrip.Width, _toolStrip.Height / 2)
            );

            g.FillRectangle(
                _backgroundBrushBottom,
                new Rectangle(0, _toolStrip.Height / 2, _toolStrip.Width, _toolStrip.Height)
            );

            var path = GetPath(GetInnerBorderBounds());

            var smoothingMode = g.SmoothingMode;

            g.SmoothingMode = SmoothingMode.AntiAlias;

            g.FillPath(SystemBrushes.Window, path);
            g.DrawPath(_innerBorderPen, path);

            g.SmoothingMode = smoothingMode;

        }

        private GraphicsPath GetPath(Rectangle rect)
        {
            var path = new GraphicsPath();

            path.AddArc(rect.X, rect.Y, CORNER_SIZE, CORNER_SIZE, 180, 90);
            path.AddArc(rect.X + (rect.Width - CORNER_SIZE), rect.Y, CORNER_SIZE, CORNER_SIZE, 270, 90);
            path.AddArc(rect.X + (rect.Width - CORNER_SIZE), rect.Y + (rect.Height - CORNER_SIZE), CORNER_SIZE, CORNER_SIZE, 0, 90);
            path.AddArc(rect.X, rect.Y + (rect.Height - CORNER_SIZE), CORNER_SIZE, CORNER_SIZE, 90, 90);
            path.CloseFigure();

            return path;
        }

        private Rectangle GetInnerBorderBounds()
        {
            int offset = _toolStrip.Padding.Left;

            foreach (ToolStripItem item in _toolStrip.Items)
            {
                if (item is ToolStripSeparator)
                {
                    offset = item.Bounds.Right + item.Margin.Right;
                }
            }

            return new Rectangle(
                (offset - 1),
                (_toolStrip.Padding.Top - 1) + ItemMargin,
                ((_toolStrip.Width - (_toolStrip.Padding.Right + offset)) + 1) - ItemMargin,
                ((_toolStrip.Height - _toolStrip.Padding.Vertical) + 1) - (ItemMargin * 2)
            );
        }

        protected override void OnRenderButtonBackground(ToolStripItemRenderEventArgs e)
        {
            base.OnRenderButtonBackground(e);

            var button = e.Item as ToolStripButton;

            if (button != null && button.DisplayStyle == ToolStripItemDisplayStyle.Image)
            {
                DrawImageButton(e);
            }
            else
            {
                DrawTextButton(e);
            }
        }

        private void DrawImageButton(ToolStripItemRenderEventArgs e)
        {
            if (e.Item is ToolStripSpriteButton)
            {
                return;
            }

            if (!e.Item.Pressed && !e.Item.Selected)
            {
                return;
            }

            var g = e.Graphics;

            var smoothingMode = g.SmoothingMode;

            g.SmoothingMode = SmoothingMode.AntiAlias;

            DrawImageButtonBorder(e, g);

            g.SmoothingMode = smoothingMode;
        }

        private void DrawTextButton(ToolStripItemRenderEventArgs e)
        {
            Brush topBrush;
            Brush bottomBrush;

            if (e.Item.Pressed)
            {
                topBrush = _itemPressedBrushTop;
                bottomBrush = _itemPressedBrushBottom;
            }
            else if (e.Item.Selected)
            {
                topBrush = _itemButtonBrushTop;
                bottomBrush = _itemButtonBrushBottom;
            }
            else if (e.Item is ToolStripButton && ((ToolStripButton)e.Item).Checked)
            {
                topBrush = _backgroundBrushTop;
                bottomBrush = _backgroundBrushBottom;
            }
            else
            {
                return;
            }

            var g = e.Graphics;

            var smoothingMode = g.SmoothingMode;

            g.SmoothingMode = SmoothingMode.AntiAlias;

            var bounds = GetItemBounds(e.Item);

            int offset = bounds.Height / 2 + 1;

            var subBounds = new Rectangle(bounds.Left, bounds.Top, bounds.Width, bounds.Top + offset);

            g.FillRectangle(topBrush, subBounds);

            subBounds = new Rectangle(bounds.Left, bounds.Top + offset, bounds.Width, offset * 2);

            g.FillRectangle(bottomBrush, subBounds);

            DrawTextButtonBorder(e, g);

            g.SmoothingMode = smoothingMode;
        }

        private void DrawTextButtonBorder(ToolStripItemRenderEventArgs e, Graphics g)
        {
            var bounds = GetItemBounds(e.Item);

            bounds.Inflate(-1, -1);

            var path = GetPath(bounds);

            g.DrawPath(_itemInnerBorderPen, path);

            bounds = GetItemBounds(e.Item);

            path = GetPath(bounds);

            g.DrawPath(_itemOuterBorderPen, path);
        }

        private void DrawImageButtonBorder(ToolStripItemRenderEventArgs e, Graphics g)
        {
            var bounds = GetItemBounds(e.Item);

            bounds.Inflate(-1, -1);

            g.DrawEllipse(_itemInnerBorderPen, bounds);

            bounds = GetItemBounds(e.Item);

            g.DrawEllipse(_itemOuterBorderPen, bounds);
        }

        private Rectangle GetItemBounds(ToolStripItem item)
        {
            if (item is ToolStripButton && ((ToolStripButton)item).DisplayStyle == ToolStripItemDisplayStyle.Image)
            {
                int size = item.Width > item.Height ? item.Height : item.Width;

                return new Rectangle(
                    (item.Width / 2) - (size / 2),
                    (item.Height / 2) - (size / 2),
                    size - 1,
                    size - 1
                );
            }
            else
            {
                return new Rectangle(
                    0,
                    0,
                    item.Width - 1,
                    item.Height - 1
                );
            }
        }

        private void LoadBrushes()
        {
            if (!_brushesLoaded)
            {
                _innerBorderBrush = new LinearGradientBrush(
                    new Point(0, 0),
                    new Point(0, _toolStrip.Height),
                    Color.FromArgb(67, 70, 75),
                    Color.FromArgb(153, 162, 168)
                );

                _innerBorderPen = new Pen(_innerBorderBrush);

                _itemOuterBorderBrush = new LinearGradientBrush(
                    new Point(0, 0),
                    new Point(0, _toolStrip.Height),
                    Color.FromArgb(187, 202, 219),
                    Color.FromArgb(170, 188, 213)
                );

                _itemOuterBorderPen = new Pen(_itemOuterBorderBrush);

                _itemInnerBorderBrush = new LinearGradientBrush(
                    new Point(0, 0),
                    new Point(0, _toolStrip.Height),
                    Color.FromArgb(253, 254, 255),
                    Color.FromArgb(239, 244, 249)
                );

                _itemInnerBorderPen = new Pen(_itemInnerBorderBrush);

                _itemPressedBorderBrush = new LinearGradientBrush(
                    new Point(0, 0),
                    new Point(0, _toolStrip.Height),
                    Color.FromArgb(187, 202, 219),
                    Color.FromArgb(170, 188, 213)
                );

                _itemPressedBorderPen = new Pen(_itemPressedBorderBrush);

                int offset = (_toolStrip.Height - _toolStrip.Padding.Vertical) / 2 + 1;

                _itemButtonBrushTop = new LinearGradientBrush(
                    new Point(0, 0),
                    new Point(0, offset),
                    Color.FromArgb(248, 251, 254),
                    Color.FromArgb(237, 242, 250)
                );

                _itemButtonBrushBottom = new LinearGradientBrush(
                    new Point(0, offset),
                    new Point(0, offset * 2),
                    Color.FromArgb(215, 228, 244),
                    Color.FromArgb(193, 210, 232)
                );

                _itemPressedBrushTop = new LinearGradientBrush(
                    new Point(0, 0),
                    new Point(0, offset),
                    Color.FromArgb(225, 235, 245),
                    Color.FromArgb(216, 228, 241)
                );

                _itemPressedBrushBottom = new LinearGradientBrush(
                    new Point(0, offset),
                    new Point(0, offset * 2),
                    Color.FromArgb(207, 219, 236),
                    Color.FromArgb(207, 220, 237)
                );

                offset = _toolStrip.Height / 2;

                _backgroundBrushTop = new LinearGradientBrush(
                    new Point(0, 0),
                    new Point(0, offset),
                    Color.FromArgb(253, 254, 255),
                    Color.FromArgb(230, 240, 250)
                );

                _backgroundBrushBottom = new LinearGradientBrush(
                    new Point(0, offset),
                    new Point(0, _toolStrip.Height),
                    Color.FromArgb(220, 230, 244),
                    Color.FromArgb(228, 239, 251)
                );

                _brushesLoaded = true;
            }
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                if (_brushesLoaded)
                {
                    _backgroundBrushBottom.Dispose();
                    _backgroundBrushTop.Dispose();
                    _innerBorderBrush.Dispose();
                    _innerBorderPen.Dispose();
                    _itemOuterBorderBrush.Dispose();
                    _itemOuterBorderPen.Dispose();
                    _itemInnerBorderBrush.Dispose();
                    _itemInnerBorderPen.Dispose();
                    _itemButtonBrushTop.Dispose();
                    _itemButtonBrushBottom.Dispose();
                    _itemPressedBorderBrush.Dispose();
                    _itemPressedBorderPen.Dispose();
                    _itemPressedBrushTop.Dispose();
                    _itemPressedBrushBottom.Dispose();
                }
            }
        }
    }
}
