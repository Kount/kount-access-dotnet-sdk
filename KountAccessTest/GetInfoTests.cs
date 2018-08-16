//-----------------------------------------------------------------------
// <copyright file="GetInfoTests.cs" company="Kount Inc">
//     Copyright 2018 Kount Inc. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace KountAccessTest
{
    using System;
    using System.Linq;
    using KountAccessSdk.Models;
    using KountAccessSdk.Service;
    using Newtonsoft.Json;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Test class for GetInfoTests
    /// </summary>
    [TestClass]
    public class GetInfoTests : AccessSDKTestBase
    {
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
                    .WithBehavioSec();

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
                    .WithInfo();

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
        public void TestGetInfoWithoutUniq_ShouldPassValidation(int dataSetNumber)
        {
            DataSetElements dataSets = GetDataSetElementsFromExpectedValueAfterBuild(dataSetNumber);
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
        public void TestGetInfoWithoutUniq_ShouldThrowException(int dataSetNumber)
        {
            DataSetElements dataSets = GetDataSetElementsFromExpectedValueAfterBuild(dataSetNumber);
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
        public void TestGetInfoWithoutUser_ShouldPassValidation(int dataSetNumber)
        {
            DataSetElements dataSets = GetDataSetElementsFromExpectedValueAfterBuild(dataSetNumber);
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
        public void TestGetInfoWithoutUser_ShouldThrowException(int dataSetNumber)
        {
            DataSetElements dataSets = GetDataSetElementsFromExpectedValueAfterBuild(dataSetNumber);
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
        public void TestGetInfoWithoutPassword_ShouldPassValidation(int dataSetNumber)
        {
            DataSetElements dataSets = GetDataSetElementsFromExpectedValueAfterBuild(dataSetNumber);
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
        public void TestGetInfoWithoutPassword_ShouldThrowException(int dataSetNumber)
        {
            DataSetElements dataSets = GetDataSetElementsFromExpectedValueAfterBuild(dataSetNumber);
            // Arrange
            MockupWebClientFactory mockFactory = new MockupWebClientFactory(this.jsonInfo);
            AccessSdk sdk = new AccessSdk(accessUrl, merchantId, apiKey, DEFAULT_VERSION, mockFactory);
            var emptyPassword = "";

            // Act
            Info infoResp = sdk.GetInfo(session, user, emptyPassword, uniq, dataSets);

            // Asert
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
