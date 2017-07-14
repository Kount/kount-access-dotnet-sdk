# kount-access-dotnet-sdk

This is the actual Kount Access .NET SDK.

Longer version of the code samples can be found [here](https://github.com/Kount/kount-access-dotnet-sdk/blob/master/KountAccessExample/Program.cs)

Required:
* Merchant ID
* API Key
* Kount Access service host

Create an SDK object:
```cs
AccessSdk sdk = new AccessSdk(accessHost, merchantId, apiKey);
```

Retrieve device information collected by the Data Collector:

```cs
// sessionId, 32-character identifier, applied for customer session, provided to data collector
DeviceInfo deviceInformation = sdk.GetDevice(sessionId);

// IP address
Console.WriteLine("IP Address: " + deviceInformation.Device.IpAddress);
Console.WriteLine("IP Geo(Country): " + deviceInformation.Device.IpGeo);
Console.WriteLine("Proxy: " + deviceInformation.Device.Proxy); // 1 (true) or 0 
// mobile device?
Console.WriteLine("Mobile: " + deviceInformation.Device.Mobile); // 1 (true) or 0 (false)
```

Get velocity for one of our customers:
```cs
// for greater security, username and password are internally hashed before transmitting the request
// you can hash them yourself, this wouldn't affect the Kount Access Service
VelocityInfo accessInfo = sdk.GetVelocity(sessionId, username, password);

// you can get the device information from the accessInfo object
Device device = accessInfo.Device;

string jsonVeloInfo = JsonConvert.SerializeObject(accessInfo);
Console.WriteLine(jsonVeloInfo); // this is the full response, which may be huge

// and let's get the number of unique user accounts used by the current sessions device within the last hour
int numUsersForDevice = accessInfo.Velocity.Device.ulh;
Console.WriteLine(
  "The number of unique user access request(s) this hour for this device is:" + numUsersForDevice);
```

And last, the `decision` endpoint usage:

```cs
DecisionInfo decisionInfo = sdk.GetDecision(sessionId, username, password); // those again are hashed internally
Decision decision = this.decisionInfo.Decision;
Console.WriteLine("errors: " + decision.Errors.Count);
Console.WriteLine("warnings: " + decision.Warnings.Count);
// and the Kount Access decision itself
Console.WriteLine("decision: " + decision.Reply.RuleEvents.Decision);
```

