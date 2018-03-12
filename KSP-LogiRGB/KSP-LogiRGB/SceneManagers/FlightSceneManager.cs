using System;
using System.Collections.Generic;
using System.Linq;
using KSP_LogiRGB.ColorSchemes;
using UnityEngine;
using KSP.UI.Screens;

namespace KSP_LogiRGB.SceneManagers
{
    /// <summary>
    ///     Manages the keyboard colors during all flight scenes.
    /// </summary>
    internal class FlightSceneManager : SceneManager
    {
        private static readonly KeyCode[] rotation =
        {
            GameSettings.ROLL_LEFT.primary.code,
            GameSettings.ROLL_RIGHT.primary.code,
            GameSettings.PITCH_DOWN.primary.code,
            GameSettings.PITCH_UP.primary.code,
            GameSettings.YAW_LEFT.primary.code,
            GameSettings.YAW_RIGHT.primary.code
        };

        private static readonly KeyCode[] timewarp =
        {
            GameSettings.TIME_WARP_INCREASE.primary.code,
            GameSettings.TIME_WARP_DECREASE.primary.code
        };

        /// <summary>
        ///     Contains all ActionGroups and their current usage state. False means
        ///     this ActionGroup has no impact on any part of the vessel.
        /// </summary>
        private readonly Dictionary<KSPActionGroup, bool> actionGroups = new Dictionary<KSPActionGroup, bool>();

        /// <summary>
        ///     The current keyboard state color scheme
        /// </summary>
        private ColorScheme currentColorScheme;

        /// <summary>
        ///     The vessel we are piloting currently. Can be a normal vessel or a single
        ///     kerbal.
        /// </summary>
        private Vessel currentVessel;

        /// <summary>
        ///     Fills the action group list with all false values;
        /// </summary>
        public FlightSceneManager()
        {
            resetActionGroups();
        }

        /// <summary>
        ///     Returns the calculated color scheme for the current game state.
        /// </summary>
        /// <returns>The final color scheme for this frame</returns>
        public ColorScheme getScheme()
        {
            update();
            return currentColorScheme;
        }

        /// <summary>
        ///     Recalculates every action group's usage.
        /// </summary>
        private void resetActionGroups()
        {
            actionGroups.Clear();
            foreach (var group in Enum.GetValues(typeof(KSPActionGroup)).Cast<KSPActionGroup>())
            {
                if (!actionGroups.ContainsKey(group) && group != KSPActionGroup.REPLACEWITHDEFAULT)
                {
                    actionGroups.Add(group, false);
                }
            }
        }

        /// <summary>
        ///     Called by the plugin on every physics frame.
        /// </summary>
        private void update()
        {
            if (currentVessel != FlightGlobals.ActiveVessel)
            {
                currentVessel = FlightGlobals.fetch.activeVessel;
                resetActionGroups();
                findUsableActionGroups();
            }

            if (currentVessel.isEVA)
            {
                currentColorScheme = new EVAScheme();
                showGauge("EVAFuel", currentVessel.evaController.Fuel, currentVessel.evaController.FuelCapacity);
            }
            else if (!currentVessel.IsControllable)
            {
                AnimationManager.Instance.setAnimation(new PowerLostAnimation());
            }
            else
            {
                currentColorScheme = new FlightScheme();
                recalculateResources();
                updateToggleables();
                updateStaging();
            }
            displayVesselHeight();
        }

        /// <summary>
        ///     Scans the ship's parts for actions in any action group. Every action group
        ///     that has any active parts gets a toggleing button lit up.
        /// </summary>
        private void findUsableActionGroups()
        {
            var allActionsList = new List<BaseAction>();

            foreach (var p in currentVessel.parts)
            {
                allActionsList.AddRange(p.Actions);
                foreach (PartModule pm in p.Modules)
                    allActionsList.AddRange(pm.Actions);
            }

            foreach (var action in allActionsList)
            {
                foreach (var group in Enum.GetValues(typeof(KSPActionGroup)).Cast<KSPActionGroup>())
                {
                    if (group != KSPActionGroup.REPLACEWITHDEFAULT)
                    {
                        actionGroups[group] = actionGroups[group] || ((action.actionGroup & group) == group);
                    }
                }
            }

            ///KSP ignores RCS and SAS action groups so we enable them manually
            actionGroups[KSPActionGroup.RCS] = true;
            actionGroups[KSPActionGroup.SAS] = true;
        }

        /// <summary>
        ///     Displays the fuel status as lights on the keyboard.
        /// </summary>
        private void recalculateResources()
        {
            IEnumerator<PartResourceDefinition> enumerator = PartResourceLibrary.Instance.resourceDefinitions.GetEnumerator();
            while (enumerator.MoveNext())
            {
                double amount, maxAmount;
                currentVessel.GetConnectedResourceTotals(enumerator.Current.id, out amount, out maxAmount);
                showGauge(enumerator.Current.name, amount, maxAmount);
            }
        }

        /// <summary>
        ///     Displays the amount of resources left as a gauge on the keyboard
        /// </summary>
        /// <param name="resource">The name of the resource</param>
        /// <param name="amount">The actual amount of the resource in the current stage</param>
        /// <param name="maxAmount">The maximal amount of the resource in the current stage</param>
        private void showGauge(string resource, double amount, double maxAmount)
        {
            Func<Color, int, Color> partialColor = (original, third) =>
            {
                var newColor = new Color(original.r, original.g, original.b, original.a);
                var ceiling = maxAmount/3*(third + 1);
                var floor = maxAmount/3*third;

                if (amount <= ceiling)
                {
                    var factor = (float) ((amount - floor)/(ceiling - floor));
                    newColor.r *= factor;
                    newColor.g *= factor;
                    newColor.b *= factor;
                }
                if (amount - floor < 0.001)
                    newColor = Color.black;
                return newColor;
            };

            Action<KeyCode[], Color> displayFuel = (keys, color) =>
            {
                for (var i = 0; i < 3; i++)
                {
                    currentColorScheme.SetKeyToColor(keys[i], partialColor(color, i));
                }
            };

            switch (resource)
            {
                case "ElectricCharge":
                    KeyCode[] electric = {KeyCode.Print, KeyCode.ScrollLock, KeyCode.Pause};
                    displayFuel(electric, Color.blue);
                    break;
                case "LiquidFuel":
                    KeyCode[] liquid = {KeyCode.Numlock, KeyCode.KeypadDivide, KeyCode.KeypadMultiply};
                    displayFuel(liquid, Color.green);
                    break;
                case "Oxidizer":
                    KeyCode[] oxidizer = {KeyCode.Keypad7, KeyCode.Keypad8, KeyCode.Keypad9};
                    displayFuel(oxidizer, Color.cyan);
                    break;
                case "MonoPropellant":
                case "EVAFuel":
                    KeyCode[] monoprop = {KeyCode.Keypad4, KeyCode.Keypad5, KeyCode.Keypad6};
                    displayFuel(monoprop, Color.yellow);
                    break;
                case "SolidFuel":
                    KeyCode[] solid = {KeyCode.Keypad1, KeyCode.Keypad2, KeyCode.Keypad3};
                    displayFuel(solid, Color.magenta);
                    break;
                case "Ablator":
                    KeyCode[] ablator = {KeyCode.Delete, KeyCode.End, KeyCode.PageDown};
                    displayFuel(ablator, new Color(244, 259, 0, 255));
                    break;
                case "XenonGas":
                    KeyCode[] xenon = {KeyCode.Insert, KeyCode.Home, KeyCode.PageUp};
                    displayFuel(xenon, Color.gray);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        ///     Updates all toggleable buttons on the keyboard.
        /// </summary>
        private void updateToggleables()
        {
            /// Updates all toggleable action group keys
            foreach (var agroup in actionGroups)
            {
                if (agroup.Key != KSPActionGroup.None)
                {
                    if (!agroup.Value)
                    {
                        currentColorScheme.SetKeyToColor(Config.Instance.actionGroupConf[agroup.Key].Key.primary.code,
                            Color.black);
                    }
                    else if (currentVessel.ActionGroups[agroup.Key])
                    {
                        currentColorScheme.SetKeyToColor(Config.Instance.actionGroupConf[agroup.Key].Key.primary.code,
                            Config.Instance.actionGroupConf[agroup.Key].Value.Value);
                    }
                    else
                    {
                        currentColorScheme.SetKeyToColor(Config.Instance.actionGroupConf[agroup.Key].Key.primary.code,
                            Config.Instance.actionGroupConf[agroup.Key].Value.Key);
                    }
                }
            }

            /// Colors the map view key
            currentColorScheme.SetKeyToColor(
                GameSettings.MAP_VIEW_TOGGLE.primary.code,
                MapView.MapIsEnabled ? Config.Instance.redGreenToggle.Value : Config.Instance.redGreenToggle.Key
                );

            /// Lights steering buttons differently if precision mode is on
            if (FlightInputHandler.fetch.precisionMode)
            {
                currentColorScheme.SetKeysToColor(rotation, Color.yellow);
                currentColorScheme.SetKeyToColor(GameSettings.PRECISION_CTRL.primary.code, Color.green);
            }
            else
            {
                currentColorScheme.SetKeysToColor(rotation, Color.white);
                currentColorScheme.SetKeyToColor(GameSettings.PRECISION_CTRL.primary.code, Color.red);
            }

            /// Lights the quicksave button green, if it is enabled, red otherwise
            if (currentVessel.IsClearToSave() == ClearToSaveStatus.CLEAR ||
                currentVessel.IsClearToSave() == ClearToSaveStatus.NOT_IN_ATMOSPHERE ||
                currentVessel.IsClearToSave() == ClearToSaveStatus.NOT_UNDER_ACCELERATION)
                currentColorScheme.SetKeyToColor(GameSettings.QUICKSAVE.primary.code, Color.green);
            else
                currentColorScheme.SetKeyToColor(GameSettings.QUICKSAVE.primary.code, Color.red);

            /// Lights up the quickload button (if allowed)
            if (HighLogic.CurrentGame.Parameters.Flight.CanQuickLoad)
                currentColorScheme.SetKeyToColor(GameSettings.QUICKLOAD.primary.code, Color.green);

            /// Colors the timewarp buttons red and green for physics and on-rails warp
            if (TimeWarp.WarpMode == TimeWarp.Modes.HIGH)
                currentColorScheme.SetKeysToColor(timewarp, Color.green);
            else
                currentColorScheme.SetKeysToColor(timewarp, Color.red);

            /// Different colors for the camera mode switch
            switch (FlightCamera.fetch.mode)
            {
                case FlightCamera.Modes.AUTO:
                    currentColorScheme.SetKeyToColor(GameSettings.CAMERA_NEXT.primary.code, Color.green);
                    break;
                case FlightCamera.Modes.CHASE:
                    currentColorScheme.SetKeyToColor(GameSettings.CAMERA_NEXT.primary.code, Color.blue);
                    break;
                case FlightCamera.Modes.FREE:
                    currentColorScheme.SetKeyToColor(GameSettings.CAMERA_NEXT.primary.code, Color.yellow);
                    break;
                case FlightCamera.Modes.LOCKED:
                    currentColorScheme.SetKeyToColor(GameSettings.CAMERA_NEXT.primary.code, Color.cyan);
                    break;
                default:
                    currentColorScheme.SetKeyToColor(GameSettings.CAMERA_NEXT.primary.code, Color.white);
                    break;
            }
        }

        /// <summary>
        ///     Updates staging key.
        /// </summary>
        private void updateStaging()
        {
            /// Solid black if staging is complete
            if (currentVessel.currentStage == 0)
            {
                currentColorScheme.SetKeyToColor(GameSettings.LAUNCH_STAGES.primary.code, Color.black);
            }
            /// Solid purple if staging is locked
            else if (InputLockManager.IsLocked(ControlTypes.STAGING))
            {
                currentColorScheme.SetKeyToColor(GameSettings.LAUNCH_STAGES.primary.code, Color.magenta);
            }
            /// Solid yellow if staging is in cooldown
            else if (!StageManager.CanSeparate)
            {
                currentColorScheme.SetKeyToColor(GameSettings.LAUNCH_STAGES.primary.code, Color.yellow);
            }
            /// Blinking green if ready to stage
            else
            {
                double frequency = 0.333;
                bool ledOn = (int)(Math.Truncate(Time.time / frequency)) % 2 != 0;
                if (ledOn)
                {
                    currentColorScheme.SetKeyToColor(GameSettings.LAUNCH_STAGES.primary.code, Color.green);
                }
                else
                {
                    currentColorScheme.SetKeyToColor(GameSettings.LAUNCH_STAGES.primary.code, Color.black);
                }
            }
        }

        /// <summary>
        ///     Height off ground display on F keys that arent quicksave and quickload. Scale is in powers of ten
        ///     from 1m to 1000km.
        /// </summary>
        private void displayVesselHeight()
        {
            KeyCode[] heightScaleKeys =
            {
                KeyCode.F1, KeyCode.F2, KeyCode.F3, KeyCode.F4, KeyCode.F6, KeyCode.F7, KeyCode.F8
            };

            for (var i = 0; i < heightScaleKeys.Length; i++)
            {
                var floor = i > 0 ? Math.Pow(10, i - 1) : 0;
                var ceiling = Math.Pow(10, i);
                var vesselHeight = calculateDistanceFromGround();
                Color newColor = new Color32(0, 100, 100, 255);

                if (vesselHeight > ceiling)
                    currentColorScheme.SetKeyToColor(heightScaleKeys[i], newColor);
                else if (vesselHeight > floor)
                {
                    var factor = (float) ((vesselHeight - floor)/(ceiling - floor));
                    newColor.r *= factor;
                    newColor.g *= factor;
                    newColor.b *= factor;
                    currentColorScheme.SetKeyToColor(heightScaleKeys[i], newColor);
                }
            }
        }

        /// <summary>
        ///     Calculates the ground distance for the vessel.
        /// </summary>
        /// <returns></returns>
        private double calculateDistanceFromGround()
        {
            var CoM = currentVessel.CoM; //Gets centre of mass
            Vector3 up = FlightGlobals.getUpAxis(CoM); //Gets up axis (needed for the raycast)
            var ASL = FlightGlobals.getAltitudeAtPos(CoM);
            RaycastHit craft;
            float trueAlt;
            float surfaceAlt;
            float bottomAlt;

            if (Physics.Raycast(CoM, -up, out craft, ASL + 10000f, 1 << 15))
            {
                trueAlt = Mathf.Min(ASL, craft.distance); //Smallest value between ASL and distance from ground
            }

            else
            {
                trueAlt = ASL;
            }

            surfaceAlt = ASL - trueAlt;
            bottomAlt = trueAlt; //Initiation to be sure the loop doesn't return a false value
            foreach (var p in currentVessel.parts)
            {
                if (p.collider != null) //Makes sure the part actually has a collider to touch ground
                {
                    var bottom = p.collider.ClosestPointOnBounds(currentVessel.mainBody.position);
                    //Gets the bottom point
                    var partAlt = FlightGlobals.getAltitudeAtPos(bottom) - surfaceAlt; //Gets the looped part alt
                    bottomAlt = Mathf.Max(0, Mathf.Min(bottomAlt, partAlt));
                    //Stores the smallest value in all the parts
                }
            }

            return bottomAlt;
        }
    }
}