using KeyShine.ColorSchemes;
using UnityEngine;

namespace KeyShine
{
    /// <summary>
    ///     Displays a warning on the keyboard, indicating that the vessel has crashed
    /// </summary>
    internal class SplashDownAnimation : KeyboardAnimation
    {
        /// <summary>
        ///     The first frame
        /// </summary>        
        private static readonly ColorScheme Wave1 = new ColorScheme(new Color32(0,9,120,255));

        /// <summary>
        ///     The second frame
        /// </summary>
        private static readonly ColorScheme Wave2 = new ColorScheme(Color.blue);

        /// <summary>
        ///     Static constructor adds waves in different places to both frames
        /// </summary>
        static SplashDownAnimation ()
        {
            KeyCode[] WaveKeys =
            {
                KeyCode.BackQuote,KeyCode.CapsLock,KeyCode.Q,KeyCode.Alpha2,KeyCode.F2,
                KeyCode.LeftWindows,KeyCode.Z,KeyCode.S,KeyCode.E,KeyCode.Alpha4,KeyCode.F4,
                KeyCode.C,KeyCode.F,KeyCode.T,KeyCode.Alpha6,KeyCode.F5,
                KeyCode.B,KeyCode.H,KeyCode.U,KeyCode.Alpha8,KeyCode.F7,
                KeyCode.M,KeyCode.K,KeyCode.O,KeyCode.Alpha0,KeyCode.F9,
                KeyCode.Period,KeyCode.Semicolon,KeyCode.LeftBracket,KeyCode.Equals,KeyCode.F11,
                KeyCode.Menu,KeyCode.RightShift,KeyCode.Return,KeyCode.BackQuote

            };

            Wave1.SetAbsoluteKeysToColor(WaveKeys, Color.blue);
            Wave2.SetAbsoluteKeysToColor(WaveKeys, new Color32(0, 9, 120, 255));
            
        }

        /// <summary>
        ///     <see cref="KeyboardAnimation.getFrame" />
        /// </summary>
        /// <returns>the current animation frame.</returns>
        public ColorScheme getFrame()
        {
            return (int)Time.realtimeSinceStartup % 2 == 0 ? Wave1 : Wave2;
        }

        /// <summary>
        ///     <see cref="KeyboardAnimation.isFinished" />
        /// </summary>
        /// <returns>true, if the animation is finished, false if not.</returns>
        public bool isFinished()
        {
            /// Exit if the scene changes.
            if (HighLogic.LoadedScene != GameScenes.FLIGHT)
                return true;

            /// Exits if craft is no longer splashed down
            if (FlightGlobals.fetch.activeVessel.situation != Vessel.Situations.SPLASHED)
                return true;     

            return false;

        }
    }
}
