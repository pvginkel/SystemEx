using System;
using System.Collections.Generic;
using System.Text;

namespace SystemEx.Win32
{
    partial class NativeMethods
    {
        /// <summary>
        /// Allow any match during resolution.  Has no effect
        /// on ME/2000 or above, use the other flags instead.
        /// </summary>
        public const uint SLR_ANY_MATCH = 0x2;
        /// <summary>
        /// Call the Microsoft Windows Installer. 
        /// </summary>
        public const uint SLR_INVOKE_MSI = 0x80;
        /// <summary>
        /// Disable distributed link tracking. By default, 
        /// distributed link tracking tracks removable media 
        /// across multiple devices based on the volume name. 
        /// It also uses the UNC path to track remote file 
        /// systems whose drive letter has changed. Setting 
        /// SLR_NOLINKINFO disables both types of tracking.
        /// </summary>
        public const uint SLR_NOLINKINFO = 0x40;
        /// <summary>
        /// Do not display a dialog box if the link cannot be resolved. 
        /// When SLR_NO_UI is set, a time-out value that specifies the 
        /// maximum amount of time to be spent resolving the link can 
        /// be specified in milliseconds. The function returns if the 
        /// link cannot be resolved within the time-out duration. 
        /// If the timeout is not set, the time-out duration will be 
        /// set to the default value of 3,000 milliseconds (3 seconds). 
        /// </summary>										    
        public const uint SLR_NO_UI = 0x1;
        /// <summary>
        /// Not documented in SDK.  Assume same as SLR_NO_UI but 
        /// intended for applications without a hWnd.
        /// </summary>
        public const uint SLR_NO_UI_WITH_MSG_PUMP = 0x101;
        /// <summary>
        /// Do not update the link information. 
        /// </summary>
        public const uint SLR_NOUPDATE = 0x8;
        /// <summary>
        /// Do not execute the search heuristics. 
        /// </summary>																																																																																																																																																																																																														
        public const uint SLR_NOSEARCH = 0x10;
        /// <summary>
        /// Do not use distributed link tracking. 
        /// </summary>
        public const uint SLR_NOTRACK = 0x20;
        /// <summary>
        /// If the link object has changed, update its path and list 
        /// of identifiers. If SLR_UPDATE is set, you do not need to 
        /// call IPersistFile::IsDirty to determine whether or not 
        /// the link object has changed. 
        /// </summary>
        public const uint SLR_UPDATE = 0x4;

        /// <summary>
        /// TASKDIALOG_FLAGS taken from CommCtrl.h.
        /// </summary>
        [Flags]
        public enum TASKDIALOG_FLAGS
        {
            /// <summary>
            /// Enable hyperlinks.
            /// </summary>
            TDF_ENABLE_HYPERLINKS = 0x0001,

            /// <summary>
            /// Use icon handle for main icon.
            /// </summary>
            TDF_USE_HICON_MAIN = 0x0002,

            /// <summary>
            /// Use icon handle for footer icon.
            /// </summary>
            TDF_USE_HICON_FOOTER = 0x0004,

            /// <summary>
            /// Allow dialog to be cancelled, even if there is no cancel button.
            /// </summary>
            TDF_ALLOW_DIALOG_CANCELLATION = 0x0008,

            /// <summary>
            /// Use command links rather than buttons.
            /// </summary>
            TDF_USE_COMMAND_LINKS = 0x0010,

            /// <summary>
            /// Use command links with no icons rather than buttons.
            /// </summary>
            TDF_USE_COMMAND_LINKS_NO_ICON = 0x0020,

            /// <summary>
            /// Show expanded info in the footer area.
            /// </summary>
            TDF_EXPAND_FOOTER_AREA = 0x0040,

            /// <summary>
            /// Expand by default.
            /// </summary>
            TDF_EXPANDED_BY_DEFAULT = 0x0080,

            /// <summary>
            /// Start with verification flag already checked.
            /// </summary>
            TDF_VERIFICATION_FLAG_CHECKED = 0x0100,

            /// <summary>
            /// Show a progress bar.
            /// </summary>
            TDF_SHOW_PROGRESS_BAR = 0x0200,

            /// <summary>
            /// Show a marquee progress bar.
            /// </summary>
            TDF_SHOW_MARQUEE_PROGRESS_BAR = 0x0400,

            /// <summary>
            /// Callback every 200 milliseconds.
            /// </summary>
            TDF_CALLBACK_TIMER = 0x0800,

            /// <summary>
            /// Center the dialog on the owner window rather than the monitor.
            /// </summary>
            TDF_POSITION_RELATIVE_TO_WINDOW = 0x1000,

            /// <summary>
            /// Right to Left Layout.
            /// </summary>
            TDF_RTL_LAYOUT = 0x2000,

            /// <summary>
            /// No default radio button.
            /// </summary>
            TDF_NO_DEFAULT_RADIO_BUTTON = 0x4000,

            /// <summary>
            /// Task Dialog can be minimized.
            /// </summary>
            TDF_CAN_BE_MINIMIZED = 0x8000
        }

        /// <summary>
        /// TASKDIALOG_ELEMENTS taken from CommCtrl.h
        /// </summary>
        public enum TASKDIALOG_ELEMENTS
        {
            /// <summary>
            /// The content element.
            /// </summary>
            TDE_CONTENT,

            /// <summary>
            /// Expanded Information.
            /// </summary>
            TDE_EXPANDED_INFORMATION,

            /// <summary>
            /// Footer.
            /// </summary>
            TDE_FOOTER,

            /// <summary>
            /// Main Instructions
            /// </summary>
            TDE_MAIN_INSTRUCTION
        }

        /// <summary>
        /// TASKDIALOG_ICON_ELEMENTS taken from CommCtrl.h
        /// </summary>
        public enum TASKDIALOG_ICON_ELEMENTS
        {
            /// <summary>
            /// Main instruction icon.
            /// </summary>
            TDIE_ICON_MAIN,

            /// <summary>
            /// Footer icon.
            /// </summary>
            TDIE_ICON_FOOTER
        }

        /// <summary>
        /// TASKDIALOG_MESSAGES taken from CommCtrl.h.
        /// </summary>
        public enum TASKDIALOG_MESSAGES : uint
        {
            // Spec is not clear on what this is for.
            ///// <summary>
            ///// Navigate page.
            ///// </summary>
            ////TDM_NAVIGATE_PAGE = WM_USER + 101,

            /// <summary>
            /// Click button.
            /// </summary>
            TDM_CLICK_BUTTON = WM_USER + 102, // wParam = Button ID

            /// <summary>
            /// Set Progress bar to be marquee mode.
            /// </summary>
            TDM_SET_MARQUEE_PROGRESS_BAR = WM_USER + 103, // wParam = 0 (nonMarque) wParam != 0 (Marquee)

            /// <summary>
            /// Set Progress bar state.
            /// </summary>
            TDM_SET_PROGRESS_BAR_STATE = WM_USER + 104, // wParam = new progress state

            /// <summary>
            /// Set progress bar range.
            /// </summary>
            TDM_SET_PROGRESS_BAR_RANGE = WM_USER + 105, // lParam = MAKELPARAM(nMinRange, nMaxRange)

            /// <summary>
            /// Set progress bar position.
            /// </summary>
            TDM_SET_PROGRESS_BAR_POS = WM_USER + 106, // wParam = new position

            /// <summary>
            /// Set progress bar marquee (animation).
            /// </summary>
            TDM_SET_PROGRESS_BAR_MARQUEE = WM_USER + 107, // wParam = 0 (stop marquee), wParam != 0 (start marquee), lparam = speed (milliseconds between repaints)

            /// <summary>
            /// Set a text element of the Task Dialog.
            /// </summary>
            TDM_SET_ELEMENT_TEXT = WM_USER + 108, // wParam = element (TASKDIALOG_ELEMENTS), lParam = new element text (LPCWSTR)

            /// <summary>
            /// Click a radio button.
            /// </summary>
            TDM_CLICK_RADIO_BUTTON = WM_USER + 110, // wParam = Radio Button ID

            /// <summary>
            /// Enable or disable a button.
            /// </summary>
            TDM_ENABLE_BUTTON = WM_USER + 111, // lParam = 0 (disable), lParam != 0 (enable), wParam = Button ID

            /// <summary>
            /// Enable or disable a radio button.
            /// </summary>
            TDM_ENABLE_RADIO_BUTTON = WM_USER + 112, // lParam = 0 (disable), lParam != 0 (enable), wParam = Radio Button ID

            /// <summary>
            /// Check or uncheck the verfication checkbox.
            /// </summary>
            TDM_CLICK_VERIFICATION = WM_USER + 113, // wParam = 0 (unchecked), 1 (checked), lParam = 1 (set key focus)

            /// <summary>
            /// Update the text of an element (no effect if origially set as null).
            /// </summary>
            TDM_UPDATE_ELEMENT_TEXT = WM_USER + 114, // wParam = element (TASKDIALOG_ELEMENTS), lParam = new element text (LPCWSTR)

            /// <summary>
            /// Designate whether a given Task Dialog button or command link should have a User Account Control (UAC) shield icon.
            /// </summary>
            TDM_SET_BUTTON_ELEVATION_REQUIRED_STATE = WM_USER + 115, // wParam = Button ID, lParam = 0 (elevation not required), lParam != 0 (elevation required)

            /// <summary>
            /// Refreshes the icon of the task dialog.
            /// </summary>
            TDM_UPDATE_ICON = WM_USER + 116  // wParam = icon element (TASKDIALOG_ICON_ELEMENTS), lParam = new icon (hIcon if TDF_USE_HICON_* was set, PCWSTR otherwise)
        }

        public enum CombineRgnStyles : int
        {
            And = 1,
            Or = 2,
            XOr = 3,
            Difference = 4,
            Copy = 5,
            Minimal = And,
            Maximal = Copy
        }

        public const Int32 GMEM_MOVEABLE = 0x0002;
        public const Int32 GMEM_ZEROINIT = 0x0040;
        public const Int32 GHND = (GMEM_MOVEABLE | GMEM_ZEROINIT);
        public const Int32 GMEM_DDESHARE = 0x2000;

        public const int ACTCTX_FLAG_ASSEMBLY_DIRECTORY_VALID = 0x004;

        // IDataObject constants
        public const Int32 DV_E_TYMED = unchecked((Int32)0x80040069);

        // Clipboard formats used for cut/copy/drag operations
        public const string CFSTR_PREFERREDDROPEFFECT = "Preferred DropEffect";
        public const string CFSTR_PERFORMEDDROPEFFECT = "Performed DropEffect";
        public const string CFSTR_FILEDESCRIPTORW = "FileGroupDescriptorW";
        public const string CFSTR_FILECONTENTS = "FileContents";

        [Flags]
        public enum SHELLFLAGSTATE : long
        {
            fShowAllObjects = 0x00000001,
            fShowExtensions = 0x00000002,
            fNoConfirmRecycle = 0x00000004,
            fShowSysFiles = 0x00000008,
            fShowCompColor = 0x00000010,
            fDoubleClickInWebView = 0x00000020,
            fDesktopHTML = 0x00000040,
            fWin95Classic = 0x00000080,
            fDontPrettyPath = 0x00000100,
            fShowAttribCol = 0x00000200,
            fMapNetDrvBtn = 0x00000400,
            fShowInfoTip = 0x00000800,
            fHideIcons = 0x00001000,
        }

        public enum CSIDL
        {
            CSIDL_DESKTOP = 0x0000,    // <desktop>
            CSIDL_INTERNET = 0x0001,    // Internet Explorer (icon on desktop)
            CSIDL_PROGRAMS = 0x0002,    // Start Menu\Programs
            CSIDL_CONTROLS = 0x0003,    // My Computer\Control Panel
            CSIDL_PRINTERS = 0x0004,    // My Computer\Printers
            CSIDL_PERSONAL = 0x0005,    // My Documents
            CSIDL_FAVORITES = 0x0006,    // <user name>\Favorites
            CSIDL_STARTUP = 0x0007,    // Start Menu\Programs\Startup
            CSIDL_RECENT = 0x0008,    // <user name>\Recent
            CSIDL_SENDTO = 0x0009,    // <user name>\SendTo
            CSIDL_BITBUCKET = 0x000a,    // <desktop>\Recycle Bin
            CSIDL_STARTMENU = 0x000b,    // <user name>\Start Menu
            CSIDL_MYDOCUMENTS = 0x000c,    // logical "My Documents" desktop icon
            CSIDL_MYMUSIC = 0x000d,    // "My Music" folder
            CSIDL_MYVIDEO = 0x000e,    // "My Videos" folder
            CSIDL_DESKTOPDIRECTORY = 0x0010,    // <user name>\Desktop
            CSIDL_DRIVES = 0x0011,    // My Computer
            CSIDL_NETWORK = 0x0012,    // Network Neighborhood (My Network Places)
            CSIDL_NETHOOD = 0x0013,    // <user name>\nethood
            CSIDL_FONTS = 0x0014,    // windows\fonts
            CSIDL_TEMPLATES = 0x0015,
            CSIDL_COMMON_STARTMENU = 0x0016,    // All Users\Start Menu
            CSIDL_COMMON_PROGRAMS = 0X0017,    // All Users\Start Menu\Programs
            CSIDL_COMMON_STARTUP = 0x0018,    // All Users\Startup
            CSIDL_COMMON_DESKTOPDIRECTORY = 0x0019,    // All Users\Desktop
            CSIDL_APPDATA = 0x001a,    // <user name>\Application Data
            CSIDL_PRINTHOOD = 0x001b,    // <user name>\PrintHood

            CSIDL_LOCAL_APPDATA = 0x001c,    // <user name>\Local Settings\Applicaiton Data (non roaming)

            CSIDL_ALTSTARTUP = 0x001d,    // non localized startup
            CSIDL_COMMON_ALTSTARTUP = 0x001e,    // non localized common startup
            CSIDL_COMMON_FAVORITES = 0x001f,

            CSIDL_INTERNET_CACHE = 0x0020,
            CSIDL_COOKIES = 0x0021,
            CSIDL_HISTORY = 0x0022,
            CSIDL_COMMON_APPDATA = 0x0023,    // All Users\Application Data
            CSIDL_WINDOWS = 0x0024,    // GetWindowsDirectory()
            CSIDL_SYSTEM = 0x0025,    // GetSystemDirectory()
            CSIDL_PROGRAM_FILES = 0x0026,    // C:\Program Files
            CSIDL_MYPICTURES = 0x0027,    // C:\Program Files\My Pictures

            CSIDL_PROFILE = 0x0028,    // USERPROFILE
            CSIDL_SYSTEMX86 = 0x0029,    // x86 system directory on RISC
            CSIDL_PROGRAM_FILESX86 = 0x002a,    // x86 C:\Program Files on RISC

            CSIDL_PROGRAM_FILES_COMMON = 0x002b,    // C:\Program Files\Common

            CSIDL_PROGRAM_FILES_COMMONX86 = 0x002c,    // x86 Program Files\Common on RISC
            CSIDL_COMMON_TEMPLATES = 0x002d,    // All Users\Templates

            CSIDL_COMMON_DOCUMENTS = 0x002e,    // All Users\Documents
            CSIDL_COMMON_ADMINTOOLS = 0x002f,    // All Users\Start Menu\Programs\Administrative Tools
            CSIDL_ADMINTOOLS = 0x0030,    // <user name>\Start Menu\Programs\Administrative Tools

            CSIDL_CONNECTIONS = 0x0031,    // Network and Dial-up Connections
            CSIDL_COMMON_MUSIC = 0x0035,    // All Users\My Music
            CSIDL_COMMON_PICTURES = 0x0036,    // All Users\My Pictures
            CSIDL_COMMON_VIDEO = 0x0037,    // All Users\My Video

            CSIDL_CDBURN_AREA = 0x003b    // USERPROFILE\Local Settings\Application Data\Microsoft\CD Burning
        }

        public const int ABM_NEW = 0;
        public const int ABM_REMOVE = 1;
        public const int ABM_QUERYPOS = 2;
        public const int ABM_SETPOS = 3;
        public const int ABM_GETSTATE = 4;
        public const int ABM_GETTASKBARPOS = 5;
        public const int ABM_ACTIVATE = 6;
        public const int ABM_GETAUTOHIDEBAR = 7;
        public const int ABM_SETAUTOHIDEBAR = 8;
        public const int ABM_WINDOWPOSCHANGED = 9;
        public const int ABM_SETSTATE = 10;

        public const int ABN_STATECHANGE = 0;
        public const int ABN_POSCHANGED = 1;
        public const int ABN_FULLSCREENAPP = 2;
        public const int ABN_WINDOWARRANGE = 3;

        public const int ABE_LEFT = 0;
        public const int ABE_TOP = 1;
        public const int ABE_RIGHT = 2;
        public const int ABE_BOTTOM = 3;

        public enum TextBoxMargin
        {
            LeftMargin,
            RightMargin
        }

        public const int EM_SETMARGINS = 0xd3;
        public const int EM_GETMARGINS = 0xd4;

        public const int EC_USEFONTINFO = 0xffff;
        public const int EC_LEFTMARGIN = 0x1;
        public const int EC_RIGHTMARGIN = 0x2;

        public const int GWL_STYLE = (-16);
        public const int GWL_EXSTYLE = (-20);

        public const int WS_OVERLAPPED = 0x00000000;
        public const int WS_POPUP = unchecked((int)0x80000000);
        public const int WS_CHILD = 0x40000000;
        public const int WS_MINIMIZE = 0x20000000;
        public const int WS_VISIBLE = 0x10000000;
        public const int WS_DISABLED = 0x08000000;
        public const int WS_CLIPSIBLINGS = 0x04000000;
        public const int WS_CLIPCHILDREN = 0x02000000;
        public const int WS_MAXIMIZE = 0x01000000;
        public const int WS_CAPTION = 0x00C00000;
        public const int WS_BORDER = 0x00800000;
        public const int WS_DLGFRAME = 0x00400000;
        public const int WS_VSCROLL = 0x00200000;
        public const int WS_HSCROLL = 0x00100000;
        public const int WS_SYSMENU = 0x00080000;
        public const int WS_THICKFRAME = 0x00040000;
        public const int WS_GROUP = 0x00020000;
        public const int WS_TABSTOP = 0x00010000;

        public const int WS_MINIMIZEBOX = 0x00020000;
        public const int WS_MAXIMIZEBOX = 0x00010000;

        public const int WS_EX_DLGMODALFRAME = 0x00000001;
        public const int WS_EX_NOPARENTNOTIFY = 0x00000004;
        public const int WS_EX_TOPMOST = 0x00000008;
        public const int WS_EX_ACCEPTFILES = 0x00000010;
        public const int WS_EX_TRANSPARENT = 0x00000020;
        public const int WS_EX_MDICHILD = 0x00000040;
        public const int WS_EX_TOOLWINDOW = 0x00000080;
        public const int WS_EX_WINDOWEDGE = 0x00000100;
        public const int WS_EX_CLIENTEDGE = 0x00000200;
        public const int WS_EX_CONTEXTHELP = 0x00000400;

        public const int WS_EX_RIGHT = 0x00001000;
        public const int WS_EX_LEFT = 0x00000000;
        public const int WS_EX_RTLREADING = 0x00002000;
        public const int WS_EX_LTRREADING = 0x00000000;
        public const int WS_EX_LEFTSCROLLBAR = 0x00004000;
        public const int WS_EX_RIGHTSCROLLBAR = 0x00000000;

        public const int WS_EX_CONTROLPARENT = 0x00010000;
        public const int WS_EX_STATICEDGE = 0x00020000;
        public const int WS_EX_APPWINDOW = 0x00040000;

        public const int WS_EX_LAYERED = 0x00080000;

        public const int WS_EX_NOINHERITLAYOUT = 0x00100000;
        public const int WS_EX_LAYOUTRTL = 0x00400000;

        public const int WS_EX_COMPOSITED = 0x02000000;
        public const int WS_EX_NOACTIVATE = 0x08000000;

        public const int CS_DROPSHADOW = 0x20000;

        public const uint BIF_RETURNONLYFSDIRS = 0x0001;
        public const uint BIF_DONTGOBELOWDOMAIN = 0x0002;
        public const uint BIF_STATUSTEXT = 0x0004;
        public const uint BIF_RETURNFSANCESTORS = 0x0008;
        public const uint BIF_EDITBOX = 0x0010;
        public const uint BIF_VALIDATE = 0x0020;
        public const uint BIF_NEWDIALOGSTYLE = 0x0040;
        public const uint BIF_USENEWUI = (BIF_NEWDIALOGSTYLE | BIF_EDITBOX);
        public const uint BIF_BROWSEINCLUDEURLS = 0x0080;
        public const uint BIF_UAHINT = 0x0100;
        public const uint BIF_NONEWFOLDERBUTTON = 0x0200;
        public const uint BIF_NOTRANSLATETARGETS = 0x0400;
        public const uint BIF_BROWSEFORCOMPUTER = 0x1000;
        public const uint BIF_BROWSEFORPRINTER = 0x2000;
        public const uint BIF_BROWSEINCLUDEFILES = 0x4000;
        public const uint BIF_SHAREABLE = 0x8000;

        public const uint SHGFI_LARGEICON = 0x0;
        public const uint SHGFI_SMALLICON = 0x1;
        public const uint SHGFI_OPENICON = 0x2;
        public const uint SHGFI_SHELLICONSIZE = 0x4;
        public const uint SHGFI_USEFILEATTRIBUTES = 0x10;
        public const uint SHGFI_ADDOVERLAYS = 0x20;
        public const uint SHGFI_ICON = 0x100;
        public const uint SHGFI_SYSICONINDEX = 0x4000;
        public const uint USEFILEATTRIBUTES = SHGFI_USEFILEATTRIBUTES;

        public const uint SSF_SHOWALLOBJECTS = 0x00000001;
        public const uint SSF_SHOWEXTENSIONS = 0x00000002;
        public const uint SSF_SHOWCOMPCOLOR = 0x00000008;
        public const uint SSF_SHOWSYSFILES = 0x00000020;
        public const uint SSF_DOUBLECLICKINWEBVIEW = 0x00000080;
        public const uint SSF_SHOWATTRIBCOL = 0x00000100;
        public const uint SSF_DESKTOPHTML = 0x00000200;
        public const uint SSF_WIN95CLASSIC = 0x00000400;
        public const uint SSF_DONTPRETTYPATH = 0x00000800;
        public const uint SSF_SHOWINFOTIP = 0x00002000;
        public const uint SSF_MAPNETDRVBUTTON = 0x00001000;
        public const uint SSF_NOCONFIRMRECYCLE = 0x00008000;
        public const uint SSF_HIDEICONS = 0x00004000;

        public const uint SHCNE_ALLEVENTS = 0x7FFFFFFF;
        public const uint SHCNE_ASSOCCHANGED = 0x08000000;
        public const uint SHCNE_ATTRIBUTES = 0x00000800;
        public const uint SHCNE_CREATE = 0x00000002;
        public const uint SHCNE_DELETE = 0x00000004;
        public const uint SHCNE_DRIVEADD = 0x00000100;
        public const uint SHCNE_DRIVEADDGUI = 0x00010000;
        public const uint SHCNE_DRIVEREMOVED = 0x00000080;
        public const uint SHCNE_EXTENDED_EVENT = 0x04000000;
        public const uint SHCNE_FREESPACE = 0x00040000;
        public const uint SHCNE_MEDIAINSERTED = 0x00000020;
        public const uint SHCNE_MEDIAREMOVED = 0x00000040;
        public const uint SHCNE_MKDIR = 0x00000008;
        public const uint SHCNE_NETSHARE = 0x00000200;
        public const uint SHCNE_NETUNSHARE = 0x00000400;
        public const uint SHCNE_RENAMEFOLDER = 0x00020000;
        public const uint SHCNE_RENAMEITEM = 0x00000001;
        public const uint SHCNE_RMDIR = 0x00000010;
        public const uint SHCNE_SERVERDISCONNECT = 0x00004000;
        public const uint SHCNE_UPDATEDIR = 0x00001000;
        public const uint SHCNE_UPDATEIMAGE = 0x00008000;

        public const uint SHCNF_DWORD = 0x0003;
        public const uint SHCNF_IDLIST = 0x0000;
        public const uint SHCNF_PATHA = 0x0001;
        public const uint SHCNF_PATHW = 0x0005;
        public const uint SHCNF_PRINTERA = 0x0002;
        public const uint SHCNF_PRINTERW = 0x0006;
        public const uint SHCNF_FLUSH = 0x1000;
        public const uint SHCNF_FLUSHNOWAIT = 0x200;

        public const int TV_FIRST = 0x1100;
        public const int TVM_SETIMAGELIST = (TV_FIRST + 9);

        public const int TVSIL_NORMAL = 0;
        public const int TVSIL_STATE = 2;

        public const int LV_FIRST = 0x1000;
        public const int LVM_SETIMAGELIST = (LV_FIRST + 3);

        public const int LVSIL_NORMAL = 0;
        public const int LVSIL_SMALL = 1;
        public const int LVSIL_STATE = 2;

        public static readonly IntPtr INVALID_HANDLE_VALUE = new IntPtr(-1);

        public const uint FILE_ATTRIBUTE_READONLY = 0x1;
        public const uint FILE_ATTRIBUTE_HIDDEN = 0x2;
        public const uint FILE_ATTRIBUTE_SYSTEM = 0x4;
        public const uint FILE_ATTRIBUTE_DIRECTORY = 0x10;
        public const uint FILE_ATTRIBUTE_ARCHIVE = 0x20;
        public const uint FILE_ATTRIBUTE_DEVICE = 0x40;
        public const uint FILE_ATTRIBUTE_NORMAL = 0x80;
        public const uint FILE_ATTRIBUTE_TEMPORARY = 0x100;
        public const uint FILE_ATTRIBUTE_SPARSE_FILE = 0x200;
        public const uint FILE_ATTRIBUTE_REPARSE_POINT = 0x400;
        public const uint FILE_ATTRIBUTE_COMPRESSED = 0x800;
        public const uint FILE_ATTRIBUTE_OFFLINE = 0x1000;
        public const uint FILE_ATTRIBUTE_NOT_CONTENT_INDEXED = 0x2000;
        public const uint FILE_ATTRIBUTE_ENCRYPTED = 0x4000;
        public const uint FILE_ATTRIBUTE_VIRTUAL = 0x10000;

        // File Descriptor Flags
        public const Int32 FD_CLSID = 0x00000001;
        public const Int32 FD_SIZEPOINT = 0x00000002;
        public const Int32 FD_ATTRIBUTES = 0x00000004;
        public const Int32 FD_CREATETIME = 0x00000008;
        public const Int32 FD_ACCESSTIME = 0x00000010;
        public const Int32 FD_WRITESTIME = 0x00000020;
        public const Int32 FD_FILESIZE = 0x00000040;
        public const Int32 FD_PROGRESSUI = 0x00004000;
        public const Int32 FD_LINKUI = 0x00008000;

        [Flags]
        public enum DeviceContextValues : uint
        {
            Window = 0x00000001,
            Cache = 0x00000002,
            NoResetAttrs = 0x00000004,
            ClipChildren = 0x00000008,
            ClipSiblings = 0x00000010,
            ParentClip = 0x00000020,
            ExcludeRgn = 0x00000040,
            IntersectRgn = 0x00000080,
            ExcludeUpdate = 0x00000100,
            IntersectUpdate = 0x00000200,
            LockWindowUpdate = 0x00000400,
            Validate = 0x00200000
        }

        public const int WM_NULL = 0x0;
        public const int WM_ERASEBKGND = 0x0014;
        public const int WM_NOTIFY = 0x004E;
        public const int WM_SETFOCUS = 0x0007;
        public const int WM_KILLFOCUS = 0x0008;
        public const int WM_PAINT = 0x000F;
        public const int WM_NCCALCSIZE = 0x0083;
        public const int WM_NCHITTEST = 0x0084;
        public const int WM_NCACTIVATE = 0x0086;
        public const int WM_NCPAINT = 0x0085;
        public const int WM_MOUSEACTIVATE = 0x0021;
        public const int WM_USER = 0x0400;
        public const int WM_ACTIVATEAPP = 0x001c;
        public const int WM_MOUSEMOVE = 0x0200;
        public const int WM_LBUTTONDOWN = 0x0201;
        public const int WM_LBUTTONUP = 0x0202;
        public const int WM_MBUTTONDOWN = 0x0207;
        public const int WM_RBUTTONDOWN = 0x0204;
        public const int WM_POINTERDOWN = 0x0246;
        public const int WM_LBUTTONDBLCLK = 0x0203;
        public const int WM_PARENTNOTIFY = 0x0210;
        public const int WM_KEYDOWN = 0x0100;
        public const int WM_XBUTTONDOWN = 0x020b;
        public const int WM_APPCOMMAND = 0x0319;
        public const int WM_SETCURSOR = 0x0020;
        public const int WM_ACTIVATE = 0x0006;
        public const int WM_COPYDATA = 0x004a;

        public const int MA_ACTIVATE = 1;
        public const int MA_ACTIVATEANDEAT = 2;
        public const int MA_NOACTIVATE = 3;
        public const int MA_NOACTIVATEANDEAT = 4;

        public const int HTTRANSPARENT = -1;
        public const int HTCAPTION = 2;
        public const int HTSYSMENU = 3;
        public const int HTGROWBOX = 4;
        public const int HTMENU = 5;
        public const int HTHSCROLL = 6;
        public const int HTVSCROLL = 7;
        public const int HTMINBUTTON = 8;
        public const int HTMAXBUTTON = 9;
        public const int HTLEFT = 10;
        public const int HTRIGHT = 11;
        public const int HTTOP = 12;
        public const int HTTOPLEFT = 13;
        public const int HTTOPRIGHT = 14;
        public const int HTBOTTOM = 15;
        public const int HTBOTTOMLEFT = 16;
        public const int HTBOTTOMRIGHT = 17;
        public const int HTBORDER = 18;
        public const int HTCLOSE = 20;
        public const int HTHELP = 21;
        public const int HTMDIMAXBUTTON = 66;
        public const int HTMDIMINBUTTON = 67;
        public const int HTMDICLOSE = 68;

        public const int HDN_BEGINTRACKA = ((0 - 300) - 6);
        public const int HDN_BEGINTRACKW = ((0 - 300) - 26);

        public const int SW_HIDE = 0;
        public const int SW_SHOWNORMAL = 1;
        public const int SW_NORMAL = 1;
        public const int SW_SHOWMINIMIZED = 2;
        public const int SW_SHOWMAXIMIZED = 3;
        public const int SW_MAXIMIZE = 3;
        public const int SW_SHOWNOACTIVATE = 4;
        public const int SW_SHOW = 5;
        public const int SW_MINIMIZE = 6;
        public const int SW_SHOWMINNOACTIVE = 7;
        public const int SW_SHOWNA = 8;
        public const int SW_RESTORE = 9;
        public const int SW_SHOWDEFAULT = 10;
        public const int SW_FORCEMINIMIZE = 11;

        public const int HWND_TOPMOST = -1;

        public const uint SWP_NOSIZE = 0x0001;
        public const uint SWP_NOMOVE = 0x0002;
        public const uint SWP_NOZORDER = 0x0004;
        public const uint SWP_NOREDRAW = 0x0008;
        public const uint SWP_NOACTIVATE = 0x0010;
        public const uint SWP_FRAMECHANGED = 0x0020;
        public const uint SWP_SHOWWINDOW = 0x0040;
        public const uint SWP_HIDEWINDOW = 0x0080;
        public const uint SWP_NOCOPYBITS = 0x0100;
        public const uint SWP_NOOWNERZORDER = 0x0200;
        public const uint SWP_NOSENDCHANGING = 0x0400;

        public const uint SWP_DEFERERASE = 0x2000;
        public const uint SWP_ASYNCWINDOWPOS = 0x4000;

        public const int HDN_FIRST = -300;

        public const int CP_NOCLOSE_BUTTON = 0x200;

        public const int RDW_INVALIDATE = 0x0001;
        public const int RDW_INTERNALPAINT = 0x0002;
        public const int RDW_ERASE = 0x0004;
        public const int RDW_VALIDATE = 0x0008;
        public const int RDW_NOINTERNALPAINT = 0x0010;
        public const int RDW_NOERASE = 0x0020;
        public const int RDW_NOCHILDREN = 0x0040;
        public const int RDW_ALLCHILDREN = 0x0080;
        public const int RDW_UPDATENOW = 0x0100;
        public const int RDW_ERASENOW = 0x0200;
        public const int RDW_FRAME = 0x0400;
        public const int RDW_NOFRAME = 0x0800;

        public const int MF_ENABLED = 0x0;
        public const int MF_GRAYED = 0x1;
        public const int MF_DISABLED = 0x02;

        public const int SC_CLOSE = 0xF060;

        public const int LWA_COLORKEY = 0x00000001;
        public const int LWA_ALPHA = 0x00000002;

        public const uint FLASHW_STOP = 0;
        public const uint FLASHW_CAPTION = 1;
        public const uint FLASHW_TRAY = 2;
        public const uint FLASHW_ALL = 3;
        public const uint FLASHW_TIMER = 4;
        public const uint FLASHW_TIMERNOFG = 12;

        public const uint PBM_SETSTATE = WM_USER + 16;

        public const int PBST_NORMAL = 0x1;
        public const int PBST_ERROR = 0x2;
        public const int PBST_PAUSED = 0x3;

        public const int MK_CONTROL = 0x8;
        public const int MK_LBUTTON = 0x1;
        public const int MK_MBUTTON = 0x10;
        public const int MK_RBUTTON = 0x2;
        public const int MK_SHIFT = 0x4;
        public const int MK_XBUTTON1 = 0x20;
        public const int MK_XBUTTON2 = 0x40;

        public const int XBUTTON1 = 0x1;
        public const int XBUTTON2 = 0x2;

        public const int APPCOMMAND_BASS_BOOST = 20;
        public const int APPCOMMAND_BASS_DOWN = 19;
        public const int APPCOMMAND_BASS_UP = 21;
        public const int APPCOMMAND_BROWSER_BACKWARD = 1;
        public const int APPCOMMAND_BROWSER_FAVORITES = 6;
        public const int APPCOMMAND_BROWSER_FORWARD = 2;
        public const int APPCOMMAND_BROWSER_HOME = 7;
        public const int APPCOMMAND_BROWSER_REFRESH = 3;
        public const int APPCOMMAND_BROWSER_SEARCH = 5;
        public const int APPCOMMAND_BROWSER_STOP = 4;
        public const int APPCOMMAND_CLOSE = 31;
        public const int APPCOMMAND_COPY = 36;
        public const int APPCOMMAND_CORRECTION_LIST = 45;
        public const int APPCOMMAND_CUT = 37;
        public const int APPCOMMAND_DICTATE_OR_COMMAND_CONTROL_TOGGLE = 43;
        public const int APPCOMMAND_FIND = 28;
        public const int APPCOMMAND_FORWARD_MAIL = 40;
        public const int APPCOMMAND_HELP = 27;
        public const int APPCOMMAND_LAUNCH_APP1 = 17;
        public const int APPCOMMAND_LAUNCH_APP2 = 18;
        public const int APPCOMMAND_LAUNCH_MAIL = 15;
        public const int APPCOMMAND_LAUNCH_MEDIA_SELECT = 16;
        public const int APPCOMMAND_MEDIA_CHANNEL_DOWN = 52;
        public const int APPCOMMAND_MEDIA_CHANNEL_UP = 51;
        public const int APPCOMMAND_MEDIA_FAST_FORWARD = 49;
        public const int APPCOMMAND_MEDIA_NEXTTRACK = 11;
        public const int APPCOMMAND_MEDIA_PAUSE = 47;
        public const int APPCOMMAND_MEDIA_PLAY = 46;
        public const int APPCOMMAND_MEDIA_PLAY_PAUSE = 14;
        public const int APPCOMMAND_MEDIA_PREVIOUSTRACK = 12;
        public const int APPCOMMAND_MEDIA_RECORD = 48;
        public const int APPCOMMAND_MEDIA_REWIND = 50;
        public const int APPCOMMAND_MEDIA_STOP = 13;
        public const int APPCOMMAND_MIC_ON_OFF_TOGGLE = 44;
        public const int APPCOMMAND_MICROPHONE_VOLUME_DOWN = 25;
        public const int APPCOMMAND_MICROPHONE_VOLUME_MUTE = 24;
        public const int APPCOMMAND_MICROPHONE_VOLUME_UP = 26;
        public const int APPCOMMAND_NEW = 29;
        public const int APPCOMMAND_OPEN = 30;
        public const int APPCOMMAND_PASTE = 38;
        public const int APPCOMMAND_PRINT = 33;
        public const int APPCOMMAND_REDO = 35;
        public const int APPCOMMAND_REPLY_TO_MAIL = 39;
        public const int APPCOMMAND_SAVE = 32;
        public const int APPCOMMAND_SEND_MAIL = 41;
        public const int APPCOMMAND_SPELL_CHECK = 42;
        public const int APPCOMMAND_TREBLE_DOWN = 22;
        public const int APPCOMMAND_TREBLE_UP = 23;
        public const int APPCOMMAND_UNDO = 34;
        public const int APPCOMMAND_VOLUME_DOWN = 9;
        public const int APPCOMMAND_VOLUME_MUTE = 8;
        public const int APPCOMMAND_VOLUME_UP = 10;

        public const int WA_ACTIVE = 1;
        public const int WA_CLICKACTIVE = 2;
        public const int WA_INACTIVE = 0;

        public const int FORMAT_MESSAGE_ALLOCATE_BUFFER = 0x100;
        public const int FORMAT_MESSAGE_ARGUMENT_ARRAY = 0x2000;
        public const int FORMAT_MESSAGE_FROM_HMODULE = 0x800;
        public const int FORMAT_MESSAGE_FROM_STRING = 0x400;
        public const int FORMAT_MESSAGE_FROM_SYSTEM = 0x1000;
        public const int FORMAT_MESSAGE_IGNORE_INSERTS = 0x200;
        public const int FORMAT_MESSAGE_MAX_WIDTH_MASK = 0xFF;

        public const int MAX_PATH = 260;

        public const int DT_CALCRECT = 0x00000400;
        public const int DT_WORDBREAK = 0x00000010;
        public const int DT_EDITCONTROL = 0x00002000;

        public const uint STANDARD_RIGHTS_READ = 0x00020000;
        public const uint TOKEN_QUERY = 0x0008;
        public const uint TOKEN_READ = (STANDARD_RIGHTS_READ | TOKEN_QUERY);

        public enum TOKEN_INFORMATION_CLASS
        {
            TokenUser = 1,
            TokenGroups,
            TokenPrivileges,
            TokenOwner,
            TokenPrimaryGroup,
            TokenDefaultDacl,
            TokenSource,
            TokenType,
            TokenImpersonationLevel,
            TokenStatistics,
            TokenRestrictedSids,
            TokenSessionId,
            TokenGroupsAndPrivileges,
            TokenSessionReference,
            TokenSandBoxInert,
            TokenAuditPolicy,
            TokenOrigin,
            TokenElevationType,
            TokenLinkedToken,
            TokenElevation,
            TokenHasRestrictions,
            TokenAccessInformation,
            TokenVirtualizationAllowed,
            TokenVirtualizationEnabled,
            TokenIntegrityLevel,
            TokenUIAccess,
            TokenMandatoryPolicy,
            TokenLogonSid,
            MaxTokenInfoClass
        }

        public enum TOKEN_ELEVATION_TYPE
        {
            TokenElevationTypeDefault = 1,
            TokenElevationTypeFull,
            TokenElevationTypeLimited
        }
    }
}
