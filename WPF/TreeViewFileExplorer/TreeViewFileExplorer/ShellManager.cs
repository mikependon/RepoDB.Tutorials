using System;
using System.Drawing;
using System.Runtime.InteropServices;
using TreeViewFileExplorer.Enums;
using TreeViewFileExplorer.Structs;

namespace TreeViewFileExplorer
{
    public class ShellManager
    {
        public static Icon GetIcon(string path, ItemType type, IconSize iconSize, ItemState state)
        {
            var attributes = (uint)(type == ItemType.Folder ? FileAttribute.Directory : FileAttribute.File);
            var flags = (uint)(ShellAttribute.Icon | ShellAttribute.UseFileAttributes);

            if (type == ItemType.Folder && state == ItemState.Open)
            {
                flags = flags | (uint)ShellAttribute.OpenIcon;
            }
            if (iconSize == IconSize.Small)
            {
                flags = flags | (uint)ShellAttribute.SmallIcon;
            }
            else
            {
                flags = flags | (uint)ShellAttribute.LargeIcon;
            }

            var fileInfo = new ShellFileInfo();
            var size = (uint)Marshal.SizeOf(fileInfo);
            var result = Interop.SHGetFileInfo(path, attributes, out fileInfo, size, flags);

            if (result == IntPtr.Zero)
            {
                throw Marshal.GetExceptionForHR(Marshal.GetHRForLastWin32Error());
            }

            try
            {
                return (Icon)Icon.FromHandle(fileInfo.hIcon).Clone();
            }
            catch
            {
                throw;
            }
            finally
            {
                Interop.DestroyIcon(fileInfo.hIcon);
            }
        }
    }
}
