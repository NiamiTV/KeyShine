using KeyShine.ColorSchemes;

namespace KeyShine.SceneManagers
{
    /// <summary>
    ///     Allows creation of keyboard color managers for multiple scenes.
    /// </summary>
    internal interface SceneManager
    {
        /// <summary>
        ///     Gets the keyboard color scheme for the current frame.
        /// </summary>
        /// <returns>The new color scheme.</returns>
        ColorScheme getScheme();
    }
}