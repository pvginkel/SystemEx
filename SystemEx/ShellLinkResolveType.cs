using System;
using System.Collections.Generic;
using System.Text;
using SystemEx.Win32;

namespace SystemEx
{
    /// <summary>
    /// Specifies flags to be used when resolving a shell link type.
    /// </summary>
    [Flags]
    [CLSCompliant(false)]
    public enum ShellLinkResolveType : uint
    {
        /// <summary>
        /// Not used.
        /// </summary>
        AnyMatch = NativeMethods.SLR_ANY_MATCH,
        /// <summary>
        /// Call the Windows installer.
        /// </summary>
        InvokeMsi = NativeMethods.SLR_INVOKE_MSI,
        /// <summary>
        /// Disable distributed link tracking. By default, distributed link tracking tracks removable media across multiple devices based on the volume name. It also uses the UNC path to track remote file systems whose drive letter has changed. Setting NoLinkInfo disables both types of tracking.
        /// </summary>
        NoLinkInfo = NativeMethods.SLR_NOLINKINFO,
        /// <summary>
        /// Do not display a dialog box if the link cannot be resolved. When NoUI is set, the high-order word of fFlags can be set to a time-out value that specifies the maximum amount of time to be spent resolving the link. The function returns if the link cannot be resolved within the time-out duration. If the high-order word is set to zero, the time-out duration will be set to the default value of 3,000 milliseconds (3 seconds). To specify a value, set the high word of fFlags to the desired time-out duration, in milliseconds.
        /// </summary>
        NoUI = NativeMethods.SLR_NO_UI,
        /// <summary>
        /// Windows XP and later.
        /// </summary>
        NoUIWithMessagePump = NativeMethods.SLR_NO_UI_WITH_MSG_PUMP,
        /// <summary>
        /// Do not update the link information.
        /// </summary>
        NoUpdate = NativeMethods.SLR_NOUPDATE,
        /// <summary>
        /// Do not execute the search heuristics.
        /// </summary>
        NoSearch = NativeMethods.SLR_NOSEARCH,
        /// <summary>
        /// Do not use distributed link tracking.
        /// </summary>
        NoTrack = NativeMethods.SLR_NOTRACK,
        /// <summary>
        /// Windows 7 and later. Update the computer GUID and user SID if necessary.
        /// </summary>
        Update = NativeMethods.SLR_UPDATE
    }
}
