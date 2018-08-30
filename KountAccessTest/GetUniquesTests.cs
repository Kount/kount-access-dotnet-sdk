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
    using NUnit.Framework;
    using NUnit.Framework.Constraints;

    /// <summary>
    /// Test class for GetUniquesTests
    /// </summary>
    public class GetUniquesTests : AccessSDKTestBase
    {
        [Test]
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

        [Test]
        public void TestGetUniquesWithoutDeviceId_ShouldThrowException()
        {
            // Arrange
            MockupWebClientFactory mockFactory = new MockupWebClientFactory(this.jsonDevicesInfo);
            AccessSdk sdk = new AccessSdk(accessUrl, merchantId, apiKey, DEFAULT_VERSION, mockFactory);

            var emptyDeviceId = "";

            // Act
            ActualValueDelegate<object> testDelegate = () => sdk.GetUniques(emptyDeviceId);

            // Assert
            Assert.That(testDelegate, Throws.TypeOf<AccessException>());
        }
    }
}
