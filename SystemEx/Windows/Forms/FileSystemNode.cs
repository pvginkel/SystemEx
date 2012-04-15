using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace SystemEx.Windows.Forms
{
    public class FileSystemNode
    {
        public FileSystemNode(TreeNode treeNode, FileInfo fileInfo)
        {
            if (treeNode == null)
                throw new ArgumentNullException("treeNode");
            if (fileInfo == null)
                throw new ArgumentNullException("fileInfo");

            treeNode.Tag = this;

            TreeNode = treeNode;
            FileInfo = fileInfo;
        }

        public FileInfo FileInfo { get; private set; }
        public TreeNode TreeNode { get; private set; }
        public object Tag { get; set; }

        internal bool IsLoaded { get; set; }
    }
}
