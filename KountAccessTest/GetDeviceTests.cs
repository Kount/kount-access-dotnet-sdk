//-----------------------------------------------------------------------
// <copyright file="GetDeviceTests.cs" company="Kount Inc">
//     Copyright 2018 Kount Inc. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace KountAccessTest
{
    using KountAccessSdk.Models;
    using KountAccessSdk.Service;
    using Newtonsoft.Json;
    using NUnit.Framework;

    /// <summary>
    /// Test class for GetDeviceTests
    /// </summary>
    public class GetDeviceTests : AccessSDKTestBase
    {
        [Test]
        public void TestGetDevice()
        {
            try
            {
                MockupWebClientFactory mockFactory = new MockupWebClientFactory(this.jsonDevInfo);

                AccessSdk sdk = new AccessSdk(accessUrl, merchantId, apiKey, DEFAULT_VERSION, mockFactory);

                DeviceInfo dInfo = sdk.GetDevice(session);

                Assert.IsNotNull(dInfo);
                this.logger.Debug(JsonConvert.SerializeObject(dInfo));

                Assert.AreEqual(fingerprint, dInfo.Device.Id);
                Assert.AreEqual(ipAddress, dInfo.Device.IpAddress);
                Assert.AreEqual(ipGeo, dInfo.Device.IpGeo);
                Assert.AreEqual(1, dInfo.Device.Mobile);
                Assert.AreEqual(0, dInfo.Device.Proxy);
                Assert.AreEqual(responseId, dInfo.ResponseId);

            }
            catch (AccessException ae)
            {

                Assert.Fail($"Bad exception {ae.ErrorType}:{ae.Message}");
            }

        }

        [Test]
        public void TestGetDeviceConnectionClosed()
        {
            try
            {
                MockupWebClientFactory mockFactory = new MockupWebClientFactory(this.jsonDevInfo);

                AccessSdk sdk = new AccessSdk("gty://bad.host.com", merchantId, apiKey, DEFAULT_VERSION, mockFactory);

                DeviceInfo dInfo = sdk.GetDevice(session);

                Assert.Fail($"AccessException Not thrown");

            }
            catch (AccessException ae)
            {
                Assert.AreEqual(ae.ErrorType, AccessErrorType.NETWORK_ERROR);
            }
        }
    }
}
