using System;
using System.Collections.Generic;
using System.Text;
using SystemEx.Windows.Forms.Internal;

namespace SystemEx.Windows.Forms
{
    public class UserControlEx : System.Windows.Forms.UserControl
    {
        private readonly FormHelper _fixer;

        public UserControlEx()
        {
            _fixer = new FormHelper(this)
            {
                EnableBoundsTracking = false
            };
        }

        protected override void SetVisibleCore(bool value)
        {
            _fixer.InitializeForm();

            base.SetVisibleCore(value);
        }
    }
}
