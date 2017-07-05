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


    public class AccessException : Exception
    {
        public AccessErrorType Error { get; private set; }
        public new string Message;

        public AccessException(AccessErrorType errorType, string message)
        {
            Error = errorType;
            Message = message;
        }
    }
}