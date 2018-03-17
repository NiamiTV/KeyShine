using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace KSP_LogiRGB.Layout.LayoutProviders.Windows
{
    internal static class User32
    {
        internal const uint ScanCodesToVirtualKeysDistinct = 0x03;
        internal const uint VirtualKeysToScanCodesDistinct = 0x04;

        internal const uint ActivateOnLoad = 0x01;
        internal const uint DoNotActivateLayout = 0x80;
        internal const uint IgnoreUserLocale = 0x10;
        
        const int KeyboardLayoutIdLength = 9;

        [DllImport("user32.dll")]
        internal static extern uint MapVirtualKeyEx(uint code, uint mappingType, IntPtr layoutHandle);

        [DllImport("user32.dll")]
        internal static extern IntPtr LoadKeyboardLayout(string layoutId, uint flags);

        [DllImport("user32.dll")]
        internal static extern bool UnloadKeyboardLayout(IntPtr layoutHandle);

        [DllImport("user32.dll")]
        internal static extern IntPtr GetKeyboardLayout(uint idThread);

        [DllImport("user32.dll")]
        internal static extern IntPtr ActivateKeyboardLayout(IntPtr layoutHandle, uint flags);
        
        [DllImport("user32.dll")]
        private static extern uint GetKeyboardLayoutList(int nBuff, [Out] IntPtr[] lpList);

        [DllImport("user32.dll")]
        private static extern long GetKeyboardLayoutName(StringBuilder pwszKLID); 

        internal static HashSet<IntPtr> GetKeyboardLayoutHandles()
        {
            var count = GetKeyboardLayoutList(0, null);
            var handles = new IntPtr[count];
            GetKeyboardLayoutList(handles.Length, handles);
            return new HashSet<IntPtr>(handles);
        }

        internal static string GetKeyboardLayoutId()
        {
            var name = new StringBuilder(KeyboardLayoutIdLength);
            GetKeyboardLayoutName(name);
            return name.ToString();
        }
    }
}