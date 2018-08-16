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
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Test class for GetDevicesTests
    /// </summary>
    [TestClass]
    public class GetDevicesTests : AccessSDKTestBase
    {
        [TestMethod]
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

        [TestMethod]
        [ExpectedException(typeof(AccessException))]
        public void TestGetDevicesWithoutUniq_ShouldThrowException()
        {
            // Arrange
            MockupWebClientFactory mockFactory = new MockupWebClientFactory(this.jsonDevicesInfo);
            AccessSdk sdk = new AccessSdk(accessUrl, merchantId, apiKey, DEFAULT_VERSION, mockFactory);

            var emptyUniq = "";

            // Act
            DevicesInfo infoResp = sdk.GetDevices(emptyUniq);

            // Assert
        }
    }
}
