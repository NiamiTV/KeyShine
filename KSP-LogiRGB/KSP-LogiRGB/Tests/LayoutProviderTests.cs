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

using KSP_LogiRGB.Layout;
using KSP_LogiRGB.Layout.LayoutProviders.Windows;
using KSP_LogiRGB.LEDControllers.Logitech;
using NUnit.Framework;
using UnityEngine;
using static KSP_LogiRGB.LEDControllers.Logitech.LogitechSDK;

namespace KSP_LogiRGB.Tests
{
    [TestFixture]
    public class LayoutProviderTests
    {
        private static ILayoutProvider _layoutProvider = new WindowsLayoutProvider();
        
        private static void AssertMapped(KeyCode from, KeyCode to)
        {
            Assert.AreEqual(to, _layoutProvider.ConvertToQwertyCode(from));
        }
        
        private static void AssertSame(KeyCode code)
        {
            AssertMapped(code, code);
        }
        
        [Test]
        public void TestKeypad()
        {
            AssertSame(KeyCode.Numlock);
            AssertSame(KeyCode.KeypadDivide);
            AssertSame(KeyCode.KeypadMultiply);
            AssertSame(KeyCode.KeypadMinus);
            AssertSame(KeyCode.KeypadPlus);
            AssertSame(KeyCode.KeypadEnter);
            
            AssertSame(KeyCode.Keypad0);
            AssertSame(KeyCode.Keypad1);
            AssertSame(KeyCode.Keypad2);
            AssertSame(KeyCode.Keypad3);
            AssertSame(KeyCode.Keypad4);
            AssertSame(KeyCode.Keypad5);
            AssertSame(KeyCode.Keypad6);
            AssertSame(KeyCode.Keypad7);
            AssertSame(KeyCode.Keypad8);
            AssertSame(KeyCode.Keypad9);
        }

        [Test]
        public void TestControlKeys()
        {
            AssertSame(KeyCode.Escape);
            AssertSame(KeyCode.LeftControl);
            AssertSame(KeyCode.LeftAlt);
            AssertSame(KeyCode.RightAlt);
            AssertSame(KeyCode.Menu);
            AssertSame(KeyCode.RightControl);
            AssertSame(KeyCode.SysReq);
            AssertSame(KeyCode.ScrollLock);
            AssertSame(KeyCode.Pause);
            
            // Unity actually maps these to LeftCommand and RightApple, but fixing it is a low priority
            // because only a madman would map their Windows Key to something else.
            AssertSame(KeyCode.LeftWindows);
            AssertSame(KeyCode.RightWindows);
        }

        [Test]
        public void TestNavigationKeys()
        {
            AssertSame(KeyCode.Insert);
            AssertSame(KeyCode.Home);
            AssertSame(KeyCode.PageUp);
            AssertSame(KeyCode.Delete);
            AssertSame(KeyCode.End);
            AssertSame(KeyCode.PageDown);
            
            
            AssertSame(KeyCode.UpArrow);
            AssertSame(KeyCode.DownArrow);
            AssertSame(KeyCode.LeftArrow);
            AssertSame(KeyCode.RightArrow);
        }

        [Test]
        public void TestRemappingHacks()
        {
            AssertMapped(KeyCode.Hash, KeyCode.Hash);
            AssertMapped(KeyCode.Less, KeyCode.Less);
            Assert.AreEqual(KeyName.NonUSHash, LogitechLEDController.KeyCodeToScanCode(KeyCode.Hash));
            Assert.AreEqual(KeyName.NonUSBackslash, LogitechLEDController.KeyCodeToScanCode(KeyCode.Less));
        }

        [Test]
        public void TestQwerty()
        {
            var qwerty = _layoutProvider.LoadQwertyLayout();
            try
            {
                AssertSame(KeyCode.BackQuote);
                AssertSame(KeyCode.Alpha1);
                AssertSame(KeyCode.Alpha2);
                AssertSame(KeyCode.Alpha3);
                AssertSame(KeyCode.Alpha4);
                AssertSame(KeyCode.Alpha5);
                AssertSame(KeyCode.Alpha6);
                AssertSame(KeyCode.Alpha7);
                AssertSame(KeyCode.Alpha8);
                AssertSame(KeyCode.Alpha9);
                AssertSame(KeyCode.Alpha0);
                AssertSame(KeyCode.Minus);
                AssertSame(KeyCode.Equals);
                AssertSame(KeyCode.Backspace);

                AssertSame(KeyCode.Tab);
                AssertSame(KeyCode.Q);
                AssertSame(KeyCode.W);
                AssertSame(KeyCode.E);
                AssertSame(KeyCode.R);
                AssertSame(KeyCode.T);
                AssertSame(KeyCode.Y);
                AssertSame(KeyCode.U);
                AssertSame(KeyCode.I);
                AssertSame(KeyCode.O);
                AssertSame(KeyCode.P);
                AssertSame(KeyCode.LeftBracket);
                AssertSame(KeyCode.RightBracket);
                AssertSame(KeyCode.Backslash);

                AssertSame(KeyCode.CapsLock);
                AssertSame(KeyCode.A);
                AssertSame(KeyCode.S);
                AssertSame(KeyCode.D);
                AssertSame(KeyCode.F);
                AssertSame(KeyCode.G);
                AssertSame(KeyCode.H);
                AssertSame(KeyCode.J);
                AssertSame(KeyCode.K);
                AssertSame(KeyCode.L);
                AssertSame(KeyCode.Semicolon);
                AssertSame(KeyCode.Quote);
                AssertSame(KeyCode.Return);

                AssertSame(KeyCode.LeftShift);
                AssertSame(KeyCode.Z);
                AssertSame(KeyCode.X);
                AssertSame(KeyCode.C);
                AssertSame(KeyCode.V);
                AssertSame(KeyCode.B);
                AssertSame(KeyCode.N);
                AssertSame(KeyCode.M);
                AssertSame(KeyCode.Comma);
                AssertSame(KeyCode.Period);
                AssertSame(KeyCode.Slash);
                AssertSame(KeyCode.RightShift);

                AssertSame(KeyCode.Space);
            }
            finally
            {
                qwerty.Dispose();
            }
        }

        [Test]
        public void TestDvorak()
        {
            var dvorak = _layoutProvider.LoadDvorakLayout();
            try
            {
                AssertSame(KeyCode.BackQuote);
                AssertSame(KeyCode.Alpha1);
                AssertSame(KeyCode.Alpha2);
                AssertSame(KeyCode.Alpha3);
                AssertSame(KeyCode.Alpha4);
                AssertSame(KeyCode.Alpha5);
                AssertSame(KeyCode.Alpha6);
                AssertSame(KeyCode.Alpha7);
                AssertSame(KeyCode.Alpha8);
                AssertSame(KeyCode.Alpha9);
                AssertSame(KeyCode.Alpha0);
                AssertMapped(KeyCode.LeftBracket, KeyCode.Minus);
                AssertMapped(KeyCode.RightBracket, KeyCode.Equals);
                AssertSame(KeyCode.Backspace);
                
                AssertSame(KeyCode.Tab);
                AssertMapped(KeyCode.Quote, KeyCode.Q);
                AssertMapped(KeyCode.Comma, KeyCode.W);
                AssertMapped(KeyCode.Period, KeyCode.E);
                AssertMapped(KeyCode.P, KeyCode.R);
                AssertMapped(KeyCode.Y, KeyCode.T);
                AssertMapped(KeyCode.F, KeyCode.Y);
                AssertMapped(KeyCode.G, KeyCode.U);
                AssertMapped(KeyCode.C, KeyCode.I);
                AssertMapped(KeyCode.R, KeyCode.O);
                AssertMapped(KeyCode.L, KeyCode.P);
                AssertMapped(KeyCode.Slash, KeyCode.LeftBracket);
                AssertMapped(KeyCode.Equals, KeyCode.RightBracket);
                AssertSame(KeyCode.Backslash);
                
                AssertSame(KeyCode.CapsLock);
                AssertMapped(KeyCode.A, KeyCode.A);
                AssertMapped(KeyCode.O, KeyCode.S);
                AssertMapped(KeyCode.E, KeyCode.D);
                AssertMapped(KeyCode.U, KeyCode.F);
                AssertMapped(KeyCode.I, KeyCode.G);
                AssertMapped(KeyCode.D, KeyCode.H);
                AssertMapped(KeyCode.H, KeyCode.J);
                AssertMapped(KeyCode.T, KeyCode.K);
                AssertMapped(KeyCode.N, KeyCode.L);
                AssertMapped(KeyCode.S, KeyCode.Semicolon);
                AssertMapped(KeyCode.Minus, KeyCode.Quote);
                AssertSame(KeyCode.Return);
                
                AssertSame(KeyCode.LeftShift);
                AssertMapped(KeyCode.Semicolon, KeyCode.Z);
                AssertMapped(KeyCode.Q, KeyCode.X);
                AssertMapped(KeyCode.J, KeyCode.C);
                AssertMapped(KeyCode.K, KeyCode.V);
                AssertMapped(KeyCode.X, KeyCode.B);
                AssertMapped(KeyCode.B, KeyCode.N);
                AssertMapped(KeyCode.M, KeyCode.M);
                AssertMapped(KeyCode.W, KeyCode.Comma);
                AssertMapped(KeyCode.V, KeyCode.Period);
                AssertMapped(KeyCode.Z, KeyCode.Slash);
                AssertSame(KeyCode.RightShift);
                
                AssertSame(KeyCode.Space);
            }
            finally
            {
                dvorak.Dispose();
            }
        }
        
        [Test]
        public void TestAzerty()
        {
            var azerty = _layoutProvider.LoadAzertyLayout();
            try
            {
                AssertMapped(KeyCode.Quote, KeyCode.BackQuote);
                AssertSame(KeyCode.Alpha1);
                AssertSame(KeyCode.Alpha2);
                AssertSame(KeyCode.Alpha3);
                AssertSame(KeyCode.Alpha4);
                AssertSame(KeyCode.Alpha5);
                AssertSame(KeyCode.Alpha6);
                AssertSame(KeyCode.Alpha7);
                AssertSame(KeyCode.Alpha8);
                AssertSame(KeyCode.Alpha9);
                AssertSame(KeyCode.Alpha0);
                AssertMapped(KeyCode.LeftBracket, KeyCode.Minus);
                AssertSame(KeyCode.Equals);
                AssertSame(KeyCode.Backspace);
                
                AssertSame(KeyCode.Tab);
                AssertMapped(KeyCode.A, KeyCode.Q);
                AssertMapped(KeyCode.Z, KeyCode.W);
                AssertMapped(KeyCode.E, KeyCode.E);
                AssertMapped(KeyCode.R, KeyCode.R);
                AssertMapped(KeyCode.T, KeyCode.T);
                AssertMapped(KeyCode.Y, KeyCode.Y);
                AssertMapped(KeyCode.U, KeyCode.U);
                AssertMapped(KeyCode.I, KeyCode.I);
                AssertMapped(KeyCode.O, KeyCode.O);
                AssertMapped(KeyCode.P, KeyCode.P);
                AssertMapped(KeyCode.RightBracket, KeyCode.LeftBracket);
                AssertMapped(KeyCode.Semicolon, KeyCode.RightBracket);
                AssertSame(KeyCode.Backslash);
                
                AssertSame(KeyCode.CapsLock);
                AssertMapped(KeyCode.Q, KeyCode.A);
                AssertMapped(KeyCode.S, KeyCode.S);
                AssertMapped(KeyCode.D, KeyCode.D);
                AssertMapped(KeyCode.F, KeyCode.F);
                AssertMapped(KeyCode.G, KeyCode.G);
                AssertMapped(KeyCode.H, KeyCode.H);
                AssertMapped(KeyCode.J, KeyCode.J);
                AssertMapped(KeyCode.K, KeyCode.K);
                AssertMapped(KeyCode.L, KeyCode.L);
                AssertMapped(KeyCode.M, KeyCode.Semicolon);
                AssertMapped(KeyCode.BackQuote, KeyCode.Quote);
                AssertSame(KeyCode.Return);
                
                AssertSame(KeyCode.LeftShift);
                AssertMapped(KeyCode.W, KeyCode.Z);
                AssertMapped(KeyCode.X, KeyCode.X);
                AssertMapped(KeyCode.C, KeyCode.C);
                AssertMapped(KeyCode.V, KeyCode.V);
                AssertMapped(KeyCode.B, KeyCode.B);
                AssertMapped(KeyCode.N, KeyCode.N);
                AssertMapped(KeyCode.Comma, KeyCode.M);
                AssertMapped(KeyCode.Period, KeyCode.Comma);
                AssertMapped(KeyCode.Slash, KeyCode.Period);
                // OEM_8 does not appear to be addressable in KSP...
                // AssertMapped(KeyCode.Slash, KeyCode.Slash); 
                AssertSame(KeyCode.RightShift);
                
                AssertSame(KeyCode.Space);
            }
            finally
            {
                azerty.Dispose();
            }
        }
    }
}