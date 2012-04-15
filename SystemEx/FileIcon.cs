using System.Text;
using System.Collections.Generic;
using System;
using System.Runtime.InteropServices;
using System.Drawing;
using SystemEx.Win32;

namespace SystemEx
{
    /// <summary>
    /// Enables extraction of icons for any file type from
    /// the Shell.
    /// </summary>
    public class FileIcon
    {
        #region Member Variables
        private string fileName;
        private string displayName;
        private string typeName;
        private FileInfoOptions flags;
        private Icon fileIcon;
        #endregion

        #region Enumerations
        
        #endregion

        #region Implementation
        /// <summary>
        /// Gets/sets the flags used to extract the icon
        /// </summary>
        public FileInfoOptions Flags
        {
            get
            {
                return flags;
            }
            set
            {
                flags = value;
            }
        }

        /// <summary>
        /// Gets/sets the filename to get the icon for
        /// </summary>
        public string FileName
        {
            get
            {
                return fileName;
            }
            set
            {
                fileName = value;
            }
        }

        /// <summary>
        /// Gets the icon for the chosen file
        /// </summary>
        public Icon ShellIcon
        {
            get
            {
                return fileIcon;
            }
        }

        /// <summary>
        /// Gets the display name for the selected file
        /// if the DisplayName flag was set.
        /// </summary>
        public string DisplayName
        {
            get
            {
                return displayName;
            }
        }

        /// <summary>
        /// Gets the type name for the selected file
        /// if the TypeName flag was set.
        /// </summary>
        public string TypeName
        {
            get
            {
                return typeName;
            }
        }

        /// <summary>
        ///  Gets the information for the specified 
        ///  file name and flags.
        /// </summary>
        public void GetInfo()
        {
            fileIcon = null;
            typeName = "";
            displayName = "";

            NativeMethods.SHFILEINFO shfi = new NativeMethods.SHFILEINFO();
            uint shfiSize = (uint)Marshal.SizeOf(shfi.GetType());

            IntPtr ret = NativeMethods.SHGetFileInfo(
               fileName, 0, ref shfi, shfiSize, (uint)(flags));
            if (ret != IntPtr.Zero)
            {
                if (shfi.hIcon != IntPtr.Zero)
                {
                    fileIcon = System.Drawing.Icon.FromHandle(shfi.hIcon);
                    // Now owned by the GDI+ object
                    //DestroyIcon(shfi.hIcon);
                }
                typeName = shfi.szTypeName;
                displayName = shfi.szDisplayName;
            }
            else
            {

                int err = NativeMethods.GetLastError();
                Console.WriteLine("Error {0}", err);
                string txtS = new string('\0', 256);
                int len = NativeMethods.FormatMessage(
                   NativeMethods.FORMAT_MESSAGE_FROM_SYSTEM | NativeMethods.FORMAT_MESSAGE_IGNORE_INSERTS,
                   IntPtr.Zero, err, 0, txtS, 256, 0);
                Console.WriteLine("Len {0} text {1}", len, txtS);

                // throw exception

            }
        }

        /// <summary>
        /// Constructs a new, default instance of the FileIcon
        /// class.  Specify the filename and call GetInfo()
        /// to retrieve an icon.
        /// </summary>
        public FileIcon()
        {
            flags =
                FileInfoOptions.Icon |
                FileInfoOptions.DisplayName |
                FileInfoOptions.TypeName |
                FileInfoOptions.Attributes |
                FileInfoOptions.ExeType;
        }
        /// <summary>
        /// Constructs a new instance of the FileIcon class
        /// and retrieves the icon, display name and type name
        /// for the specified file.      
        /// </summary>
        /// <param name="fileName">The filename to get the icon, 
        /// display name and type name for</param>
        public FileIcon(string fileName)
            : this()
        {
            this.fileName = fileName;
            GetInfo();
        }
        /// <summary>
        /// Constructs a new instance of the FileIcon class
        /// and retrieves the information specified in the 
        /// flags.
        /// </summary>
        /// <param name="fileName">The filename to get information
        /// for</param>
        /// <param name="flags">The flags to use when extracting the
        /// icon and other shell information.</param>
        public FileIcon(string fileName, FileInfoOptions flags)
        {
            this.fileName = fileName;
            this.flags = flags;
            GetInfo();
        }

        #endregion
    }
}
