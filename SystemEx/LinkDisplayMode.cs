using System;
using System.Collections.Generic;
using System.Text;

namespace SystemEx
{
    /// <summary>
    /// The initial display mode used when a shortcut is executed.
    /// </summary>
    [CLSCompliant(false)]
    public enum LinkDisplayMode : uint
    {
        SW_HIDE = 0,
        SW_SHOWNORMAL = 1,
        SW_NORMAL = 1,
        SW_SHOWMINIMIZED = 2,
        SW_SHOWMAXIMIZED = 3,
        SW_MAXIMIZE = 3,
        SW_SHOWNOACTIVATE = 4,
        SW_SHOW = 5,
        SW_MINIMIZE = 6,
        SW_SHOWMINNOACTIVE = 7,
        SW_SHOWNA = 8,
        SW_RESTORE = 9,
        SW_SHOWDEFAULT = 10,
        SW_MAX = 10
    }
}
