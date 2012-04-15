using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SystemEx.Windows.Forms
{
    [DesignTimeVisible(false)]
    public partial class MessageFormContent : UserControl
    {
        private Image _icon = null;
        private const int ImageOffset = 6;
        private int _calculatedHeight = -1;
        private PictureBox _pictureBox = null;

        public MessageFormContent()
        {
            InitializeComponent();
        }

        protected override Padding DefaultPadding
        {
            get { return new Padding(12); }
        }

        public Image Icon
        {
            get { return _icon; }
            set
            {
                _icon = value;

                if (_icon == null)
                {
                    if (_pictureBox != null)
                        Controls.Remove(_pictureBox);
                }
                else
                {
                    if (_pictureBox == null)
                    {
                        _pictureBox = new PictureBox();
                        Controls.Add(_pictureBox);
                    }

                    _pictureBox.Image = Icon;
                    _pictureBox.SizeMode = PictureBoxSizeMode.AutoSize;
                    _pictureBox.Location = new Point(Padding.Left, Padding.Top);
                }

                ProcessChange();
            }
        }

        public int CalculatedHeight { get { return _calculatedHeight; } }

        public event EventHandler CalculatedHeightChanged;

        [Bindable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public override string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }

        protected virtual void OnCalculatedHeightChanged(EventArgs e)
        {
            if (CalculatedHeightChanged != null)
            {
                CalculatedHeightChanged(this, e);
            }
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);

            ProcessChange();
        }

        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);

            ProcessChange();
        }

        protected override void OnPaddingChanged(EventArgs e)
        {
            base.OnPaddingChanged(e);

            ProcessChange();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            ProcessChange();
        }

        private void ProcessChange()
        {
            int height = CalculateHeight();

            bool changed = height != _calculatedHeight;

            _calculatedHeight = height;

            if (changed)
            {
                OnCalculatedHeightChanged(EventArgs.Empty);
            }

            Invalidate();
        }

        private int CalculateHeight()
        {
            int width = Width - (Padding.Horizontal + (Icon == null ? 0 : Icon.Width + ImageOffset));

            var size = TextRenderer.MeasureText(
                this.Text, MessageForm.LargeThemedFont, new Size(width, int.MaxValue),
                TextFormatFlags.NoPrefix | TextFormatFlags.WordBreak);

            return Padding.Vertical + size.Height;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            int left;

            if (Icon != null)
            {
                left = Padding.Left + Icon.Width + ImageOffset;
            }
            else
            {
                left = Padding.Left;
            }

            var size = new Size(Width - (Padding.Right + left), Height - Padding.Vertical);

            TextRenderer.DrawText(e.Graphics, Text, MessageForm.LargeThemedFont,
                new Rectangle(new Point(left, Padding.Top), size), Color.FromArgb(0, 51, 153), BackColor,
                TextFormatFlags.NoPrefix | TextFormatFlags.WordBreak);
        }
    }
}
