using System;
using System.Collections.Generic;
using KSP_LogiRGB.ColorSchemes;
using UnityEngine;
using static KSP_LogiRGB.LEDControllers.Logitech.LogitechSDK;

namespace KSP_LogiRGB.LEDControllers.Logitech
{
    internal class LogitechLEDController : ILEDController
    {
        /// <summary>
        ///     Unity <c>KeyCode</c>s to Logitech layout translation dictionary
        /// </summary>
        private static readonly Dictionary<KeyCode, ScanCode> KeyMapping =
            new Dictionary<KeyCode, ScanCode>
            {
                {KeyCode.A, ScanCode.A},
                {KeyCode.Alpha0, ScanCode.Zero},
                {KeyCode.Alpha1, ScanCode.One},
                {KeyCode.Alpha2, ScanCode.Two},
                {KeyCode.Alpha3, ScanCode.Three},
                {KeyCode.Alpha4, ScanCode.Four},
                {KeyCode.Alpha5, ScanCode.Five},
                {KeyCode.Alpha6, ScanCode.Six},
                {KeyCode.Alpha7, ScanCode.Seven},
                {KeyCode.Alpha8, ScanCode.Eight},
                {KeyCode.Alpha9, ScanCode.Nine},
                // Not sure if this will actually work on a European keyboard.
                {KeyCode.AltGr, ScanCode.RightAlt},
                //{ KeyCode.Ampersand, keyboardNames.D7 },
                //{ KeyCode.Asterisk, keyboardNames.D8 },
                // { KeyCode.At, keyboardNames.OemApostrophe }, blinking

                {KeyCode.B, ScanCode.B},
                {KeyCode.BackQuote, ScanCode.Tilde},
                {KeyCode.Backslash, ScanCode.Backslash},
                {KeyCode.Backspace, ScanCode.Backspace},
                {KeyCode.Break, ScanCode.PauseBreak},

                {KeyCode.C, ScanCode.C},
                {KeyCode.CapsLock, ScanCode.CapsLock},
                //{ KeyCode.Caret, keyboardNames.D6 },
                //{ KeyCode.Colon, keyboardNames.OemPeriod },
                {KeyCode.Comma, ScanCode.Comma},
                {KeyCode.D, ScanCode.D},
                {KeyCode.Delete, ScanCode.KeyboardDelete},
                //{ KeyCode.Dollar, keyboardNames.D4 },
                //{ KeyCode.DoubleQuote, keyboardNames.D2 },
                {KeyCode.DownArrow, ScanCode.ArrowDown},
                {KeyCode.E, ScanCode.E},
                {KeyCode.End, ScanCode.End},
                {KeyCode.Equals, ScanCode.KeyboardEquals},
                {KeyCode.Escape, ScanCode.Esc},
                //{ KeyCode.Exclaim, keyboardNames.D1 },

                {KeyCode.F, ScanCode.F},
                {KeyCode.F1, ScanCode.F1},
                {KeyCode.F2, ScanCode.F2},
                {KeyCode.F3, ScanCode.F3},
                {KeyCode.F4, ScanCode.F4},
                {KeyCode.F5, ScanCode.F5},
                {KeyCode.F6, ScanCode.F6},
                {KeyCode.F7, ScanCode.F7},
                {KeyCode.F8, ScanCode.F8},
                {KeyCode.F9, ScanCode.F9},
                {KeyCode.F10, ScanCode.F10},
                {KeyCode.F11, ScanCode.F11},
                {KeyCode.F12, ScanCode.F12},
                {KeyCode.G, ScanCode.G},
                {KeyCode.Greater, ScanCode.Period},

                {KeyCode.H, ScanCode.H},
                //{ KeyCode.Hash, keyboardNames.EurPound },
                {KeyCode.Home, ScanCode.Home},
                {KeyCode.I, ScanCode.I},
                {KeyCode.Insert, ScanCode.Insert},
                {KeyCode.J, ScanCode.J},
                {KeyCode.K, ScanCode.K},
                {KeyCode.Keypad0, ScanCode.NumZero},
                {KeyCode.Keypad1, ScanCode.NumOne},
                {KeyCode.Keypad2, ScanCode.NumTwo},
                {KeyCode.Keypad3, ScanCode.NumThree},
                {KeyCode.Keypad4, ScanCode.NumFour},
                {KeyCode.Keypad5, ScanCode.NumFive},
                {KeyCode.Keypad6, ScanCode.NumSix},
                {KeyCode.Keypad7, ScanCode.NumSeven},
                {KeyCode.Keypad8, ScanCode.NumEight},
                {KeyCode.Keypad9, ScanCode.NumNine},
                {KeyCode.KeypadDivide, ScanCode.NumSlash},
                {KeyCode.KeypadEnter, ScanCode.NumEnter},
                {KeyCode.KeypadMinus, ScanCode.NumMinus},
                {KeyCode.KeypadMultiply, ScanCode.NumAsterisk},
                {KeyCode.KeypadPeriod, ScanCode.NumPeriod},
                {KeyCode.KeypadPlus, ScanCode.NumPlus},
                {KeyCode.L, ScanCode.L},
                {KeyCode.LeftAlt, ScanCode.LeftAlt},
                //{ KeyCode.LeftApple, keyboardNames.LeftAlt }, removed due to blinking issues
                {KeyCode.LeftArrow, ScanCode.ArrowLeft},
                {KeyCode.LeftBracket, ScanCode.OpenBracket},
                //{ KeyCode.LeftCommand, keyboardNames.LeftAlt }, !!!! Duplicate of RightApple
                {KeyCode.LeftControl, ScanCode.LeftControl},
                //{ KeyCode.LeftParen, keyboardNames.D9 },
                {KeyCode.LeftShift, ScanCode.LeftShift},
                {KeyCode.LeftWindows, ScanCode.LeftWindows},
                {KeyCode.Less, ScanCode.Comma},

                {KeyCode.M, ScanCode.M},
                {KeyCode.Menu, ScanCode.ApplicationSelect},
                {KeyCode.Minus, ScanCode.Minus},
                {KeyCode.N, ScanCode.N},
                {KeyCode.Numlock, ScanCode.NumLock},
                {KeyCode.O, ScanCode.O},
                {KeyCode.P, ScanCode.P},
                {KeyCode.PageDown, ScanCode.PageDown},
                {KeyCode.PageUp, ScanCode.PageUp},
                {KeyCode.Pause, ScanCode.PauseBreak},
                {KeyCode.Period, ScanCode.Period},
                {KeyCode.Plus, ScanCode.KeyboardEquals},
                {KeyCode.Print, ScanCode.PrintScreen},
                {KeyCode.Q, ScanCode.Q},
                {KeyCode.Question, ScanCode.ForwardSlash},
                {KeyCode.Quote, ScanCode.Apostrophe},
                {KeyCode.R, ScanCode.R},
                {KeyCode.Return, ScanCode.Enter},
                {KeyCode.RightAlt, ScanCode.RightAlt},
                //{ KeyCode.RightApple, LEDControl.Keys.RightAlt }, removed due to blinking issues
                {KeyCode.RightArrow, ScanCode.ArrowRight},
                {KeyCode.RightBracket, ScanCode.CloseBracket},
                //{ KeyCode.RightCommand, LEDControl.Keys.RightAlt }, !!!! Duplicate of RightApple
                {KeyCode.RightControl, ScanCode.RightControl},
                //{ KeyCode.RightParen, LEDControl.Keys.D0 },
                {KeyCode.RightShift, ScanCode.RightShift},
                {KeyCode.RightWindows, ScanCode.RightWindows},
                {KeyCode.S, ScanCode.S},
                {KeyCode.ScrollLock, ScanCode.ScrollLock},
                {KeyCode.Semicolon, ScanCode.Semicolon},
                {KeyCode.Slash, ScanCode.ForwardSlash},
                {KeyCode.Space, ScanCode.Space},
                {KeyCode.SysReq, ScanCode.PrintScreen},

                {KeyCode.T, ScanCode.T},
                {KeyCode.Tab, ScanCode.Tab},
                {KeyCode.U, ScanCode.U},
                {KeyCode.Underscore, ScanCode.Minus},
                {KeyCode.UpArrow, ScanCode.ArrowUp},
                {KeyCode.V, ScanCode.V},
                {KeyCode.W, ScanCode.W},
                {KeyCode.X, ScanCode.X},
                {KeyCode.Y, ScanCode.Y},
                {KeyCode.Z, ScanCode.Z}
            };

        /// <summary>
        ///     Converts a Unity <c>KeyCode</c> into a Logitech <c>ScanCode</c>.
        /// </summary>
        /// <param name="code">The Unity <c>KeyCode</c> to convert.</param>
        /// <returns>The Logitech <c>ScanCode</c>.</returns>
        public static ScanCode KeyCodeToScanCode(KeyCode code)
        {
            return KeyMapping[code];
        }

        /// <summary>
        ///     Constructor. Starts the Logitech Link.
        /// </summary>
        public LogitechLEDController()
        {
            LogiLedInit();
            LogiLedSetTargetDevice(LogiDevicetypePerkeyRGB);
        }

        /// <summary>
        ///     Clean up any resources we've been using, closes the link with Logitech.
        /// </summary>
        ~LogitechLEDController()
        {
            LogiLedShutdown();
        }

        /// <inheritdoc />
        public void Send(ColorScheme scheme)
        {
            ApplyToKeyboard(scheme);
        }

        /// <summary>
        ///     Applies the color scheme to the keyboard.
        /// </summary>
        /// <param name="colorScheme">The color scheme to apply.</param>
        private static void ApplyToKeyboard(ColorScheme colorScheme)
        {
            var colorMap = new Dictionary<ScanCode, Color>();

            foreach (ScanCode key in Enum.GetValues(typeof(ScanCode)))
            {
                colorMap.Add(key, colorScheme.BaseColor);
            }

            // Absolute keys do not need to be converted to QWERTY, they are already specified in QWERTY.
            foreach (var entry in colorScheme.AbsoluteKeys)
            {
                if (KeyMapping.ContainsKey(entry.Key))
                {
                    colorMap[KeyCodeToScanCode(entry.Key)] = entry.Value;
                }
            }

            // Mapped keys DO need to be converted to QWERTY before use.
            foreach (var entry in colorScheme.MappedKeys)
            {
                var qwertyKey = KSPLogitechRGBPlugin.Instance.LayoutProvider.ConvertToQwertyCode(entry.Key);

                if (KeyMapping.ContainsKey(qwertyKey))
                {
                    colorMap[KeyCodeToScanCode(qwertyKey)] = entry.Value;
                }
            }

            foreach (var entry in colorMap)
            {
                LogiLedSetLightingForKeyWithScanCode(
                    Convert.ToInt32(entry.Key),
                    Convert.ToInt32(entry.Value.r * 100),
                    Convert.ToInt32(entry.Value.g * 100),
                    Convert.ToInt32(entry.Value.b * 100)
                );
            }
        }
    }
}