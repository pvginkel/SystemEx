using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using SystemEx.Windows.Forms.Internal;

namespace SystemEx.Windows.Forms
{
    public class UserControlEx : System.Windows.Forms.UserControl
    {
        private readonly FormHelper _fixer;

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
