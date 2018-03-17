using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using Expansions.Missions.Editor;
using Microsoft.Win32;

namespace KSP_LogiRGB.Layout.LayoutProviders.Windows
{
    public class WindowsLayoutLoader
    {
        private FileInfo _fileInfo;

        private delegate IntPtr KbdLayerDescriptor();

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr LoadLibraryEx(string lpFileName, IntPtr hReservedNull, uint dwFlags);

        [DllImport("kernel32.dll")]
        private static extern IntPtr GetProcAddress(IntPtr hModule, string procedureName);

        [DllImport("kernel32.dll")]
        private static extern bool FreeLibrary(IntPtr hModule);

        public WindowsLayoutLoader()
        {
            var idString = User32.GetKeyboardLayoutId();
            RegistryKey key =
                Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Control\Keyboard Layouts\" +
                                                 idString);
            if (key == null)
            {
                Console.Error.WriteLine("Could not find a layout entry in the registry for the layout: " +
                                        idString + ".");
                return;
            }

            var layoutDll = (string) key.GetValue("Layout File");

            if (layoutDll == null)
            {
                Console.Error.WriteLine("Could not find a layout file in the registry for the layout: " +
                                        idString + ".");
                return;
            }

            var dllPath = Path.Combine(Environment.SystemDirectory, layoutDll);
            if (!File.Exists(dllPath))
            {
                Console.Error.WriteLine("Could not find the layout file in the system directory. (" +
                                        dllPath + ")");
                return;
            }

            var dllHandle = LoadLibraryEx(dllPath, IntPtr.Zero, 0);
            if (dllHandle == IntPtr.Zero)
            {
                Console.Error.WriteLine("Could not load the library.");
                return;
            }

            var layoutDescriptorAddress = GetProcAddress(dllHandle, "KbdLayerDescriptor");
            if (layoutDescriptorAddress == IntPtr.Zero)
            {
                Console.Error.WriteLine("Could not load the layout descriptor.");
                return;
            }

            var kbdLayerDescriptor =
                (KbdLayerDescriptor) Marshal.GetDelegateForFunctionPointer(layoutDescriptorAddress,
                    typeof(KbdLayerDescriptor));

            IntPtr pkbdTable = kbdLayerDescriptor();
            IntPtr pModifers = Marshal.ReadIntPtr(pkbdTable);
            var modifiers = new Modifiers(pModifers);
            
            IntPtr pVKToCharTable = new IntPtr(pkbdTable.ToInt64() + 8);
            var vkToCharTable = new VirtualKeyToCharTable(Marshal.ReadIntPtr(pVKToCharTable));
            
            FreeLibrary(dllHandle);
        }
    }

    /// <summary>
    /// Keyboard Shift State defines. These correspond to the bit mask defined
    /// by the VkKeyScan() API.
    /// </summary>
    [Flags]
    enum ShiftState : byte
    {
        /// <summary>
        /// Base shift state. no modifiers active.
        /// </summary>
        Base = 0,
        Shift = 1,
        Control = 2,
        Alt = 4,

        // Used by Japanese keyboards for selecting half-width, full-width or kanji characters.
        Kana = 8,

        // These "Oyayubi" thumb shifts are used by some Japanese keyboards as well as other ones.
        RightOyayubi = 0x10,
        LeftOyayubi = 0x20,

        // Nobody really knows wtf this one is. Oh well.
        GroupSelector = 0x80
    }

    class Modifiers
    {
        private Dictionary<byte, ShiftState> _virtualKeyToModifierMap = new Dictionary<byte, ShiftState>();
        private Dictionary<ShiftState, int> _modifierIndex = new Dictionary<ShiftState, int>();
        private Dictionary<int, ShiftState> _modifications = new Dictionary<int, ShiftState>();

        public Dictionary<int, ShiftState> Modifications => new Dictionary<int, ShiftState>(_modifications);

        public Modifiers(IntPtr pointer)
        {
            IntPtr pVirtualKeyMap = Marshal.ReadIntPtr(pointer);
            loadModMap(pVirtualKeyMap);
            pointer = new IntPtr(pointer.ToInt64() + 8);
            ushort maxModBits = (ushort) Marshal.ReadInt16(pointer);
            pointer = new IntPtr(pointer.ToInt64() + 2);
            for (int n = 0; n <= maxModBits; ++n)
            {
                var index = Marshal.ReadByte(pointer);
                if (index != 0x0F)
                {
                    _modifications.Add(index, (ShiftState) n);
                }

                pointer = new IntPtr(pointer.ToInt64() + 1);
            }
        }

        private void loadModMap(IntPtr virtualKeyMapPtr)
        {
            IntPtr ptr = virtualKeyMapPtr;
            int i = 0;
            while (true)
            {
                byte vk = Marshal.ReadByte(ptr);
                ptr = new IntPtr(ptr.ToInt64() + 1);
                ShiftState shiftState = (ShiftState) Marshal.ReadByte(ptr);
                ptr = new IntPtr(ptr.ToInt64() + 1);
                if (vk == 0)
                {
                    break;
                }

                _virtualKeyToModifierMap.Add(vk, shiftState);
                _modifierIndex.Add(shiftState, i);
                i += 1;
            }
        }
    }

    class VirtualKeyToCharTable
    {
        public VirtualKeyToCharTable(IntPtr pointer)
        {
            IntPtr offset = pointer;
            while (true) {
                IntPtr entry = Marshal.ReadIntPtr(offset);
                int modifierTableEntry = Marshal.ReadByte(new IntPtr(offset.ToInt64() + 8));
                int size = Marshal.ReadByte(new IntPtr(offset.ToInt64() + 9));
                if (entry == IntPtr.Zero)
                {
                    return;
                }
                readSubTable(entry, modifierTableEntry);
                Console.WriteLine("0x" + entry.ToString("x16") + " " + modifierTableEntry + " " + size);
                offset = new IntPtr(offset.ToInt64() + 16);
            }
        }

        private void readSubTable(IntPtr subTable, int modifierTableEntry)
        {
            
        }
    }
}