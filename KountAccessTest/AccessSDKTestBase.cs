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
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System;
    using NUnit.Framework;

    /// <summary>
    /// Test class for AccessSDK
    /// </summary>
    [TestFixture]
    public class AccessSDKTestBase
    {
        /// <summary>
        /// The Logger to use.
        /// </summary>
        protected ILogger logger;

        protected const string DEFAULT_VERSION = "0400";
        // Setup data for comparisons.
        protected static int merchantId = 999999;
        
        protected static string host = merchantId.ToString() + ".kountaccess.com";
        protected static string accessUrl = "https://" + host + "/access";
        protected static string session = "askhjdaskdgjhagkjhasg47862345shg";
        protected static string sessionUrl = "https://" + host + "/api/session=" + session;
        protected static string user = "greg@test.com";
        protected static string password = "password";
        protected static string apiKey = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiIxMDAxMDAiLCJhdWQiOiJLb3VudC4wIiwiaWF0IjoxNDI0OTg5NjExLCJzY3AiOnsia2MiOm51bGwsImFwaSI6ZmFsc2UsInJpcyI6ZmFsc2V9fQ.S7kazxKVgDCrNxjuieg5ChtXAiuSO2LabG4gzDrh1x8";
        protected static string fingerprint = "75012bd5e5b264c4b324f5c95a769541";
        protected static string ipAddress = "64.128.91.251";
        protected static string ipGeo = "US";
        protected static string responseId = "bf10cd20cf61286669e87342d029e405";
        protected static string decision = "A";
        protected static string uniq = "55e9fbfda2ce489d83b4a99c84c6f3e1";

        protected DeviceInfo deviceInfo;
        protected DecisionInfo decisionInfo;
        protected VelocityInfo velocityInfo;
        protected Info info;
        protected DevicesInfo devicesInfo;
        protected UniquesInfo uniquesInfo;

        protected string jsonDevInfo;
        protected string jsonVeloInfo;
        protected string jsonDeciInfo;
        protected string jsonInfo;
        protected string jsonDevicesInfo;
        protected string jsonUniquesInfo;

        [SetUp]
        public void TestSdkInit()
        {
            ILoggerFactory factory = LogFactory.GetLoggerFactory();
            this.logger = factory.GetLogger(typeof(AccessSDKTestBase).ToString());

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

            devicesInfo = new DevicesInfo();
            devicesInfo.ResponseId = responseId;
            devicesInfo.Devices = new List<DeviceBasicInfo>()
            {
                new DeviceBasicInfo() { DeviceId = "54569fcbd187483a8a1570a3c67d1113", FriendlyName = "Device A", TrustState = DeviceTrustState.Trusted, DateFirstSeen = DateTime.UtcNow.AddHours(-1) },
                new DeviceBasicInfo() { DeviceId = "abcdef12345678910abcdef987654321", FriendlyName = "Device B", TrustState = DeviceTrustState.Banned, DateFirstSeen = DateTime.UtcNow.AddHours(-2) }
            };

            jsonDevicesInfo = JsonConvert.SerializeObject(devicesInfo);

            uniquesInfo = new UniquesInfo();
            uniquesInfo.ResponseId = responseId;
            uniquesInfo.Uniques = new List<Unique>()
            {
                new Unique() { UniqueId = "55e9fbfda2ce489d83b4a99c84c6f3e1", DateLastSeen = DateTime.UtcNow.AddHours(-1), TrustState = DeviceTrustState.Trusted  },
                new Unique() { UniqueId = "55e9fbfda2ce489d83b4a99c84c6f3e2", DateLastSeen = DateTime.UtcNow.AddHours(-2), TrustState = DeviceTrustState.Banned  }
            };

            jsonUniquesInfo = JsonConvert.SerializeObject(uniquesInfo);
        }
    }
}