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

using KSP_LogiRGB.LEDControllers.Logitech;
using NUnit.Framework;

namespace KSP_LogiRGB.Tests
{
    [TestFixture]
    public class DynamicLoadTests
    {
        [Test]
        public void TestArchitecture()
        {
            Assert.IsTrue(LogitechSDK.Init());
            Assert.IsTrue(LogitechSDK.SetTargetDevice(LogitechSDK.LogiDevicetypePerkeyRGB));
            Assert.IsTrue(LogitechSDK.SetKeyLighting(LogitechSDK.KeyName.W, 100, 0, 100));
            Assert.IsTrue(LogitechSDK.SetKeyLighting(0x15b, 100, 0, 100)); // Left Windows-Key
            
            LogitechSDK.Shutdown();
            Assert.IsFalse(LogitechSDK.SetKeyLighting(LogitechSDK.KeyName.W, 100, 0, 100));
            Assert.IsFalse(LogitechSDK.SetKeyLighting(0x015b, 100, 0, 100));
        }
    }
}