using System;
using System.Collections.Generic;
using KSP_LogiRGB.ColorSchemes;
using UnityEngine;

namespace KSP_LogiRGB.Logitech
{
    internal class LogitechDrain : IDataDrain
    {
        /// <summary>
        ///     Unity KeyCodes to Logitech layout translation dictionary
        /// </summary>
        private static readonly Dictionary<KeyCode, LEDControl.Keys> keyMapping = new Dictionary<KeyCode, LEDControl.Keys>
        {
            {KeyCode.A, LEDControl.Keys.A},
            {KeyCode.Alpha0, LEDControl.Keys.Zero},
            {KeyCode.Alpha1, LEDControl.Keys.One},
            {KeyCode.Alpha2, LEDControl.Keys.Two},
            {KeyCode.Alpha3, LEDControl.Keys.Three},
            {KeyCode.Alpha4, LEDControl.Keys.Four},
            {KeyCode.Alpha5, LEDControl.Keys.Five},
            {KeyCode.Alpha6, LEDControl.Keys.Six},
            {KeyCode.Alpha7, LEDControl.Keys.Seven},
            {KeyCode.Alpha8, LEDControl.Keys.Eight},
            {KeyCode.Alpha9, LEDControl.Keys.Nine},
            //{ KeyCode.AltGr, keyboardNames.Function }, // abused to use the fn key
            //{ KeyCode.Ampersand, keyboardNames.D7 },
            //{ KeyCode.Asterisk, keyboardNames.D8 },
            // { KeyCode.At, keyboardNames.OemApostrophe }, blinking

            {KeyCode.B, LEDControl.Keys.B},
            {KeyCode.BackQuote, LEDControl.Keys.Tilde},
            {KeyCode.Backslash, LEDControl.Keys.Backslash},
            {KeyCode.Backspace, LEDControl.Keys.Backspace},
            //{ KeyCode.Break, LEDControl.Keys.PauseBreak },

            {KeyCode.C, LEDControl.Keys.C},
            {KeyCode.CapsLock, LEDControl.Keys.CapsLock},
            //{ KeyCode.Caret, keyboardNames.D6 },
            //{ KeyCode.Colon, keyboardNames.OemPeriod },
            {KeyCode.Comma, LEDControl.Keys.Comma},
            {KeyCode.D, LEDControl.Keys.D},
            {KeyCode.Delete, LEDControl.Keys.KeyboardDelete},
            //{ KeyCode.Dollar, keyboardNames.D4 },
            //{ KeyCode.DoubleQuote, keyboardNames.D2 },
            {KeyCode.DownArrow, LEDControl.Keys.ArrowDown},
            {KeyCode.E, LEDControl.Keys.E},
            {KeyCode.End, LEDControl.Keys.End},
            {KeyCode.Equals, LEDControl.Keys.KeyboardEquals},
            {KeyCode.Escape, LEDControl.Keys.Esc},
            //{ KeyCode.Exclaim, keyboardNames.D1 },

            {KeyCode.F, LEDControl.Keys.F},
            {KeyCode.F1, LEDControl.Keys.F1},
            {KeyCode.F2, LEDControl.Keys.F2},
            {KeyCode.F3, LEDControl.Keys.F3},
            {KeyCode.F4, LEDControl.Keys.F4},
            {KeyCode.F5, LEDControl.Keys.F5},
            {KeyCode.F6, LEDControl.Keys.F6},
            {KeyCode.F7, LEDControl.Keys.F7},
            {KeyCode.F8, LEDControl.Keys.F8},
            {KeyCode.F9, LEDControl.Keys.F9},
            {KeyCode.F10, LEDControl.Keys.F10},
            {KeyCode.F11, LEDControl.Keys.F11},
            {KeyCode.F12, LEDControl.Keys.F12},
            {KeyCode.G, LEDControl.Keys.G},
            //{ KeyCode.Greater, keyboardNames.OemPeriod },

            {KeyCode.H, LEDControl.Keys.H},
            //{ KeyCode.Hash, keyboardNames.EurPound },
            {KeyCode.Home, LEDControl.Keys.Home},
            {KeyCode.I, LEDControl.Keys.I},
            {KeyCode.Insert, LEDControl.Keys.Insert},
            {KeyCode.J, LEDControl.Keys.J},
            {KeyCode.K, LEDControl.Keys.K},
            {KeyCode.Keypad0, LEDControl.Keys.NumZero},
            {KeyCode.Keypad1, LEDControl.Keys.NumOne},
            {KeyCode.Keypad2, LEDControl.Keys.NumTwo},
            {KeyCode.Keypad3, LEDControl.Keys.NumThree},
            {KeyCode.Keypad4, LEDControl.Keys.NumFour},
            {KeyCode.Keypad5, LEDControl.Keys.NumFive},
            {KeyCode.Keypad6, LEDControl.Keys.NumSix},
            {KeyCode.Keypad7, LEDControl.Keys.NumSeven},
            {KeyCode.Keypad8, LEDControl.Keys.NumEight},
            {KeyCode.Keypad9, LEDControl.Keys.NumNine},
            {KeyCode.KeypadDivide, LEDControl.Keys.NumSlash},
            {KeyCode.KeypadEnter, LEDControl.Keys.NumEnter},
            {KeyCode.KeypadMinus, LEDControl.Keys.NumMinus},
            {KeyCode.KeypadMultiply, LEDControl.Keys.NumAsterisk},
            {KeyCode.KeypadPeriod, LEDControl.Keys.NumPeriod},
            {KeyCode.KeypadPlus, LEDControl.Keys.NumPlus},
            {KeyCode.L, LEDControl.Keys.L},
            {KeyCode.LeftAlt, LEDControl.Keys.LeftAlt},
            //{ KeyCode.LeftApple, keyboardNames.LeftAlt }, removed due to blinking issues
            {KeyCode.LeftArrow, LEDControl.Keys.ArrowLeft},
            {KeyCode.LeftBracket, LEDControl.Keys.OpenBracket},
            //{ KeyCode.LeftCommand, keyboardNames.LeftAlt }, !!!! Duplicate of RightApple
            {KeyCode.LeftControl, LEDControl.Keys.LeftControl},
            //{ KeyCode.LeftParen, keyboardNames.D9 },
            {KeyCode.LeftShift, LEDControl.Keys.LeftShift},
            {KeyCode.LeftWindows, LEDControl.Keys.LeftWindows},
            //{ KeyCode.Less, keyboardNames.OemComma },

            {KeyCode.M, LEDControl.Keys.M},
            {KeyCode.Menu, LEDControl.Keys.ApplicationSelect},
            {KeyCode.Minus, LEDControl.Keys.Minus},
            {KeyCode.N, LEDControl.Keys.N},
            {KeyCode.Numlock, LEDControl.Keys.NumLock},
            {KeyCode.O, LEDControl.Keys.O},
            {KeyCode.P, LEDControl.Keys.P},
            {KeyCode.PageDown, LEDControl.Keys.PageDown},
            {KeyCode.PageUp, LEDControl.Keys.PageUp},
            {KeyCode.Pause, LEDControl.Keys.PauseBreak},
            {KeyCode.Period, LEDControl.Keys.Period},
            //{ KeyCode.Plus, LEDControl.Keys.EQUALS },
            {KeyCode.Print, LEDControl.Keys.PrintScreen},
            {KeyCode.Q, LEDControl.Keys.Q},
            //{ KeyCode.Question, LEDControl.Keys.OemSlash },
            {KeyCode.Quote, LEDControl.Keys.Apostrophe},
            {KeyCode.R, LEDControl.Keys.R},
            {KeyCode.Return, LEDControl.Keys.Enter},
            {KeyCode.RightAlt, LEDControl.Keys.RightAlt},
            //{ KeyCode.RightApple, LEDControl.Keys.RightAlt }, removed due to blinking issues
            {KeyCode.RightArrow, LEDControl.Keys.ArrowRight},
            {KeyCode.RightBracket, LEDControl.Keys.CloseBracket},
            //{ KeyCode.RightCommand, LEDControl.Keys.RightAlt }, !!!! Duplicate of RightApple
            {KeyCode.RightControl, LEDControl.Keys.RightControl},
            //{ KeyCode.RightParen, LEDControl.Keys.D0 },
            {KeyCode.RightShift, LEDControl.Keys.RightShift},
            {KeyCode.RightWindows, LEDControl.Keys.RightWindows},
            {KeyCode.S, LEDControl.Keys.S},
            {KeyCode.ScrollLock, LEDControl.Keys.ScrollLock},
            {KeyCode.Semicolon, LEDControl.Keys.Semicolon},
            {KeyCode.Slash, LEDControl.Keys.ForwardSlash},
            {KeyCode.Space, LEDControl.Keys.Space},
            //{ KeyCode.SysReq, keyboardNames.PRINT_SCREEN },

            {KeyCode.T, LEDControl.Keys.T},
            {KeyCode.Tab, LEDControl.Keys.Tab},
            {KeyCode.U, LEDControl.Keys.U},
            //{ KeyCode.Underscore, keyboardNames.OemMinus },
            {KeyCode.UpArrow, LEDControl.Keys.ArrowUp},
            {KeyCode.V, LEDControl.Keys.V},
            {KeyCode.W, LEDControl.Keys.W},
            {KeyCode.X, LEDControl.Keys.X},
            {KeyCode.Y, LEDControl.Keys.Y},
            {KeyCode.Z, LEDControl.Keys.Z}
        };

        public LogitechDrain()
        {
            LEDControl.LogiLedInit();
            LEDControl.LogiLedSetTargetDevice(LEDControl.LogiDevicetypePerkeyRGB);
            LEDControl.LogiLedSetLighting(0, 0, 0);
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
            foreach (var entry in colorScheme)
            {
                if (keyMapping.ContainsKey(entry.Key))
                {
                    LEDControl.LogiLedSetLightingForKeyWithScanCode(
                        Convert.ToInt32(keyMapping[entry.Key]),
                        Convert.ToInt32(entry.Value.r * 100),
                        Convert.ToInt32(entry.Value.g * 100),
                        Convert.ToInt32(entry.Value.b * 100)
                    );
                }
            }
        }
    }
}