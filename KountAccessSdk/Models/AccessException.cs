//-----------------------------------------------------------------------
// <copyright file="AccessException.cs" company="Kount Inc">
//     Copyright 2017 Kount Inc. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace KountAccessSdk.Models
{
    using System;

    public enum AccessErrorType
    {
        // Any Network Error (host not available, host not found, HTTP 404, etc.)
        NETWORK_ERROR,

        // Problems encrypting/decrypting data.
        ENCRYPTION_ERROR,

        // Missing or malformed data (bad hostnames, missing/empty fields)
        INVALID_DATA,
    }

    /// <summary>
    /// Definition for AccessException
    /// </summary>
    public class AccessException : Exception
    {
        public AccessErrorType ErrorType { get; private set; }
        public new string Message;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="errorType">Access Error Type</param>
        /// <param name="message">error message</param>
        public AccessException(AccessErrorType errorType, string message)
        {
            ErrorType = errorType;
            Message = message;
        }
    }
}