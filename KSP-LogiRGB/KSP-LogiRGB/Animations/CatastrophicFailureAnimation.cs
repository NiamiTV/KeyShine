using KSP_LogiRGB.ColorSchemes;
using UnityEngine;

namespace KSP_LogiRGB
{
    /// <summary>
    ///     Displays a warning on the keyboard, indicating that the vessel has crashed
    /// </summary>
    internal class CatastrophicFailureAnimation : KeyboardAnimation
    {
        /// <summary>
        ///     The red frame
        /// </summary>
        private static readonly ColorScheme red = new ColorScheme(Color.red);

        /// <summary>
        ///     The blue frame
        /// </summary>
        private static readonly ColorScheme blue = new ColorScheme(Color.blue);

        /// <summary>
        ///     Static constructor adds lightning bolts in different colors to both frames
        /// </summary>
        static CatastrophicFailureAnimation()
        {
            KeyCode[] WarningKeys =
            {
                /// Warning Symbol
                KeyCode.Space, KeyCode.C,KeyCode.F,KeyCode.T,KeyCode.Alpha6,KeyCode.F5,KeyCode.Alpha7,KeyCode.U,KeyCode.J,KeyCode.M
            };

            blue.SetAbsoluteKeysToColor(WarningKeys, Color.white);
            red.SetAbsoluteKeysToColor(WarningKeys, Color.blue);
        }

        /// <summary>
        ///     <see cref="KeyboardAnimation.getFrame" />
        /// </summary>
        /// <returns>the current animation frame.</returns>
        public ColorScheme getFrame()
        {
            return (int)Time.realtimeSinceStartup % 2 == 0 ? red : blue;
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

            ///FlightEndModes.CATASTROPHIC_FAILURE
            

            return false; /// Keep playing until scene change
            
        }
    }
}
