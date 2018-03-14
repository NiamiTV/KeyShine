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