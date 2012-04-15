using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;
using SystemEx.Win32;

namespace SystemEx.Windows.Forms
{
    public class TextBox : System.Windows.Forms.TextBox
    {
        private const int EditButtonWidth = 23;
        private const int EditButtonSpacing = 0;
        private const int EditButtonOffset = 1;

        private int _leftMargin = 0;
        private int _rightMargin = 0;
        private CharacterCasingEx _characterCasing = CharacterCasingEx.None;
        private EditButtonCollection _editButtons = new EditButtonCollection();
        private int _minimumWidth;
        private bool _inDesignMode;

        [Category("Behavior")]
        [Browsable(true)]
        [DefaultValue(CharacterCasingEx.None)]
        public new CharacterCasingEx CharacterCasing
        {
            get { return _characterCasing; }
            set { _characterCasing = value; }
        }

        private int LeftMargin
        {
            get
            {
                return _leftMargin;
            }
            set
            {
                if (_leftMargin != value)
                {
                    _leftMargin = value;

                    UpdateMargins();
                }
            }
        }

        private int RightMargin
        {
            get
            {
                return _rightMargin;
            }
            set
            {
                if (_rightMargin != value)
                {
                    _rightMargin = value;

                    UpdateMargins();
                }
            }
        }

        [Category("Appearance")]
        public EditButtonCollection LeftButtons { get; private set; }

        [Category("Appearance")]
        public EditButtonCollection RightButtons { get; private set; }

        public TextBox()
        {
            _inDesignMode = ControlUtil.GetIsInDesignMode(this);

            LeftButtons = new EditButtonCollection();
            LeftButtons.Changed += new EventHandler(LeftButtons_Changed);

            RightButtons = new EditButtonCollection();
            RightButtons.Changed += new EventHandler(RightButtons_Changed);
        }

        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        {
            if ((specified & BoundsSpecified.Width) != 0 && _minimumWidth > 0)
                width = Math.Max(width, _minimumWidth);

            base.SetBoundsCore(x, y, width, height, specified);
        }

        void LeftButtons_Changed(object sender, EventArgs e)
        {
            RebuildEditButtons();
        }

        void RightButtons_Changed(object sender, EventArgs e)
        {
            RebuildEditButtons();
        }

        private void RebuildEditButtons()
        {
            // For now, we simply rebuild when the buttons have changed.

            SuspendLayout();

            foreach (var control in _editButtons)
            {
                Controls.Remove(control);
            }

            _editButtons.Clear();

            foreach (var button in LeftButtons)
            {
                AddEditButton(button);
            }

            foreach (var button in RightButtons)
            {
                AddEditButton(button);
            }

            ResumeLayout(false);
            PerformLayout();
        }

        protected override void OnLayout(LayoutEventArgs levent)
        {
            if (_editButtons.Count > 0)
            {
                PerformEditButtonLayout(levent);
            }

            base.OnLayout(levent);
        }

        private void PerformEditButtonLayout(LayoutEventArgs levent)
        {
            var nonClientSize = ControlUtil.GetNonClientSize(this);

            // If we have no border, we keep the size equal to the control
            // size. Otherwise, we fall on pixel out of the border size.

            int leftOffset = Math.Min(nonClientSize.Left, EditButtonOffset);
            int topOffset = Math.Min(nonClientSize.Top, EditButtonOffset);
            int rightOffset = Math.Min(nonClientSize.Right, EditButtonOffset);
            int bottomOffset = Math.Min(nonClientSize.Bottom, EditButtonOffset);

            int height = Height - ((nonClientSize.Top - topOffset) + (nonClientSize.Bottom - bottomOffset));

            // We require the text box to at least be the width of all buttons.

            _minimumWidth =
                (_editButtons.Count * EditButtonWidth) +
                Math.Max(LeftButtons.Count - 1, 0) * EditButtonSpacing +
                Math.Max(RightButtons.Count - 1, 0) * EditButtonSpacing +
                (leftOffset + rightOffset);

            // Force a resize when applicable.

            if (Width < _minimumWidth)
                Width = _minimumWidth;
            
            // Layout the left buttons.

            for (int i = 0; i < LeftButtons.Count; i++)
            {
                LeftButtons[i].SetBounds(
                    i * (EditButtonSpacing + EditButtonWidth) - leftOffset,
                    -topOffset,
                    EditButtonWidth,
                    height
                );
            }

            // And layout the right buttons.

            int clientWidth = ClientSize.Width;

            for (int i = 0; i < RightButtons.Count; i++)
            {
                RightButtons[i].SetBounds(
                    clientWidth - ((((i + 1) * EditButtonWidth) + (i * EditButtonSpacing)) - rightOffset),
                    -topOffset,
                    EditButtonWidth,
                    height
                );
            }

            // Set the margins.

            if (LeftButtons.Count == 0)
                LeftMargin = 0;
            else
                LeftMargin = (((LeftButtons.Count) * EditButtonWidth) + (Math.Min(LeftButtons.Count - 1, 0) * EditButtonSpacing)) - leftOffset + 1;

            if (RightButtons.Count == 0)
                RightMargin = 0;
            else
                RightMargin = (((RightButtons.Count) * EditButtonWidth) + (Math.Min(RightButtons.Count - 1, 0) * EditButtonSpacing)) - rightOffset + 1;
        }

        private void AddEditButton(Button button)
        {
            _editButtons.Add(button);

            Controls.Add(button);

            button.TabStop = false;
            button.Cursor = Cursors.Default;

            // From http://stackoverflow.com/questions/4489663
            button.UseVisualStyleBackColor = true;
        }

        private void UpdateMargins()
        {
            if (!_inDesignMode && IsHandleCreated)
            {
                uint margins = ((uint)_leftMargin & 0xffff) + (((uint)_rightMargin & 0xffff) << 16);
                uint message = NativeMethods.EC_LEFTMARGIN | NativeMethods.EC_RIGHTMARGIN;

                NativeMethods.SendMessage(Handle, NativeMethods.EM_SETMARGINS, message, margins);
            }
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            FixMargins();
        }

        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);

            FixMargins();
        }

        private void FixMargins()
        {
            if (_leftMargin != 0 || _rightMargin != 0)
                UpdateMargins();
        }

        protected override void OnLeave(EventArgs e)
        {
            if (Text.Length > 0)
            {
                switch (_characterCasing)
                {
                    case CharacterCasingEx.Upper:
                        Text = Text.ToUpper();
                        break;

                    case CharacterCasingEx.Lower:
                        Text = Text.ToLower();
                        break;

                    case CharacterCasingEx.UpperFirst:
                        Text =
                            Text.Substring(0, 1).ToUpper() +
                            (Text.Length > 1 ? Text.Substring(1, Text.Length - 1) : "");
                        break;
                }
            }

            base.OnLeave(e);
        }
    }

    public enum CharacterCasingEx
    {
        None,
        Upper,
        Lower,
        UpperFirst
    }
}
