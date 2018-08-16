//-----------------------------------------------------------------------
// <copyright file="DataSetElementsTest.cs" company="Kount Inc">
//     Copyright 2017 Kount Inc. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace KountAccessTest
{
    using KountAccessSdk.Models;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Test class for DataSetElements
    /// </summary>
    [TestClass]
    public class DataSetElementsTest
    {
        #region Call Build with single "with" included

        [TestMethod]
        public void TestCallWithBuildOnly_DefaultValueIsZero()
        {
            // Arrange
            var dataSetElements = new DataSetElements();

            // Act
            var dataSetNumber = dataSetElements.Build();

            // Assert
            Assert.AreEqual(0, dataSetNumber);
        }

        [TestMethod]
        public void TestCallBuildWithDeviceInfo_ShouldReturnOne()
        {
            // Arrange
            var dataSetElements = new DataSetElements();

            // Act
            var dataSetNumber = dataSetElements
                .WithInfo()
                .Build();

            // Assert
            Assert.AreEqual(1, dataSetNumber);
        }

        [TestMethod]
        public void TestCallBuildWithVelocity_ShouldReturnTwo()
        {
            // Arrange
            var dataSetElements = new DataSetElements();

            // Act
            var dataSetNumber = dataSetElements
                .WithVelocity()
                .Build();

            // Assert
            Assert.AreEqual(2, dataSetNumber);
        }

        [TestMethod]
        public void TestCallBuildWithDecision_ShouldReturnFour()
        {
            // Arrange
            var dataSetElements = new DataSetElements();

            // Act
            var dataSetNumber = dataSetElements
                .WithDecision()
                .Build();

            // Assert
            Assert.AreEqual(4, dataSetNumber);
        }

        [TestMethod]
        public void TestCallBuildWithTrustedDeviceInfo_ShouldReturnEight()
        {
            // Arrange
            var dataSetElements = new DataSetElements();

            // Act
            var dataSetNumber = dataSetElements
                .WithTrusted()
                .Build();

            // Assert
            Assert.AreEqual(8, dataSetNumber);
        }

        [TestMethod]
        public void TestCallBuildWithBehavioSec_ShouldReturnSixteen()
        {
            // Arrange
            var dataSetElements = new DataSetElements();

            // Act
            var dataSetNumber = dataSetElements
                .WithBehavioSec()
                .Build();

            // Assert
            Assert.AreEqual(16, dataSetNumber);
        }

        #endregion

        #region Call Build with two "with" included

        [TestMethod]
        public void TestCallBuildWithDeviceInfoAndVelocity_ShouldReturnThree()
        {
            // Arrange
            var dataSetElements = new DataSetElements();

            // Act
            var dataSetNumber = dataSetElements
                .WithInfo()
                .WithVelocity()
                .Build();

            // Assert
            Assert.AreEqual(3, dataSetNumber);
        }

        [TestMethod]
        public void TestCallBuildWithVelocityAndDecision_ShouldReturnSix()
        {
            // Arrange
            var dataSetElements = new DataSetElements();

            // Act
            var dataSetNumber = dataSetElements
                .WithVelocity()
                .WithDecision()
                .Build();

            // Assert
            Assert.AreEqual(6, dataSetNumber);
        }

        [TestMethod]
        public void TestCallBuildWithDecisionAndTrustedDeviceInfo_ShouldReturnTwelve()
        {
            // Arrange
            var dataSetElements = new DataSetElements();

            // Act
            var dataSetNumber = dataSetElements
                .WithDecision()
                .WithTrusted()
                .Build();

            // Assert
            Assert.AreEqual(12, dataSetNumber);
        }

        [TestMethod]
        public void TestCallBuildWithTrustedDeviceInfoAndBehavioSec_ShouldReturnTwentyFour()
        {
            // Arrange
            var dataSetElements = new DataSetElements();

            // Act
            var dataSetNumber = dataSetElements
                .WithTrusted()
                .WithBehavioSec()
                .Build();

            // Assert
            Assert.AreEqual(24, dataSetNumber);
        }

        #endregion

        #region Call Build with three "with" included

        [TestMethod]
        public void TestCallBuildWithDeviceInfo_Velocity_Decision_ShouldReturnSeven()
        {
            // Arrange
            var dataSetElements = new DataSetElements();

            // Act
            var dataSetNumber = dataSetElements
                .WithInfo()
                .WithVelocity()
                .WithDecision()
                .Build();

            // Assert
            Assert.AreEqual(7, dataSetNumber);
        }

        [TestMethod]
        public void TestCallBuildWithVelocity_Decision_TrustedDeviceInfo_ShouldReturnFourteen()
        {
            // Arrange
            var dataSetElements = new DataSetElements();

            // Act
            var dataSetNumber = dataSetElements
                .WithVelocity()
                .WithDecision()
                .WithTrusted()
                .Build();

            // Assert
            Assert.AreEqual(14, dataSetNumber);
        }

        [TestMethod]
        public void TestCallBuildWith_Decision_TrustedDeviceInfo_BehavioSec_ShouldReturnTwentyfour()
        {
            // Arrange
            var dataSetElements = new DataSetElements();

            // Act
            var dataSetNumber = dataSetElements
                .WithDecision()
                .WithTrusted()
                .WithBehavioSec()
                .Build();

            // Assert
            Assert.AreEqual(28, dataSetNumber);
        }

        #endregion

        #region Call Build with all "with" included

        [TestMethod]
        public void TestCallBuildWithAllDataSetsIncluded_ShouldReturnThirtyOne()
        {
            // Arrange
            var dataSetElements = new DataSetElements();

            // Act
            var dataSetNumber = dataSetElements
                .WithInfo()
                .WithVelocity()
                .WithDecision()
                .WithTrusted()
                .WithBehavioSec()
                .Build();

            // Assert
            Assert.AreEqual(31, dataSetNumber);
        }

        #endregion

        #region Call Build with all "with" included and duplications

        [TestMethod]
        public void TestCallBuildWithAllDataSetsIncludedAndDuplications_ShouldReturnThirtyOne()
        {
            // Arrange
            var dataSetElements = new DataSetElements();

            // Act
            var dataSetNumber = dataSetElements
                .WithInfo()
                .WithInfo()
                .WithInfo()
                .WithInfo()
                .WithVelocity()
                .WithVelocity()
                .WithVelocity()
                .WithVelocity()
                .WithDecision()
                .WithBehavioSec()
                .WithBehavioSec()
                .WithBehavioSec()
                .WithBehavioSec()
                .WithTrusted()
                .WithTrusted()
                .WithTrusted()
                .WithTrusted()
                .WithBehavioSec()
                .WithBehavioSec()
                .Build();

            // Assert
            Assert.AreEqual(31, dataSetNumber);
        }

        #endregion
    }
}
