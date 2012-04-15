using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics;
using SystemEx.Win32;

namespace SystemEx.Windows.Forms
{
    public class SystemImageList
    {
        private readonly IntPtr _handle;

        public SystemImageList()
        {
            var fileInfo = new NativeMethods.SHFILEINFO();

            _handle = NativeMethods.SHGetFileInfo(
                ".txt",
                0,
                ref fileInfo,
                (uint)Marshal.SizeOf(fileInfo),
                NativeMethods.SHGFI_USEFILEATTRIBUTES | NativeMethods.SHGFI_SYSICONINDEX | NativeMethods.SHGFI_SMALLICON
            );

            Debug.Assert(_handle != IntPtr.Zero);
        }

        public void Assign(System.Windows.Forms.TreeView treeView)
        {
            if (treeView == null)
                throw new ArgumentNullException("treeView");

            NativeMethods.SendMessage(
                treeView.Handle,
                (uint)NativeMethods.TVM_SETIMAGELIST,
                (IntPtr)NativeMethods.TVSIL_NORMAL,
                _handle
            );
        }

        public void Assign(System.Windows.Forms.ListView listView)
        {
            if (listView == null)
                throw new ArgumentNullException("listView");

            NativeMethods.SendMessage(
                listView.Handle,
                (uint)NativeMethods.LVM_SETIMAGELIST,
                (IntPtr)NativeMethods.LVSIL_SMALL,
                _handle
            );
        }

        [CLSCompliant(false)]
        public int AddShellIcon(string path, ShellIconType flags)
        {
            var info = new NativeMethods.SHFILEINFO();

            try
            {
                var imageList = NativeMethods.SHGetFileInfo(
                    path, 0, ref info, (uint)Marshal.SizeOf(info),
                    ((uint)flags | NativeMethods.SHGFI_SMALLICON | NativeMethods.SHGFI_SYSICONINDEX)
                    & ~NativeMethods.SHGFI_ICON
                );

                if (imageList == IntPtr.Zero)
                    return -1;

                if (imageList != _handle)
                    throw new ArgumentException("Could not get image", "path");

                return info.iIcon;
            }
            finally
            {
                if (info.hIcon != IntPtr.Zero)
                    NativeMethods.DestroyIcon(info.hIcon);
            }
        }
    }
}
