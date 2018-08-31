//-----------------------------------------------------------------------
// <copyright file="GetVelocityTests.cs" company="Kount Inc">
//     Copyright 2018 Kount Inc. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace KountAccessTest
{
    using KountAccessSdk.Models;
    using KountAccessSdk.Service;
    using NUnit.Framework;

    /// <summary>
    /// Test class for GetVelocityTests
    /// </summary>
    public class AccessSDKConstructorTests : AccessSDKTestBase
    {
        [Test]
        public void TestConstructorAccessSDKHappyPath()
        {
            // Create the SDK.  If any of these values are invalid, an
            // AccessException will be thrown along with a
            // message detailing why.
            try
            {
                AccessSdk sdk = new AccessSdk(accessUrl, merchantId, apiKey);

                Assert.IsNotNull(sdk);
            }
            catch (AccessException ae)
            {

                Assert.Fail($"Bad exception {ae.ErrorType}:{ae.Message}");
            }
        }

        [Test]
        public void TestConstructorAccessSDKMissingApiKey()
        {
            try
            {
                AccessSdk sdk = new AccessSdk(host, merchantId, null);
                Assert.Fail("Should have failed apiKey");
            }
            catch (AccessException ae)
            {
                Assert.AreEqual(AccessErrorType.INVALID_DATA, ae.ErrorType);
            }
        }

        [Test]
        public void TestConstructorAccessSDKMissingHost()
        {
            try
            {
                AccessSdk sdk = new AccessSdk(null, merchantId, apiKey);
                Assert.Fail("Should have failed host");
            }
            catch (AccessException ae)
            {
                Assert.AreEqual(AccessErrorType.INVALID_DATA, ae.ErrorType);
            }
        }

        [Test]
        public void TestConstructorAccessSDKBadMerchant()
        {
            try
            {
                AccessSdk sdk = new AccessSdk(host, -1, apiKey);
                Assert.Fail("Should have failed merchantId");
            }
            catch (AccessException ae)
            {
                Assert.AreEqual(AccessErrorType.INVALID_DATA, ae.ErrorType);
            }
        }

        [Test]
        public void TestConstructorAccessSDKBlankApiKey()
        {
            try
            {
                AccessSdk sdk = new AccessSdk(host, merchantId, "    ");
                Assert.Fail("Should have failed apiKey");
            }
            catch (AccessException ae)
            {
                Assert.AreEqual(AccessErrorType.INVALID_DATA, ae.ErrorType);
            }
        }
    }
}
