using UnityEngine;

namespace KeyShine.ColorSchemes
{
    /// <summary>
    ///     Displays an image on the keyboard vaguely similar to the logo minus the
    ///     KSP text.
    /// </summary>
    internal class LogoScheme : ColorScheme
    {
        /// <summary>
        ///     Overlays the defined keys on top of a blue base color scheme.
        /// </summary>
        public LogoScheme() : base(Color.blue)
        {
            KeyCode[] redkeys =
            {
                ///Rocket
                KeyCode.LeftWindows, KeyCode.Z, KeyCode.S, KeyCode.X, KeyCode.C, KeyCode.F4,
                KeyCode.D, KeyCode.E, KeyCode.Alpha4, KeyCode.A,

                ///Stripes
                KeyCode.LeftShift, KeyCode.V, KeyCode.B, KeyCode.N, KeyCode.M, KeyCode.Comma, KeyCode.Period,
                KeyCode.Slash, KeyCode.RightShift, KeyCode.Less
            };
            SetAbsoluteKeysToColor(redkeys, Color.red);

            KeyCode[] whitekeys =
            {
                KeyCode.LeftControl, KeyCode.LeftAlt, KeyCode.Space, KeyCode.AltGr, KeyCode.RightAlt,
                KeyCode.RightWindows,
                KeyCode.Menu, KeyCode.RightControl, KeyCode.Q, KeyCode.W, KeyCode.Alpha3, KeyCode.F3,
                KeyCode.Alpha5, KeyCode.R, KeyCode.F, KeyCode.CapsLock, KeyCode.G, KeyCode.H,
                KeyCode.J, KeyCode.K, KeyCode.L, KeyCode.Semicolon, KeyCode.Quote, KeyCode.Hash
            };
            SetAbsoluteKeysToColor(whitekeys, Color.white);
        }
    }
}