using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;

namespace SystemEx.Win32
{
    partial class NativeMethods
    {
        [ComImport]
        [Guid("0000010C-0000-0000-C000-000000000046")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IPersist
        {
            [PreserveSig]
            //[helpstring("Returns the class identifier for the component object")]
            void GetClassID(out Guid pClassID);
        }

        [ComImport]
        [Guid("0000010B-0000-0000-C000-000000000046")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IPersistFile
        {
            // can't get this to go if I extend IPersist, so put it here:
            [PreserveSig]
            void GetClassID(out Guid pClassID);

            //[helpstring("Checks for changes since last file write")]		
            void IsDirty();

            //[helpstring("Opens the specified file and initializes the object from its contents")]		
            void Load(
                [MarshalAs(UnmanagedType.LPWStr)] string pszFileName,
                uint dwMode);

            //[helpstring("Saves the object into the specified file")]		
            void Save(
                [MarshalAs(UnmanagedType.LPWStr)] string pszFileName,
                [MarshalAs(UnmanagedType.Bool)] bool fRemember);

            //[helpstring("Notifies the object that save is completed")]		
            void SaveCompleted(
                [MarshalAs(UnmanagedType.LPWStr)] string pszFileName);

            //[helpstring("Gets the current name of the file associated with the object")]		
            void GetCurFile(
                [MarshalAs(UnmanagedType.LPWStr)] out string ppszFileName);
        }

        [ComImport]
        [Guid("000214EE-0000-0000-C000-000000000046")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IShellLinkA
        {
            //[helpstring("Retrieves the path and filename of a shell link object")]
            void GetPath(
                [Out, MarshalAs(UnmanagedType.LPStr)] StringBuilder pszFile,
                int cchMaxPath,
                ref _WIN32_FIND_DATAA pfd,
                uint fFlags);

            //[helpstring("Retrieves the list of shell link item identifiers")]
            void GetIDList(out IntPtr ppidl);

            //[helpstring("Sets the list of shell link item identifiers")]
            void SetIDList(IntPtr pidl);

            //[helpstring("Retrieves the shell link description string")]
            void GetDescription(
                [Out, MarshalAs(UnmanagedType.LPStr)] StringBuilder pszFile,
                int cchMaxName);

            //[helpstring("Sets the shell link description string")]
            void SetDescription(
                [MarshalAs(UnmanagedType.LPStr)] string pszName);

            //[helpstring("Retrieves the name of the shell link working directory")]
            void GetWorkingDirectory(
                [Out, MarshalAs(UnmanagedType.LPStr)] StringBuilder pszDir,
                int cchMaxPath);

            //[helpstring("Sets the name of the shell link working directory")]
            void SetWorkingDirectory(
                [MarshalAs(UnmanagedType.LPStr)] string pszDir);

            //[helpstring("Retrieves the shell link command-line arguments")]
            void GetArguments(
                [Out, MarshalAs(UnmanagedType.LPStr)] StringBuilder pszArgs,
                int cchMaxPath);

            //[helpstring("Sets the shell link command-line arguments")]
            void SetArguments(
                [MarshalAs(UnmanagedType.LPStr)] string pszArgs);

            //[propget, helpstring("Retrieves or sets the shell link hot key")]
            void GetHotkey(out short pwHotkey);
            //[propput, helpstring("Retrieves or sets the shell link hot key")]
            void SetHotkey(short pwHotkey);

            //[propget, helpstring("Retrieves or sets the shell link show command")]
            void GetShowCmd(out uint piShowCmd);
            //[propput, helpstring("Retrieves or sets the shell link show command")]
            void SetShowCmd(uint piShowCmd);

            //[helpstring("Retrieves the location (path and index) of the shell link icon")]
            void GetIconLocation(
                [Out, MarshalAs(UnmanagedType.LPStr)] StringBuilder pszIconPath,
                int cchIconPath,
                out int piIcon);

            //[helpstring("Sets the location (path and index) of the shell link icon")]
            void SetIconLocation(
                [MarshalAs(UnmanagedType.LPStr)] string pszIconPath,
                int iIcon);

            //[helpstring("Sets the shell link relative path")]
            void SetRelativePath(
                [MarshalAs(UnmanagedType.LPStr)] string pszPathRel,
                uint dwReserved);

            //[helpstring("Resolves a shell link. The system searches for the shell link object and updates the shell link path and its list of identifiers (if necessary)")]
            void Resolve(
                IntPtr hWnd,
                uint fFlags);

            //[helpstring("Sets the shell link path and filename")]
            void SetPath(
                [MarshalAs(UnmanagedType.LPStr)] string pszFile);
        }

        [ComImport]
        [Guid("000214F9-0000-0000-C000-000000000046")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IShellLinkW
        {
            //[helpstring("Retrieves the path and filename of a shell link object")]
            void GetPath(
                [Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszFile,
                int cchMaxPath,
                ref _WIN32_FIND_DATAW pfd,
                uint fFlags);

            //[helpstring("Retrieves the list of shell link item identifiers")]
            void GetIDList(out IntPtr ppidl);

            //[helpstring("Sets the list of shell link item identifiers")]
            void SetIDList(IntPtr pidl);

            //[helpstring("Retrieves the shell link description string")]
            void GetDescription(
                [Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszFile,
                int cchMaxName);

            //[helpstring("Sets the shell link description string")]
            void SetDescription(
                [MarshalAs(UnmanagedType.LPWStr)] string pszName);

            //[helpstring("Retrieves the name of the shell link working directory")]
            void GetWorkingDirectory(
                [Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszDir,
                int cchMaxPath);

            //[helpstring("Sets the name of the shell link working directory")]
            void SetWorkingDirectory(
                [MarshalAs(UnmanagedType.LPWStr)] string pszDir);

            //[helpstring("Retrieves the shell link command-line arguments")]
            void GetArguments(
                [Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszArgs,
                int cchMaxPath);

            //[helpstring("Sets the shell link command-line arguments")]
            void SetArguments(
                [MarshalAs(UnmanagedType.LPWStr)] string pszArgs);

            //[propget, helpstring("Retrieves or sets the shell link hot key")]
            void GetHotkey(out short pwHotkey);
            //[propput, helpstring("Retrieves or sets the shell link hot key")]
            void SetHotkey(short pwHotkey);

            //[propget, helpstring("Retrieves or sets the shell link show command")]
            void GetShowCmd(out uint piShowCmd);
            //[propput, helpstring("Retrieves or sets the shell link show command")]
            void SetShowCmd(uint piShowCmd);

            //[helpstring("Retrieves the location (path and index) of the shell link icon")]
            void GetIconLocation(
                [Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszIconPath,
                int cchIconPath,
                out int piIcon);

            //[helpstring("Sets the location (path and index) of the shell link icon")]
            void SetIconLocation(
                [MarshalAs(UnmanagedType.LPWStr)] string pszIconPath,
                int iIcon);

            //[helpstring("Sets the shell link relative path")]
            void SetRelativePath(
                [MarshalAs(UnmanagedType.LPWStr)] string pszPathRel,
                uint dwReserved);

            //[helpstring("Resolves a shell link. The system searches for the shell link object and updates the shell link path and its list of identifiers (if necessary)")]
            void Resolve(
                IntPtr hWnd,
                uint fFlags);

            //[helpstring("Sets the shell link path and filename")]
            void SetPath(
                [MarshalAs(UnmanagedType.LPWStr)] string pszFile);
        }

        [Guid("00021401-0000-0000-C000-000000000046")]
        [ClassInterface(ClassInterfaceType.None)]
        [ComImport]
        public class CShellLink
        {
        }

        public enum EShellLinkGP : uint
        {
            SLGP_SHORTPATH = 1,
            SLGP_UNCPRIORITY = 2
        }

        [ComImport, InterfaceType(ComInterfaceType.InterfaceIsIUnknown), Guid("0000000B-0000-0000-C000-000000000046")]
        public interface IStorage
        {
            [return: MarshalAs(UnmanagedType.Interface)]
            IStream CreateStream([In, MarshalAs(UnmanagedType.BStr)] string pwcsName, [In, MarshalAs(UnmanagedType.U4)] int grfMode, [In, MarshalAs(UnmanagedType.U4)] int reserved1, [In, MarshalAs(UnmanagedType.U4)] int reserved2);
            [return: MarshalAs(UnmanagedType.Interface)]
            IStream OpenStream([In, MarshalAs(UnmanagedType.BStr)] string pwcsName, IntPtr reserved1, [In, MarshalAs(UnmanagedType.U4)] int grfMode, [In, MarshalAs(UnmanagedType.U4)] int reserved2);
            [return: MarshalAs(UnmanagedType.Interface)]
            IStorage CreateStorage([In, MarshalAs(UnmanagedType.BStr)] string pwcsName, [In, MarshalAs(UnmanagedType.U4)] int grfMode, [In, MarshalAs(UnmanagedType.U4)] int reserved1, [In, MarshalAs(UnmanagedType.U4)] int reserved2);
            [return: MarshalAs(UnmanagedType.Interface)]
            IStorage OpenStorage([In, MarshalAs(UnmanagedType.BStr)] string pwcsName, IntPtr pstgPriority, [In, MarshalAs(UnmanagedType.U4)] int grfMode, IntPtr snbExclude, [In, MarshalAs(UnmanagedType.U4)] int reserved);
            void CopyTo(int ciidExclude, [In, MarshalAs(UnmanagedType.LPArray)] Guid[] pIIDExclude, IntPtr snbExclude, [In, MarshalAs(UnmanagedType.Interface)] IStorage stgDest);
            void MoveElementTo([In, MarshalAs(UnmanagedType.BStr)] string pwcsName, [In, MarshalAs(UnmanagedType.Interface)] IStorage stgDest, [In, MarshalAs(UnmanagedType.BStr)] string pwcsNewName, [In, MarshalAs(UnmanagedType.U4)] int grfFlags);
            void Commit(int grfCommitFlags);
            void Revert();
            void EnumElements([In, MarshalAs(UnmanagedType.U4)] int reserved1, IntPtr reserved2, [In, MarshalAs(UnmanagedType.U4)] int reserved3, [MarshalAs(UnmanagedType.Interface)] out object ppVal);
            void DestroyElement([In, MarshalAs(UnmanagedType.BStr)] string pwcsName);
            void RenameElement([In, MarshalAs(UnmanagedType.BStr)] string pwcsOldName, [In, MarshalAs(UnmanagedType.BStr)] string pwcsNewName);
            void SetElementTimes([In, MarshalAs(UnmanagedType.BStr)] string pwcsName, [In] System.Runtime.InteropServices.ComTypes.FILETIME pctime, [In] System.Runtime.InteropServices.ComTypes.FILETIME patime, [In] System.Runtime.InteropServices.ComTypes.FILETIME pmtime);
            void SetClass([In] ref Guid clsid);
            void SetStateBits(int grfStateBits, int grfMask);
            void Stat([Out]out System.Runtime.InteropServices.ComTypes.STATSTG pStatStg, int grfStatFlag);
        }

        [ComImport, Guid("0000000A-0000-0000-C000-000000000046"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface ILockBytes
        {
            void ReadAt([In, MarshalAs(UnmanagedType.U8)] long ulOffset, [Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] byte[] pv, [In, MarshalAs(UnmanagedType.U4)] int cb, [Out, MarshalAs(UnmanagedType.LPArray)] int[] pcbRead);
            void WriteAt([In, MarshalAs(UnmanagedType.U8)] long ulOffset, IntPtr pv, [In, MarshalAs(UnmanagedType.U4)] int cb, [Out, MarshalAs(UnmanagedType.LPArray)] int[] pcbWritten);
            void Flush();
            void SetSize([In, MarshalAs(UnmanagedType.U8)] long cb);
            void LockRegion([In, MarshalAs(UnmanagedType.U8)] long libOffset, [In, MarshalAs(UnmanagedType.U8)] long cb, [In, MarshalAs(UnmanagedType.U4)] int dwLockType);
            void UnlockRegion([In, MarshalAs(UnmanagedType.U8)] long libOffset, [In, MarshalAs(UnmanagedType.U8)] long cb, [In, MarshalAs(UnmanagedType.U4)] int dwLockType);
            void Stat([Out]out System.Runtime.InteropServices.ComTypes.STATSTG pstatstg, [In, MarshalAs(UnmanagedType.U4)] int grfStatFlag);
        }
    }
}
