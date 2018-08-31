// <copyright file="HashValueTests.cs" company="Kount Inc">
//     Copyright 2018 Kount Inc. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace KountAccessTest
{
    using KountAccessSdk.Models;
    using KountAccessSdk.Service;
    using NUnit.Framework;

    /// <summary>
    /// Test class for HashValueTests
    /// </summary>
    [TestFixture]
    public class HashValueTests
    {
        [Test]
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
    }
}
