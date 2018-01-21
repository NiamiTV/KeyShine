using System.Linq;
using KSP_LogiRGB.ColorSchemes;
using UnityEngine;

namespace KSP_LogiRGB
{
    /// <summary>
    ///     Displays a warning on the keyboard, indicating that the vessel is currently out of power and cannot
    ///     be controlled. Consists of two frames alternating at 1fps.
    /// </summary>
    internal class PowerLostAnimation : KeyboardAnimation
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
        static PowerLostAnimation()
        {
            KeyCode[] lightningKeys =
            {
                /// Left lightning
                KeyCode.F3, KeyCode.Alpha3, KeyCode.W, KeyCode.E, KeyCode.R, KeyCode.D, KeyCode.X, KeyCode.LeftAlt,

                /// Right lightning
                KeyCode.F9, KeyCode.Alpha0, KeyCode.O, KeyCode.P, KeyCode.LeftBracket, KeyCode.Semicolon, KeyCode.Period,
                KeyCode.RightAlt
            };

            blue.SetKeysToColor(lightningKeys, Color.white);
            red.SetKeysToColor(lightningKeys, Color.blue);
        }

        /// <summary>
        ///     <see cref="KeyboardAnimation.getFrame" />
        /// </summary>
        /// <returns>the current animation frame.</returns>
        public ColorScheme getFrame()
        {
            return (int) Time.realtimeSinceStartup%2 == 0 ? red : blue;
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

            /// Get current vessel charge level
            double electricCharge, maxElectricCharge;
            FlightGlobals.ActiveVessel.GetConnectedResourceTotals(
                PartResourceLibrary.Instance.resourceDefinitions["ElectricCharge"].id,
                out electricCharge, out maxElectricCharge);

            if (electricCharge > 0.0001)
            {
                /// Charge available
                return true;
            }
            else if (maxElectricCharge < 0.0001)
            {
                /// No charge stored on ship
                return true;
            }
            else
            {
                /// Charge depleted - continue animation
                return false;
            }
        }
    }
}