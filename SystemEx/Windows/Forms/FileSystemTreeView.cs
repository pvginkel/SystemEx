using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.IO;
using System.Threading;

namespace SystemEx.Windows.Forms
{
    public class FileSystemTreeView : TreeView
    {
        private static SystemImageList _systemImageList;
        private static int _folderIcon;
        private FileSystemTreeViewMode _mode = FileSystemTreeViewMode.Directories;
        private string _root;
        private readonly object _syncLock = new object();
        private Queue<FileSystemNode> _iconResolveQueue = new Queue<FileSystemNode>();
        private bool _resolvingIcons;
        private bool _showHidden;

        public FileSystemTreeView()
        {
            if (_systemImageList == null)
                LoadSystemImageList();

            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            ShowRootLines = false;
            ShowLines = false;

            VisualStyleUtil.StyleTreeView(this);

            _systemImageList.Assign(this);
        }

        private void LoadSystemImageList()
        {
            _systemImageList = new SystemImageList();

            string tempPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());

            Directory.CreateDirectory(tempPath);

            try
            {
                _folderIcon = _systemImageList.AddShellIcon(tempPath, 0);
            }
            finally
            {
                try
                {
                    Directory.Delete(tempPath);
                }
                catch
                {
                    // Ignore exceptions
                }
            }
        }

        [DefaultValue(false)]
        public new bool ShowRootLines
        {
            get { return base.ShowRootLines; }
            set { base.ShowRootLines = value; }
        }

        [DefaultValue(false)]
        public new bool ShowLines
        {
            get { return base.ShowLines; }
            set { base.ShowLines = value; }
        }

        [DefaultValue(false)]
        public bool ShowHidden
        {
            get { return _showHidden; }
            set
            {
                if (_showHidden != value)
                {
                    _showHidden = value;

                    Reload();
                }
            }
        }

        [DefaultValue(FileSystemTreeViewMode.Directories)]
        public FileSystemTreeViewMode Mode
        {
            get { return _mode; }
            set
            {
                if (_mode != value)
                {
                    _mode = value;

                    Reload();
                }
            }
        }

        [DefaultValue(null)]
        public string Root
        {
            get { return _root; }
            set
            {
                if (_root != value)
                {
                    _root = value;

                    Reload();
                }
            }
        }

        [Category("Behavior")]
        public FileSystemNodeEventHandler NodeCreated;

        protected virtual void OnNodeCreated(FileSystemNodeEventArgs e)
        {
            var ev = NodeCreated;

            if (ev != null)
                ev(this, e);
        }

        [Category("Behavior")]
        public FileSystemNodeEventHandler NodeSelected;

        protected virtual void OnNodeSelected(FileSystemNodeEventArgs e)
        {
            var ev = NodeSelected;

            if (ev != null)
                ev(this, e);
        }

        public void Reload()
        {
            if (ControlUtil.GetIsInDesignMode(this))
                return;
            if (!IsHandleCreated)
                return;

            BeginUpdate();

            Nodes.Clear();

            if (!String.IsNullOrEmpty(_root))
            {
                var node = CreateNode(_root);

                if (node != null)
                    Nodes.Add(node.TreeNode);

                OnNodeCreated(new FileSystemNodeEventArgs(node));

                StartResolver();
            }

            if (Nodes.Count > 0)
                Nodes[0].Expand();

            EndUpdate();
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            if (!String.IsNullOrEmpty(_root))
                Reload();
        }

        protected override void OnBeforeExpand(TreeViewCancelEventArgs e)
        {
            base.OnBeforeExpand(e);

            var node = (FileSystemNode)e.Node.Tag;

            if (!node.IsLoaded)
            {
                node.TreeNode.Nodes.Clear();

                ThreadPool.QueueUserWorkItem(p => LoadNode((FileSystemNode)p), node);

                node.IsLoaded = true;
            }
        }

        private void LoadNode(FileSystemNode node)
        {
            var nodes = new List<FileSystemNode>();

            AddNodes(nodes, node, true);

            if ((Mode & FileSystemTreeViewMode.FilesAndDirectories) != 0)
                AddNodes(nodes, node, false);

            try
            {
                BeginInvoke(
                    new InvokeDelegate<FileSystemNode, List<FileSystemNode>>((n, l) =>
                    {
                        BeginUpdate();

                        foreach (var item in l)
                        {
                            n.TreeNode.Nodes.Add(item.TreeNode);

                            OnNodeCreated(new FileSystemNodeEventArgs(item));
                        }

                        node.TreeNode.Expand();

                        EndUpdate();
                    }),
                    new object[] { node, nodes }
                );
            }
            catch (InvalidOperationException)
            {
                return;
            }

            StartResolver();
        }

        private void AddNodes(List<FileSystemNode> nodes, FileSystemNode node, bool directories)
        {
            var items = new List<string>();

            foreach (string path in (directories ? Directory.GetDirectories(node.FileInfo.FullName) : Directory.GetFiles(node.FileInfo.FullName)))
            {
                items.Add(Path.GetFileName(path));
            }

            items.Sort((a, b) => String.Compare(a, b, true));

            foreach (string item in items)
            {
                var subNode = CreateNode(Path.Combine(node.FileInfo.FullName, item));

                if (subNode != null)
                    nodes.Add(subNode);
            }
        }

        private FileSystemNode CreateNode(string path)
        {
            var fileInfo = new FileInfo(path);

            if (!_showHidden && (fileInfo.Attributes & FileAttributes.Hidden) != 0)
                return null;

            int imageIndex;

            if ((fileInfo.Attributes & FileAttributes.Directory) != 0)
            {
                imageIndex = _folderIcon;
            }
            else
            {
                string dummyName = "dummy" + Path.GetExtension(path);

                imageIndex = _systemImageList.AddShellIcon(dummyName, ShellIconType.UseFileAttributes);
            }

            string name = String.IsNullOrEmpty(fileInfo.Name) ? fileInfo.FullName : fileInfo.Name;

            var node = new FileSystemNode(new TreeNode(name, imageIndex, imageIndex), fileInfo);

            if ((fileInfo.Attributes & FileAttributes.Directory) != 0)
            {
                if (!DirectoryContainsItems(fileInfo))
                    node.IsLoaded = true;
                else
                    node.TreeNode.Nodes.Add(new TreeNode("Loading..."));
            }

            _iconResolveQueue.Enqueue(node);

            return node;
        }

        private void StartResolver()
        {
            lock (_syncLock)
            {
                if (!_resolvingIcons && _iconResolveQueue.Count > 0)
                {
                    _resolvingIcons = true;

                    ThreadPool.QueueUserWorkItem(p => ResolveIcons(), null);
                }
            }
        }

        private void ResolveIcons()
        {
            while (true)
            {
                FileSystemNode node = null;

                lock (_syncLock)
                {
                    if (_iconResolveQueue.Count > 0)
                    {
                        node = _iconResolveQueue.Dequeue();
                    }
                    else
                    {
                        _resolvingIcons = false;

                        return;
                    }
                }

                int imageIndex = _systemImageList.AddShellIcon(
                    node.FileInfo.FullName,
                    (node.FileInfo.Attributes & FileAttributes.Directory) == 0
                    ? ShellIconType.UseFileAttributes
                    : 0
                );

                if (imageIndex != node.TreeNode.ImageIndex)
                {
                    try
                    {
                        BeginInvoke(
                            new InvokeDelegate<FileSystemNode, int>((n, i) =>
                            {
                                BeginUpdate();

                                n.TreeNode.ImageIndex = i;
                                n.TreeNode.SelectedImageIndex = i;

                                EndUpdate();
                            }),
                            new object[] { node, imageIndex }
                        );
                    }
                    catch (InvalidOperationException)
                    {
                        return;
                    }
                }
            }
        }

        private bool DirectoryContainsItems(FileInfo fileInfo)
        {
            string path = fileInfo.FullName.TrimEnd(Path.DirectorySeparatorChar) + Path.DirectorySeparatorChar + "*";

            var findData = new Win32.NativeMethods.WIN32_FIND_DATA();

            var handle = Win32.NativeMethods.FindFirstFile(path, out findData);

            if (handle != Win32.NativeMethods.INVALID_HANDLE_VALUE)
            {
                try
                {
                    do
                    {
                        if (findData.cFileName == "." || findData.cFileName == "..")
                            continue;

                        if (!_showHidden && (findData.dwFileAttributes & Win32.NativeMethods.FILE_ATTRIBUTE_HIDDEN) != 0)
                            continue;

                        if (Mode == FileSystemTreeViewMode.FilesAndDirectories)
                            return true;
                        else if ((findData.dwFileAttributes & Win32.NativeMethods.FILE_ATTRIBUTE_DIRECTORY) != 0)
                            return true;
                    }
                    while (Win32.NativeMethods.FindNextFile(handle, out findData));
                }
                finally
                {
                    Win32.NativeMethods.FindClose(handle);
                }
            }

            return false;
        }

        protected override void OnAfterSelect(TreeViewEventArgs e)
        {
            base.OnAfterSelect(e);

            OnNodeSelected(e.Node == null ? null : new FileSystemNodeEventArgs((FileSystemNode)e.Node.Tag));
        }

        private delegate void InvokeDelegate<T1, T2>(T1 arg1, T2 arg2);
    }
}
