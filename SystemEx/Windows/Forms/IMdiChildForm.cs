using System;
using System.Collections.Generic;
using System.Text;

namespace SystemEx.Windows.Forms
{
    public interface IMdiChildForm
    {
        System.Windows.Forms.Form MdiOwner { get; }
    }
}
