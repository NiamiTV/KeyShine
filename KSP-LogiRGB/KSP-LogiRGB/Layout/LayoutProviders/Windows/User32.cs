//
// Copyright © Randy The Dev, 2018
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace KeyShine.Layout.LayoutProviders.Windows
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