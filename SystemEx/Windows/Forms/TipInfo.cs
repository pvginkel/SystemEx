using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace SystemEx.Windows.Forms
{
    public class TipInfo : IDisposable
    {
        private const int TEXT_INDENT = 8;
        private const int TITLE_SPACING = 9;
        private const int IMAGE_OFFSET = 13;
        private const int MINIMUM_WIDTH = 210;
        private const float CORNER_SIZE = 3F;

        private Control _owner;
        private string _title = null;
        private string _text = null;
        private Image _image = null;
        private int _width = 0;
        private Size _size = Size.Empty;
        private int _titleHeight = 0;
        private Font _titleFont;
        private Font _textFont;
        private Color _lightColor;
        private Color _darkColor;
        private Color _textColor;
        private Color _borderColor;
        private GraphicsPath _path = null;
        private LinearGradientBrush _gradientBrush = null;
        private Pen _borderPen = null;
        private bool _disposed = false;
        private Padding _largePadding;
        private Padding _smallPadding;

        public TipInfo(Control owner)
        {
            _owner = owner;

            _largePadding = new Padding(3, 7, 3, 7);
            _smallPadding = new Padding(2, 5, 2, 4);

            _textFont = SystemFonts.MessageBoxFont;
            _titleFont = new Font(SystemFonts.MessageBoxFont, FontStyle.Bold);

            _lightColor = Color.FromArgb(255, 255, 255);
            _darkColor = Color.FromArgb(201, 217, 239);
            _textColor = Color.FromArgb(76, 76, 76);
            _borderColor = Color.FromArgb(118, 118, 118);
        }

        private void CalculateDimensions()
        {
            //
            // Set the flag early on to prevent possible endless loops
            //

            int width = GetWidth();
            var padding = GetPadding();

            int height = 0;
            Size textSize;

            var g = _owner.CreateGraphics();

            var textFlags = (TextFormatFlags)(TextFormatFlags.NoPrefix | TextFormatFlags.WordBreak);

            //
            // Calculate the room we have for text
            //

            int textWidth = width - (padding.Left + padding.Right);

            //
            // We have a shortcut for when the title and the image aren't present
            //

            if (_title == null && _image == null)
            {
                textSize = TextRenderer.MeasureText(
                    g, _text, _textFont, new Size(textWidth, int.MaxValue), textFlags);

                _size = new Size(
                    textSize.Width + padding.Left + padding.Right,
                    textSize.Height + padding.Top + padding.Bottom);

                return;
            }

            //
            // Calculate what we need for the title
            //

            if (_title != null)
            {
                var titleSize = TextRenderer.MeasureText(
                    g, _title, _titleFont, new Size(textWidth, int.MaxValue), textFlags);

                _titleHeight = titleSize.Height;

                height += _titleHeight + TITLE_SPACING;

                textWidth -= TEXT_INDENT * 2;
            }
            else
            {
                _titleHeight = 0;
            }

            //
            // Process the image
            //

            if (_image != null)
            {
                textWidth -= _image.Width + IMAGE_OFFSET;
            }

            //
            // Calculate what we need for the text
            //

            textSize = TextRenderer.MeasureText(
                g, _text, _textFont, new Size(textWidth, int.MaxValue), textFlags);

            if (_image != null && _image.Height > textSize.Height)
            {
                height += _image.Height;
            }
            else
            {
                height += textSize.Height;
            }

            height += padding.Top + padding.Bottom;

            g.Dispose();

            //
            // Invalidate all pre-calculated objects
            //

            if (_path != null)
            {
                _path.Dispose();
                _path = null;
            }

            if (_gradientBrush != null)
            {
                _gradientBrush.Dispose();
                _gradientBrush = null;
            }

            //
            // Store the size
            //

            _size = new Size(width, height);
        }

        public void Paint(Graphics g)
        {
            if (_disposed)
                throw new ObjectDisposedException("TipInfo already disposed");

            var padding = GetPadding();
            int textWidth = this.Size.Width - (padding.Left + padding.Right);
            int leftOffset = padding.Left;
            int topOffset = padding.Top;
            Rectangle dimensions;

            var flags = (TextFormatFlags)(TextFormatFlags.NoPrefix | TextFormatFlags.WordBreak);

            //
            // Draw the rounded rectangle
            //

            var path = GetPath();

            var smoothingMode = g.SmoothingMode;

            g.SmoothingMode = SmoothingMode.AntiAlias;

            g.FillPath(GetGradientBrush(), path);
            g.DrawPath(GetPen(), path);

            g.SmoothingMode = smoothingMode;

            //
            // Draw the title
            //

            if (_title != null)
            {
                dimensions = new Rectangle(leftOffset, topOffset, textWidth, int.MaxValue);

                TextRenderer.DrawText(g, _title, _titleFont, dimensions, _textColor, Color.Transparent, flags);

                topOffset += _titleHeight + TITLE_SPACING;
                leftOffset += TEXT_INDENT;
                textWidth -= TEXT_INDENT * 2;
            }

            //
            // Draw the icon
            //

            if (_image != null)
            {
                dimensions = new Rectangle(new Point(leftOffset, topOffset), _image.Size);

                g.DrawImage(_image, dimensions);

                int offset = dimensions.Width + IMAGE_OFFSET;

                leftOffset += offset;
                textWidth -= offset;
            }

            //
            // Draw the text
            //

            dimensions = new Rectangle(leftOffset, topOffset, textWidth, int.MaxValue);

            TextRenderer.DrawText(g, _text, _textFont, dimensions, _textColor, Color.Transparent, flags);
        }

        private LinearGradientBrush GetGradientBrush()
        {
            if (_gradientBrush == null)
            {
                var rect = GetDimensions();

                _gradientBrush = new LinearGradientBrush(rect, _lightColor, _darkColor, 90);
            }

            return _gradientBrush;
        }

        private Pen GetPen()
        {
            if (_borderPen == null)
            {
                _borderPen = new Pen(_borderColor);
            }

            return _borderPen;
        }

        private GraphicsPath GetPath()
        {
            if (_path == null)
            {
                _path = new GraphicsPath();

                var rect = GetDimensions();

                _path.AddArc(rect.X, rect.Y, CORNER_SIZE, CORNER_SIZE, 180, 90);
                _path.AddArc(rect.X + (rect.Width - CORNER_SIZE), rect.Y, CORNER_SIZE, CORNER_SIZE, 270, 90);
                _path.AddArc(rect.X + (rect.Width - CORNER_SIZE), rect.Y + (rect.Height - CORNER_SIZE), CORNER_SIZE, CORNER_SIZE, 0, 90);
                _path.AddArc(rect.X, rect.Y + (rect.Height - CORNER_SIZE), CORNER_SIZE, CORNER_SIZE, 90, 90);
                _path.CloseFigure();
            }

            return _path;
        }

        private Rectangle GetDimensions()
        {
            return new Rectangle(0, 0, this.Size.Width - 1, this.Size.Height - 1);
        }

        private Padding GetPadding()
        {
            return (_title == null && _image == null ? _smallPadding : _largePadding);
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                //
                // The image isn't ours; we do not dispose of it
                //

                if (_gradientBrush != null)
                {
                    _gradientBrush.Dispose();
                }

                if (_path != null)
                {
                    _path.Dispose();
                }

                if (_borderPen != null)
                {
                    _borderPen.Dispose();
                }

                if (_titleFont != null)
                {
                    _titleFont.Dispose();
                }

                if (_textFont != null)
                {
                    _textFont.Dispose();
                }

                _disposed = true;
            }
        }

        private int GetWidth()
        {
            int width = _width;

            if (width < _owner.Width)
            {
                width = _owner.Width;
            }

            int minimumWidth = MINIMUM_WIDTH;

            if (_image != null)
            {
                minimumWidth += _image.Width + IMAGE_OFFSET;
            }

            if (width < minimumWidth)
            {
                width = minimumWidth;
            }

            return width;
        }

        public Control Owner
        {
            get { return _owner; }
        }

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;

                _size = Size.Empty;
            }
        }

        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;

                _size = Size.Empty;
            }
        }

        public Image Image
        {
            get { return _image; }
            set
            {
                _image = value;

                _size = Size.Empty;
            }
        }

        public int Width
        {
            get { return _width; }
            set
            {
                _width = value;

                _size = Size.Empty;
            }
        }

        public Size Size
        {
            get
            {
                if (_size == Size.Empty)
                {
                    CalculateDimensions();
                }

                return _size;
            }
        }
    }
}
