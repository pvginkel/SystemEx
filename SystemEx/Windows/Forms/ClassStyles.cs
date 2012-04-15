using System;
using System.Collections.Generic;
using System.Text;

namespace SystemEx.Windows.Forms
{
    [Flags]
    public enum ClassStyles
    {
        ByteAlignClient = 0x1000,
        ByteAllignWindow = 0x2000,
        ClassDc = 0x0040,
        DoubleClicks = 0x0008,
        DropShadow = 0x00020000,
        GlobalClass = 0x4000,
        RedrawOnHorizontalResize = 0x0002,
        NoCloseButton = 0x0200,
        OwnDc = 0x0020,
        ParentDc = 0x0080,
        SaveBits = 0x0800,
        RedrawOnVerticalResize = 0x0001
    }
}
