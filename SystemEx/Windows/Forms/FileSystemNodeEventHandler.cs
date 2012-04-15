using System;
using System.Collections.Generic;
using System.Text;

namespace SystemEx.Windows.Forms
{
    public class FileSystemNodeEventArgs : EventArgs
    {
        public FileSystemNodeEventArgs(FileSystemNode node)
        {
            Node = node;
        }

        public FileSystemNode Node { get; private set; }
    }

    public delegate void FileSystemNodeEventHandler(object sender, FileSystemNodeEventArgs e);
}
