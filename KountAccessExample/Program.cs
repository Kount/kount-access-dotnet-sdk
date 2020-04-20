//-----------------------------------------------------------------------
// <copyright file="KountAccessExample.cs" company="Kount Inc">
//     Copyright 2017 Kount Inc. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace KountAccessExample
{
    using KountAccessSdk.Enums;
    using KountAccessSdk.Models;
    using KountAccessSdk.Service;
    using Newtonsoft.Json;
    using System;
    using System.ComponentModel;

    internal class Program
    {
        private static void Main(string[] args)
        {
            var sample = new KountAccessExample();

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }

    /// <summary>
    ///
    /// This is an example implementation of the Kount Access API SDK.In this example we will show how to create, prepare,
    /// and make requests to the Kount Access API, and what to expect as a result. Before you can make API requests, you'll
    /// need to have made collector request(s) prior, and you'll have to use the session id(s) that were returned.
    ///
    /// @author custserv@kount.com
    /// @version 1.0.0
    /// @copyright 2017 Kount, Inc.All Rights Reserved.
    ///
    /// </summary>
    public class KountAccessExample
    {
        /**
         * Fake user session (this should be retrieved from the Kount Access Data Collector Client SDK.) This will be a
         * value up to 32 characters.
         */
        private string session = "abcdef12345678910abcdef123456789"; // "THIS_IS_THE_USERS_SESSION_FROM_JAVASCRIPT_CLIENT_SDK";

        /**
         * Merchant's customer ID at Kount. This should be the id you were issued from Kount.
         */
        private int merchantId = 0;

        /**
         * This should be the API Key you were issued from Kount.
         */
        private string apiKey = "PUT_YOUR_API_KEY_HERE";

        /**
         * Sample host. this should be the name of the Kount Access API server you want to connect to. We will use sandbox01
         * as the example.
         */
        private string host = "https://api-sandbox01.kountaccess.com";

        /// <summary>
        /// Simple Example within the Constructor.
        /// </summary>
        public KountAccessExample()
        {
            try
            {
                // Create the SDK. If any of these values are invalid, an com.kount.kountaccess.AccessException will be
                // thrown along with a message detailing why.
                AccessSdk sdk = new AccessSdk(host, merchantId, apiKey);

                // If you want the device information for a particular user's session, just pass in the sessionId. This
                // contains the id (fingerprint), IP address, IP Geo Location (country), whether the user was using a proxy
                // (and it was bypassed), and ...
                DeviceInfo deviceInfo = sdk.GetDevice(this.session);

                this.PrintDeviceInfo(deviceInfo.Device);

                // ... if you want to see the velocity information in relation to the users session and their account
                // information, you can make an access (velocity) request. Usernames and passwords will be hashed prior to
                // transmission to Kount within the SDK. You may optionally hash prior to passing them in as long as the
                // hashing method is consistent for the same value.
                String username = "billyjoe@bobtown.org";
                String password = "notreally";
                VelocityInfo accessInfo = sdk.GetVelocity(session, username, password);

                // Let's see the response
                Console.WriteLine("Response: " + accessInfo);

                // Each Access Request has its own uniqueID
                Console.WriteLine("This is our access response_id: " + accessInfo.ResponseId);

                // The device is included in an access request:
                this.PrintDeviceInfo(accessInfo.Device);

                // you can get the device information from the accessInfo object
                Device device = accessInfo.Device;

                string jsonVeloInfo = JsonConvert.SerializeObject(accessInfo);
                Console.WriteLine(jsonVeloInfo);

                this.PrintVelocityInfo(accessInfo.Velocity);

                // Or you can access specific Metrics directly. Let's say we want the
                // number of unique user accounts used by the current sessions device
                // within the last hour
                int numUsersForDevice = accessInfo.Velocity.Device.ulh;
                Console.WriteLine(
                        "The number of unique user access request(s) this hour for this device is:" + numUsersForDevice);

                // Decision Information is stored in a JSONObject, by entity type
                DecisionInfo decisionInfo = sdk.GetDecision(session, username, password);
                Decision decision = decisionInfo.Decision;
                // Let's look at the data
                this.PrintDecisionInfo(decision);

                // Get Kount Access data for session based on what was requested in the info flag
                String uniq = "uniq(customer identifier)";
                DataSetElements dataSet = new DataSetElements()
                    .WithInfo()
                    .WithVelocity()
                    .WithDecision()
                    .WithTrusted()
                    .WithBehavioSec();

                Info info = sdk.GetInfo(session, username, password, uniq, dataSet);
                this.PrintDeviceInfo(info.Device);
                this.PrintDecisionInfo(info.Decision);
                this.PrintVelocityInfo(info.Velocity);
                this.PrintFields(info.Trusted);
                this.PrintFields(info.BehavioSec);

                // Get devices that belong to a uniq user.
                DevicesInfo devices = sdk.GetDevices(uniq);
                foreach (var d in devices.Devices)
                {
                    this.PrintFields(d);
                }

                // Get the uniq users that belong to a device.
                string deviceId = "DEVICE_ID";
                var uniques = sdk.GetUniques(deviceId);
                foreach (var u in uniques.Uniques)
                {
                    this.PrintFields(u);
                }

                // Update device trust referenced by session ID
                sdk.SetDeviceTrustBySession(session, uniq, DeviceTrustState.Banned);

                // Update device trust referenced by device ID
                sdk.SetDeviceTrustByDevice(uniq, deviceId, DeviceTrustState.Trusted);

            }
            catch (AccessException ae)
            {
                // These can be thrown if there were any issues making the request.
                // See the AccessException class for more information.
                Console.WriteLine("ERROR Type: " + ae.ErrorType);
                Console.WriteLine("ERROR: " + ae.Message);
            }
        }

        private void PrintDeviceInfo(Device device)
        {
            // Fingerprint
            Console.WriteLine("Got Fingerprint:" + device.Id);

            // IP Address & Geo information
            Console.WriteLine("Got IP Address: " + device.IpAddress);
            Console.WriteLine("Got IP Geo(Country): " + device.IpGeo);

            string isProxy = (device.Proxy == 1) ? "true" : "false";
            Console.WriteLine("is Proxy: " + isProxy);

            // whether we detected the use of a mobile device.
            string isMobile = (device.Mobile == 1) ? "true" : "false";
            Console.WriteLine("isMobile: " + isMobile);
        }

        /// <summary>
        /// Example method to walk through the velocity data.
        /// </summary>
        /// <param name="velocity">Velocity type</param>
        private void PrintVelocityInfo(Velocity velocity)
        {
            Console.WriteLine("Got Account data: ");
            this.PrintFields(velocity.Account);
            Console.WriteLine("Got Device data: ");
            this.PrintFields(velocity.Device);
            Console.WriteLine("Got IpAddress data: ");
            this.PrintFields(velocity.IpAddress);
            Console.WriteLine("Got Password data: ");
            this.PrintFields(velocity.Password);
            Console.WriteLine("Got User data: ");
            this.PrintFields(velocity.User);
        }

        /// <summary>
        /// Example method to walk through the velocity data.
        /// </summary>
        /// <param name="decision"></param>
        private void PrintDecisionInfo(Decision decision)
        {
            Console.WriteLine("Got errors: " + decision.Errors.Count);
            Console.WriteLine("Got reply: ");
            this.PrintFields(decision.Reply);
            Console.WriteLine("Got warnings: " + decision.Warnings.Count);
            Console.WriteLine("Got decision: " + decision.Reply.RuleEvents.Decision);
        }

        /// <summary>
        /// Printing all properties of an object
        /// </summary>
        /// <param name="obj"></param>
        private void PrintFields(object obj)
        {
            foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(obj))
            {
                string name = descriptor.Name;
                object value = descriptor.GetValue(obj);

                string strVal = (value == null) ? "null" : value.ToString();
                Console.WriteLine(" {0}: {1}", name, strVal);
            }
        }
    }
}