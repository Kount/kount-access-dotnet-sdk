//-----------------------------------------------------------------------
// <copyright file="GetInfoTests.cs" company="Kount Inc">
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
    /// Test class for GetInfoTests
    /// </summary>
    public class GetInfoTests : AccessSDKTestBase
    {
        #region Get Info Tests
        [Test]
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
                    .WithBehavioSec();

            // Act
            Info infoResp = sdk.GetInfo(session, user, password, uniq, allDataSets);

            this.logger.Debug(JsonConvert.SerializeObject(info));

            // Assert
            Assert.IsNotNull(info);
            Assert.AreEqual(info.ResponseId, infoResp.ResponseId);

            Assert.AreEqual(info.Device.Id, infoResp.Device.Id);
            Assert.IsTrue(info.Velocity.Password.Equals(infoResp.Velocity.Password));
        }

        [Test]
        public void TestGetInfoWithoutSession_ShouldThrowException()
        {
            // Arrange
            MockupWebClientFactory mockFactory = new MockupWebClientFactory(this.jsonInfo);
            AccessSdk sdk = new AccessSdk(accessUrl, merchantId, apiKey, DEFAULT_VERSION, mockFactory);
            var dataSets = new DataSetElements()
                    .WithInfo();

            var emptySessionId = "";

            // Act
            ActualValueDelegate<object> testDelegate = () => sdk.GetInfo(emptySessionId, user, password, uniq, dataSets);

            // Assert
            Assert.That(testDelegate, Throws.TypeOf<AccessException>());
        }

        [TestCase(1)] // WithInfo
        [TestCase(2)] // WithVelocity
        [TestCase(3)] // WithInfo and WithVelocity
        [TestCase(4)] // WithDecision
        [TestCase(5)] // WithInfo and WithDecision
        [TestCase(6)] // WithVelocity and WithDecision
        [TestCase(7)] // WithInfo, WithVelocity and WithDecision
        public void TestGetInfoWithoutUniq_ShouldPassValidation(int dataSetNumber)
        {
            DataSetElements dataSets = GetDataSetElementsFromExpectedValueAfterBuild(dataSetNumber);
            // Arrange
            MockupWebClientFactory mockFactory = new MockupWebClientFactory(this.jsonInfo);
            AccessSdk sdk = new AccessSdk(accessUrl, merchantId, apiKey, DEFAULT_VERSION, mockFactory);
            var emptyUniq = "";

            // Act
            Info infoResp = sdk.GetInfo(session, user, password, emptyUniq, dataSets);

            // Assert
            Assert.IsNotNull(infoResp);
        }

        [TestCase(8)]
        [TestCase(9)]
        [TestCase(10)]
        [TestCase(11)]
        [TestCase(12)]
        [TestCase(13)]
        [TestCase(14)]
        [TestCase(15)]
        [TestCase(16)]
        [TestCase(17)]
        [TestCase(18)]
        [TestCase(19)]
        [TestCase(20)]
        [TestCase(21)]
        [TestCase(22)]
        [TestCase(23)]
        [TestCase(24)]
        [TestCase(24)]
        [TestCase(25)]
        [TestCase(26)]
        [TestCase(27)]
        [TestCase(28)]
        [TestCase(29)]
        [TestCase(30)]
        [TestCase(31)]
        public void TestGetInfoWithoutUniq_ShouldThrowException(int dataSetNumber)
        {
            DataSetElements dataSets = GetDataSetElementsFromExpectedValueAfterBuild(dataSetNumber);
            // Arrange
            MockupWebClientFactory mockFactory = new MockupWebClientFactory(this.jsonInfo);
            AccessSdk sdk = new AccessSdk(accessUrl, merchantId, apiKey, DEFAULT_VERSION, mockFactory);
            var emptyUniq = "";

            // Act
            ActualValueDelegate<object> testDelegate = () => sdk.GetInfo(session, user, password, emptyUniq, dataSets);

            // Assert
            Assert.That(testDelegate, Throws.TypeOf<AccessException>());
        }

        [TestCase(1)] // WithInfo
        [TestCase(8)] // WithTrusted
        [TestCase(9)] // WithInfo and WithTrusted
        [TestCase(16)] // WithBehavioSec
        [TestCase(17)] // WithInfo and WithBehavioSec
        [TestCase(24)] // WithTrusted and WithBehavioSec
        [TestCase(25)] // WithInfo, WithTrusted and WithBehavioSec
        public void TestGetInfoWithoutUser_ShouldPassValidation(int dataSetNumber)
        {
            DataSetElements dataSets = GetDataSetElementsFromExpectedValueAfterBuild(dataSetNumber);
            // Arrange
            MockupWebClientFactory mockFactory = new MockupWebClientFactory(this.jsonInfo);
            AccessSdk sdk = new AccessSdk(accessUrl, merchantId, apiKey, DEFAULT_VERSION, mockFactory);
            var emptyUser = "";

            // Act
            Info infoResp = sdk.GetInfo(session, emptyUser, password, uniq, dataSets);

            // Assert
            Assert.IsNotNull(infoResp);
        }

        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        [TestCase(7)]
        [TestCase(10)]
        [TestCase(11)]
        [TestCase(12)]
        [TestCase(13)]
        [TestCase(14)]
        [TestCase(15)]
        [TestCase(18)]
        [TestCase(19)]
        [TestCase(20)]
        [TestCase(21)]
        [TestCase(22)]
        [TestCase(23)]
        [TestCase(26)]
        [TestCase(27)]
        [TestCase(28)]
        [TestCase(29)]
        [TestCase(30)]
        [TestCase(31)]
        public void TestGetInfoWithoutUser_ShouldThrowException(int dataSetNumber)
        {
            DataSetElements dataSets = GetDataSetElementsFromExpectedValueAfterBuild(dataSetNumber);
            // Arrange
            MockupWebClientFactory mockFactory = new MockupWebClientFactory(this.jsonInfo);
            AccessSdk sdk = new AccessSdk(accessUrl, merchantId, apiKey, DEFAULT_VERSION, mockFactory);
            var emptyUser = "";

            // Act
            ActualValueDelegate<object> testDelegate = () => sdk.GetInfo(session, emptyUser, password, uniq, dataSets);

            // Assert
            Assert.That(testDelegate, Throws.TypeOf<AccessException>());
        }

        [TestCase(1)] // WithInfo
        [TestCase(8)] // WithTrusted
        [TestCase(9)] // WithInfo and WithTrusted
        [TestCase(16)] // WithBehavioSec
        [TestCase(17)] // WithInfo and WithBehavioSec
        [TestCase(24)] // WithTrusted and WithBehavioSec
        [TestCase(25)] // WithInfo, WithTrusted and WithBehavioSec
        public void TestGetInfoWithoutPassword_ShouldPassValidation(int dataSetNumber)
        {
            DataSetElements dataSets = GetDataSetElementsFromExpectedValueAfterBuild(dataSetNumber);
            // Arrange
            MockupWebClientFactory mockFactory = new MockupWebClientFactory(this.jsonInfo);
            AccessSdk sdk = new AccessSdk(accessUrl, merchantId, apiKey, DEFAULT_VERSION, mockFactory);
            var emptyPassword = "";

            // Act
            Info infoResp = sdk.GetInfo(session, user, emptyPassword, uniq, dataSets);

            // Assert
            Assert.IsNotNull(infoResp);
        }

        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        [TestCase(7)]
        [TestCase(10)]
        [TestCase(11)]
        [TestCase(12)]
        [TestCase(13)]
        [TestCase(14)]
        [TestCase(15)]
        [TestCase(18)]
        [TestCase(19)]
        [TestCase(20)]
        [TestCase(21)]
        [TestCase(22)]
        [TestCase(23)]
        [TestCase(26)]
        [TestCase(27)]
        [TestCase(28)]
        [TestCase(29)]
        [TestCase(30)]
        [TestCase(31)]
        public void TestGetInfoWithoutPassword_ShouldThrowException(int dataSetNumber)
        {
            DataSetElements dataSets = GetDataSetElementsFromExpectedValueAfterBuild(dataSetNumber);
            // Arrange
            MockupWebClientFactory mockFactory = new MockupWebClientFactory(this.jsonInfo);
            AccessSdk sdk = new AccessSdk(accessUrl, merchantId, apiKey, DEFAULT_VERSION, mockFactory);
            var emptyPassword = "";

            // Act
            ActualValueDelegate<object> testDelegate = () => sdk.GetInfo(session, user, emptyPassword, uniq, dataSets);

            // Assert
            Assert.That(testDelegate, Throws.TypeOf<AccessException>());
        }
        #endregion

        #region Helpers

        private DataSetElements GetDataSetElementsFromExpectedValueAfterBuild(int expectedValue)
        {
            var dse = new DataSetElements();

            var info = new DataSetElements().WithInfo().Build();
            if ((expectedValue & info) == info)
            {
                dse.WithInfo();
            }

            var velocity = new DataSetElements().WithVelocity().Build();
            if ((expectedValue & velocity) == velocity)
            {
                dse.WithVelocity();
            }

            var decision = new DataSetElements().WithDecision().Build();
            if ((expectedValue & decision) == decision)
            {
                dse.WithDecision();
            }

            var trusted = new DataSetElements().WithTrusted().Build();
            if ((expectedValue & trusted) == trusted)
            {
                dse.WithTrusted();
            }

            var behavioSec = new DataSetElements().WithBehavioSec().Build();
            if ((expectedValue & behavioSec) == behavioSec)
            {
                dse.WithBehavioSec();
            }

            if (expectedValue != dse.Build())
            {
                throw new AccessException(AccessErrorType.INVALID_DATA, "Expected value and DataSetElements.Build() value are different.");
            }

            return dse;
        }

        #endregion
    }
}
