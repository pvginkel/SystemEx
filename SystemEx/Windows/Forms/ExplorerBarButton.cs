using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing;

namespace SystemEx.Windows.Forms
{
    [DesignTimeVisible(false)]
    [DefaultProperty("Text")]
    public class ExplorerBarButton
    {
        private ExplorerBar _owner;
        private string _text = "";
        private Image _largeImage;
        private Image _smallImage;
        private bool _visible = true;
        private bool _allowed = true;
        private Rectangle _rectangle;
        private bool _isLarge;
        private object _tag;
        private Rectangle _imageBounds = Rectangle.Empty;

        [Category("Action")]
        public event EventHandler Clicked;

        [Category("Appearance")]
        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }

        [DefaultValue(true)]
        [Category("Appearance")]
        public bool Visible
        {
            get { return _visible; }
            set
            {
                _visible = value;

                if (!_visible)
                    _rectangle = Rectangle.Empty;
            }
        }

        [DefaultValue(false)]
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool Selected
        {
            get { return _owner != null && this.Equals(_owner.SelectedButton); }
        }

        [DefaultValue(false)]
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool Checked
        {
            get { return _owner != null && this.Equals(_owner.CheckedButton); }
            set { _owner.CheckedButton = this; }
        }

        [Category("Behavior")]
        [DefaultValue(true)]
        public bool Allowed
        {
            get { return _allowed; }
            set
            {
                _allowed = value;

                if (!_allowed)
                    Visible = false;
            }
        }

        [Category("Appearance")]
        public Image LargeImage
        {
            get { return _largeImage; }
            set { _largeImage = value; }
        }

        [Category("Appearance")]
        public Image SmallImage
        {
            get { return _smallImage; }
            set { _smallImage = value; }
        }

        public override string ToString()
        {
            return _text;
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Rectangle Bounds
        {
            get { return _rectangle; }
            set { _rectangle = value; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        internal ExplorerBar Owner
        {
            get { return _owner; }
            set { _owner = value; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsLarge
        {
            get { return _isLarge; }
            internal set { _isLarge = value; }
        }

        [TypeConverter(typeof(StringConverter))]
        public object Tag
        {
            get { return _tag; }
            set { _tag = value; }
        }

        public ExplorerBarButton()
        {
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Rectangle ImageBounds
        {
            get { return _imageBounds; }
            internal set { _imageBounds = value; }
        }

        protected virtual void OnClicked(EventArgs e)
        {
            if (Clicked != null)
            {
                Clicked(this, e);
            }
        }

        public void PerformClick()
        {
            OnClicked(EventArgs.Empty);
        }

        internal Image GetImage(bool large)
        {
            if (large)
                return _largeImage ?? _smallImage;
            else
                return _smallImage ?? _largeImage;
        }
    }
}
