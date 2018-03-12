using System.Collections.Generic;
using UnityEngine;

namespace KSP_LogiRGB.ColorSchemes
{
    /// <summary>
    ///     Represents a base color scheme, saving all the colors per key.
    /// </summary>
    public class ColorScheme
    {
        /// <summary>
        ///     The base color of the scheme. Any key that is not assigned a color will have this one instead.
        /// </summary>
        public Color BaseColor;

        /// <summary>
        ///     A list of keys that are dependent on the user's keyboard layout. These should be correspond to
        ///     keys that the user needs will press. These colors will be rendered on top of absolute
        ///     assignments.
        /// </summary>
        public Dictionary<KeyCode, Color> MappedKeys;

        /// <summary>
        ///     A list of keys that are not dependent on the user's keyboard layout. These are useful if you
        ///     want to draw graphics on the user's keyboard, and don't want to worry about silly things like
        ///     keyboard layouts! These colors will be rendered below any assignments mapped to user keys.
        /// </summary>
        public Dictionary<KeyCode, Color> AbsoluteKeys;

        /// <summary>
        ///     Creates a new ColorScheme rendering all keys black;
        /// </summary>
        public ColorScheme() : this(Color.black)
        {
        }

        /// <summary>
        ///     Creates a new ColorScheme rendering all keys in the defined color.
        /// </summary>
        /// <param name="baseColor">The color to use</param>
        public ColorScheme(Color baseColor)
        {
            BaseColor = baseColor;
            MappedKeys = new Dictionary<KeyCode, Color>();
            AbsoluteKeys = new Dictionary<KeyCode, Color>();
        }

        /// <summary>
        ///     Sets a mapped key to the defined color.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="color"></param>
        public void SetKeyToColor(KeyCode key, Color color)
        {
            MappedKeys[key] = color;
        }

        /// <summary>
        ///     Sets a number of mapped keys to the defined color.
        /// </summary>
        /// <param name="keys">An array of keys to light up</param>
        /// <param name="color">The color to use</param>
        public void SetKeysToColor(KeyCode[] keys, Color color)
        {
            foreach (var key in keys)
            {
                SetKeyToColor(key, color);
            }
        }

        /// <summary>
        ///     Sets an absolute key to the defined color.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="color"></param>
        public void SetAbsoluteKeyToColor(KeyCode key, Color color)
        {
            AbsoluteKeys[key] = color;
        }

        /// <summary>
        ///     Sets a number of absolute keys to the defined color.
        /// </summary>
        /// <param name="keys">An array of keys to light up</param>
        /// <param name="color">The color to use</param>
        public void SetAbsoluteKeysToColor(KeyCode[] keys, Color color)
        {
            foreach (var key in keys)
            {
                SetAbsoluteKeyToColor(key, color);
            }
        }
    }
}