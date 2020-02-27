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

using System.Collections.Generic;
using UnityEngine;

namespace KeyShine.ColorSchemes
{
    /// <summary>
    ///     A color scheme that defines multiple layers of colours to be set to a keyboard.
    /// </summary>
    public class ColorScheme
    {
        /// <summary>
        ///     The base layer of the scheme. Any key that is not assigned a color will have this one instead.
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
        ///     Default scheme with black keys.
        /// </summary>
        public ColorScheme() : this(Color.black)
        {
        }

        /// <summary>
        ///     Creates a new [ColorScheme] with the given color as a base layer.
        /// </summary>
        /// <param name="baseColor">The base color to use</param>
        public ColorScheme(Color baseColor)
        {
            BaseColor = baseColor;
            MappedKeys = new Dictionary<KeyCode, Color>();
            AbsoluteKeys = new Dictionary<KeyCode, Color>();
        }

        /// <summary>
        ///     Sets a mapped key to the defined color.
        /// </summary>
        /// <param name="key">
        ///     A key that should light up, location depending on the user's keyboard layout.
        /// </param>
        /// <param name="color">The color it should be assigned.</param>
        public void SetKeyCodeToColor(KeyCode key, Color color)
        {
            MappedKeys[key] = color;
        }

        /// <summary>
        ///     Sets a number of mapped keys to the defined color.
        /// </summary>
        /// <param name="keys">
        ///     An array of keys to light up, their location depending on the user's keyboard layout.
        /// </param>
        /// <param name="color">The color to light the keys with.</param>
        public void SetKeyCodesToColor(KeyCode[] keys, Color color)
        {
            foreach (var key in keys)
            {
                SetKeyCodeToColor(key, color);
            }
        }

        /// <summary>
        ///     Sets the base color
        /// </summary>
        /// <param name="color">The color to change the base color to</param>
        public void SetBaseColor(Color color)
        {
            BaseColor = color;
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