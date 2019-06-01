using System;

namespace TreeViewFileExplorer.Enums
{
    [Flags]
    public enum ShellAttribute : uint
    {
        LargeIcon = 0,              // 0x000000000
        SmallIcon = 1,              // 0x000000001
        OpenIcon = 2,               // 0x000000002
        ShellIconSize = 4,          // 0x000000004
        Pidl = 8,                   // 0x000000008
        UseFileAttributes = 16,     // 0x000000010
        AddOverlays = 32,           // 0x000000020
        OverlayIndex = 64,          // 0x000000040
        Others = 128,               // Not defined, really?
        Icon = 256,                 // 0x000000100  
        DisplayName = 512,          // 0x000000200
        TypeName = 1024,            // 0x000000400
        Attributes = 2048,          // 0x000000800
        IconLocation = 4096,        // 0x000001000
        ExeType = 8192,             // 0x000002000
        SystemIconIndex = 16384,    // 0x000004000
        LinkOverlay = 32768,        // 0x000008000 
        Selected = 65536,           // 0x000010000
        AttributeSpecified = 131072 // 0x000020000
    }
}
