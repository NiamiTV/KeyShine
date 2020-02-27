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
using KeyShine.ColorSchemes;
using UnityEngine;
using static KeyShine.LEDControllers.Logitech.LogitechSDK;

namespace KeyShine.LEDControllers.Logitech
{
    public class LogitechLEDController : ILEDController
    {
        /// <summary>
        ///     Unity <c>KeyCode</c>s to Logitech layout translation dictionary
        /// </summary>
        private static readonly Dictionary<KeyCode, KeyName> KeyMapping =
            new Dictionary<KeyCode, KeyName>
            {
                {KeyCode.A, KeyName.A},
                {KeyCode.Alpha0, KeyName.Zero},
                {KeyCode.Alpha1, KeyName.One},
                {KeyCode.Alpha2, KeyName.Two},
                {KeyCode.Alpha3, KeyName.Three},
                {KeyCode.Alpha4, KeyName.Four},
                {KeyCode.Alpha5, KeyName.Five},
                {KeyCode.Alpha6, KeyName.Six},
                {KeyCode.Alpha7, KeyName.Seven},
                {KeyCode.Alpha8, KeyName.Eight},
                {KeyCode.Alpha9, KeyName.Nine},
                // Not sure if this will actually work on a European keyboard.
                {KeyCode.AltGr, KeyName.RightAlt},
                //{ KeyCode.Ampersand, keyboardNames.D7 },
                //{ KeyCode.Asterisk, keyboardNames.D8 },
                // { KeyCode.At, keyboardNames.OemApostrophe }, blinking

                {KeyCode.B, KeyName.B},
                {KeyCode.BackQuote, KeyName.Tilde},
                {KeyCode.Backslash, KeyName.Backslash},
                {KeyCode.Backspace, KeyName.Backspace},
                {KeyCode.Break, KeyName.PauseBreak},

                {KeyCode.C, KeyName.C},
                {KeyCode.CapsLock, KeyName.CapsLock},
                //{ KeyCode.Caret, keyboardNames.D6 },
                //{ KeyCode.Colon, keyboardNames.OemPeriod },
                {KeyCode.Comma, KeyName.Comma},
                {KeyCode.D, KeyName.D},
                {KeyCode.Delete, KeyName.KeyboardDelete},
                //{ KeyCode.Dollar, keyboardNames.D4 },
                //{ KeyCode.DoubleQuote, keyboardNames.D2 },
                {KeyCode.DownArrow, KeyName.ArrowDown},
                {KeyCode.E, KeyName.E},
                {KeyCode.End, KeyName.End},
                {KeyCode.Equals, KeyName.KeyboardEquals},
                {KeyCode.Escape, KeyName.Esc},
                //{ KeyCode.Exclaim, keyboardNames.D1 },

                {KeyCode.F, KeyName.F},
                {KeyCode.F1, KeyName.F1},
                {KeyCode.F2, KeyName.F2},
                {KeyCode.F3, KeyName.F3},
                {KeyCode.F4, KeyName.F4},
                {KeyCode.F5, KeyName.F5},
                {KeyCode.F6, KeyName.F6},
                {KeyCode.F7, KeyName.F7},
                {KeyCode.F8, KeyName.F8},
                {KeyCode.F9, KeyName.F9},
                {KeyCode.F10, KeyName.F10},
                {KeyCode.F11, KeyName.F11},
                {KeyCode.F12, KeyName.F12},
                {KeyCode.G, KeyName.G},

                {KeyCode.H, KeyName.H},
                //{ KeyCode.Hash, keyboardNames.EurPound },
                {KeyCode.Home, KeyName.Home},
                {KeyCode.I, KeyName.I},
                {KeyCode.Insert, KeyName.Insert},
                {KeyCode.J, KeyName.J},
                {KeyCode.K, KeyName.K},
                {KeyCode.Keypad0, KeyName.NumZero},
                {KeyCode.Keypad1, KeyName.NumOne},
                {KeyCode.Keypad2, KeyName.NumTwo},
                {KeyCode.Keypad3, KeyName.NumThree},
                {KeyCode.Keypad4, KeyName.NumFour},
                {KeyCode.Keypad5, KeyName.NumFive},
                {KeyCode.Keypad6, KeyName.NumSix},
                {KeyCode.Keypad7, KeyName.NumSeven},
                {KeyCode.Keypad8, KeyName.NumEight},
                {KeyCode.Keypad9, KeyName.NumNine},
                {KeyCode.KeypadDivide, KeyName.NumSlash},
                {KeyCode.KeypadEnter, KeyName.NumEnter},
                {KeyCode.KeypadMinus, KeyName.NumMinus},
                {KeyCode.KeypadMultiply, KeyName.NumAsterisk},
                {KeyCode.KeypadPeriod, KeyName.NumPeriod},
                {KeyCode.KeypadPlus, KeyName.NumPlus},
                {KeyCode.L, KeyName.L},
                {KeyCode.LeftAlt, KeyName.LeftAlt},
                //{KeyCode.LeftApple, keyboardNames.LeftAlt }, removed due to blinking issues
                {KeyCode.LeftArrow, KeyName.ArrowLeft},
                {KeyCode.LeftBracket, KeyName.OpenBracket},
                {KeyCode.LeftCommand, KeyName.LeftWindows}, // I don't know why it does this.
                {KeyCode.LeftControl, KeyName.LeftControl},
                //{ KeyCode.LeftParen, keyboardNames.D9 },
                {KeyCode.LeftShift, KeyName.LeftShift},
                {KeyCode.LeftWindows, KeyName.LeftWindows},
//                {KeyCode.Less, KeyName.Comma},

                {KeyCode.M, KeyName.M},
                {KeyCode.Menu, KeyName.ApplicationSelect},
                {KeyCode.Minus, KeyName.Minus},
                {KeyCode.N, KeyName.N},
                {KeyCode.Numlock, KeyName.NumLock},
                {KeyCode.O, KeyName.O},
                {KeyCode.P, KeyName.P},
                {KeyCode.PageDown, KeyName.PageDown},
                {KeyCode.PageUp, KeyName.PageUp},
                {KeyCode.Pause, KeyName.PauseBreak},
                {KeyCode.Period, KeyName.Period},
                {KeyCode.Plus, KeyName.KeyboardEquals},
                {KeyCode.Print, KeyName.PrintScreen},
                {KeyCode.Q, KeyName.Q},
                {KeyCode.Question, KeyName.ForwardSlash},
                {KeyCode.Quote, KeyName.Apostrophe},
                {KeyCode.R, KeyName.R},
                {KeyCode.Return, KeyName.Enter},
                {KeyCode.RightAlt, KeyName.RightAlt},
                {KeyCode.RightApple, KeyName.RightWindows}, // Same deal as LeftCommand. So weird. Blame KSP.
                {KeyCode.RightArrow, KeyName.ArrowRight},
                {KeyCode.RightBracket, KeyName.CloseBracket},
                //{ KeyCode.RightCommand, LEDControl.Keys.RightAlt }, !!!! Duplicate of RightApple
                {KeyCode.RightControl, KeyName.RightControl},
                //{ KeyCode.RightParen, LEDControl.Keys.D0 },
                {KeyCode.RightShift, KeyName.RightShift},
                {KeyCode.RightWindows, KeyName.RightWindows},
                {KeyCode.S, KeyName.S},
                {KeyCode.ScrollLock, KeyName.ScrollLock},
                {KeyCode.Semicolon, KeyName.Semicolon},
                {KeyCode.Slash, KeyName.ForwardSlash},
                {KeyCode.Space, KeyName.Space},
                {KeyCode.SysReq, KeyName.PrintScreen},

                {KeyCode.T, KeyName.T},
                {KeyCode.Tab, KeyName.Tab},
                {KeyCode.U, KeyName.U},
                {KeyCode.Underscore, KeyName.Minus},
                {KeyCode.UpArrow, KeyName.ArrowUp},
                {KeyCode.V, KeyName.V},
                {KeyCode.W, KeyName.W},
                {KeyCode.X, KeyName.X},
                {KeyCode.Y, KeyName.Y},
                {KeyCode.Z, KeyName.Z},
                
                // Abuse keycodes to enable the logo to work...
                {KeyCode.Less, KeyName.NonUSBackslash},
                {KeyCode.Hash, KeyName.NonUSHash}
            };

        /// <summary>
        ///     Converts a Unity <c>KeyCode</c> into a Logitech <c>ScanCode</c>.
        /// </summary>
        /// <param name="code">The Unity <c>KeyCode</c> to convert.</param>
        /// <returns>The Logitech <c>ScanCode</c>.</returns>
        public static KeyName KeyCodeToScanCode(KeyCode code)
        {
            return KeyMapping[code];
        }

        /// <summary>
        ///     Constructor. Starts the Logitech Link.
        /// </summary>
        public LogitechLEDController()
        {
            LogitechSDK.Init();
            LogitechSDK.SetTargetDevice(LogiDevicetypePerkeyRGB);
        }

        /// <summary>
        ///     Clean up any resources we've been using, closes the link with Logitech.
        /// </summary>
        ~LogitechLEDController()
        {
            LogitechSDK.Shutdown();
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
            var colorMap = new Dictionary<KeyName, Color>();

            foreach (KeyName key in Enum.GetValues(typeof(KeyName)))
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
                
                var qwertyKey = KeyShine.Instance.LayoutProvider.ConvertToQwertyCode(entry.Key);

                if (KeyMapping.ContainsKey(qwertyKey))
                {
                    colorMap[KeyCodeToScanCode(qwertyKey)] = entry.Value;
                }
            }

            foreach (var entry in colorMap)
            {
                LogitechSDK.SetKeyLighting(
                    entry.Key,
                    Convert.ToInt32(entry.Value.r * 100),
                    Convert.ToInt32(entry.Value.g * 100),
                    Convert.ToInt32(entry.Value.b * 100)
                );
            }
        }
    }
}