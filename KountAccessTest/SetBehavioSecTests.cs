//-----------------------------------------------------------------------
// <copyright file="SetBehavioSecTests.cs" company="Kount Inc">
//     Copyright 2018 Kount Inc. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace KountAccessTest
{
    using KountAccessSdk.Models;
    using KountAccessSdk.Service;
    using NUnit.Framework;

    /// <summary>
    /// Test class for SetBehavioSecTests
    /// </summary>
    public class SetBehavioSecTests : AccessSDKTestBase
    {
        private const string TimingData = "[[\"m\",\"n\",{\"doNotTrack\": \"1\",\"cookieEnabled\": true,\"geolocation\": {},\"mediaDevices\": {},\"webdriver\": false,\"appCodeName\": \"Mozilla\",\"appName\": \"Netscape\",\"appVersion\": \"5.0 (Macintosh; Intel Mac OS X 10_13_5) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/11.1.1 Safari/605.1.15\",\"platform\": \"MacIntel\",\"product\": \"Gecko\",\"productSub\": \"20030107\",\"userAgent\": \"Mozilla/5.0 (Macintosh; Intel Mac OS X 10_13_5) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/11.1.1 Safari/605.1.15\",\"vendor\": \"Apple Computer, Inc.\",\"vendorSub\": \"\",\"language\": \"en-US\",\"languages\": [\"en-US\"],\"onLine\": true}],[\"m\",\"s\",{\"height\": 1200,\"width\": 1920,\"colorDepth\": 24,\"pixelDepth\": 24,\"availLeft\": -1920,\"availTop\": 111,\"availHeight\": 1200,\"availWidth\": 1920}],[\"m\",\"v\",253]]";
        private const string behavioEnvironment = "/sandbox";

        [Test]
        public void TestBehavioSec_ShouldNotThrowException()
        {
            // Arrange                                                                                                                              
            MockupWebClientFactory mockFactory = new MockupWebClientFactory(this.jsonUniquesInfo);
            AccessSdk sdk = new AccessSdk(accessUrl, merchantId, apiKey, DEFAULT_VERSION, mockFactory);
            sdk.BehavioHost = accessUrl;
            sdk.BehavioEnvironment = behavioEnvironment;

            // Act                                                                                                                                  
            sdk.SetBehavioSec(session, uniq, TimingData);

            // Assert                                                                                                                                
        }

        [Test]
        public void TestBehavioSec_SetUrlInConstructor_ShouldNotThrowException()
        {
            // Arrange                                                                                                                              
            MockupWebClientFactory mockFactory = new MockupWebClientFactory(this.jsonUniquesInfo);
            AccessSdk sdk = new AccessSdk(accessUrl, merchantId, apiKey, DEFAULT_VERSION, mockFactory, accessUrl);
            sdk.BehavioEnvironment = behavioEnvironment;

            // Act                                                                                                                                  
            sdk.SetBehavioSec(session, uniq, TimingData);

            // Assert                                                                                                                                
        }

        [Test]
        public void TestBehavioSec_SetEnvironmentInConstructor_ShouldNotThrowException()
        {
            // Arrange                                                                                                                              
            MockupWebClientFactory mockFactory = new MockupWebClientFactory(this.jsonUniquesInfo);
            AccessSdk sdk = new AccessSdk(accessUrl, merchantId, apiKey, DEFAULT_VERSION, mockFactory, accessUrl, behavioEnvironment);

            // Act                                                                                                                                  
            sdk.SetBehavioSec(session, uniq, TimingData);

            // Assert                                                                                                                                
        }

        [Test]
        public void TestBehavioSecWithoutHost_ShouldThrowException()
        {
            // Arrange                                                                                                                              
            MockupWebClientFactory mockFactory = new MockupWebClientFactory(this.jsonUniquesInfo);
            AccessSdk sdk = new AccessSdk(accessUrl, merchantId, apiKey, DEFAULT_VERSION, mockFactory);
            // sdk.BehavioHost = host;                                                                                                              

            // Act
            // Assert      
            Assert.That(() => sdk.SetBehavioSec(session, uniq, TimingData), Throws.TypeOf<AccessException>());
        }

        [Test]
        public void TestBehavioSecWithoutEnvironment_ShouldThrowException()
        {
            // Arrange                                                                                                                              
            MockupWebClientFactory mockFactory = new MockupWebClientFactory(this.jsonUniquesInfo);
            AccessSdk sdk = new AccessSdk(accessUrl, merchantId, apiKey, DEFAULT_VERSION, mockFactory);
            sdk.BehavioHost = accessUrl;                                                                                                              

            // Act
            // Assert
            Assert.That(() => sdk.SetBehavioSec(session, uniq, TimingData), Throws.TypeOf<AccessException>());
        }

        [Test]
        public void TestBehavioSecWithoutSessionId_ShouldThrowException()
        {
            // Arrange                                                                                                                              
            MockupWebClientFactory mockFactory = new MockupWebClientFactory(this.jsonDevicesInfo);
            AccessSdk sdk = new AccessSdk(accessUrl, merchantId, apiKey, DEFAULT_VERSION, mockFactory);

            var emptySessionId = "";

            // Act
            // Assert
            Assert.That(() => sdk.SetBehavioSec(emptySessionId, uniq, TimingData), Throws.TypeOf<AccessException>());
        }

        [Test]
        public void TestBehavioSecWithoutUniq_ShouldThrowException()
        {
            // Arrange                                                                                                                              
            MockupWebClientFactory mockFactory = new MockupWebClientFactory(this.jsonDevicesInfo);
            AccessSdk sdk = new AccessSdk(accessUrl, merchantId, apiKey, DEFAULT_VERSION, mockFactory);

            var emptyUniq = "";

            // Act
            // Assert
            Assert.That(() => sdk.SetBehavioSec(session, emptyUniq, TimingData), Throws.TypeOf<AccessException>());
        }
                                                                             
        [Test]
        public void TestBehavioSecWithoutTiming_ShouldThrowException()
        {
            // Arrange                                                                                                                              
            MockupWebClientFactory mockFactory = new MockupWebClientFactory(this.jsonDevicesInfo);
            AccessSdk sdk = new AccessSdk(accessUrl, merchantId, apiKey, DEFAULT_VERSION, mockFactory);

            var emptyTiming = "";

            // Act
            // Assert
            Assert.That(() => sdk.SetBehavioSec(session, uniq, emptyTiming), Throws.TypeOf<AccessException>());
        }

        [Test]
        public void TestBehavioSecInvalidTiming_ShouldThrowException()
        {
            // Arrange                                                                                                                              
            MockupWebClientFactory mockFactory = new MockupWebClientFactory(this.jsonDevicesInfo);
            AccessSdk sdk = new AccessSdk(accessUrl, merchantId, apiKey, DEFAULT_VERSION, mockFactory);

            var invalidTiming = "invalid json";

            // Act
            // Assert
            Assert.That(() => sdk.SetBehavioSec(session, uniq, invalidTiming), Throws.TypeOf<AccessException>());
        }
    }
}
