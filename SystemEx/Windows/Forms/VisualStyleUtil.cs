using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Windows.Forms.VisualStyles;
using SystemEx.Win32;

namespace SystemEx.Windows.Forms
{
    public static class VisualStyleUtil
    {
        public static void StyleTreeView(System.Windows.Forms.TreeView treeView)
        {
            if (treeView == null)
                throw new ArgumentNullException("treeView");

            if (Application.VisualStyleState != VisualStyleState.NoneEnabled)
                NativeMethods.SetWindowTheme(treeView.Handle, "explorer", null);
        }

        public static void StyleListView(System.Windows.Forms.ListView listView)
        {
            if (listView == null)
                throw new ArgumentNullException("listView");

            if (Application.VisualStyleState != VisualStyleState.NoneEnabled)
                NativeMethods.SetWindowTheme(listView.Handle, "explorer", null);
        }
    }
}
