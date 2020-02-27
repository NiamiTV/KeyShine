using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using KeyShine.ColorSchemes;
using static KeyShine.LEDControllers.Corsair.CorsairSDK;
using UnityEngine;

namespace KeyShine.LEDControllers.Corsair
{
    class CorsairLEDController : ILEDController
    {
        

        //CorsairLedColor led = new CorsairLedColor();

        public void Send(ColorScheme scheme)
        {
            //led.ledId = CorsairLedId.CLDRAM_11;
        }

        //[DllImport("CUESDK.x64_2017.dll", CallingConvention = CallingConvention.Cdecl)]
        //public static extern void CorsairGetLedsColors();
    }
}
