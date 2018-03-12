using System;

namespace KSP_LogiRGB.Layout.LayoutProviders.Windows
{
    internal class WindowsTemporaryLayout: ITemporaryLayout
    {
        private IntPtr originalLayout;
        private IntPtr temporaryLayout;
        private bool shouldUnload;

        internal WindowsTemporaryLayout(string code)
        {
            // Get a list of keyboard layouts already loaded.
            var loadedHandles = User32.GetKeyboardLayoutHandles();

            // Store the handle to the current layout.
            originalLayout = User32.GetKeyboardLayout(0);

            // Load the layout, User32.IgnoreUserLocale prevents Windows for loading a layout for the user's
            // given locale, which would not be removed along with the intended layout.
            temporaryLayout = User32.LoadKeyboardLayout(
                code, User32.IgnoreUserLocale | User32.ActivateOnLoad);

            // Arrange for the layout to be unloaded at disposal if it was not already loaded.
            if (!loadedHandles.Contains(temporaryLayout))
            {
                shouldUnload = true;
            }
            else
            {
                shouldUnload = false;
            }
        }

        /// <summary>
        /// Reverts to the previous layout and unloads the temporary one if necessary.
        /// </summary>
        public void Dispose()
        {
            if (shouldUnload)
            {
                User32.ActivateKeyboardLayout(originalLayout, 0);
                User32.UnloadKeyboardLayout(temporaryLayout);
            }
        }
    }
}