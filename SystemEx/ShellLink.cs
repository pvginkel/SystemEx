using System.Collections.Generic;
using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using SystemEx.Win32;

// Taken from http://vbaccelerator.com/home/NET/Code/Libraries/Shell_Projects/Creating_and_Modifying_Shortcuts/article.asp

namespace SystemEx
{
    /// <summary>
    /// Utility class to aid working with shortcut (.lnk) files.
    /// </summary>
    public sealed class ShellLink : IDisposable
    {
        // Use Unicode (W) under NT, otherwise use ANSI		
        private NativeMethods.IShellLinkW _linkW;
        private NativeMethods.IShellLinkA _linkA;
        private string _shortcutFile = "";
        private bool _disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="ShellLink"/> class.
        /// </summary>
        public ShellLink()
        {
            if (System.Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                _linkW = (NativeMethods.IShellLinkW)new NativeMethods.CShellLink();
            }
            else
            {
                _linkA = (NativeMethods.IShellLinkA)new NativeMethods.CShellLink();
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ShellLink"/> class from a
        /// specific link file.
        /// </summary>
        /// <param name="linkFile">The Shortcut file to open.</param>
        public ShellLink(string linkFile)
            : this()
        {
            Open(linkFile);
        }

        /// <summary>
        /// Releases all resources used by the <see cref="ShellLink"/>.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (_linkW != null)
                    {
                        Marshal.FinalReleaseComObject(_linkW);
                        _linkW = null;
                    }

                    if (_linkA != null)
                    {
                        Marshal.FinalReleaseComObject(_linkA);
                        _linkA = null;
                    }
                }

                _disposed = true;
            }
        }

        /// <summary>
        /// Get or sets the path of the shortcut file.
        /// </summary>
        public string ShortcutFile
        {
            get
            {
                return _shortcutFile;
            }
            set
            {
                _shortcutFile = value;
            }
        }

        public Icon LargeIcon
        {
            get
            {
                return getIcon(true);
            }
        }

        public Icon SmallIcon
        {
            get
            {
                return getIcon(false);
            }
        }

        private Icon getIcon(bool large)
        {
            // Get icon index and path:
            int iconIndex = 0;
            StringBuilder iconPath = new StringBuilder(260, 260);
            if (_linkA == null)
            {
                _linkW.GetIconLocation(iconPath, iconPath.Capacity, out iconIndex);
            }
            else
            {
                _linkA.GetIconLocation(iconPath, iconPath.Capacity, out iconIndex);
            }
            string iconFile = iconPath.ToString();

            // If there are no details set for the icon, then we must use
            // the shell to get the icon for the target:
            if (iconFile.Length == 0)
            {
                // Use the FileIcon object to get the icon:
                FileInfoOptions flags = FileInfoOptions.Icon | FileInfoOptions.Attributes;

                if (large)
                {
                    flags = flags | FileInfoOptions.LargeIcon;
                }
                else
                {
                    flags = flags | FileInfoOptions.SmallIcon;
                }
                FileIcon fileIcon = new FileIcon(Target, flags);
                return fileIcon.ShellIcon;
            }
            else
            {
                // Use ExtractIconEx to get the icon:
                IntPtr[] hIconEx = new IntPtr[1] { IntPtr.Zero };
                int iconCount;
                if (large)
                {
                    iconCount = NativeMethods.ExtractIconEx(
                        iconFile,
                        iconIndex,
                        hIconEx,
                        null,
                        1);
                }
                else
                {
                    iconCount = NativeMethods.ExtractIconEx(
                        iconFile,
                        iconIndex,
                        null,
                        hIconEx,
                        1);
                }
                // If success then return as a GDI+ object
                Icon icon = null;
                if (hIconEx[0] != IntPtr.Zero)
                {
                    icon = Icon.FromHandle(hIconEx[0]);
                    //UnManagedMethods.DestroyIcon(hIconEx[0]);
                }
                return icon;
            }
        }

        /// <summary>
        /// Get or sets the path to the file containing the icon for this shortcut.
        /// </summary>
        public string IconPath
        {
            get
            {
                StringBuilder iconPath = new StringBuilder(260, 260);
                int iconIndex;
                if (_linkA == null)
                {
                    _linkW.GetIconLocation(iconPath, iconPath.Capacity, out iconIndex);
                }
                else
                {
                    _linkA.GetIconLocation(iconPath, iconPath.Capacity, out iconIndex);
                }
                return iconPath.ToString();
            }
            set
            {
                StringBuilder iconPath = new StringBuilder(260, 260);
                int iconIndex;
                if (_linkA == null)
                {
                    _linkW.GetIconLocation(iconPath, iconPath.Capacity, out iconIndex);
                }
                else
                {
                    _linkA.GetIconLocation(iconPath, iconPath.Capacity, out iconIndex);
                }
                if (_linkA == null)
                {
                    _linkW.SetIconLocation(value, iconIndex);
                }
                else
                {
                    _linkA.SetIconLocation(value, iconIndex);
                }
            }
        }

        /// <summary>
        /// Get or sets the index of this icon within the icon path's resources.
        /// </summary>
        public int IconIndex
        {
            get
            {
                StringBuilder iconPath = new StringBuilder(260, 260);
                int iconIndex;
                if (_linkA == null)
                {
                    _linkW.GetIconLocation(iconPath, iconPath.Capacity, out iconIndex);
                }
                else
                {
                    _linkA.GetIconLocation(iconPath, iconPath.Capacity, out iconIndex);
                }
                return iconIndex;
            }
            set
            {
                StringBuilder iconPath = new StringBuilder(260, 260);
                int iconIndex;
                if (_linkA == null)
                {
                    _linkW.GetIconLocation(iconPath, iconPath.Capacity, out iconIndex);
                }
                else
                {
                    _linkA.GetIconLocation(iconPath, iconPath.Capacity, out iconIndex);
                }
                if (_linkA == null)
                {
                    _linkW.SetIconLocation(iconPath.ToString(), value);
                }
                else
                {
                    _linkA.SetIconLocation(iconPath.ToString(), value);
                }
            }
        }

        /// <summary>
        /// Get or sets the fully qualified path to the link's target.
        /// </summary>
        public string Target
        {
            get
            {
                StringBuilder target = new StringBuilder(260, 260);
                if (_linkA == null)
                {
                    NativeMethods._WIN32_FIND_DATAW fd = new NativeMethods._WIN32_FIND_DATAW();
                    _linkW.GetPath(target, target.Capacity, ref fd, (uint)NativeMethods.EShellLinkGP.SLGP_UNCPRIORITY);
                }
                else
                {
                    NativeMethods._WIN32_FIND_DATAA fd = new NativeMethods._WIN32_FIND_DATAA();
                    _linkA.GetPath(target, target.Capacity, ref fd, (uint)NativeMethods.EShellLinkGP.SLGP_UNCPRIORITY);
                }
                return target.ToString();
            }
            set
            {
                if (_linkA == null)
                {
                    _linkW.SetPath(value);
                }
                else
                {
                    _linkA.SetPath(value);
                }
            }
        }

        /// <summary>
        /// Get or sets the working directory for the Link.
        /// </summary>
        public string WorkingDirectory
        {
            get
            {
                StringBuilder path = new StringBuilder(260, 260);
                if (_linkA == null)
                {
                    _linkW.GetWorkingDirectory(path, path.Capacity);
                }
                else
                {
                    _linkA.GetWorkingDirectory(path, path.Capacity);
                }
                return path.ToString();
            }
            set
            {
                if (_linkA == null)
                {
                    _linkW.SetWorkingDirectory(value);
                }
                else
                {
                    _linkA.SetWorkingDirectory(value);
                }
            }
        }

        /// <summary>
        /// Get or sets the description of the link.
        /// </summary>
        public string Description
        {
            get
            {
                StringBuilder description = new StringBuilder(1024, 1024);
                if (_linkA == null)
                {
                    _linkW.GetDescription(description, description.Capacity);
                }
                else
                {
                    _linkA.GetDescription(description, description.Capacity);
                }
                return description.ToString();
            }
            set
            {
                if (_linkA == null)
                {
                    _linkW.SetDescription(value);
                }
                else
                {
                    _linkA.SetDescription(value);
                }
            }
        }

        /// <summary>
        /// Get or sets any command line arguments associated with the link.
        /// </summary>
        public string Arguments
        {
            get
            {
                StringBuilder arguments = new StringBuilder(260, 260);
                if (_linkA == null)
                {
                    _linkW.GetArguments(arguments, arguments.Capacity);
                }
                else
                {
                    _linkA.GetArguments(arguments, arguments.Capacity);
                }
                return arguments.ToString();
            }
            set
            {
                if (_linkA == null)
                {
                    _linkW.SetArguments(value);
                }
                else
                {
                    _linkA.SetArguments(value);
                }
            }
        }

        /// <summary>
        /// Get or sets the initial display mode when the shortcut is
        /// run.
        /// </summary>
        [CLSCompliant(false)]
        public LinkDisplayMode DisplayMode
        {
            get
            {
                uint cmd;
                if (_linkA == null)
                {
                    _linkW.GetShowCmd(out cmd);
                }
                else
                {
                    _linkA.GetShowCmd(out cmd);
                }
                return (LinkDisplayMode)cmd;
            }
            set
            {
                if (_linkA == null)
                {
                    _linkW.SetShowCmd((uint)value);
                }
                else
                {
                    _linkA.SetShowCmd((uint)value);
                }
            }
        }

        public Keys HotKey
        {
            get
            {
                short key;
                if (_linkA == null)
                {
                    _linkW.GetHotkey(out key);
                }
                else
                {
                    _linkA.GetHotkey(out key);
                }
                return (Keys)key;
            }
            set
            {
                if (_linkA == null)
                {
                    _linkW.SetHotkey((short)value);
                }
                else
                {
                    _linkA.SetHotkey((short)value);
                }
            }
        }

        /// <summary>
        /// Saves the shortcut to the path stored in <see cref="ShortcutFile"/>.
        /// </summary>
        public void Save()
        {
            Save(_shortcutFile);
        }

        /// <summary>
        /// Saves the shortcut to the specified location/
        /// </summary>
        /// <param name="linkFile">The path to store the shortcut to.</param>
        public void Save(string linkFile)
        {
            // Save the object to disk
            if (_linkA == null)
            {
                ((NativeMethods.IPersistFile)_linkW).Save(linkFile, true);
                _shortcutFile = linkFile;
            }
            else
            {
                ((NativeMethods.IPersistFile)_linkA).Save(linkFile, true);
                _shortcutFile = linkFile;
            }
        }

        /// <summary>
        /// Loads a shortcut from the specified location.
        /// </summary>
        /// <param name="linkFile">The shortcut file to load.</param>
        public void Open(string linkFile)
        {
            Open(linkFile,
                IntPtr.Zero,
                (ShellLinkResolveType)(NativeMethods.SLR_ANY_MATCH | NativeMethods.SLR_NO_UI),
                1);
        }

        /// <summary>
        /// Loads a shortcut from the specified location, and allows flags controlling
        /// the UI behavior if the shortcut's target isn't found to be set.
        /// </summary>
        /// <param name="linkFile">The path to load the shortcut from.</param>
        /// <param name="hWnd">The window handle of the application's UI, if any.</param>
        /// <param name="resolveFlags">Flags controlling resolution behavior.</param>
        [CLSCompliant(false)]
        public void Open(string linkFile, IntPtr hWnd, ShellLinkResolveType resolveFlags)
        {
            Open(linkFile,
                hWnd,
                resolveFlags,
                1);
        }

        /// <summary>
        /// Loads a shortcut from the specified location, and allows flags controlling
        /// the UI behavior if the shortcut's target isn't found to be set and a timeout.
        /// </summary>
        /// <param name="linkFile">The path to load the shortcut from.</param>
        /// <param name="hWnd">The window handle of the application's UI, if any.</param>
        /// <param name="resolveFlags">Flags controlling resolution behavior.</param>
        /// <param name="timeout">Timeout if <c>SLR_NO_UI</c> is specified, in milliseconds.</param>
        [CLSCompliant(false)]
        public void Open(string linkFile, IntPtr hWnd, ShellLinkResolveType resolveFlags, ushort timeout)
        {
            uint flags;

            if (((uint)resolveFlags & NativeMethods.SLR_NO_UI)
                == NativeMethods.SLR_NO_UI)
            {
                flags = (uint)((int)resolveFlags | (timeout << 16));
            }
            else
            {
                flags = (uint)resolveFlags;
            }

            if (_linkA == null)
            {
                ((NativeMethods.IPersistFile)_linkW).Load(linkFile, 0); //STGM_DIRECT)
                _linkW.Resolve(hWnd, flags);
                _shortcutFile = linkFile;
            }
            else
            {
                ((NativeMethods.IPersistFile)_linkA).Load(linkFile, 0); //STGM_DIRECT)
                _linkA.Resolve(hWnd, flags);
                _shortcutFile = linkFile;
            }
        }
    }
}
