using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using SystemEx.Win32;

namespace SystemEx.Windows.Forms
{
    public static class BrowseForFolderDialog
    {
        [CLSCompliant(false)]
        public static string Show(IWin32Window owner, string title, BrowseForFolderOptions options)
        {
            NativeMethods.BROWSEINFO mbif = new NativeMethods.BROWSEINFO();

            mbif.hwndOwner = owner == null ? IntPtr.Zero : owner.Handle;
            mbif.lpfn = IntPtr.Zero;
            mbif.lpszTitle = title;
            mbif.ulFlags = (uint)options;

            int itemlstptr = 0;

            itemlstptr = NativeMethods.SHBrowseForFolder(mbif);

            IntPtr sourcePath = Marshal.AllocCoTaskMem(256);

            bool bRes = false;

            string dirPath = null;

            if (itemlstptr >= 0)
            {
                bRes = NativeMethods.SHGetPathFromIDList(itemlstptr, sourcePath);

                dirPath = Marshal.PtrToStringAnsi(sourcePath);
            }

            Marshal.FreeCoTaskMem(sourcePath);

            if (dirPath != null && dirPath.Trim() == "")
            {
                dirPath = null;
            }

            return dirPath;
        }
    }
}
