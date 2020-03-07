using System;
using System.IO;
using System.Runtime.InteropServices;
using UnityEngine;

namespace KeyShine.LEDControllers.Corsair
{
    class CorsairSDK
    {
        public struct CorsairLedColor      // contains information about led and its color.
        {
            public CorsairLedId ledId;             // identifier of LED to set.
            public int r;                          // red   brightness[0..255].
            public int g;                          // green brightness[0..255].
            public int b;                          // blue  brightness[0..255].

            public CorsairLedColor(CorsairLedId led, Color color)
            {
                ledId = led;
                /*r = rr;
                g = gg;
                b = bb;
                */
                r = (int)(color.r * 100);
                g = (int)(color.g * 100);
                b = (int)(color.b * 100);
            }
        };
        public enum CorsairLedId
        {
            CLI_Invalid = 0,
            Esc = 1,
            F1 = 2,
            F2 = 3,
            F3 = 4,
            F4 = 5,
            F5 = 6,
            F6 = 7,
            F7 = 8,
            F8 = 9,
            F9 = 10,
            F10 = 11,
            F11 = 12,
            F12 = 73,
            PrintScreen = 74,
            ScrollLock = 75,
            PauseBreak = 76,
            Tilde = 13,
            One = 14,
            Two = 15,
            Three = 16,
            Four = 17,
            Five = 18,
            Six = 19,
            Seven = 20,
            Eight = 21,
            Nine = 22,
            Zero = 23,
            Minus = 24,
            KeyboardEquals = 85,
            Backspace = 87,
            Insert = 77,
            Home = 78,
            PageUp = 79,
            NumLock = 103,
            NumSlash = 104,
            NumAsterisk = 105,
            NumMinus = 106,
            Tab = 25,
            Q = 26,
            W = 27,
            E = 28,
            R = 29,
            T = 30,
            Y = 31,
            U = 32,
            I = 33,
            O = 34,
            P = 35,
            OpenBracket = 36,
            CloseBracket = 80,
            Backslash = 81,
            KeyboardDelete = 88,
            End = 89,
            PageDown = 90,
            CapsLock = 37,
            A = 38,
            S = 39,
            D = 40,
            F = 41,
            G = 42,
            H = 43,
            J = 44,
            K = 45,
            L = 46,
            Semicolon = 47,
            Apostrophe = 48,
            Enter = 83,
            NumFour = 113,
            NumFive = 114,
            NumSix = 115,
            LeftShift = 49,
            Z = 51,
            X = 52,
            C = 53,
            V = 54,
            B = 55,
            N = 56,
            M = 57,
            Comma = 58,
            Period = 59,
            ForwardSlash = 60,
            RightShift = 91,
            ArrowUp = 93,
            NumOne = 116,
            NumTwo = 117,
            NumThree = 118,
            NumEnter = 108,
            LeftControl = 61,
            LeftGui = 62,
            LeftAlt = 63,
            Space = 65,
            RightAlt = 68,
            RightGui = 69,
            ApplicationSelect = 70,
            RightControl = 92,
            ArrowLeft = 94,
            ArrowDown = 95,
            ArrowRight = 96,
            NumZero = 119,
            NumPeriod = 120,
            NonUSBackslash = 50,
            NonUSHash = 82,
            G1 = 121,
            G2 = 122,
            G3 = 123,
            G4 = 124,
            G5 = 125,
            G6 = 126,
            G7 = 127,
            G8 = 128,
            G9 = 129,
            G10 = 130,
            Lang2 = 64,
            Lang1 = 66,
            International2 = 67,
            LedProgramming = 71,
            Brightness = 72,
            International1 = 84,
            International3 = 86,
            WinLock = 97,
            Mute = 98,
            Stop = 99,
            ScanPreviousTrack = 100,
            PlayPause = 101,
            ScanNextTrack = 102,
            NumPlus = 107,
            NumSeven = 109,
            NumEight = 110,
            NumNine = 111,
            NumComma = 112,
            VolumeUp = 131,
            VolumeDown = 132,
            MR = 133,
            M1 = 134,
            M2 = 135,
            M3 = 136,
            G11 = 137,
            G12 = 138,
            G13 = 139,
            G14 = 140,
            G15 = 141,
            G16 = 142,
            G17 = 143,
            G18 = 144,
            International5 = 145,
            International4 = 146,
            Fn = 147,
            Logo = 154,

        };

        public delegate void CorsairLedSetLedsColors(int length, CorsairLedColor[] colors);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr LoadLibraryEx(string lpFileName, IntPtr hReservedNull, uint dwFlags);

        [DllImport("kernel32.dll")]
        private static extern IntPtr GetProcAddress(IntPtr hModule, string procedureName);

        [DllImport("kernel32.dll")]
        private static extern bool FreeLibrary(IntPtr hModule);

        private static IntPtr _dllHandle = IntPtr.Zero;
        private static CorsairLedSetLedsColors _CorsairSetLedsColors;




        public static string DllPath
        {
            get
            {
                var platform = IntPtr.Size == 8 ? ".x64" : "";
                var dllDir = Path.GetDirectoryName(
                    new Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).LocalPath);
                Debug.Assert(dllDir != null, nameof(dllDir) + " != null");
                var apiDir = Path.Combine(dllDir, "Corsair");
                return Path.Combine(apiDir, "CUESDK" + platform + "_2017.dll");
            }
        }


        public static bool Init()
        {
            // Handle the case when the library is already loaded.
            if (_dllHandle != IntPtr.Zero)
            {
                return true;
            }

            _dllHandle = LoadLibraryEx(DllPath, IntPtr.Zero, 0);
            if (_dllHandle == IntPtr.Zero)
            {
                return false;
            }
            /*
            var initAddress = GetProcAddress(_dllHandle, "LogiLedInit");
            if (initAddress == IntPtr.Zero)
            {
                Shutdown();
                return false;
            }

            if (!success)
            {
                Shutdown();
            }
            */
            var CorsairSetLedsColorsAddr =
                GetProcAddress(_dllHandle, "CorsairSetLedsColors");


            if (CorsairSetLedsColorsAddr == IntPtr.Zero)
            {
                Shutdown();
                return false;
            }



            _CorsairSetLedsColors =
                (CorsairLedSetLedsColors)Marshal.GetDelegateForFunctionPointer(CorsairSetLedsColorsAddr,
                    typeof(CorsairLedSetLedsColors));



            if (_CorsairSetLedsColors == null)
            {
                Shutdown();
                return false;
            }


            return true;
        }

        public static void CorsairSetLedsColors(int length, CorsairLedColor[] colors)
        {
            _CorsairSetLedsColors.Invoke(length, colors);
        }


        public static void Shutdown()
        {
            if (_dllHandle != IntPtr.Zero)
            {
                //_shutdown?.Invoke();

                //_shutdown = null;
                //_setTargetDevice = null;
                _CorsairSetLedsColors = null;
                //  _setLightingForKeyName = null;

                FreeLibrary(_dllHandle);
                _dllHandle = IntPtr.Zero;
            }
        }
        //[DllImport(, CallingConvention = CallingConvention.Cdecl)]
        //public static extern void CorsairSetLedsColorsx64(int size, CorsairLedColor[] ledColor);

        //[DllImport(@"Corsair\CUESDK_2017.dll", CallingConvention = CallingConvention.Cdecl)]
        //public static extern void CorsairSetLedsColors(int size, CorsairLedColor[] ledColor);
    }
}
