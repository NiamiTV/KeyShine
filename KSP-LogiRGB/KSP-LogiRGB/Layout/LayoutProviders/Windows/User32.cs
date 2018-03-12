using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace KSP_LogiRGB.Layout.LayoutProviders.Windows
{
    public class User32
    {
        public const uint VirtualKeysToScanCodes = 0x00;
        public const uint ScanCodesToVirtualKeys = 0x01;
        public const uint VirtualKeysToCharacters = 0x02;
        public const uint ScanCodesToVirtualKeysExtended = 0x03;
        public const uint VirtualKeysToScanCodesExtended = 0x04;

        public const uint ActivateLayout = 0x01;
        public const uint DoNotActivateLayout = 0x80;
        public const uint IgnoreUserLocale = 0x10;

        internal const int LayoutCodeLength = 9;

        [DllImport("user32.dll")]
        public static extern uint MapVirtualKey(uint code, uint mappingType);

        [DllImport("user32.dll")]
        public static extern uint MapVirtualKeyEx(uint code, uint mappingType, IntPtr layoutHandle);

        [DllImport("user32.dll")]
        public static extern IntPtr LoadKeyboardLayout(string layoutId, uint flags);

        [DllImport("user32.dll")]
        public static extern bool UnloadKeyboardLayout(IntPtr layoutHandle);

        [DllImport("user32.dll")]
        public static extern IntPtr GetKeyboardLayout(uint idThread);

        [DllImport("user32.dll")]
        public static extern IntPtr ActivateKeyboardLayout(IntPtr layoutHandle, uint flags);

        [DllImport("user32.dll")]
        private static extern bool GetKeyboardLayoutName([Out] StringBuilder stringBuilder);

        [DllImport("user32.dll")]
        private static extern uint GetKeyboardLayoutList(int nBuff, [Out] IntPtr[] lpList);
        
        public static string GetLayoutId()
        {
            var name = new StringBuilder(LayoutCodeLength);
            GetKeyboardLayoutName(name);

            return name.ToString();
        }

        public static HashSet<IntPtr> GetKeyboardLayoutHandles()
        {
            var count = GetKeyboardLayoutList(0, null);
            var handles = new IntPtr[count];
            GetKeyboardLayoutList(handles.Length, handles);
            return new HashSet<IntPtr>(handles);
        }
    }
}