//-----------------------------------------------------------------------
// <copyright file="SetDeviceTrustBySessionTests.cs" company="Kount Inc">
//     Copyright 2018 Kount Inc. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using KountAccessSdk.Enums;

namespace KountAccessTest
{
    using KountAccessSdk.Models;
    using KountAccessSdk.Service;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Test class for SetDeviceTrustBySessionTests
    /// </summary>
    [TestClass]
    public class SetDeviceTrustBySessionTests : AccessSDKTestBase
    {
        [TestMethod]
        public void TestDeviceTrustBySession_ShouldNotThrowException()
        {
            // Arrange
            MockupWebClientFactory mockFactory = new MockupWebClientFactory(this.jsonUniquesInfo);
            AccessSdk sdk = new AccessSdk(accessUrl, merchantId, apiKey, DEFAULT_VERSION, mockFactory);

            // Act
            sdk.SetDeviceTrustBySession(session, uniq, DeviceTrustState.Trusted);

            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof(AccessException))]
        public void TestDeviceTrustBySessionWithoutSessionId_ShouldThrowException()
        {
            // Arrange
            MockupWebClientFactory mockFactory = new MockupWebClientFactory(this.jsonDevicesInfo);
            AccessSdk sdk = new AccessSdk(accessUrl, merchantId, apiKey, DEFAULT_VERSION, mockFactory);

            var emptySession = "";

            // Act
            sdk.SetDeviceTrustBySession(emptySession, uniq, DeviceTrustState.Trusted);

            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof(AccessException))]
        public void TestDeviceTrustBySessionWithoutUniq_ShouldThrowException()
        {
            // Arrange
            MockupWebClientFactory mockFactory = new MockupWebClientFactory(this.jsonDevicesInfo);
            AccessSdk sdk = new AccessSdk(accessUrl, merchantId, apiKey, DEFAULT_VERSION, mockFactory);

            var emptyUniq = "";

            // Act
            sdk.SetDeviceTrustBySession(session, emptyUniq, DeviceTrustState.Trusted);

            // Assert
        }
    }
}
