﻿//
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
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;

namespace KeyShine.LEDControllers.Logitech
{
    public static class LogitechSDK
    {
        public enum KeyName
        {
            Esc = 0x01,
            F1 = 0x3b,
            F2 = 0x3c,
            F3 = 0x3d,
            F4 = 0x3e,
            F5 = 0x3f,
            F6 = 0x40,
            F7 = 0x41,
            F8 = 0x42,
            F9 = 0x43,
            F10 = 0x44,
            F11 = 0x57,
            F12 = 0x58,
            PrintScreen = 0x137,
            ScrollLock = 0x46,
            PauseBreak = 0x145,
            Tilde = 0x29,
            One = 0x02,
            Two = 0x03,
            Three = 0x04,
            Four = 0x05,
            Five = 0x06,
            Six = 0x07,
            Seven = 0x08,
            Eight = 0x09,
            Nine = 0x0A,
            Zero = 0x0B,
            Minus = 0x0C,
            KeyboardEquals = 0x0D,
            Backspace = 0x0E,
            Insert = 0x152,
            Home = 0x147,
            PageUp = 0x149,
            NumLock = 0x45,
            NumSlash = 0x135,
            NumAsterisk = 0x37,
            NumMinus = 0x4A,
            Tab = 0x0F,
            Q = 0x10,
            W = 0x11,
            E = 0x12,
            R = 0x13,
            T = 0x14,
            Y = 0x15,
            U = 0x16,
            I = 0x17,
            O = 0x18,
            P = 0x19,
            OpenBracket = 0x1A,
            CloseBracket = 0x1B,
            Backslash = 0x2B,
            KeyboardDelete = 0x153,
            End = 0x14F,
            PageDown = 0x151,
            NumSeven = 0x47,
            NumEight = 0x48,
            NumNine = 0x49,
            NumPlus = 0x4E,
            CapsLock = 0x3A,
            A = 0x1E,
            S = 0x1F,
            D = 0x20,
            F = 0x21,
            G = 0x22,
            H = 0x23,
            J = 0x24,
            K = 0x25,
            L = 0x26,
            Semicolon = 0x27,
            Apostrophe = 0x28,
            Enter = 0x1C,
            NumFour = 0x4B,
            NumFive = 0x4C,
            NumSix = 0x4D,
            LeftShift = 0x2A,
            Z = 0x2C,
            X = 0x2D,
            C = 0x2E,
            V = 0x2F,
            B = 0x30,
            N = 0x31,
            M = 0x32,
            Comma = 0x33,
            Period = 0x34,
            ForwardSlash = 0x35,
            RightShift = 0x36,
            ArrowUp = 0x148,
            NumOne = 0x4F,
            NumTwo = 0x50,
            NumThree = 0x51,
            NumEnter = 0x11C,
            LeftControl = 0x1D,
            LeftWindows = 0x15B,
            LeftAlt = 0x38,
            Space = 0x39,
            RightAlt = 0x138,
            RightWindows = 0x15C,
            ApplicationSelect = 0x15D,
            RightControl = 0x11D,
            ArrowLeft = 0x14B,
            ArrowDown = 0x150,
            ArrowRight = 0x14D,
            NumZero = 0x52,
            NumPeriod = 0x53,
            NonUSBackslash = 0x56,
            NonUSHash = 0x5d,
            G1 = 0xFFF1,
            G2 = 0xFFF2,
            G3 = 0xFFF3,
            G4 = 0xFFF4,
            G5 = 0xFFF5,
            G6 = 0xFFF6,
            G7 = 0xFFF7,
            G8 = 0xFFF8,
            G9 = 0xFFF9,
            GLogo = 0xFFFF1,
            GBadge = 0xFFFF2
        };

        //LED SDK
        private const int LogiDevicetypeMonochromeOrd = 0;
        private const int LogiDevicetypeRGBOrd = 1;
        private const int LogiDevicetypePerkeyRGBOrd = 2;

        public const int LogiDevicetypeMonochrome = 1 << LogiDevicetypeMonochromeOrd;
        public const int LogiDevicetypeRGB = 1 << LogiDevicetypeRGBOrd;
        public const int LogiDevicetypePerkeyRGB = 1 << LogiDevicetypePerkeyRGBOrd;
        public const int LogiLedBitmapWidth = 21;
        public const int LogiLEDBitmapHeight = 6;
        public const int LogiLEDBitmapBytesPerKey = 4;

        public const int LogiLEDBitmapSize =
            LogiLedBitmapWidth * LogiLEDBitmapHeight * LogiLEDBitmapBytesPerKey;

        public const int LogiLEDDurationInfinite = 0;

//        [DllImport("LogitechLedEnginesWrapper ", CallingConvention = CallingConvention.Cdecl)]
        private delegate bool LogiLedInit();

//        //Config option functions
//        [DllImport("LogitechLedEnginesWrapper ", CallingConvention = CallingConvention.Cdecl)]
//        public static extern bool LogiLedGetConfigOptionNumber(
//            [MarshalAs(UnmanagedType.LPWStr)] String configPath,
//            ref double defaultNumber);
//
//        [DllImport("LogitechLedEnginesWrapper ", CallingConvention = CallingConvention.Cdecl)]
//        public static extern bool LogiLedGetConfigOptionBool(
//            [MarshalAs(UnmanagedType.LPWStr)] String configPath,
//            ref bool defaultRed);
//
//        [DllImport("LogitechLedEnginesWrapper ", CallingConvention = CallingConvention.Cdecl)]
//        public static extern bool LogiLedGetConfigOptionColor(
//            [MarshalAs(UnmanagedType.LPWStr)] String configPath,
//            ref int defaultRed, ref int defaultGreen, ref int defaultBlue);
//
//        [DllImport("LogitechLedEnginesWrapper ", CallingConvention = CallingConvention.Cdecl)]
//        public static extern bool LogiLedGetConfigOptionKeyInput(
//            [MarshalAs(UnmanagedType.LPWStr)] String configPath,
//            StringBuilder buffer, int bufsize);
//        /////////////////////
//
//        [DllImport("LogitechLedEnginesWrapper ", CallingConvention = CallingConvention.Cdecl)]
        private delegate bool LogiLedSetTargetDevice(int targetDevice);
//
//        [DllImport("LogitechLedEnginesWrapper ", CallingConvention = CallingConvention.Cdecl)]
//        public static extern bool LogiLedGetSdkVersion(ref int majorNum, ref int minorNum, ref int buildNum);
//
//        [DllImport("LogitechLedEnginesWrapper ", CallingConvention = CallingConvention.Cdecl)]
//        public static extern bool LogiLedSaveCurrentLighting();
//
//        [DllImport("LogitechLedEnginesWrapper ", CallingConvention = CallingConvention.Cdecl)]
//        public static extern bool LogiLedSetLighting(int redPercentage, int greenPercentage,
//            int bluePercentage);
//
//        [DllImport("LogitechLedEnginesWrapper ", CallingConvention = CallingConvention.Cdecl)]
//        public static extern bool LogiLedRestoreLighting();
//
//        [DllImport("LogitechLedEnginesWrapper ", CallingConvention = CallingConvention.Cdecl)]
//        public static extern bool LogiLedFlashLighting(int redPercentage, int greenPercentage,
//            int bluePercentage,
//            int milliSecondsDuration, int milliSecondsInterval);
//
//        [DllImport("LogitechLedEnginesWrapper ", CallingConvention = CallingConvention.Cdecl)]
//        public static extern bool LogiLedPulseLighting(int redPercentage, int greenPercentage,
//            int bluePercentage,
//            int milliSecondsDuration, int milliSecondsInterval);
//
//        [DllImport("LogitechLedEnginesWrapper ", CallingConvention = CallingConvention.Cdecl)]
//        public static extern bool LogiLedStopEffects();
//
//        [DllImport("LogitechLedEnginesWrapper ", CallingConvention = CallingConvention.Cdecl)]
//        public static extern bool LogiLedExcludeKeysFromBitmap(KeyName[] keyNameList, int listCount);
//
//        [DllImport("LogitechLedEnginesWrapper ", CallingConvention = CallingConvention.Cdecl)]
//        public static extern bool LogiLedSetLightingFromBitmap(byte[] bitmap);

//        [DllImport("LogitechLedEnginesWrapper ", CallingConvention = CallingConvention.Cdecl)]
        private delegate bool LogiLedSetLightingForKeyWithScanCode(int keyCode, int redPercentage,
            int greenPercentage, int bluePercentage);

//        [DllImport("LogitechLedEnginesWrapper ", CallingConvention = CallingConvention.Cdecl)]
//        private delegate bool LogiLedSetLightingForKeyWithHidCode(int keyCode, int redPercentage,
//            int greenPercentage, int bluePercentage);
//
//        [DllImport("LogitechLedEnginesWrapper ", CallingConvention = CallingConvention.Cdecl)]
//        public static extern bool LogiLedSetLightingForKeyWithQuartzCode(int keyCode, int redPercentage,
//            int greenPercentage, int bluePercentage);
//
//        [DllImport("LogitechLedEnginesWrapper ", CallingConvention = CallingConvention.Cdecl)]
        private delegate bool LogiLedSetLightingForKeyWithKeyName(KeyName keyNameCode, int redPercentage,
            int greenPercentage, int bluePercentage);
//
//        [DllImport("LogitechLedEnginesWrapper ", CallingConvention = CallingConvention.Cdecl)]
//        public static extern bool LogiLedSaveLightingForKey(KeyName keyNameName);
//
//        [DllImport("LogitechLedEnginesWrapper ", CallingConvention = CallingConvention.Cdecl)]
//        public static extern bool LogiLedRestoreLightingForKey(KeyName keyNameName);
//
//        [DllImport("LogitechLedEnginesWrapper ", CallingConvention = CallingConvention.Cdecl)]
//        public static extern bool LogiLedFlashSingleKey(KeyName keyNameName, int redPercentage,
//            int greenPercentage,
//            int bluePercentage, int msDuration, int msInterval);
//
//        [DllImport("LogitechLedEnginesWrapper ", CallingConvention = CallingConvention.Cdecl)]
//        public static extern bool LogiLedPulseSingleKey(KeyName keyNameName, int startRedPercentage,
//            int startGreenPercentage, int startBluePercentage, int finishRedPercentage,
//            int finishGreenPercentage,
//            int finishBluePercentage, int msDuration, bool isInfinite);
//
//        [DllImport("LogitechLedEnginesWrapper ", CallingConvention = CallingConvention.Cdecl)]
//        public static extern bool LogiLedStopEffectsOnKey(KeyName keyNameName);

//        [DllImport("LogitechLedEnginesWrapper ", CallingConvention = CallingConvention.Cdecl)]
        public delegate void LogiLedShutdown();

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr LoadLibraryEx(string lpFileName, IntPtr hReservedNull, uint dwFlags);

        [DllImport("kernel32.dll")]
        private static extern IntPtr GetProcAddress(IntPtr hModule, string procedureName);

        [DllImport("kernel32.dll")]
        private static extern bool FreeLibrary(IntPtr hModule);

        private static IntPtr _dllHandle = IntPtr.Zero;
        private static LogiLedShutdown _shutdown;
        private static LogiLedSetTargetDevice _setTargetDevice;
        private static LogiLedSetLightingForKeyWithScanCode _setLightingForScanCode;
        private static LogiLedSetLightingForKeyWithKeyName _setLightingForKeyName;

        public static string DllPath
        {
            get
            {
                var platform = IntPtr.Size == 8 ? "x64" : "x86";
                var dllDir = Path.GetDirectoryName(
                    new Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).LocalPath);
                Debug.Assert(dllDir != null, nameof(dllDir) + " != null");
                var apiDir = Path.Combine(dllDir, "Logitech");
                return Path.Combine(apiDir, "LogitechLedEnginesWrapper.nativedll." + platform);
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

            var initAddress = GetProcAddress(_dllHandle, "LogiLedInit");
            if (initAddress == IntPtr.Zero)
            {
                Shutdown();
                return false;
            }

            var success =
                ((LogiLedInit) Marshal.GetDelegateForFunctionPointer(initAddress, typeof(LogiLedInit)))();

            if (!success)
            {
                Shutdown();
            }

            var shutdownAddress = GetProcAddress(_dllHandle, "LogiLedShutdown");
            var setTargetDeviceAddress = GetProcAddress(_dllHandle, "LogiLedSetTargetDevice");
            var setLightingForKeyWithScanCodeAddress =
                GetProcAddress(_dllHandle, "LogiLedSetLightingForKeyWithScanCode");
            var setLightingForKeyWithKeyNameAddress =
                GetProcAddress(_dllHandle, "LogiLedSetLightingForKeyWithKeyName");

            foreach (var address in new[]
                {shutdownAddress, setTargetDeviceAddress, setLightingForKeyWithScanCodeAddress, setLightingForKeyWithKeyNameAddress})
            {
                if (address == IntPtr.Zero)
                {
                    Shutdown();
                    return false;
                }
            }

            _shutdown =
                (LogiLedShutdown) Marshal.GetDelegateForFunctionPointer(shutdownAddress,
                    typeof(LogiLedShutdown));
            _setTargetDevice =
                (LogiLedSetTargetDevice) Marshal.GetDelegateForFunctionPointer(setTargetDeviceAddress,
                    typeof(LogiLedSetTargetDevice));
            _setLightingForScanCode =
                (LogiLedSetLightingForKeyWithScanCode) Marshal.GetDelegateForFunctionPointer(
                    setLightingForKeyWithScanCodeAddress,
                    typeof(LogiLedSetLightingForKeyWithScanCode));
            _setLightingForKeyName =
                (LogiLedSetLightingForKeyWithKeyName) Marshal.GetDelegateForFunctionPointer(
                    setLightingForKeyWithKeyNameAddress,
                    typeof(LogiLedSetLightingForKeyWithKeyName));

            foreach (var methodDelegate in new object[]
                {_shutdown, _setTargetDevice, _setLightingForScanCode})
            {
                if (methodDelegate == null)
                {
                    Shutdown();
                    return false;
                }
            }

            return success;
        }

        public static bool SetTargetDevice(int deviceType)
        {
            return _setTargetDevice != null && _setTargetDevice.Invoke(deviceType);
        }

        public static bool SetKeyLighting(KeyName code, int r, int g, int b)
        {
            return _setLightingForScanCode != null && _setLightingForKeyName.Invoke(code, r, g, b);
        }

        public static bool SetKeyLighting(uint scanCode, int r, int g, int b)
        {
            return _setLightingForScanCode != null && _setLightingForScanCode.Invoke((int) scanCode, r, g, b);
        }

        public static void Shutdown()
        {
            if (_dllHandle != IntPtr.Zero)
            {
                _shutdown?.Invoke();

                _shutdown = null;
                _setTargetDevice = null;
                _setLightingForScanCode = null;
                _setLightingForKeyName = null;

                FreeLibrary(_dllHandle);
                _dllHandle = IntPtr.Zero;
            }
        }
    }
}