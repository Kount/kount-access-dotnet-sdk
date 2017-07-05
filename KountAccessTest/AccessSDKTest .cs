namespace KountAccessTest
{
    using KountAccessSdk.Models;
    using KountAccessSdk.Service;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Newtonsoft.Json;
    using System.Collections.Generic;

    [TestClass]
    public class AccessSDKTest
    {
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

                Assert.Fail($"Bad exception {ae.Error}:{ae.Message}");
            }
        }

        [TestInitialize]
        public void TestSdkInit()
        {
            deviceInfo = new DeviceInfo();
            deviceInfo.Device = new Device { Country = ipGeo, Region = "ID", GeoLat = 43.37, GeoLong = -116.200, Id = fingerprint, IpAddress = ipAddress, IpGeo = ipGeo, Mobile = 0, Proxy = 0 };
            deviceInfo.ResponceId = responseId;
            jsonDevInfo = JsonConvert.SerializeObject(deviceInfo);

            velocityInfo = new VelocityInfo();
            velocityInfo.Device = deviceInfo.Device;
            velocityInfo.ResponceId = responseId;
            velocityInfo.Velocity = new Velocity();
            velocityInfo.Velocity.Account = new SubAccount { dlh = 1, dlm = 1, iplh = 1, iplm = 1, plh = 1, plm = 1, ulh = 1, ulm = 1 };
            velocityInfo.Velocity.Device = new SubDevice { alh = 1, alm = 1, iplh = 3, iplm = 3, plh = 2, plm = 2, ulh = 1, ulm = 1 };
            velocityInfo.Velocity.IpAddress = new SubAddress { ulm = 3, ulh = 3, plm = 3, plh = 3, alh = 2, alm = 2, dlh = 1, dlm = 1 };
            velocityInfo.Velocity.Password = new SubPassword { dlm = 2, dlh = 2, alm = 2, alh = 2, iplh = 1, iplm = 1, ulh = 3, ulm = 3 };
            velocityInfo.Velocity.User = new SubUser { iplm = 3, iplh = 3, alh = 2, alm = 2, dlh = 2, dlm = 2, plh = 1, plm = 1 };
            jsonVeloInfo = JsonConvert.SerializeObject(velocityInfo);

            decisionInfo = new DecisionInfo();
            decisionInfo.Device = deviceInfo.Device;
            decisionInfo.ResponceId = responseId;
            decisionInfo.Velocity = velocityInfo.Velocity;
            decisionInfo.Decision = new Decision
            {
                Errors = new List<string> { "E1", "E2" },
                Warnings = new List<string> { "W1", "W2" },
                Reply = new Reply {
                    RuleEvents = new RuleEvents {
                        Decision = decision,
                        Total = 10,
                        Events = new List<string> { "Event 1", "Event 2" } } }
            };

            jsonDeciInfo = JsonConvert.SerializeObject(decisionInfo);
        }
    }
}