using System;
using System.Runtime.InteropServices;
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
            AssertSame(KeyCode.LeftWindows);
            AssertSame(KeyCode.LeftAlt);
            AssertSame(KeyCode.RightAlt);
            AssertSame(KeyCode.RightWindows);
            AssertSame(KeyCode.Menu);
            AssertSame(KeyCode.RightControl);
            AssertSame(KeyCode.SysReq);
            AssertSame(KeyCode.ScrollLock);
            AssertSame(KeyCode.Pause);
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
                AssertMapped(KeyCode.RightBracket, KeyCode.Plus);
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
                AssertMapped(KeyCode.Plus, KeyCode.RightBracket);
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
    }
}