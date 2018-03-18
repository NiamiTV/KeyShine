using KSP_LogiRGB.ColorSchemes;
using UnityEngine;

namespace KSP_LogiRGB.SceneManagers
{
    /// <summary>
    ///     Manages the keyboard colors for VAB and SPH scenes.
    /// </summary>
    internal class VABSceneManager : SceneManager
    {
        /// <summary>
        ///     The base color scheme, used by all editor scenes
        /// </summary>
        private ColorScheme currentColorScheme;

        /// <summary>
        ///     Returns the rendered color scheme for the current game state.
        /// </summary>
        /// <returns>The finalized color scheme</returns>
        public ColorScheme getScheme()
        {
            if (currentColorScheme == null)
            {
                reset();
            }

            update();
            return currentColorScheme;
        }

        /// <summary>
        ///     Called during every physics frame of the game. Recalculates the colors
        ///     according to the editor's state.
        /// </summary>
        private void update()
        {
            updatePlacementState();
            updateToggleables();
        }

        /// <summary>
        ///     Lights up the corresponding key to the current editor construction mode.
        /// </summary>
        private void updatePlacementState()
        {
            currentColorScheme.SetKeyCodesToColor(new[]
            {
                GameSettings.Editor_modePlace.primary.code, GameSettings.Editor_modeOffset.primary.code,
                GameSettings.Editor_modeRotate.primary.code, GameSettings.Editor_modeRoot.primary.code
            }, Color.white);

            var state = EditorLogic.fetch.EditorConstructionMode;

            switch (state)
            {
                case ConstructionMode.Place:
                    currentColorScheme.SetKeyCodeToColor(GameSettings.Editor_modePlace.primary.code, Color.blue);
                    break;
                case ConstructionMode.Move:
                    currentColorScheme.SetKeyCodeToColor(GameSettings.Editor_modeOffset.primary.code, Color.blue);
                    break;
                case ConstructionMode.Rotate:
                    currentColorScheme.SetKeyCodeToColor(GameSettings.Editor_modeRotate.primary.code, Color.blue);
                    break;
                case ConstructionMode.Root:
                    currentColorScheme.SetKeyCodeToColor(GameSettings.Editor_modeRoot.primary.code, Color.blue);
                    break;
            }
        }

        /// <summary>
        ///     Lights up all toggleable keys in a color signifying the button's state.
        /// </summary>
        private void updateToggleables()
        {
            currentColorScheme.SetKeyCodesToColor(
                new[] {GameSettings.Editor_toggleSymMode.primary.code, GameSettings.Editor_toggleAngleSnap.primary.code },
                Color.red);

            if (EditorLogic.fetch.symmetryMode > 0)
            {
                currentColorScheme.SetKeyCodeToColor(GameSettings.Editor_toggleSymMode.primary.code, Color.green);
            }

            if (EditorLogic.fetch.symmetryMethod == SymmetryMethod.Mirror)
                currentColorScheme.SetKeyCodeToColor(GameSettings.Editor_toggleSymMethod.primary.code, Color.blue);
            else if (EditorLogic.fetch.symmetryMethod == SymmetryMethod.Radial)
                currentColorScheme.SetKeyCodeToColor(GameSettings.Editor_toggleSymMethod.primary.code, Color.green);

            if (GameSettings.VAB_USE_ANGLE_SNAP)
                currentColorScheme.SetKeyCodeToColor(GameSettings.Editor_toggleAngleSnap.primary.code, Color.green);
        }

        /// <summary>
        ///     Resets the color scheme to the original one.
        /// </summary>
        private void reset()
        {
            currentColorScheme = new VabScheme();
        }
    }
}