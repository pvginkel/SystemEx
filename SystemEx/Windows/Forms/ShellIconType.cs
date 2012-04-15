using System.Text;
using System.Collections.Generic;
using System;

namespace SystemEx.Windows.Forms
{
    [Flags]
    [CLSCompliant(false)]
    public enum ShellIconType : uint
    {
        Icon = Win32.NativeMethods.SHGFI_ICON,
        Large = Win32.NativeMethods.SHGFI_LARGEICON,
        Small = Win32.NativeMethods.SHGFI_SMALLICON,
        UseFileAttributes = Win32.NativeMethods.SHGFI_USEFILEATTRIBUTES,
        ShellIconSize = Win32.NativeMethods.SHGFI_SHELLICONSIZE,
        AddOverlays = Win32.NativeMethods.SHGFI_ADDOVERLAYS,
        Open = Win32.NativeMethods.SHGFI_OPENICON,
        SysIconIndex = Win32.NativeMethods.SHGFI_SYSICONINDEX
    }
}
