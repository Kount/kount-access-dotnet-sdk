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
    using NUnit.Framework;

    /// <summary>
    /// Test class for SetDeviceTrustByDeviceTests
    /// </summary>
    public class SetDeviceTrustByDeviceTests : AccessSDKTestBase
    {
        [Test]
        public void TestDeviceTrustByDevice_ShouldNotThrowException()
        {
            // Arrange
            MockupWebClientFactory mockFactory = new MockupWebClientFactory(this.jsonUniquesInfo);
            AccessSdk sdk = new AccessSdk(accessUrl, merchantId, apiKey, DEFAULT_VERSION, mockFactory);

            // Act
            sdk.SetDeviceTrustByDevice(uniq, "abcdef12345678910abcdef987654321", DeviceTrustState.Trusted);

            // Assert
        }

        [Test]
        public void TestDeviceTrustByDeviceWithoutDeviceId_ShouldThrowException()
        {
            // Arrange
            MockupWebClientFactory mockFactory = new MockupWebClientFactory(this.jsonDevicesInfo);
            AccessSdk sdk = new AccessSdk(accessUrl, merchantId, apiKey, DEFAULT_VERSION, mockFactory);

            var emptyDeviceId = "";

            // Act
            // Assert
            Assert.That(() => sdk.SetDeviceTrustByDevice(uniq, emptyDeviceId, DeviceTrustState.Trusted), Throws.TypeOf<AccessException>());
        }

        [Test]
        public void TestDeviceTrustByDeviceWithoutUniq_ShouldThrowException()
        {
            // Arrange
            MockupWebClientFactory mockFactory = new MockupWebClientFactory(this.jsonDevicesInfo);
            AccessSdk sdk = new AccessSdk(accessUrl, merchantId, apiKey, DEFAULT_VERSION, mockFactory);

            var emptyUniq = "";

            // Act
            // Assert
            Assert.That(() => sdk.SetDeviceTrustByDevice(emptyUniq, "abcdef12345678910abcdef987654321", DeviceTrustState.Trusted), Throws.TypeOf<AccessException>());
        }
    }
}
