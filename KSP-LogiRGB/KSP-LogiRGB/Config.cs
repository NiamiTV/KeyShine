﻿using System.Collections.Generic;
using UnityEngine;

namespace KeyShine
{
    public class Config
    {
        private static Config instance;

        /// <summary>
        ///     Configures the key binding and colors for every action group
        /// </summary>
        public readonly Dictionary<KSPActionGroup, KeyValuePair<KeyBinding, KeyValuePair<Color, Color>>> actionGroupConf;

        public readonly KeyValuePair<Color, Color> cyanBlueToggle = new KeyValuePair<Color, Color>(Color.cyan,
            Color.blue);

        public readonly KeyValuePair<Color, Color> redGreenToggle = new KeyValuePair<Color, Color>(Color.red,
            Color.green);

        public readonly KeyValuePair<Color, Color> redOrangeToggle = new KeyValuePair<Color, Color>(Color.red,
            new Color32(255, 100, 0, 255));

        /// <summary>
        ///     Percentage limit which triggers low resource alert
        /// </summary>
        public readonly float LowResourceAlert = 0.1f;

        /// <summary>
        ///     Colour of dynamic throttle
        /// </summary>
        public readonly Color32 ThrottleColour = new Color32(255,0,0,255);


        private Config()
        {
            actionGroupConf = new Dictionary<KSPActionGroup, KeyValuePair<KeyBinding, KeyValuePair<Color, Color>>>
            {
                {
                    KSPActionGroup.Abort,
                    new KeyValuePair<KeyBinding, KeyValuePair<Color, Color>>(GameSettings.AbortActionGroup,
                        redOrangeToggle)
                },
                {
                    KSPActionGroup.Brakes,
                    new KeyValuePair<KeyBinding, KeyValuePair<Color, Color>>(GameSettings.BRAKES, redGreenToggle)
                },
                {
                    KSPActionGroup.Custom01,
                    new KeyValuePair<KeyBinding, KeyValuePair<Color, Color>>(GameSettings.CustomActionGroup1,
                        cyanBlueToggle)
                },
                {
                    KSPActionGroup.Custom02,
                    new KeyValuePair<KeyBinding, KeyValuePair<Color, Color>>(GameSettings.CustomActionGroup2,
                        cyanBlueToggle)
                },
                {
                    KSPActionGroup.Custom03,
                    new KeyValuePair<KeyBinding, KeyValuePair<Color, Color>>(GameSettings.CustomActionGroup3,
                        cyanBlueToggle)
                },
                {
                    KSPActionGroup.Custom04,
                    new KeyValuePair<KeyBinding, KeyValuePair<Color, Color>>(GameSettings.CustomActionGroup4,
                        cyanBlueToggle)
                },
                {
                    KSPActionGroup.Custom05,
                    new KeyValuePair<KeyBinding, KeyValuePair<Color, Color>>(GameSettings.CustomActionGroup5,
                        cyanBlueToggle)
                },
                {
                    KSPActionGroup.Custom06,
                    new KeyValuePair<KeyBinding, KeyValuePair<Color, Color>>(GameSettings.CustomActionGroup6,
                        cyanBlueToggle)
                },
                {
                    KSPActionGroup.Custom07,
                    new KeyValuePair<KeyBinding, KeyValuePair<Color, Color>>(GameSettings.CustomActionGroup7,
                        cyanBlueToggle)
                },
                {
                    KSPActionGroup.Custom08,
                    new KeyValuePair<KeyBinding, KeyValuePair<Color, Color>>(GameSettings.CustomActionGroup8,
                        cyanBlueToggle)
                },
                {
                    KSPActionGroup.Custom09,
                    new KeyValuePair<KeyBinding, KeyValuePair<Color, Color>>(GameSettings.CustomActionGroup9,
                        cyanBlueToggle)
                },
                {
                    KSPActionGroup.Custom10,
                    new KeyValuePair<KeyBinding, KeyValuePair<Color, Color>>(GameSettings.CustomActionGroup10,
                        cyanBlueToggle)
                },
                {
                    KSPActionGroup.Gear,
                    new KeyValuePair<KeyBinding, KeyValuePair<Color, Color>>(GameSettings.LANDING_GEAR, redGreenToggle)
                },
                {
                    KSPActionGroup.Light,
                    new KeyValuePair<KeyBinding, KeyValuePair<Color, Color>>(GameSettings.HEADLIGHT_TOGGLE,
                        redGreenToggle)
                },
                {
                    KSPActionGroup.RCS,
                    new KeyValuePair<KeyBinding, KeyValuePair<Color, Color>>(GameSettings.RCS_TOGGLE, redGreenToggle)
                },
                {
                    KSPActionGroup.SAS,
                    new KeyValuePair<KeyBinding, KeyValuePair<Color, Color>>(GameSettings.SAS_TOGGLE, redGreenToggle)
                },
                {
                    KSPActionGroup.Stage,
                    new KeyValuePair<KeyBinding, KeyValuePair<Color, Color>>(GameSettings.UIMODE_STAGING, redGreenToggle)
                }
            };
        }

        public static Config Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Config();
                }
                return instance;
            }
        }
    }
}