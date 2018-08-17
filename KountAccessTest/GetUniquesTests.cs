//-----------------------------------------------------------------------
// <copyright file="GetUniquesTests.cs" company="Kount Inc">
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
    /// Test class for GetUniquesTests
    /// </summary>
    [TestClass]
    public class GetUniquesTests : AccessSDKTestBase
    {
        [TestMethod]
        public void TestGetUniques()
        {
            // Arrange
            MockupWebClientFactory mockFactory = new MockupWebClientFactory(this.jsonUniquesInfo);
            AccessSdk sdk = new AccessSdk(accessUrl, merchantId, apiKey, DEFAULT_VERSION, mockFactory);

            // Act
            UniquesInfo uniquesResp = sdk.GetUniques("54569fcbd187483a8a1570a3c67d1113");

            this.logger.Debug(JsonConvert.SerializeObject(uniquesResp));

            // Assert
            Assert.IsNotNull(uniquesResp);
            Assert.AreEqual(this.uniquesInfo.ResponseId, uniquesResp.ResponseId);

            Assert.AreEqual(this.uniquesInfo.Uniques.Count, uniquesResp.Uniques.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(AccessException))]
        public void TestGetUniquesWithoutDeviceId_ShouldThrowException()
        {
            // Arrange
            MockupWebClientFactory mockFactory = new MockupWebClientFactory(this.jsonDevicesInfo);
            AccessSdk sdk = new AccessSdk(accessUrl, merchantId, apiKey, DEFAULT_VERSION, mockFactory);

            var emptyDeviceId = "";

            // Act
            UniquesInfo infoResp = sdk.GetUniques(emptyDeviceId);

            // Assert
        }
    }
}
