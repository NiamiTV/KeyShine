using System;
using System.Collections.Generic;
using UnityEngine;

namespace KSP_LogiRGB.Layout.LayoutProviders.Windows
{
    public class WindowsLayoutProvider : ILayoutProvider
    {
        private const string QwertyUS = "00000409";
        private const string DvorakUS = "00010409";

        private enum VirtualKey : ushort
        {
            None = 0x00,
            Backspace = 0x08,
            Tab = 0x09,
            Clear = 0x0C,
            Return = 0x0D,
            Pause = 0x1B,
            Escape = 0x13,
            Space = 0x20,
            Quote = 0xDE,
            Plus = 0xBB,
            Comma = 0xBC,
            Minus = 0xBD,
            Period = 0xBE,
            Slash = 0xBF,
            Alpha0 = 0x30,
            Alpha1 = 0x31,
            Alpha2 = 0x32,
            Alpha3 = 0x33,
            Alpha4 = 0x34,
            Alpha5 = 0x35,
            Alpha6 = 0x36,
            Alpha7 = 0x37,
            Alpha8 = 0x38,
            Alpha9 = 0x39,
            Semicolon = 0xBA,
            LeftBracket = 0xDB,
            Backslash = 0xDC,
            RightBracket = 0xDD,
            BackQuote = 0xC0,
            A = 0x41,
            B = 0x42,
            C = 0x43,
            D = 0x44,
            E = 0x45,
            F = 0x46,
            G = 0x47,
            H = 0x48,
            I = 0x49,
            J = 0x4A,
            K = 0x4B,
            L = 0x4C,
            M = 0x4D,
            N = 0x4E,
            O = 0x4F,
            P = 0x50,
            Q = 0x51,
            R = 0x52,
            S = 0x53,
            T = 0x54,
            U = 0x55,
            V = 0x56,
            W = 0x57,
            X = 0x58,
            Y = 0x59,
            Z = 0x5A,
            Delete = 0x2E,
            UpArrow = 0x26,
            DownArrow = 0x28,
            RightArrow = 0x27,
            LeftArrow = 0x25,
            Insert = 0x2D,
            Home = 0x24,
            End = 0x23,
            PageUp = 0x21,
            PageDown = 0x22,
            F1 = 0x70,
            F2 = 0x71,
            F3 = 0x72,
            F4 = 0x73,
            F5 = 0x74,
            F6 = 0x75,
            F7 = 0x76,
            F8 = 0x77,
            F9 = 0x78,
            F10 = 0x79,
            F11 = 0x7A,
            F12 = 0x7B,
            F13 = 0x7C,
            F14 = 0x7D,
            F15 = 0x7E,
            Numlock = 0x90,
            CapsLock = 0x14,
            ScrollLock = 0x91,
            RightShift = 0xA1,
            LeftShift = 0xA0,
            RightControl = 0xA3,
            LeftControl = 0xA2,
            RightAlt = 0xA5,
            LeftAlt = 0xA4,
            LeftWindows = 0x5B,
            RightWindows = 0x5C,
            Help = 0x2F,
            Print = 0x2A,
            SysReq = 0x2C,
            Menu = 0x5D
        }

        private static Dictionary<KeyCode, VirtualKey> _unityToVirtualMap;

        private static Dictionary<KeyCode, VirtualKey> KeyCodeToVirtualKeyMap()
        {
            if (_unityToVirtualMap == null)
            {
                _unityToVirtualMap = new Dictionary<KeyCode, VirtualKey>();

                foreach (KeyCode keyCode in Enum.GetValues(typeof(KeyCode)))
                {
                    try
                    {
                        VirtualKey userKey = (VirtualKey) Enum.Parse(
                            typeof(VirtualKey),
                            Enum.GetName(typeof(KeyCode), keyCode) ?? "");
                        _unityToVirtualMap[keyCode] = userKey;
                    }
                    catch (ArgumentException)
                    {
                        _unityToVirtualMap[keyCode] = VirtualKey.None;
                    }
                }
            }

            return _unityToVirtualMap;
        }
        
        private static Dictionary<VirtualKey, KeyCode> _qwertyMapping;
        private static IntPtr? _previousLayout;

        private static Dictionary<VirtualKey, KeyCode> GetQwertyMapping()
        {
            if (_qwertyMapping == null || _previousLayout != User32.GetKeyboardLayout(0))
            {
                CalculateQwertyMapping();
            }

            return _qwertyMapping;
        }

        private static void CalculateQwertyMapping()
        {
            // Get a list of keyboard layouts already loaded.
            var loadedHandles = User32.GetKeyboardLayoutHandles();

            // Store the handle to the current layout.
            _previousLayout = User32.GetKeyboardLayout(0);

            // Load the US QWERTY layout, User32.IgnoreUserLocale prevents Windows for loading a layout for
            // the user's given locale, which won't be removed properly after the method ends.
            var qwertyLayoutHandle = User32.LoadKeyboardLayout(
                    QwertyUS, User32.DoNotActivateLayout | User32.IgnoreUserLocale);

            // Special fast path for the off-chance (lol) that the user is already using the US QWERTY layout.
            if (_previousLayout == qwertyLayoutHandle)
            {
                // An empty map will cause the mapper to always default to the QWERTY mapping.
                _qwertyMapping = new Dictionary<VirtualKey, KeyCode>();
                return;
            }
            
            // Make sure we are still using the previous layout to map the keys.
            User32.ActivateKeyboardLayout(_previousLayout.Value, 0);

            _qwertyMapping = new Dictionary<VirtualKey, KeyCode>();

            foreach (ushort virtualKey in Enum.GetValues(typeof(VirtualKey)))
            {
                // Get the virtual key code corresponding to the scan code.
                var scanCode = Convert.ToUInt16(User32.MapVirtualKeyEx(
                    virtualKey,
                    User32.VirtualKeysToScanCodesDistinct,
                    _previousLayout.Value));

                if (scanCode != 0)
                {
                    // Get the equivalent QWERTY key code for the given scan code.
                    var qwertyKeyCode = User32.MapVirtualKeyEx(
                        scanCode,
                        User32.ScanCodesToVirtualKeysDistinct,
                        qwertyLayoutHandle);

                    try
                    {
                        // Get the Unity keycode corresponding to the QWERTY virtual key.
                        var keyName = Enum.GetName(typeof(VirtualKey), (VirtualKey) qwertyKeyCode);
                        KeyCode code = (KeyCode)Enum.Parse(
                            typeof(KeyCode),
                            keyName ?? throw new ArgumentException());

                        _qwertyMapping[(VirtualKey)virtualKey] = code;
                    }
                    catch (ArgumentException)
                    {
                        // Do nothing.
                    }
                }
            }

            // Unload the QWERTY layout if it was only loaded by this function.
            if (_previousLayout != qwertyLayoutHandle && !loadedHandles.Contains(qwertyLayoutHandle))
            {
                User32.UnloadKeyboardLayout(qwertyLayoutHandle);
            }
        }

        /// <inheritdoc />
        public KeyCode ConvertToQwertyCode(KeyCode nativeCode)
        {
            if (KeyCodeToVirtualKeyMap().ContainsKey(nativeCode))
            {
                var virtualKey = KeyCodeToVirtualKeyMap()[nativeCode];
                if (virtualKey != VirtualKey.None && GetQwertyMapping().ContainsKey(virtualKey))
                {
                    return GetQwertyMapping()[virtualKey];
                }
            }

            return nativeCode;
        }

        /// <inheritdoc />
        public ITemporaryLayout LoadQwertyLayout()
        {
            return new WindowsTemporaryLayout(QwertyUS);
        }

        /// <inheritdoc />
        public ITemporaryLayout LoadDvorakLayout()
        {
            return new WindowsTemporaryLayout(DvorakUS);
        }
    }
}