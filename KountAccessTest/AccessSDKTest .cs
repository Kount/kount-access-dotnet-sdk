//-----------------------------------------------------------------------
// <copyright file="AccessSDKTest.cs" company="Kount Inc">
//     Copyright 2017 Kount Inc. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace KountAccessTest
{
    using KountAccessSdk.Interfaces;
    using KountAccessSdk.Log.Factory;
    using KountAccessSdk.Models;
    using KountAccessSdk.Service;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Test class for AccessSDK
    /// </summary>
    [TestClass]
    public class AccessSDKTest
    {
        /// <summary>
        /// The Logger to use.
        /// </summary>
        private ILogger logger;

        private const string DEFAULT_VERSION = "0210"; 
        // Setup data for comparisons.
        private static int merchantId = 999999;

        private static string host = merchantId.ToString() + ".kountaccess.com";
        private static string accessUrl = "https://" + host + "/access";
        private static string session = "askhjdaskdgjhagkjhasg47862345shg";
        private static string sessionUrl = "https://" + host + "/api/session=" + session;
        private static string user = "greg@test.com";
        private static string password = "password";
        private static string apiKey = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiIxMDAxMDAiLCJhdWQiOiJLb3VudC4wIiwiaWF0IjoxNDI0OTg5NjExLCJzY3AiOnsia2MiOm51bGwsImFwaSI6ZmFsc2UsInJpcyI6ZmFsc2V9fQ.S7kazxKVgDCrNxjuieg5ChtXAiuSO2LabG4gzDrh1x8";
        private static string fingerprint = "75012bd5e5b264c4b324f5c95a769541";
        private static string ipAddress = "64.128.91.251";
        private static string ipGeo = "US";
        private static string responseId = "bf10cd20cf61286669e87342d029e405";
        private static string decision = "A";

        private DeviceInfo deviceInfo;
        private DecisionInfo decisionInfo;
        private VelocityInfo velocityInfo;

        private string jsonDevInfo;
        private string jsonVeloInfo;
        private string jsonDeciInfo;

        [TestInitialize]
        public void TestSdkInit()
        {
            ILoggerFactory factory = LogFactory.GetLoggerFactory();
            this.logger = factory.GetLogger(typeof(AccessSDKTest).ToString());

            deviceInfo = new DeviceInfo();
            deviceInfo.Device = new Device { Country = ipGeo, Region = "ID", GeoLat = 43.37, GeoLong = -116.200, Id = fingerprint, IpAddress = ipAddress, IpGeo = ipGeo, Mobile = 1, Proxy = 0 };
            deviceInfo.ResponseId = responseId;
            jsonDevInfo = JsonConvert.SerializeObject(deviceInfo);

            velocityInfo = new VelocityInfo();
            velocityInfo.Device = deviceInfo.Device;
            velocityInfo.ResponseId = responseId;
            velocityInfo.Velocity = new Velocity();
            velocityInfo.Velocity.Account = new SubAccount { dlh = 1, dlm = 1, iplh = 1, iplm = 1, plh = 1, plm = 1, ulh = 1, ulm = 1 };
            velocityInfo.Velocity.Device = new SubDevice { alh = 1, alm = 1, iplh = 3, iplm = 3, plh = 2, plm = 2, ulh = 1, ulm = 1 };
            velocityInfo.Velocity.IpAddress = new SubAddress { ulm = 3, ulh = 3, plm = 3, plh = 3, alh = 2, alm = 2, dlh = 1, dlm = 1 };
            velocityInfo.Velocity.Password = new SubPassword { dlm = 2, dlh = 2, alm = 2, alh = 2, iplh = 1, iplm = 1, ulh = 3, ulm = 3 };
            velocityInfo.Velocity.User = new SubUser { iplm = 3, iplh = 3, alh = 2, alm = 2, dlh = 2, dlm = 2, plh = 1, plm = 1 };
            jsonVeloInfo = JsonConvert.SerializeObject(velocityInfo);

            decisionInfo = new DecisionInfo();
            decisionInfo.Device = deviceInfo.Device;
            decisionInfo.ResponseId = responseId;
            decisionInfo.Velocity = velocityInfo.Velocity;
            decisionInfo.Decision = new Decision
            {
                Errors = new List<string> { "E1", "E2" },
                Warnings = new List<string> { "W1", "W2" },
                Reply = new Reply
                {
                    RuleEvents = new RuleEvents
                    {
                        Decision = decision,
                        Total = 10,
                        Events = new List<string> { "Event 1", "Event 2" }
                    }
                }
            };

            jsonDeciInfo = JsonConvert.SerializeObject(decisionInfo);
        }

        [TestMethod]
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

        [TestMethod]
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

        [TestMethod]
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

        [TestMethod]
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

        [TestMethod]
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

        [TestMethod]
        public void TestHashValue()
        {
            try
            {
                var hash = AccessSdk.HashValue("admin");
                Assert.AreEqual("8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918", hash);

                var pass = AccessSdk.HashValue("password");
                Assert.AreEqual("5e884898da28047151d0e56f8dc6292773603d0d6aabbdd62a11ef721d1542d8", pass);

            }
            catch (AccessException ae)
            {

                Assert.Fail($"Bad exception {ae.ErrorType}:{ae.Message}");
            }
        }

        [TestMethod]
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

        [TestMethod]
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
                Console.WriteLine(ae.Message);
                Assert.AreEqual(ae.ErrorType, AccessErrorType.NETWORK_ERROR);
            }

        }


        [TestMethod]
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

        [TestMethod]
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

        [TestMethod]
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