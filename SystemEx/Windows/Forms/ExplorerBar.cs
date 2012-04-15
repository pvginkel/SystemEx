using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;

namespace SystemEx.Windows.Forms
{
    [DefaultEvent("ButtonClicked")]
    public class ExplorerBar : Control
    {
        private const int ImageDimensionLarge = 24;
        private const int ImageDimensionSmall = 16;
        private const int ContainerMargin = 16;

        private ExplorerBarButtonCollection _buttons;
        private System.Windows.Forms.ToolTip _toolTip = new System.Windows.Forms.ToolTip();
        private ExplorerBarRenderer _renderer = null;
        private ExplorerBarRenderMode _renderMode;
        private ExplorerBarButton _selectedButton;
        private ExplorerBarButton _checkedButton;
        private bool _isResizing;
        private bool _canGrow;
        private bool _canShrink;
        private int _maxLargeButtonCount = 0;
        private int _maxSmallButtonCount = 0;
        private ExplorerBarButton _dropDownButton = null;
        private Rectangle _dockBounds = Rectangle.Empty;
        private Rectangle _gripBounds = Rectangle.Empty;

        public ExplorerBar()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.ResizeRedraw, true);

            _dropDownButton = new ExplorerBarButton();
            _dropDownButton.Owner = this;

            RenderMode = ExplorerBarRenderMode.Themed;

            _buttons = new ExplorerBarButtonCollection(this);
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Category("Behavior")]
        public ExplorerBarButtonCollection Buttons
        {
            get { return _buttons; }
        }

        [Browsable(false)]
        public ExplorerBarButton CheckedButton
        {
            get { return _checkedButton; }
            set
            {
                if (!Object.ReferenceEquals(_checkedButton, value))
                {
                    if (value != null && !this.Equals(value.Owner))
                    {
                        throw new ArgumentException("Cannot set button to Checked that is not owned by the current ExplorerBar", "value");
                    }

                    _checkedButton = value;

                    Invalidate();

                    _checkedButton.PerformClick();
                }
            }
        }

        [DefaultValue(typeof(ExplorerBarRenderMode), "Themed")]
        [Category("Appearance")]
        public ExplorerBarRenderMode RenderMode
        {
            get { return _renderMode; }
            set
            {
                _renderMode = value;

                switch (_renderMode)
                {
                    case ExplorerBarRenderMode.Office2003:
                        _renderer = new ExplorerBarOffice2003Renderer();
                        break;

                    case ExplorerBarRenderMode.Office2007:
                        _renderer = new ExplorerBarOffice2007Renderer();
                        break;

                    case ExplorerBarRenderMode.Themed:
                        _renderer = new ExplorerBarThemedRenderer();
                        break;

                    default:
                        _renderer = null;
                        break;
                }

                if (_renderer != null)
                {
                    _renderer.Initialize(this);

                    _dropDownButton.SmallImage = _renderer.DropDownImage;
                }

                if (_dropDownButton.SmallImage == null)
                {
                    _dropDownButton.SmallImage = Properties.Resources.DropDown2003;
                }

                Invalidate();
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ExplorerBarRenderer Renderer
        {
            get { return _renderer; }
            set
            {
                this.RenderMode = ExplorerBarRenderMode.Custom;

                _renderer = value;

                _renderer.Initialize(this);

                _dropDownButton.SmallImage = _renderer.DropDownImage;

                Invalidate();
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override Size MinimumSize
        {
            get
            {
                return new Size(
                    ContainerMargin,
                    GetBottomContainerRectangle().Height + GetGripRectangle().Height + 1
                );
            }
            set
            {
                // ignore
            }
        }

        public override Color ForeColor
        {
            get { return base.ForeColor; }
            set
            {
                base.ForeColor = value;

                Invalidate();
            }
        }

        public override Font Font
        {
            get
            {
                return base.Font;
            }
            set
            {
                base.Font = value;
                Invalidate();
            }
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);

            var button = _buttons.GetItemAtPoint(e.Location);

            if (button != null && e.Button == MouseButtons.Left)
            {
                _checkedButton = button;

                _checkedButton.PerformClick();

                Invalidate();
            }
            else if (_dropDownButton.Visible && _dropDownButton.Bounds.Contains(e.Location))
            {
                CreateContextMenu();
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            _isResizing = GetGripRectangle().Contains(e.Location);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);

            _selectedButton = null;

            Invalidate();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            _selectedButton = null;

            int buttonHeight = _renderer.ButtonHeight;

            if (_isResizing)
            {
                if (e.Y < -buttonHeight)
                {
                    if (_canGrow)
                    {
                        this.Height += buttonHeight;
                    }
                }
                else if (e.Y > _renderer.ButtonHeight)
                {
                    if (_canShrink)
                    {
                        this.Height -= buttonHeight;
                    }
                }
            }
            else
            {
                if (GetGripRectangle().Contains(e.Location))
                {
                    this.Cursor = Cursors.SizeNS;
                }
                else if (_dropDownButton.Visible && _dropDownButton.Bounds.Contains(e.Location))
                {
                    this.Cursor = Cursors.Hand;

                    _selectedButton = _dropDownButton;

                    Invalidate();

                    string toolTipTag = _toolTip.Tag as string;

                    if (toolTipTag == null || toolTipTag != "Configure")
                    {
                        _toolTip.Active = true;
                        _toolTip.SetToolTip(this, "Configure buttons");
                        _toolTip.Tag = "Configure";
                    }
                }
                else
                {
                    var button = _buttons.GetItemAtPoint(e.Location);

                    if (button != null)
                    {
                        this.Cursor = Cursors.Hand;

                        _selectedButton = button;

                        Invalidate();

                        if (!button.IsLarge)
                        {
                            if (!button.Equals(_toolTip.Tag))
                            {
                                _toolTip.Active = true;
                                _toolTip.SetToolTip(this, button.Text);
                                _toolTip.Tag = button;
                            }
                        }
                        else
                        {
                            _toolTip.Active = false;
                        }
                    }
                    else
                    {
                        this.Cursor = Cursors.Default;
                    }
                }
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            _isResizing = false;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var gripRectangle = GetGripRectangle();
            var bottomContainerRectangle = GetBottomContainerRectangle();

            _maxLargeButtonCount = (int)Math.Floor(
                (this.Height - bottomContainerRectangle.Height - (gripRectangle.Height + 1)) /
                (double)_renderer.ButtonHeight
            );

            int visibleButtons = _buttons.CountVisible();

            if (visibleButtons < _maxLargeButtonCount)
            {
                _maxLargeButtonCount = visibleButtons;
            }

            _canShrink = _maxLargeButtonCount != 0;
            _canGrow = _maxLargeButtonCount < visibleButtons;

            int height =
                (_maxLargeButtonCount * _renderer.ButtonHeight) +
                gripRectangle.Height + 1 +
                bottomContainerRectangle.Height;

            if (height != this.Height)
            {
                this.Height = height;
                bottomContainerRectangle = GetBottomContainerRectangle();
            }

            _renderer.PerformRenderBackground(e.Graphics);

            _gripBounds = GetGripRectangle();

            _renderer.PerformRenderGrip(e.Graphics);

            int syncLargeButtons = 0;
            int buttonOffset = 0;

            for (int i = 0; i < _buttons.Count; i++)
            {
                var button = _buttons[i];

                if (button.Visible)
                {
                    if (syncLargeButtons == _maxLargeButtonCount)
                    {
                        break;
                    }

                    button.Bounds = new Rectangle(
                        1,
                        (syncLargeButtons * _renderer.ButtonHeight) + gripRectangle.Height + 2,
                        this.Width - 2,
                        _renderer.ButtonHeight - 1
                    );

                    button.IsLarge = true;

                    PaintButton(button, e.Graphics);

                    buttonOffset = i + 1;
                    syncLargeButtons++;
                }
            }

            _maxSmallButtonCount = (int)Math.Floor(
                (this.Width - _dropDownButton.Bounds.Width - _renderer.SmallButtonWidth) /
                (double)_renderer.SmallButtonWidth);

            if ((visibleButtons - _maxLargeButtonCount) <= 0)
            {
                _maxSmallButtonCount = 0;
            }
            if (_maxSmallButtonCount > (visibleButtons - _maxLargeButtonCount))
            {
                _maxSmallButtonCount = visibleButtons - _maxLargeButtonCount;
            }

            _dockBounds = new Rectangle(
                bottomContainerRectangle.Left + 1,
                bottomContainerRectangle.Top + 1,
                bottomContainerRectangle.Width - 2,
                bottomContainerRectangle.Height - 2
            );

            _renderer.PerformRenderDockBackground(e.Graphics);

            int startX =
                (this.Width - 1) -
                _dropDownButton.Bounds.Width -
                (_maxSmallButtonCount * _renderer.SmallButtonWidth);

            int syncSmallButtons = 0;

            _dropDownButton.Bounds = new Rectangle(
                (this.Width - 1) - _renderer.SmallButtonWidth,
                _dockBounds.Y,
                _renderer.SmallButtonWidth,
                _dockBounds.Height
            );

            if (_dropDownButton.Visible)
            {
                PaintButton(_dropDownButton, e.Graphics);
            }

            for (int i = buttonOffset; i < _buttons.Count; i++)
            {
                if (syncSmallButtons == _maxSmallButtonCount)
                {
                    break;
                }

                var button = _buttons[i];

                if (button.Visible)
                {
                    button.Bounds = new Rectangle(
                        startX + (syncSmallButtons * _renderer.SmallButtonWidth),
                        _dockBounds.Y,
                        _renderer.SmallButtonWidth,
                        _dockBounds.Height
                    );

                    button.IsLarge = false;

                    PaintButton(button, e.Graphics);

                    syncSmallButtons++;

                    buttonOffset = i + 1;
                }
            }

            for (int i = buttonOffset; i < _buttons.Count; i++)
            {
                _buttons[i].Bounds = Rectangle.Empty;
            }
        }

        internal ExplorerBarButton SelectedButton
        {
            get { return _selectedButton; }
        }

        private void PaintButton(ExplorerBarButton button, Graphics g)
        {
            _renderer.PerformRenderItemBackground(g, button);

            if (button.IsLarge)
            {
                _renderer.PerformRenderItemText(g, button);

                button.ImageBounds = new Rectangle(
                    10,
                    button.Bounds.Y + (int)Math.Floor((_renderer.ButtonHeight / 2.0) - (ImageDimensionLarge / 2.0)),
                    ImageDimensionLarge,
                    ImageDimensionLarge
                );
            }
            else
            {
                button.ImageBounds = new Rectangle(
                    button.Bounds.X + (int)Math.Floor((button.Bounds.Width / 2.0) - (ImageDimensionSmall / 2.0)),
                    button.Bounds.Y + (int)Math.Floor((button.Bounds.Height / 2.0) - (ImageDimensionSmall / 2.0)),
                    ImageDimensionSmall,
                    ImageDimensionSmall
                );
            }

            _renderer.PerformRenderItemImage(g, button);
        }

        private Rectangle GetBottomContainerRectangle()
        {
            return new Rectangle(0, this.Height - _renderer.ButtonHeight, this.Width, _renderer.ButtonHeight);
        }

        private Rectangle GetGripRectangle()
        {
            return new Rectangle(1, 1, this.Width - 2, _renderer.GripHeight);
        }

        private void CreateContextMenu()
        {
            if (this.ContextMenu != null)
            {
                this.ContextMenu.Show(this, new Point(this.Width, this.Height - (_renderer.ButtonHeight / 2)));
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Rectangle DockBounds
        {
            get { return _dockBounds; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Rectangle GripBounds
        {
            get { return _gripBounds; }
        }

        [Category("Appearance")]
        [DefaultValue(true)]
        public bool ShowDropDownButton
        {
            get { return _dropDownButton.Visible; }
            set
            {
                _dropDownButton.Visible = value;

                Invalidate();
            }
        }
    }
}
