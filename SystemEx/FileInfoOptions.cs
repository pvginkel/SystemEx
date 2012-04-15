using System;
using System.Collections.Generic;
using System.Text;

namespace SystemEx
{
    [Flags]
    public enum FileInfoOptions
    {
        Icon = 0x100,                // get icon 
        DisplayName = 0x200,         // get display name 
        TypeName = 0x400,            // get type name 
        Attributes = 0x800,          // get attributes 
        IconLocation = 0x1000,       // get icon location 
        ExeType = 0x2000,            // return exe type 
        SysIconIndex = 0x4000,       // get system icon index 
        LinkOverlay = 0x8000,        // put a link overlay on icon 
        Selected = 0x10000,          // show icon in selected state 
        OnlyAttributesSpecified = 0x20000,    // get only specified attributes 
        LargeIcon = 0x0,             // get large icon 
        SmallIcon = 0x1,             // get small icon 
        OpenIcon = 0x2,              // get open icon 
        ShellIconSize = 0x4,         // get shell size icon 
        //SHGFI_PIDL = 0x8,                  // pszPath is a pidl 
        UseFileAttributes = 0x10,     // use passed dwFileAttribute 
        AddOverlays = 0x000000020,     // apply the appropriate overlays
        OverlayIndex = 0x000000040     // Get the index of the overlay
    }
}
