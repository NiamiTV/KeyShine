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