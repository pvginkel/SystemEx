using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;

namespace SystemEx.Win32
{
    partial class NativeMethods
    {
        [DllImport(ExternDll.User32, CharSet = CharSet.Auto)]
        public extern static int ExtractIconEx(
            [MarshalAs(UnmanagedType.LPTStr)] 
			string lpszFile,
            int nIconIndex,
            IntPtr[] phIconLarge,
            IntPtr[] phIconSmall,
            int nIcons);

        /// <summary>
        /// TaskDialogIndirect taken from commctl.h
        /// </summary>
        /// <param name="pTaskConfig">All the parameters about the Task Dialog to Show.</param>
        /// <param name="pnButton">The push button pressed.</param>
        /// <param name="pnRadioButton">The radio button that was selected.</param>
        /// <param name="pfVerificationFlagChecked">The state of the verification checkbox on dismiss of the Task Dialog.</param>
        [DllImport(ExternDll.Comctl32, CharSet = CharSet.Unicode, PreserveSig = false)]
        public static extern void TaskDialogIndirect(
            [In] ref TASKDIALOGCONFIG pTaskConfig,
            [Out] out int pnButton,
            [Out] out int pnRadioButton,
            [Out] out bool pfVerificationFlagChecked);

        [DllImport(ExternDll.Gdi32)]
        public static extern IntPtr CreateRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect);

        [DllImport(ExternDll.Gdi32)]
        public static extern int CombineRgn(IntPtr hrgnDest, IntPtr hrgnSrc1, IntPtr hrgnSrc2, CombineRgnStyles fnCombineMode);

        [DllImport(ExternDll.Gdi32)]
        public static extern bool DeleteObject(IntPtr hObject);

        [DllImport(ExternDll.Wininet, CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool InternetSetCookie(string lpszUrlName, string lbszCookieName, string lpszCookieData);

        [DllImport(ExternDll.Kernel32)]
        public static extern uint GetTickCount();

        [DllImport(ExternDll.Kernel32)]
        public static extern IntPtr GlobalLock(IntPtr hMem);

        [DllImport(ExternDll.Kernel32, CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GlobalAlloc(int uFlags, int dwBytes);

        [DllImport(ExternDll.Kernel32, CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GlobalFree(HandleRef handle);

        [DllImport(ExternDll.Kernel32)]
        public extern static IntPtr CreateActCtx(ref ACTCTX actctx);

        [DllImport(ExternDll.Kernel32)]
        public extern static bool ActivateActCtx(IntPtr hActCtx, out IntPtr lpCookie);

        [DllImport(ExternDll.Kernel32)]
        public extern static bool DeactivateActCtx(int dwFlags, IntPtr lpCookie);

        [DllImport(ExternDll.Kernel32)]
        public static extern IntPtr LocalAlloc(uint uFlags, UIntPtr uBytes);

        [DllImport(ExternDll.Kernel32, SetLastError = true)]
        public static extern IntPtr LocalFree(IntPtr hMem);

        [DllImport(ExternDll.Ole32, PreserveSig = false)]
        public static extern ILockBytes CreateILockBytesOnHGlobal(IntPtr hGlobal, bool fDeleteOnRelease);

        [DllImport(ExternDll.Ole32, CharSet = CharSet.Auto, PreserveSig = false)]
        public static extern IntPtr GetHGlobalFromILockBytes(ILockBytes pLockBytes);

        [DllImport(ExternDll.Ole32, CharSet = CharSet.Unicode, PreserveSig = false)]
        public static extern IStorage StgCreateDocfileOnILockBytes(ILockBytes plkbyt, uint grfMode, uint reserved);

        [DllImport(ExternDll.Shell32, CharSet = CharSet.Auto)]
        public static extern int SHBrowseForFolder(BROWSEINFO mBinf);

        [DllImport(ExternDll.Shell32)]
        public static extern bool SHGetPathFromIDList(int ilPtr, IntPtr sPath);

        [DllImport(ExternDll.User32, CharSet = CharSet.Auto)]
        public static extern uint SendMessage(IntPtr handle, uint message, uint wParam, uint lParam);

        [DllImport(ExternDll.User32)]
        public static extern int GetWindowLong(IntPtr hWnd, int index);

        [DllImport(ExternDll.User32)]
        public static extern int SetWindowLong(IntPtr hWnd, int index, int value);

        [DllImport(ExternDll.Shell32)]
        public static extern IntPtr SHGetFileInfo(string pszPath, uint dwFileAttributes, ref SHFILEINFO psfi, uint cbSizeFileInfo, uint uFlags);

        [DllImport(ExternDll.User32)]
        public static extern int DestroyIcon(IntPtr hIcon);

        [DllImport(ExternDll.Shell32, CallingConvention = CallingConvention.StdCall)]
        public static extern uint SHAppBarMessage(int dwMessage, ref APPBARDATA pData);

        [DllImport(ExternDll.User32)]
        public static extern int GetSystemMetrics(int Index);

        [DllImport(ExternDll.User32, ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        public static extern bool MoveWindow(IntPtr hWnd, int x, int y, int cx, int cy, bool repaint);

        [DllImport(ExternDll.User32, CharSet = CharSet.Auto)]
        public static extern int RegisterWindowMessage(string msg);

        [DllImport(ExternDll.Shell32)]
        public static extern void SHGetSettings(out SHELLFLAGSTATE lpsfs, uint dwMask);

        [DllImport(ExternDll.Shell32)]
        public static extern void SHChangeNotify(uint wEventId, uint uFlags, IntPtr dwItem1, IntPtr dwItem2);

        [DllImport(ExternDll.Kernel32, CharSet = CharSet.Auto)]
        public static extern IntPtr FindFirstFile(string lpFileName, out WIN32_FIND_DATA lpFindFileData);

        [DllImport(ExternDll.Kernel32, CharSet = CharSet.Auto)]
        public static extern bool FindNextFile(IntPtr hFindFile, out WIN32_FIND_DATA lpFindFileData);

        [DllImport(ExternDll.Kernel32)]
        public static extern bool FindClose(IntPtr hFindFile);

        [DllImport(ExternDll.User32)]
        public static extern bool ShowWindow(IntPtr hWnd, Int32 nCmdShow);

        [DllImport(ExternDll.User32)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, uint uFlags);

        [DllImport(ExternDll.User32, ExactSpelling = true, CharSet = CharSet.Auto)]
        public static extern bool GetWindowRect(IntPtr hWnd, [In, Out] ref NativeMethods.RECT rect);

        [DllImport(ExternDll.User32)]
        public static extern bool GetClientRect(IntPtr hWnd, ref RECT lpRect);

        [DllImport(ExternDll.User32)]
        public static extern IntPtr GetDesktopWindow();

        [DllImport(ExternDll.User32)]
        public static extern IntPtr GetForegroundWindow();

        [DllImport(ExternDll.User32)]
        public static extern bool GetLastInputInfo(ref LASTINPUTINFO lpii);

        [DllImport(ExternDll.User32)]
        public static extern IntPtr GetWindowDC(IntPtr hWnd);

        [DllImport(ExternDll.User32)]
        public static extern IntPtr GetDCEx(IntPtr hWnd, IntPtr hrgnClip, DeviceContextValues flags);

        [DllImport(ExternDll.User32)]
        public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

        [DllImport(ExternDll.User32)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool RedrawWindow(IntPtr hWnd, [In] ref RECT lprcUpdate, IntPtr hrgnUpdate, uint flags);

        [DllImport(ExternDll.User32)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool RedrawWindow(IntPtr hWnd, IntPtr lprcUpdate, IntPtr hrgnUpdate, uint flags);

        [DllImport(ExternDll.User32)]
        public static extern IntPtr SetForegroundWindow(IntPtr hWnd);

        [DllImport(ExternDll.User32)]
        public static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport(ExternDll.User32)]
        public static extern int EnableMenuItem(IntPtr hMenu, int wIDEnableItem, int wEnable);

        [DllImport(ExternDll.User32)]
        public static extern bool ClientToScreen(IntPtr hWnd, ref Point lpPoint);

        [DllImport(ExternDll.User32)]
        public static extern bool SetLayeredWindowAttributes(IntPtr hwnd, uint crKey, byte bAlpha, uint dwFlags);

        [DllImport(ExternDll.User32)]
        public extern static IntPtr SetActiveWindow(IntPtr handle);

        /// <summary>
        /// Win32 SendMessage.
        /// </summary>
        /// <param name="hWnd">Window handle to send to.</param>
        /// <param name="Msg">The windows message to send.</param>
        /// <param name="wParam">Specifies additional message-specific information.</param>
        /// <param name="lParam">Specifies additional message-specific information.</param>
        /// <returns>The return value specifies the result of the message processing; it depends on the message sent.</returns>
        [DllImport(ExternDll.User32)]
        public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        [DllImport(ExternDll.User32)]
        public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, ref COPYDATASTRUCT lpCds);

        /// <summary>
        /// Win32 SendMessage.
        /// </summary>
        /// <param name="hWnd">Window handle to send to.</param>
        /// <param name="Msg">The windows message to send.</param>
        /// <param name="wParam">Specifies additional message-specific information.</param>
        /// <param name="lParam">Specifies additional message-specific information as a string.</param>
        /// <returns>The return value specifies the result of the message processing; it depends on the message sent.</returns>
        [DllImport(ExternDll.User32, EntryPoint = "SendMessage")]
        public static extern IntPtr SendMessageWithString(IntPtr hWnd, uint Msg, IntPtr wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);

        [DllImport(ExternDll.User32)]
        public static extern IntPtr ChildWindowFromPoint(IntPtr hwndParent, Point ptParentClientCoords);

        [DllImport(ExternDll.User32)]
        public static extern bool EnableWindow(IntPtr hWnd, bool fEnabled);

        [DllImport(ExternDll.User32)]
        public static extern bool FlashWindow(IntPtr hWnd, bool fInvert);

        [DllImport(ExternDll.User32)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool FlashWindowEx(ref FLASHWINFO pwfi);

        [DllImport(ExternDll.User32, SetLastError = true)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport(ExternDll.User32, SetLastError = true)]
        public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        [DllImport(ExternDll.User32)]
        public static extern bool AllowSetForegroundWindow(uint dwProcessId);

        [DllImport(ExternDll.User32, SetLastError = true, CharSet = CharSet.Auto)]
        public static extern bool SetWindowText(IntPtr hwnd, String lpString);

        [DllImport(ExternDll.User32)]
        public static extern IntPtr SHGetFileInfo(
           string pszPath,
           int dwFileAttributes,
           ref SHFILEINFO psfi,
           uint cbFileInfo,
           uint uFlags);

        [DllImport(ExternDll.Kernel32)]
        public extern static int FormatMessage(
           int dwFlags,
           IntPtr lpSource,
           int dwMessageId,
           int dwLanguageId,
           string lpBuffer,
           uint nSize,
           int argumentsLong);

        [DllImport(ExternDll.Kernel32)]
        public extern static int GetLastError();

        [DllImport(ExternDll.User32)]
        public static extern int DrawText(IntPtr hdc, string lpStr, int nCount, ref RECT lpRect, int wFormat);

        [DllImport(ExternDll.User32)]
        public static extern IntPtr GetDC(IntPtr hWnd);

        [DllImport(ExternDll.Gdi32)]
        public static extern IntPtr SelectObject(IntPtr hDC, IntPtr hObject);

        [DllImport(ExternDll.Advapi32, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool OpenProcessToken(IntPtr ProcessHandle, UInt32 DesiredAccess, out IntPtr TokenHandle);

        [DllImport(ExternDll.Advapi32, SetLastError = true)]
        public static extern bool GetTokenInformation(IntPtr TokenHandle, TOKEN_INFORMATION_CLASS TokenInformationClass, IntPtr TokenInformation, uint TokenInformationLength, out uint ReturnLength);

        [DllImport(ExternDll.Uxtheme, CharSet = CharSet.Unicode)]
        public extern static int SetWindowTheme(IntPtr hWnd, string pszSubAppName, string pszSubIdList);
    }
}
