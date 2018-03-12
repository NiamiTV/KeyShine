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
        private static readonly Dictionary<KeyCode, Keys> KeyMapping =
            new Dictionary<KeyCode, Keys>
            {
                {KeyCode.A, Keys.A},
                {KeyCode.Alpha0, Keys.Zero},
                {KeyCode.Alpha1, Keys.One},
                {KeyCode.Alpha2, Keys.Two},
                {KeyCode.Alpha3, Keys.Three},
                {KeyCode.Alpha4, Keys.Four},
                {KeyCode.Alpha5, Keys.Five},
                {KeyCode.Alpha6, Keys.Six},
                {KeyCode.Alpha7, Keys.Seven},
                {KeyCode.Alpha8, Keys.Eight},
                {KeyCode.Alpha9, Keys.Nine},
                // Not sure if this will actually work on a European keyboard.
                {KeyCode.AltGr, Keys.RightAlt},
                //{ KeyCode.Ampersand, keyboardNames.D7 },
                //{ KeyCode.Asterisk, keyboardNames.D8 },
                // { KeyCode.At, keyboardNames.OemApostrophe }, blinking

                {KeyCode.B, Keys.B},
                {KeyCode.BackQuote, Keys.Tilde},
                {KeyCode.Backslash, Keys.Backslash},
                {KeyCode.Backspace, Keys.Backspace},
                //{ KeyCode.Break, LEDControl.Keys.PauseBreak },

                {KeyCode.C, Keys.C},
                {KeyCode.CapsLock, Keys.CapsLock},
                //{ KeyCode.Caret, keyboardNames.D6 },
                //{ KeyCode.Colon, keyboardNames.OemPeriod },
                {KeyCode.Comma, Keys.Comma},
                {KeyCode.D, Keys.D},
                {KeyCode.Delete, Keys.KeyboardDelete},
                //{ KeyCode.Dollar, keyboardNames.D4 },
                //{ KeyCode.DoubleQuote, keyboardNames.D2 },
                {KeyCode.DownArrow, Keys.ArrowDown},
                {KeyCode.E, Keys.E},
                {KeyCode.End, Keys.End},
                {KeyCode.Equals, Keys.KeyboardEquals},
                {KeyCode.Escape, Keys.Esc},
                //{ KeyCode.Exclaim, keyboardNames.D1 },

                {KeyCode.F, Keys.F},
                {KeyCode.F1, Keys.F1},
                {KeyCode.F2, Keys.F2},
                {KeyCode.F3, Keys.F3},
                {KeyCode.F4, Keys.F4},
                {KeyCode.F5, Keys.F5},
                {KeyCode.F6, Keys.F6},
                {KeyCode.F7, Keys.F7},
                {KeyCode.F8, Keys.F8},
                {KeyCode.F9, Keys.F9},
                {KeyCode.F10, Keys.F10},
                {KeyCode.F11, Keys.F11},
                {KeyCode.F12, Keys.F12},
                {KeyCode.G, Keys.G},
                {KeyCode.Greater, Keys.Period},

                {KeyCode.H, Keys.H},
                //{ KeyCode.Hash, keyboardNames.EurPound },
                {KeyCode.Home, Keys.Home},
                {KeyCode.I, Keys.I},
                {KeyCode.Insert, Keys.Insert},
                {KeyCode.J, Keys.J},
                {KeyCode.K, Keys.K},
                {KeyCode.Keypad0, Keys.NumZero},
                {KeyCode.Keypad1, Keys.NumOne},
                {KeyCode.Keypad2, Keys.NumTwo},
                {KeyCode.Keypad3, Keys.NumThree},
                {KeyCode.Keypad4, Keys.NumFour},
                {KeyCode.Keypad5, Keys.NumFive},
                {KeyCode.Keypad6, Keys.NumSix},
                {KeyCode.Keypad7, Keys.NumSeven},
                {KeyCode.Keypad8, Keys.NumEight},
                {KeyCode.Keypad9, Keys.NumNine},
                {KeyCode.KeypadDivide, Keys.NumSlash},
                {KeyCode.KeypadEnter, Keys.NumEnter},
                {KeyCode.KeypadMinus, Keys.NumMinus},
                {KeyCode.KeypadMultiply, Keys.NumAsterisk},
                {KeyCode.KeypadPeriod, Keys.NumPeriod},
                {KeyCode.KeypadPlus, Keys.NumPlus},
                {KeyCode.L, Keys.L},
                {KeyCode.LeftAlt, Keys.LeftAlt},
                //{ KeyCode.LeftApple, keyboardNames.LeftAlt }, removed due to blinking issues
                {KeyCode.LeftArrow, Keys.ArrowLeft},
                {KeyCode.LeftBracket, Keys.OpenBracket},
                //{ KeyCode.LeftCommand, keyboardNames.LeftAlt }, !!!! Duplicate of RightApple
                {KeyCode.LeftControl, Keys.LeftControl},
                //{ KeyCode.LeftParen, keyboardNames.D9 },
                {KeyCode.LeftShift, Keys.LeftShift},
                {KeyCode.LeftWindows, Keys.LeftWindows},
                {KeyCode.Less, Keys.Comma},

                {KeyCode.M, Keys.M},
                {KeyCode.Menu, Keys.ApplicationSelect},
                {KeyCode.Minus, Keys.Minus},
                {KeyCode.N, Keys.N},
                {KeyCode.Numlock, Keys.NumLock},
                {KeyCode.O, Keys.O},
                {KeyCode.P, Keys.P},
                {KeyCode.PageDown, Keys.PageDown},
                {KeyCode.PageUp, Keys.PageUp},
                {KeyCode.Pause, Keys.PauseBreak},
                {KeyCode.Period, Keys.Period},
                {KeyCode.Plus, Keys.KeyboardEquals},
                {KeyCode.Print, Keys.PrintScreen},
                {KeyCode.Q, Keys.Q},
                {KeyCode.Question, Keys.ForwardSlash},
                {KeyCode.Quote, Keys.Apostrophe},
                {KeyCode.R, Keys.R},
                {KeyCode.Return, Keys.Enter},
                {KeyCode.RightAlt, Keys.RightAlt},
                //{ KeyCode.RightApple, LEDControl.Keys.RightAlt }, removed due to blinking issues
                {KeyCode.RightArrow, Keys.ArrowRight},
                {KeyCode.RightBracket, Keys.CloseBracket},
                //{ KeyCode.RightCommand, LEDControl.Keys.RightAlt }, !!!! Duplicate of RightApple
                {KeyCode.RightControl, Keys.RightControl},
                //{ KeyCode.RightParen, LEDControl.Keys.D0 },
                {KeyCode.RightShift, Keys.RightShift},
                {KeyCode.RightWindows, Keys.RightWindows},
                {KeyCode.S, Keys.S},
                {KeyCode.ScrollLock, Keys.ScrollLock},
                {KeyCode.Semicolon, Keys.Semicolon},
                {KeyCode.Slash, Keys.ForwardSlash},
                {KeyCode.Space, Keys.Space},
                {KeyCode.SysReq, Keys.PrintScreen},

                {KeyCode.T, Keys.T},
                {KeyCode.Tab, Keys.Tab},
                {KeyCode.U, Keys.U},
                {KeyCode.Underscore, Keys.Minus},
                {KeyCode.UpArrow, Keys.ArrowUp},
                {KeyCode.V, Keys.V},
                {KeyCode.W, Keys.W},
                {KeyCode.X, Keys.X},
                {KeyCode.Y, Keys.Y},
                {KeyCode.Z, Keys.Z}
            };

        public LogitechLEDController()
        {
            LogiLedInit();
            LogiLedSetTargetDevice(LogiDevicetypePerkeyRGB);
            LogiLedSetLighting(0, 0, 0);
        }

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
            var colorMap = new Dictionary<Keys, Color>();

            foreach (Keys key in Enum.GetValues(typeof(Keys)))
            {
                colorMap.Add(key, colorScheme.BaseColor);
            }

            foreach (var entry in colorScheme.AbsoluteKeys)
            {
                var qwertyKey = KSPLogitechRGBPlugin.Instance.LayoutProvider.ConvertToQwertyCode(entry.Key);

                if (KeyMapping.ContainsKey(qwertyKey))
                {
                    colorMap[KeyMapping[qwertyKey]] = entry.Value;
                }
            }

            foreach (var entry in colorScheme.MappedKeys)
            {
                if (KeyMapping.ContainsKey(entry.Key))
                {
                    colorMap[KeyMapping[entry.Key]] = entry.Value;
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