using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SystemEx.Windows.Forms.Internal;

namespace SystemEx.Windows.Forms
{
    public class UserControlEx : System.Windows.Forms.UserControl
    {
        private readonly FormHelper _fixer;

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public new AutoScaleMode AutoScaleMode
        {
            get { return base.AutoScaleMode; }
            set
            {
                // This value is set by the designer. To not have to manually change the
                // defaults set by the designer, it's silently ignored here at runtime.

                if (_fixer.InDesignMode)
                    base.AutoScaleMode = value;
                else
                    base.AutoScaleMode = AutoScaleMode.Dpi;
            }
        }

        public new SizeF AutoScaleDimensions
        {
            get { return base.AutoScaleDimensions; }
            set
            {
                // This value is set by the designer. To not have to manually change the
                // defaults set by the designer, it's silently ignored here at runtime.

                if (_fixer.InDesignMode)
                    base.AutoScaleDimensions = value;
                else
                    base.AutoScaleDimensions = new SizeF(96, 96);
            }
        }

        public event ControlEventHandler FixControl;

        public UserControlEx()
        {
            _fixer = new FormHelper(this);
            _fixer.EnableBoundsTracking = true;
            _fixer.FixControl += (s, e) => OnFixControl(e);
        }

        protected override void SetVisibleCore(bool value)
        {
            _fixer.InitializeForm();

            base.SetVisibleCore(value);
        }

        protected virtual void OnFixControl(ControlEventArgs e)
        {
            FixControl?.Invoke(this, e);
        }
    }
}
