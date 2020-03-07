using KeyShine.ColorSchemes;
using System;
using System.Collections.Generic;
using UnityEngine;
using static KeyShine.LEDControllers.Corsair.CorsairSDK;

namespace KeyShine.LEDControllers.Corsair
{
    class CorsairLEDController : ILEDController
    {


        //CorsairLedColor led = new CorsairLedColor();

        private static readonly Dictionary<KeyCode, CorsairLedId> KeyMapping =
        new Dictionary<KeyCode, CorsairLedId>
        {
                {KeyCode.A, CorsairLedId.A},
                {KeyCode.Alpha0, CorsairLedId.Zero},
                {KeyCode.Alpha1, CorsairLedId.One},
                {KeyCode.Alpha2, CorsairLedId.Two},
                {KeyCode.Alpha3, CorsairLedId.Three},
                {KeyCode.Alpha4, CorsairLedId.Four},
                {KeyCode.Alpha5, CorsairLedId.Five},
                {KeyCode.Alpha6, CorsairLedId.Six},
                {KeyCode.Alpha7, CorsairLedId.Seven},
                {KeyCode.Alpha8, CorsairLedId.Eight},
                {KeyCode.Alpha9, CorsairLedId.Nine},
                // Not sure if this will actually work on a European keyboard.
                {KeyCode.AltGr, CorsairLedId.RightAlt},
                //{ KeyCode.Ampersand, keyboardNames.D7 },
                //{ KeyCode.Asterisk, keyboardNames.D8 },
                // { KeyCode.At, keyboardNames.OemApostrophe }, blinking

                {KeyCode.B, CorsairLedId.B},
                {KeyCode.BackQuote, CorsairLedId.Tilde},
                {KeyCode.Backslash, CorsairLedId.Backslash},
                {KeyCode.Backspace, CorsairLedId.Backspace},
                {KeyCode.Break, CorsairLedId.PauseBreak},

                {KeyCode.C, CorsairLedId.C},
                {KeyCode.CapsLock, CorsairLedId.CapsLock},
                //{ KeyCode.Caret, keyboardNames.D6 },
                //{ KeyCode.Colon, keyboardNames.OemPeriod },
                {KeyCode.Comma, CorsairLedId.Comma},
                {KeyCode.D, CorsairLedId.D},
                {KeyCode.Delete, CorsairLedId.KeyboardDelete},
                //{ KeyCode.Dollar, keyboardNames.D4 },
                //{ KeyCode.DoubleQuote, keyboardNames.D2 },
                {KeyCode.DownArrow, CorsairLedId.ArrowDown},
                {KeyCode.E, CorsairLedId.E},
                {KeyCode.End, CorsairLedId.End},
                {KeyCode.Equals, CorsairLedId.KeyboardEquals},
                {KeyCode.Escape, CorsairLedId.Esc},
                //{ KeyCode.Exclaim, keyboardNames.D1 },

                {KeyCode.F, CorsairLedId.F},
                {KeyCode.F1, CorsairLedId.F1},
                {KeyCode.F2, CorsairLedId.F2},
                {KeyCode.F3, CorsairLedId.F3},
                {KeyCode.F4, CorsairLedId.F4},
                {KeyCode.F5, CorsairLedId.F5},
                {KeyCode.F6, CorsairLedId.F6},
                {KeyCode.F7, CorsairLedId.F7},
                {KeyCode.F8, CorsairLedId.F8},
                {KeyCode.F9, CorsairLedId.F9},
                {KeyCode.F10, CorsairLedId.F10},
                {KeyCode.F11, CorsairLedId.F11},
                {KeyCode.F12, CorsairLedId.F12},
                {KeyCode.G, CorsairLedId.G},

                {KeyCode.H, CorsairLedId.H},
                //{ KeyCode.Hash, keyboardNames.EurPound },
                {KeyCode.Home, CorsairLedId.Home},
                {KeyCode.I, CorsairLedId.I},
                {KeyCode.Insert, CorsairLedId.Insert},
                {KeyCode.J, CorsairLedId.J},
                {KeyCode.K, CorsairLedId.K},
                {KeyCode.Keypad0, CorsairLedId.NumZero},
                {KeyCode.Keypad1, CorsairLedId.NumOne},
                {KeyCode.Keypad2, CorsairLedId.NumTwo},
                {KeyCode.Keypad3, CorsairLedId.NumThree},
                {KeyCode.Keypad4, CorsairLedId.NumFour},
                {KeyCode.Keypad5, CorsairLedId.NumFive},
                {KeyCode.Keypad6, CorsairLedId.NumSix},
                {KeyCode.Keypad7, CorsairLedId.NumSeven},
                {KeyCode.Keypad8, CorsairLedId.NumEight},
                {KeyCode.Keypad9, CorsairLedId.NumNine},
                {KeyCode.KeypadDivide, CorsairLedId.NumSlash},
                {KeyCode.KeypadEnter, CorsairLedId.NumEnter},
                {KeyCode.KeypadMinus, CorsairLedId.NumMinus},
                {KeyCode.KeypadMultiply, CorsairLedId.NumAsterisk},
                {KeyCode.KeypadPeriod, CorsairLedId.NumPeriod},
                {KeyCode.KeypadPlus, CorsairLedId.NumPlus},
                {KeyCode.L, CorsairLedId.L},
                {KeyCode.LeftAlt, CorsairLedId.LeftAlt},    
                //{KeyCode.LeftApple, keyboardNames.LeftAlt }, removed due to blinking issues
                {KeyCode.LeftArrow, CorsairLedId.ArrowLeft},
                {KeyCode.LeftBracket, CorsairLedId.OpenBracket},
                {KeyCode.LeftCommand, CorsairLedId.LeftGui}, // I don't know why it does this.
                {KeyCode.LeftControl, CorsairLedId.LeftControl},
                //{ KeyCode.LeftParen, keyboardNames.D9 },
                {KeyCode.LeftShift, CorsairLedId.LeftShift},
                {KeyCode.LeftWindows, CorsairLedId.LeftGui},
//                {KeyCode.Less, CorsairLedId.Comma},

                {KeyCode.M, CorsairLedId.M},
                {KeyCode.Menu, CorsairLedId.ApplicationSelect},
                {KeyCode.Minus, CorsairLedId.Minus},
                {KeyCode.N, CorsairLedId.N},
                {KeyCode.Numlock, CorsairLedId.NumLock},
                {KeyCode.O, CorsairLedId.O},
                {KeyCode.P, CorsairLedId.P},
                {KeyCode.PageDown, CorsairLedId.PageDown},
                {KeyCode.PageUp, CorsairLedId.PageUp},
                {KeyCode.Pause, CorsairLedId.PauseBreak},
                {KeyCode.Period, CorsairLedId.Period},
                {KeyCode.Plus, CorsairLedId.KeyboardEquals},
                {KeyCode.Print, CorsairLedId.PrintScreen},
                {KeyCode.Q, CorsairLedId.Q},
                {KeyCode.Question, CorsairLedId.ForwardSlash},
                {KeyCode.Quote, CorsairLedId.Apostrophe},
                {KeyCode.R, CorsairLedId.R},
                {KeyCode.Return, CorsairLedId.Enter},
                {KeyCode.RightAlt, CorsairLedId.RightAlt},
                {KeyCode.RightApple, CorsairLedId.RightGui}, // Same deal as LeftCommand. So weird. Blame KSP.
                {KeyCode.RightArrow, CorsairLedId.ArrowRight},
                {KeyCode.RightBracket, CorsairLedId.CloseBracket},
                //{ KeyCode.RightCommand, LEDControl.Keys.RightAlt }, !!!! Duplicate of RightApple
                {KeyCode.RightControl, CorsairLedId.RightControl},
                //{ KeyCode.RightParen, LEDControl.Keys.D0 },
                {KeyCode.RightShift, CorsairLedId.RightShift},
                {KeyCode.RightWindows, CorsairLedId.RightGui},
                {KeyCode.S, CorsairLedId.S},
                {KeyCode.ScrollLock, CorsairLedId.ScrollLock},
                {KeyCode.Semicolon, CorsairLedId.Semicolon},
                {KeyCode.Slash, CorsairLedId.ForwardSlash},
                {KeyCode.Space, CorsairLedId.Space},
                {KeyCode.SysReq, CorsairLedId.PrintScreen},

                {KeyCode.T, CorsairLedId.T},
                {KeyCode.Tab, CorsairLedId.Tab},
                {KeyCode.U, CorsairLedId.U},
                {KeyCode.Underscore, CorsairLedId.Minus},
                {KeyCode.UpArrow, CorsairLedId.ArrowUp},
                {KeyCode.V, CorsairLedId.V},
                {KeyCode.W, CorsairLedId.W},
                {KeyCode.X, CorsairLedId.X},
                {KeyCode.Y, CorsairLedId.Y},
                {KeyCode.Z, CorsairLedId.Z},
                
                // Abuse keycodes to enable the logo to work...
                {KeyCode.Less, CorsairLedId.NonUSBackslash},
                {KeyCode.Hash, CorsairLedId.NonUSHash}
        };

        public void Send(ColorScheme colorScheme)
        {
            ApplyToKeyboard(colorScheme);
        }

        public CorsairLEDController()
        {
            Init();
        }

        public static CorsairLedId KeyCodeToScanCode(KeyCode code)
        {
            return KeyMapping[code];
        }

        public void ApplyToKeyboard(ColorScheme colorScheme)
        {


            /*
            //foreach (CorsairLedId key in Enum.GetValues(typeof(CorsairLedId)))
            //{
                
            //}

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

            ColorList.Add(key);
            */


            //List<CorsairLedColor> ColorList = new List<CorsairLedColor>();
            var colorMap = new Dictionary<CorsairLedId, Color32>();

            
            try
            {
                foreach (CorsairLedId key in Enum.GetValues(typeof(CorsairLedId)))
                {
                    //ColorList.Add(new CorsairLedColor(key, colorScheme.BaseColor));
                    colorMap.Add(key, colorScheme.BaseColor);
                }
                foreach (var key in colorScheme.AbsoluteKeys)
                {
                    //ColorList.Add(new CorsairLedColor(KeyCodeToScanCode(key.Key), key.Value));
                    colorMap[KeyCodeToScanCode(key.Key)] = key.Value;
                }
                foreach (var key in colorScheme.MappedKeys)
                {
                    var qwertyKey = KeyShine.Instance.LayoutProvider.ConvertToQwertyCode(key.Key);

                    if (KeyMapping.ContainsKey(qwertyKey))
                    {
                        //ColorList.Add(new CorsairLedColor(KeyCodeToScanCode(key.Key), key.Value));
                        colorMap[KeyCodeToScanCode(key.Key)] = key.Value;
                    }
                }


            }
            catch (Exception e)
            {
                KeyShine.extPrint(e.Message);
            }


            CorsairLedColor[] ColorMapOut = new CorsairLedColor[157];
            var ii = 0;
            foreach (CorsairLedId led in Enum.GetValues(typeof(CorsairLedId)))
            {
                ColorMapOut[ii] = new CorsairLedColor(led,new Color32(255,0,0,0));
            }



            CorsairSetLedsColors(ColorMapOut.Length,ColorMapOut);

            //CorsairSDK.CorsairSetLedsColors();


        }
    }


}

