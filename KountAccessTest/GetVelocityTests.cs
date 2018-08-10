//-----------------------------------------------------------------------
// <copyright file="GetVelocityTests.cs" company="Kount Inc">
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
    /// Test class for GetVelocityTests
    /// </summary>
    public class GetVelocityTests : AccessSDKTestBase
    {
        [Test]
        public void TestGetVelocity()
        {
            try
            {
                MockupWebClientFactory mockFactory = new MockupWebClientFactory(this.jsonVeloInfo);

                AccessSdk sdk = new AccessSdk(accessUrl, merchantId, apiKey, DEFAULT_VERSION, mockFactory);

                VelocityInfo vInfo = sdk.GetVelocity(session, user, password);

                Assert.IsNotNull(vInfo);

                this.logger.Debug(JsonConvert.SerializeObject(vInfo));

                Assert.IsTrue(velocityInfo.Velocity.Password.Equals(vInfo.Velocity.Password));
                Assert.AreEqual(vInfo.ResponseId, responseId);


            }
            catch (AccessException ae)
            {

                Assert.Fail($"Bad exception {ae.ErrorType}:{ae.Message}");
            }

        }

        [Test]
        public void TestGetVelocityWebExceptionWithResponse()
        {
            try
            {
                MockupWebClientFactory mockFactory = new MockupWebClientFactory(this.jsonVeloInfo);

                AccessSdk sdk = new AccessSdk("gty://bad.host.com", merchantId, apiKey, DEFAULT_VERSION, mockFactory);

                VelocityInfo vInfo = sdk.GetVelocity(session, user, password);

                Assert.Fail($"AccessException Not thrown");


            }
            catch (AccessException ae)
            {

                Assert.AreEqual(ae.ErrorType, AccessErrorType.NETWORK_ERROR);
                Assert.IsTrue("BAD RESPONSE(OK):OK. UNKNOWN NETWORK ISSUE.".Equals(ae.Message.Trim()));
            }
        }
    }
}
