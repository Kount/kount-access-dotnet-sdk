# kount-access-dotnet-sdk

This is the actual Kount Access .NET SDK.


Required:
* Merchant ID
* API Key
* Kount Access service host

Create an SDK object:
```c#
  AccessSdk sdk = new AccessSdk(accessHost, merchantId, apiKey);
```

Retrieve device information collected by the Data Collector:

```c#
  // sessionId, 32-character identifier, applied for customer session, provided to data collector
  DeviceInfo deviceInformation = sdk.GetDevice(sessionId);

  // IP address

  // mobile device?
```

Get velocity for one of our customers:
  // for greater security, username and password are internally hashed before transmitting the request
  // you can hash them yourself, this wouldn't affect the Kount Access Service

  // you can get the device information from the accessInfo object
  

  // and let's get the number of unique user accounts used by the current sessions device within the last hour
  Console.WriteLine(
    "The number of unique user access request(s) this hour for this device is:" + numUsersForDevice);
```

And last, the `decision` endpoint usage:

  // and the Kount Access decision itself
```

