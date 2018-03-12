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
    }
}