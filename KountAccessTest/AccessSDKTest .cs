//-----------------------------------------------------------------------
// <copyright file="AccessSDKTest.cs" company="Kount Inc">
//     Copyright 2017 Kount Inc. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace KountAccessTest
{
    using KountAccessSdk.Enums;
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

        private const string DEFAULT_VERSION = "0400"; 
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
        private static string uniq = "55e9fbfda2ce489d83b4a99c84c6f3e1";

        private DeviceInfo deviceInfo;
        private DecisionInfo decisionInfo;
        private VelocityInfo velocityInfo;
        private Info info;

        private string jsonDevInfo;
        private string jsonVeloInfo;
        private string jsonDeciInfo;
        private string jsonInfo;

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

            info = new Info();
            info.Device = deviceInfo.Device;
            info.Decision = decisionInfo.Decision;
            info.Velocity = velocityInfo.Velocity;
            info.ResponseId = responseId;
            info.Trusted = new TrustState() { State = DeviceTrustState.Trusted };
            info.BehavioSec = new BehavioSec()
            {
                Confidence = 0,
                IsBot = false,
                IsTrained = false,
                PolicyId = 4,
                Score = 0
            };

            jsonInfo = JsonConvert.SerializeObject(info);
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

        #region Get Info Tests
        [TestMethod]
        public void TestGetInfo()
        {
            // Arrange
            MockupWebClientFactory mockFactory = new MockupWebClientFactory(this.jsonInfo);
            AccessSdk sdk = new AccessSdk(accessUrl, merchantId, apiKey, DEFAULT_VERSION, mockFactory);
            var allDataSets = new DataSetElements()
                    .WithInfo()
                    .WithVelocity()
                    .WithDecision()
                    .WithTrusted()
                    .WithBehavioSec()
                    .Build();

            // Act
            Info infoResp = sdk.GetInfo(session, user, password, uniq, allDataSets);           

            this.logger.Debug(JsonConvert.SerializeObject(info));

            // Asert
            Assert.IsNotNull(info);
            Assert.AreEqual(info.ResponseId, infoResp.ResponseId);

            Assert.AreEqual(info.Device.Id, infoResp.Device.Id);
            Assert.IsTrue(info.Velocity.Password.Equals(infoResp.Velocity.Password));
        }

        [TestMethod]
        [ExpectedException(typeof(AccessException))]
        public void TestGetInfoWithoutSession_ShouldThrowException()
        {
            // Arrange
            MockupWebClientFactory mockFactory = new MockupWebClientFactory(this.jsonInfo);
            AccessSdk sdk = new AccessSdk(accessUrl, merchantId, apiKey, DEFAULT_VERSION, mockFactory);
            var dataSets = new DataSetElements()
                    .WithInfo()
                    .Build();

            var emptySessionId = "";

            // Act
            Info infoResp = sdk.GetInfo(emptySessionId, user, password, uniq, dataSets);

            // Asert
        }

        [TestMethod]
        [DataRow(1)] // WithInfo
        [DataRow(2)] // WithVelocity
        [DataRow(3)] // WithInfo and WithVelocity
        [DataRow(4)] // WithDecision
        [DataRow(5)] // WithInfo and WithDecision
        [DataRow(6)] // WithVelocity and WithDecision
        [DataRow(7)] // WithInfo, WithVelocity and WithDecision
        public void TestGetInfoWithoutUniq_ShouldPassValidation(int dataSets)
        {
            // Arrange
            MockupWebClientFactory mockFactory = new MockupWebClientFactory(this.jsonInfo);
            AccessSdk sdk = new AccessSdk(accessUrl, merchantId, apiKey, DEFAULT_VERSION, mockFactory);
            var emptyUniq = "";

            // Act
            Info infoResp = sdk.GetInfo(session, user, password, emptyUniq, dataSets);

            // Asert
            Assert.IsNotNull(infoResp);
        }

        [TestMethod]
        [DataRow(8)] 
        [DataRow(9)] 
        [DataRow(10)] 
        [DataRow(11)] 
        [DataRow(12)] 
        [DataRow(13)] 
        [DataRow(14)] 
        [DataRow(15)] 
        [DataRow(16)] 
        [DataRow(17)] 
        [DataRow(18)] 
        [DataRow(19)] 
        [DataRow(20)] 
        [DataRow(21)] 
        [DataRow(22)] 
        [DataRow(23)] 
        [DataRow(24)] 
        [DataRow(24)] 
        [DataRow(25)] 
        [DataRow(26)] 
        [DataRow(27)] 
        [DataRow(28)] 
        [DataRow(29)] 
        [DataRow(30)] 
        [DataRow(31)] 
        [ExpectedException(typeof(AccessException))]
        public void TestGetInfoWithoutUniq_ShouldThrowException(int dataSets)
        {
            // Arrange
            MockupWebClientFactory mockFactory = new MockupWebClientFactory(this.jsonInfo);
            AccessSdk sdk = new AccessSdk(accessUrl, merchantId, apiKey, DEFAULT_VERSION, mockFactory);
            var emptyUniq = "";

            // Act
            Info infoResp = sdk.GetInfo(session, user, password, emptyUniq, dataSets);

            // Asert
        }

        [TestMethod]
        [DataRow(1)] // WithInfo
        [DataRow(8)] // WithTrusted
        [DataRow(9)] // WithInfo and WithTrusted
        [DataRow(16)] // WithBehavioSec
        [DataRow(17)] // WithInfo and WithBehavioSec
        [DataRow(24)] // WithTrusted and WithBehavioSec
        [DataRow(25)] // WithInfo, WithTrusted and WithBehavioSec
        public void TestGetInfoWithoutUser_ShouldPassValidation(int dataSets)
        {
            // Arrange
            MockupWebClientFactory mockFactory = new MockupWebClientFactory(this.jsonInfo);
            AccessSdk sdk = new AccessSdk(accessUrl, merchantId, apiKey, DEFAULT_VERSION, mockFactory);
            var emptyUser = "";

            // Act
            Info infoResp = sdk.GetInfo(session, emptyUser, password, uniq, dataSets);

            // Asert
            Assert.IsNotNull(infoResp);
        }

        [TestMethod]
        [DataRow(2)]
        [DataRow(3)]
        [DataRow(4)]
        [DataRow(5)]
        [DataRow(6)]
        [DataRow(7)]
        [DataRow(10)]
        [DataRow(11)]
        [DataRow(12)]
        [DataRow(13)]
        [DataRow(14)]
        [DataRow(15)]
        [DataRow(18)]
        [DataRow(19)]
        [DataRow(20)]
        [DataRow(21)]
        [DataRow(22)]
        [DataRow(23)] 
        [DataRow(26)]
        [DataRow(27)]
        [DataRow(28)]
        [DataRow(29)]
        [DataRow(30)]
        [DataRow(31)]
        [ExpectedException(typeof(AccessException))]
        public void TestGetInfoWithoutUser_ShouldThrowException(int dataSets)
        {
            // Arrange
            MockupWebClientFactory mockFactory = new MockupWebClientFactory(this.jsonInfo);
            AccessSdk sdk = new AccessSdk(accessUrl, merchantId, apiKey, DEFAULT_VERSION, mockFactory);
            var emptyUser = "";

            // Act
            Info infoResp = sdk.GetInfo(session, emptyUser, password, uniq, dataSets);

            // Asert
        }

        [TestMethod]
        [DataRow(1)] // WithInfo
        [DataRow(8)] // WithTrusted
        [DataRow(9)] // WithInfo and WithTrusted
        [DataRow(16)] // WithBehavioSec
        [DataRow(17)] // WithInfo and WithBehavioSec
        [DataRow(24)] // WithTrusted and WithBehavioSec
        [DataRow(25)] // WithInfo, WithTrusted and WithBehavioSec
        public void TestGetInfoWithoutPassword_ShouldPassValidation(int dataSets)
        {
            // Arrange
            MockupWebClientFactory mockFactory = new MockupWebClientFactory(this.jsonInfo);
            AccessSdk sdk = new AccessSdk(accessUrl, merchantId, apiKey, DEFAULT_VERSION, mockFactory);
            var emptyPassword = "";

            // Act
            Info infoResp = sdk.GetInfo(session, user, emptyPassword, uniq, dataSets);

            // Asert
            Assert.IsNotNull(infoResp);
        }

        [TestMethod]
        [DataRow(2)] 
        [DataRow(3)] 
        [DataRow(4)] 
        [DataRow(5)] 
        [DataRow(6)] 
        [DataRow(7)] 
        [DataRow(10)]
        [DataRow(11)]
        [DataRow(12)]
        [DataRow(13)]
        [DataRow(14)]
        [DataRow(15)]
        [DataRow(18)]
        [DataRow(19)]
        [DataRow(20)]
        [DataRow(21)]
        [DataRow(22)]
        [DataRow(23)]
        [DataRow(26)]
        [DataRow(27)]
        [DataRow(28)]
        [DataRow(29)]
        [DataRow(30)]
        [DataRow(31)]
        [ExpectedException(typeof(AccessException))]
        public void TestGetInfoWithoutPassword_ShouldThrowException(int dataSets)
        {
            // Arrange
            MockupWebClientFactory mockFactory = new MockupWebClientFactory(this.jsonInfo);
            AccessSdk sdk = new AccessSdk(accessUrl, merchantId, apiKey, DEFAULT_VERSION, mockFactory);
            var emptyPassword = "";

            // Act
            Info infoResp = sdk.GetInfo(session, user, emptyPassword, uniq, dataSets);

            // Asert
        }
        #endregion
    }
}