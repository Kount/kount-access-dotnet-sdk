//-----------------------------------------------------------------------
// <copyright file="SetDeviceTrustBySessionTests.cs" company="Kount Inc">
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
    /// Test class for SetDeviceTrustBySessionTests
    /// </summary>
    public class SetDeviceTrustBySessionTests : AccessSDKTestBase
    {
        [Test]
        public void TestDeviceTrustBySession_ShouldNotThrowException()
        {
            // Arrange
            MockupWebClientFactory mockFactory = new MockupWebClientFactory(this.jsonUniquesInfo);
            AccessSdk sdk = new AccessSdk(accessUrl, merchantId, apiKey, DEFAULT_VERSION, mockFactory);

            // Act
            sdk.SetDeviceTrustBySession(session, uniq, DeviceTrustState.Trusted);

            // Assert
        }

        [Test]
        public void TestDeviceTrustBySessionWithoutSessionId_ShouldThrowException()
        {
            // Arrange
            MockupWebClientFactory mockFactory = new MockupWebClientFactory(this.jsonDevicesInfo);
            AccessSdk sdk = new AccessSdk(accessUrl, merchantId, apiKey, DEFAULT_VERSION, mockFactory);

            var emptySession = "";

            // Act
            // Assert
            Assert.That(() => sdk.SetDeviceTrustBySession(emptySession, uniq, DeviceTrustState.Trusted), Throws.TypeOf<AccessException>());
        }

        [Test]
        public void TestDeviceTrustBySessionWithoutUniq_ShouldThrowException()
        {
            // Arrange
            MockupWebClientFactory mockFactory = new MockupWebClientFactory(this.jsonDevicesInfo);
            AccessSdk sdk = new AccessSdk(accessUrl, merchantId, apiKey, DEFAULT_VERSION, mockFactory);

            var emptyUniq = "";

            // Act
            // Assert
            Assert.That(() => sdk.SetDeviceTrustBySession(session, emptyUniq, DeviceTrustState.Trusted), Throws.TypeOf<AccessException>());
        }
    }
}
