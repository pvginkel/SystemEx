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
        internal FormHelper Fixer { get; }

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

                if (Fixer.InDesignMode)
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

                if (Fixer.InDesignMode)
                    base.AutoScaleDimensions = value;
                else
                    base.AutoScaleDimensions = new SizeF(96, 96);
            }
        }

        public event ControlEventHandler FixControl;

        public UserControlEx()
        {
            Fixer = new FormHelper(this);
            Fixer.EnableBoundsTracking = true;
            Fixer.FixControl += (s, e) => OnFixControl(e);
        }

        protected override void SetVisibleCore(bool value)
        {
            Fixer.InitializeForm();

            base.SetVisibleCore(value);
        }

        protected virtual void OnFixControl(ControlEventArgs e)
        {
            FixControl?.Invoke(this, e);
        }
    }
}
