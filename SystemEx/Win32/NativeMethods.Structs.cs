using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using SystemEx.Windows.Forms;

namespace SystemEx.Win32
{
    partial class NativeMethods
    {
        [StructLayout(LayoutKind.Sequential, Pack = 4, Size = 0, CharSet = CharSet.Unicode)]
        public struct _WIN32_FIND_DATAW
        {
            public uint dwFileAttributes;
            public _FILETIME ftCreationTime;
            public _FILETIME ftLastAccessTime;
            public _FILETIME ftLastWriteTime;
            public uint nFileSizeHigh;
            public uint nFileSizeLow;
            public uint dwReserved0;
            public uint dwReserved1;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)] // MAX_PATH
            public string cFileName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 14)]
            public string cAlternateFileName;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 4, Size = 0, CharSet = CharSet.Ansi)]
        public struct _WIN32_FIND_DATAA
        {
            public uint dwFileAttributes;
            public _FILETIME ftCreationTime;
            public _FILETIME ftLastAccessTime;
            public _FILETIME ftLastWriteTime;
            public uint nFileSizeHigh;
            public uint nFileSizeLow;
            public uint dwReserved0;
            public uint dwReserved1;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)] // MAX_PATH
            public string cFileName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 14)]
            public string cAlternateFileName;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 4, Size = 0)]
        public struct _FILETIME
        {
            public uint dwLowDateTime;
            public uint dwHighDateTime;
        }

        /// <summary>
        /// TASKDIALOGCONFIG taken from commctl.h.
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
        public struct TASKDIALOGCONFIG
        {
            /// <summary>
            /// Size of the structure in bytes.
            /// </summary>
            public uint cbSize;

            /// <summary>
            /// Parent window handle.
            /// </summary>
            [SuppressMessage("Microsoft.Reliability", "CA2006:UseSafeHandleToEncapsulateNativeResources")] // Managed code owns actual resource. Passed to native in syncronous call. No lifetime issues.
            public IntPtr hwndParent;

            /// <summary>
            /// Module instance handle for resources.
            /// </summary>
            [SuppressMessage("Microsoft.Reliability", "CA2006:UseSafeHandleToEncapsulateNativeResources")] // Managed code owns actual resource. Passed to native in syncronous call. No lifetime issues.
            public IntPtr hInstance;

            /// <summary>
            /// Flags.
            /// </summary>
            public TASKDIALOG_FLAGS dwFlags;            // TASKDIALOG_FLAGS (TDF_XXX) flags

            /// <summary>
            /// Bit flags for commonly used buttons.
            /// </summary>
            public TaskDialogCommonButtons dwCommonButtons;    // TASKDIALOG_COMMON_BUTTON (TDCBF_XXX) flags

            /// <summary>
            /// Window title.
            /// </summary>
            [MarshalAs(UnmanagedType.LPWStr)]
            public string pszWindowTitle;                         // string or MAKEINTRESOURCE()

            /// <summary>
            /// The Main icon. Overloaded member. Can be string, a handle, a special value or a resource ID.
            /// </summary>
            [SuppressMessage("Microsoft.Reliability", "CA2006:UseSafeHandleToEncapsulateNativeResources")] // Managed code owns actual resource. Passed to native in syncronous call. No lifetime issues.
            public IntPtr MainIcon;

            /// <summary>
            /// Main Instruction.
            /// </summary>
            [MarshalAs(UnmanagedType.LPWStr)]
            public string pszMainInstruction;

            /// <summary>
            /// Content.
            /// </summary>
            [MarshalAs(UnmanagedType.LPWStr)]
            public string pszContent;

            /// <summary>
            /// Count of custom Buttons.
            /// </summary>
            public uint cButtons;

            /// <summary>
            /// Array of custom buttons.
            /// </summary>
            [SuppressMessage("Microsoft.Reliability", "CA2006:UseSafeHandleToEncapsulateNativeResources")] // Managed code owns actual resource. Passed to native in syncronous call. No lifetime issues.
            public IntPtr pButtons;

            /// <summary>
            /// ID of default button.
            /// </summary>
            public int nDefaultButton;

            /// <summary>
            /// Count of radio Buttons.
            /// </summary>
            public uint cRadioButtons;

            /// <summary>
            /// Array of radio buttons.
            /// </summary>
            [SuppressMessage("Microsoft.Reliability", "CA2006:UseSafeHandleToEncapsulateNativeResources")] // Managed code owns actual resource. Passed to native in syncronous call. No lifetime issues.
            public IntPtr pRadioButtons;

            /// <summary>
            /// ID of default radio button.
            /// </summary>
            public int nDefaultRadioButton;

            /// <summary>
            /// Text for verification check box. often "Don't ask be again".
            /// </summary>
            [MarshalAs(UnmanagedType.LPWStr)]
            public string pszVerificationText;

            /// <summary>
            /// Expanded Information.
            /// </summary>
            [MarshalAs(UnmanagedType.LPWStr)]
            public string pszExpandedInformation;

            /// <summary>
            /// Text for expanded control.
            /// </summary>
            [MarshalAs(UnmanagedType.LPWStr)]
            public string pszExpandedControlText;

            /// <summary>
            /// Text for expanded control.
            /// </summary>
            [MarshalAs(UnmanagedType.LPWStr)]
            public string pszCollapsedControlText;

            /// <summary>
            /// Icon for the footer. An overloaded member link MainIcon.
            /// </summary>
            [SuppressMessage("Microsoft.Reliability", "CA2006:UseSafeHandleToEncapsulateNativeResources")] // Managed code owns actual resource. Passed to native in syncronous call. No lifetime issues.
            public IntPtr FooterIcon;

            /// <summary>
            /// Footer Text.
            /// </summary>
            [MarshalAs(UnmanagedType.LPWStr)]
            public string pszFooter;

            /// <summary>
            /// Function pointer for callback.
            /// </summary>
            public TaskDialogCallback pfCallback;

            /// <summary>
            /// Data that will be passed to the call back.
            /// </summary>
            [SuppressMessage("Microsoft.Reliability", "CA2006:UseSafeHandleToEncapsulateNativeResources")] // Managed code owns actual resource. Passed to native in syncronous call. No lifetime issues.
            public IntPtr lpCallbackData;

            /// <summary>
            /// Width of the Task Dialog's area in DLU's.
            /// </summary>
            public uint cxWidth;                                // width of the Task Dialog's client area in DLU's. If 0, Task Dialog will calculate the ideal width.
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;

            public static implicit operator Rectangle(RECT other)
            {
                return new Rectangle(
                    other.left,
                    other.top,
                    other.right - other.left,
                    other.bottom - other.top
                );
            }

            public static implicit operator RECT(Rectangle other)
            {
                return new RECT
                {
                    left = other.Left,
                    top = other.Top,
                    right = other.Right,
                    bottom = other.Bottom
                };
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int x;
            public int y;

            public POINT(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public sealed class POINTL
        {
            public int x;
            public int y;
        }

        [StructLayout(LayoutKind.Sequential)]
        public sealed class SIZEL
        {
            public int cx;
            public int cy;
        }

        public struct ACTCTX
        {
            public int cbSize;
            public uint dwFlags;
            public string lpSource;
            public ushort wProcessorArchitecture;
            public ushort wLangId;
            public string lpAssemblyDirectory;
            public string lpResourceName;
            public string lpApplicationName;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct LASTINPUTINFO
        {
            public uint cbSize;
            public uint dwTime;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public class BROWSEINFO
        {
            public IntPtr hwndOwner;
            public IntPtr pidlRoot;
            public IntPtr pszDisplayName;
            public string lpszTitle;
            public uint ulFlags;
            public IntPtr lpfn;
            public IntPtr lParam;
            public int iImage;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public sealed class FILEGROUPDESCRIPTORA
        {
            public uint cItems;
            public FILEDESCRIPTORA[] fgd;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public sealed class FILEDESCRIPTORA
        {
            public uint dwFlags;
            public Guid clsid;
            public SIZEL sizel;
            public POINTL pointl;
            public uint dwFileAttributes;
            public System.Runtime.InteropServices.ComTypes.FILETIME ftCreationTime;
            public System.Runtime.InteropServices.ComTypes.FILETIME ftLastAccessTime;
            public System.Runtime.InteropServices.ComTypes.FILETIME ftLastWriteTime;
            public uint nFileSizeHigh;
            public uint nFileSizeLow;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string cFileName;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public sealed class FILEGROUPDESCRIPTORW
        {
            public uint cItems;
            public FILEDESCRIPTORW[] fgd;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public sealed class FILEDESCRIPTORW
        {
            public uint dwFlags;
            public Guid clsid;
            public SIZEL sizel;
            public POINTL pointl;
            public uint dwFileAttributes;
            public System.Runtime.InteropServices.ComTypes.FILETIME ftCreationTime;
            public System.Runtime.InteropServices.ComTypes.FILETIME ftLastAccessTime;
            public System.Runtime.InteropServices.ComTypes.FILETIME ftLastWriteTime;
            public uint nFileSizeHigh;
            public uint nFileSizeLow;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string cFileName;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct APPBARDATA
        {
            public int cbSize;
            public IntPtr hWnd;
            public int uCallbackMessage;
            public int uEdge;
            public RECT rc;
            public IntPtr lParam;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 4, Size = 0, CharSet = CharSet.Auto)]
        public struct WIN32_FIND_DATA
        {
            public uint dwFileAttributes;
            public System.Runtime.InteropServices.ComTypes.FILETIME ftCreationTime;
            public System.Runtime.InteropServices.ComTypes.FILETIME ftLastAccessTime;
            public System.Runtime.InteropServices.ComTypes.FILETIME ftLastWriteTime;
            public uint nFileSizeHigh;
            public uint nFileSizeLow;
            public uint dwReserved0;
            public uint dwReserved1;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)] // MAX_PATH
            public string cFileName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 14)]
            public string cAlternateFileName;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct SHFILEINFO
        {
            public IntPtr hIcon;
            public int iIcon;
            public uint dwAttributes;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szDisplayName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
            public string szTypeName;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NMHDR
        {
            public IntPtr hwndFrom;
            public IntPtr idFrom;
            public int code;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct NCCALCSIZE_PARAMS
        {
            public RECT rgrc1;
            public RECT rgrc2;
            public RECT rgrc3;
            public IntPtr lppos;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct FLASHWINFO
        {
            public uint cbSize;
            public IntPtr hWnd;
            public uint dwFlags;
            public uint uCount;
            public uint dwTimeout;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct COPYDATASTRUCT
        {
            public IntPtr dwData;
            public int cbData;
            public IntPtr lpData;
        }
    }
}
