//-----------------------------------------------------------------------
// <copyright file="GetDevicesTests.cs" company="Kount Inc">
//     Copyright 2018 Kount Inc. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace KountAccessTest
{
    using KountAccessSdk.Models;
    using KountAccessSdk.Service;
    using Newtonsoft.Json;
    using NUnit.Framework;
    using NUnit.Framework.Constraints;

    /// <summary>
    /// Test class for GetDevicesTests
    /// </summary>
    public class GetDevicesTests : AccessSDKTestBase
    {
        [Test]
        public void TestGetDevices()
        {
            // Arrange
            MockupWebClientFactory mockFactory = new MockupWebClientFactory(this.jsonDevicesInfo);
            AccessSdk sdk = new AccessSdk(accessUrl, merchantId, apiKey, DEFAULT_VERSION, mockFactory);

            // Act
            DevicesInfo devicesResp = sdk.GetDevices(uniq);

            this.logger.Debug(JsonConvert.SerializeObject(devicesResp));

            // Assert
            Assert.IsNotNull(devicesResp);
            Assert.AreEqual(this.devicesInfo.ResponseId, devicesResp.ResponseId);

            Assert.AreEqual(this.devicesInfo.Devices.Count, devicesResp.Devices.Count);
        }

        [Test]
        public void TestGetDevicesWithoutUniq_ShouldThrowException()
        {
            // Arrange
            MockupWebClientFactory mockFactory = new MockupWebClientFactory(this.jsonDevicesInfo);
            AccessSdk sdk = new AccessSdk(accessUrl, merchantId, apiKey, DEFAULT_VERSION, mockFactory);

            var emptyUniq = "";

            // Act
            ActualValueDelegate<object> testDelegate = () => sdk.GetDevices(emptyUniq);

            // Assert
            Assert.That(testDelegate, Throws.TypeOf<AccessException>());
        }
    }
}
