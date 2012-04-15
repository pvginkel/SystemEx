using System;
using System.Collections.Generic;
using System.Text;
using SystemEx.Win32;

namespace SystemEx.Windows.Forms
{
    [Flags]
    [CLSCompliant(false)]
    public enum BrowseForFolderOptions : uint
    {
        ReturnOnlyFileSystemDirectories = NativeMethods.BIF_RETURNONLYFSDIRS,
        DoNotGoBelowDomain = NativeMethods.BIF_DONTGOBELOWDOMAIN,
        StatusText = NativeMethods.BIF_STATUSTEXT,
        ReturnFileSystemAncestors = NativeMethods.BIF_RETURNFSANCESTORS,
        EditBox= NativeMethods.BIF_EDITBOX,
        Validate = NativeMethods.BIF_VALIDATE,
        NewDialogStyle = NativeMethods.BIF_NEWDIALOGSTYLE,
        UseNewUI = NativeMethods.BIF_USENEWUI,
        BrowseIncludeUrls = NativeMethods.BIF_BROWSEINCLUDEURLS,
        UAHint = NativeMethods.BIF_UAHINT,
        NoNewFolderButton = NativeMethods.BIF_NONEWFOLDERBUTTON,
        NoTranslateTargets = NativeMethods.BIF_NOTRANSLATETARGETS,
        BrowseForComputer = NativeMethods.BIF_BROWSEFORCOMPUTER,
        BrowseForPrinter = NativeMethods.BIF_BROWSEFORPRINTER,
        BrowseIncludeFiles = NativeMethods.BIF_BROWSEINCLUDEFILES,
        Sharable = NativeMethods.BIF_SHAREABLE,
    }
}
