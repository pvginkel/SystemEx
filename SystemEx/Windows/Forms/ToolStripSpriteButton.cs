using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;

namespace SystemEx.Windows.Forms
{
    public class ToolStripSpriteButton : ToolStripButton
    {
        private Image _selectedImage = null;
        private Image _pressedImage = null;
        private Image _disabledImage = null;

        [Category("Appearance")]
        public Image SelectedImage
        {
            get { return _selectedImage; }
            set { _selectedImage = value; }
        }

        [Category("Appearance")]
        public Image PressedImage
        {
            get { return _pressedImage; }
            set { _pressedImage = value; }
        }

        [Category("Appearance")]
        public Image DisabledImage
        {
            get { return _disabledImage; }
            set { _disabledImage = value; }
        }
    }
}
