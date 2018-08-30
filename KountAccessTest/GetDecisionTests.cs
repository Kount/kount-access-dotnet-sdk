//-----------------------------------------------------------------------
// <copyright file="GetDecisionTests.cs" company="Kount Inc">
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
    /// Test class for GetDecisionTests
    /// </summary>
    public class GetDecisionTests : AccessSDKTestBase
    {
        [Test]
        public void TestGetDecision()
        {
            try
            {
                MockupWebClientFactory mockFactory = new MockupWebClientFactory(this.jsonDeciInfo);

                AccessSdk sdk = new AccessSdk(accessUrl, merchantId, apiKey, DEFAULT_VERSION, mockFactory);

                DecisionInfo decisionInfo = sdk.GetDecision(session, user, password);

                Assert.IsNotNull(decisionInfo);

                this.logger.Debug(JsonConvert.SerializeObject(decisionInfo));


                Assert.AreEqual(decision, decisionInfo.Decision.Reply.RuleEvents.Decision);

                Assert.AreEqual(deviceInfo.Device.Id, decisionInfo.Device.Id);
                Assert.IsTrue(velocityInfo.Velocity.Password.Equals(decisionInfo.Velocity.Password));
            }
            catch (AccessException ae)
            {
                Assert.Fail($"Bad exception {ae.ErrorType}:{ae.Message}");
            }
        }
    }
}
