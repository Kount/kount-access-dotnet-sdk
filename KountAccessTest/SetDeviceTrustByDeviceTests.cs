//-----------------------------------------------------------------------
// <copyright file="SetDeviceTrustByDeviceTests.cs" company="Kount Inc">
//     Copyright 2018 Kount Inc. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace KountAccessTest
{
    using KountAccessSdk.Enums;
    using KountAccessSdk.Models;
    using KountAccessSdk.Service;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Test class for SetDeviceTrustByDeviceTests
    /// </summary>
    [TestClass]
    public class SetDeviceTrustByDeviceTests : AccessSDKTestBase
    {
        [TestMethod]
        public void TestDeviceTrustByDevice_ShouldNotThrowException()
        {
            // Arrange
            MockupWebClientFactory mockFactory = new MockupWebClientFactory(this.jsonUniquesInfo);
            AccessSdk sdk = new AccessSdk(accessUrl, merchantId, apiKey, DEFAULT_VERSION, mockFactory);

            // Act
            sdk.SetDeviceTrustByDevice(uniq, "abcdef12345678910abcdef987654321", DeviceTrustState.Trusted);

            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof(AccessException))]
        public void TestDeviceTrustByDeviceWithoutDeviceId_ShouldThrowException()
        {
            // Arrange
            MockupWebClientFactory mockFactory = new MockupWebClientFactory(this.jsonDevicesInfo);
            AccessSdk sdk = new AccessSdk(accessUrl, merchantId, apiKey, DEFAULT_VERSION, mockFactory);

            var emptyDeviceId = "";

            // Act
            sdk.SetDeviceTrustByDevice(uniq, emptyDeviceId, DeviceTrustState.Trusted);

            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof(AccessException))]
        public void TestDeviceTrustByDeviceWithoutUniq_ShouldThrowException()
        {
            // Arrange
            MockupWebClientFactory mockFactory = new MockupWebClientFactory(this.jsonDevicesInfo);
            AccessSdk sdk = new AccessSdk(accessUrl, merchantId, apiKey, DEFAULT_VERSION, mockFactory);

            var emptyUniq = "";

            // Act
            sdk.SetDeviceTrustByDevice(emptyUniq, "abcdef12345678910abcdef987654321", DeviceTrustState.Trusted);

            // Assert
        }
    }
}
