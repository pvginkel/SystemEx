using System.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Permissions;
using SystemEx.Win32;

namespace SystemEx.Windows.Forms
{
    public class OleDataObject : DataObject, System.Runtime.InteropServices.ComTypes.IDataObject, IDisposable
    {
        private static readonly TYMED[] ALLOWED_TYMEDS =
            new TYMED[] { 
                TYMED.TYMED_HGLOBAL,
                TYMED.TYMED_ISTREAM, 
                TYMED.TYMED_ENHMF,
                TYMED.TYMED_MFPICT,
                TYMED.TYMED_GDI};

        private OleDataObjectItem[] m_SelectedItems;
        private Int32 m_lindex;
        private bool m_Disposed = false;

        public OleDataObject(OleDataObjectItem[] selectedItems)
        {
            m_SelectedItems = selectedItems;

            SetData(NativeMethods.CFSTR_FILEDESCRIPTORW, null);
            SetData(NativeMethods.CFSTR_FILECONTENTS, null);
            SetData(NativeMethods.CFSTR_PERFORMEDDROPEFFECT, null);
        }

        public override object GetData(string format, bool autoConvert)
        {
            if (String.Compare(format, Win32.NativeMethods.CFSTR_FILEDESCRIPTORW, StringComparison.OrdinalIgnoreCase) == 0 && m_SelectedItems != null)
            {
                base.SetData(Win32.NativeMethods.CFSTR_FILEDESCRIPTORW, GetFileDescriptor(m_SelectedItems));
            }
            else if (String.Compare(format, Win32.NativeMethods.CFSTR_FILECONTENTS, StringComparison.OrdinalIgnoreCase) == 0)
            {
                base.SetData(Win32.NativeMethods.CFSTR_FILECONTENTS, GetFileContents(m_SelectedItems, m_lindex));
            }
            else if (String.Compare(format, Win32.NativeMethods.CFSTR_PERFORMEDDROPEFFECT, StringComparison.OrdinalIgnoreCase) == 0)
            {
                //TODO: Cleanup routines after paste has been performed
            }
            return base.GetData(format, autoConvert);
        }

        [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        void System.Runtime.InteropServices.ComTypes.IDataObject.GetData(ref System.Runtime.InteropServices.ComTypes.FORMATETC formatetc, out System.Runtime.InteropServices.ComTypes.STGMEDIUM medium)
        {
            if (formatetc.cfFormat == (Int16)DataFormats.GetFormat(Win32.NativeMethods.CFSTR_FILECONTENTS).Id)
                m_lindex = formatetc.lindex;

            medium = new System.Runtime.InteropServices.ComTypes.STGMEDIUM();
            if (GetTymedUseable(formatetc.tymed))
            {
                if ((formatetc.tymed & TYMED.TYMED_HGLOBAL) != TYMED.TYMED_NULL)
                {
                    medium.tymed = TYMED.TYMED_HGLOBAL;
                    medium.unionmember = Win32.NativeMethods.GlobalAlloc(Win32.NativeMethods.GHND | Win32.NativeMethods.GMEM_DDESHARE, 1);
                    if (medium.unionmember == IntPtr.Zero)
                    {
                        throw new OutOfMemoryException();
                    }
                    try
                    {
                        ((System.Runtime.InteropServices.ComTypes.IDataObject)this).GetDataHere(ref formatetc, ref medium);
                        return;
                    }
                    catch
                    {
                        Win32.NativeMethods.GlobalFree(new HandleRef((STGMEDIUM)medium, medium.unionmember));
                        medium.unionmember = IntPtr.Zero;
                        throw;
                    }
                }
                medium.tymed = formatetc.tymed;
                ((System.Runtime.InteropServices.ComTypes.IDataObject)this).GetDataHere(ref formatetc, ref medium);
            }
            else
            {
                Marshal.ThrowExceptionForHR(Win32.NativeMethods.DV_E_TYMED);
            }
        }

        private static Boolean GetTymedUseable(TYMED tymed)
        {
            for (Int32 i = 0; i < ALLOWED_TYMEDS.Length; i++)
            {
                if ((tymed & ALLOWED_TYMEDS[i]) != TYMED.TYMED_NULL)
                {
                    return true;
                }
            }
            return false;
        }

        private MemoryStream GetFileDescriptor(OleDataObjectItem[] SelectedItems)
        {
            MemoryStream FileDescriptorMemoryStream = new MemoryStream();
            // Write out the FILEGROUPDESCRIPTOR.cItems value
            FileDescriptorMemoryStream.Write(BitConverter.GetBytes(SelectedItems.Length), 0, sizeof(UInt32));

            Win32.NativeMethods.FILEDESCRIPTORW FileDescriptor = new Win32.NativeMethods.FILEDESCRIPTORW();
            foreach (OleDataObjectItem si in SelectedItems)
            {
                FileDescriptor.cFileName = si.FileName;
                Int64 FileWriteTimeUtc = si.WriteTime.ToFileTimeUtc();
                FileDescriptor.ftLastWriteTime.dwHighDateTime = (Int32)(FileWriteTimeUtc >> 32);
                FileDescriptor.ftLastWriteTime.dwLowDateTime = (Int32)(FileWriteTimeUtc & 0xFFFFFFFF);
                FileDescriptor.nFileSizeHigh = (UInt32)(si.FileSize >> 32);
                FileDescriptor.nFileSizeLow = (UInt32)(si.FileSize & 0xFFFFFFFF);
                FileDescriptor.dwFlags = Win32.NativeMethods.FD_WRITESTIME | Win32.NativeMethods.FD_FILESIZE | Win32.NativeMethods.FD_PROGRESSUI;

                // Marshal the FileDescriptor structure into a byte array and write it to the MemoryStream.
                Int32 FileDescriptorSize = Marshal.SizeOf(FileDescriptor);
                IntPtr FileDescriptorPointer = Marshal.AllocHGlobal(FileDescriptorSize);
                Marshal.StructureToPtr(FileDescriptor, FileDescriptorPointer, true);
                Byte[] FileDescriptorByteArray = new Byte[FileDescriptorSize];
                Marshal.Copy(FileDescriptorPointer, FileDescriptorByteArray, 0, FileDescriptorSize);
                Marshal.FreeHGlobal(FileDescriptorPointer);
                FileDescriptorMemoryStream.Write(FileDescriptorByteArray, 0, FileDescriptorByteArray.Length);
            }
            return FileDescriptorMemoryStream;
        }

        private MemoryStream GetFileContents(OleDataObjectItem[] SelectedItems, Int32 FileNumber)
        {
            if (SelectedItems != null && FileNumber < SelectedItems.Length)
            {
                return SelectedItems[FileNumber].MemoryStream;
            }
            else
            {
                return null;
            }
        }

        public void Dispose()
        {
            if (!m_Disposed)
            {
                m_Disposed = true;

                foreach (var item in m_SelectedItems)
                {
                    item.Dispose();
                }

                m_SelectedItems = null;
            }
        }
    }
}
