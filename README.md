# Rss3-unity-api
this plugin for unity helps to get information from various blockchain networks using [RSS3 API](https://docs.rss3.io/reference/getnotesbyinstance),
to install use the [unitypackage](https://github.com/lucasespinosa28/Rss3-unity-api/releases/tag/beta)

# Code examples
## Profile
```csharp
  string[] networks = { "ethereum" };
  var profile = await RSS3.Profiles.AsyncGetRquest("0xc8b960d09c0078c18dcbe7eb9ab9d816bcca8944", networks);
  var FirstProfile = profile.result[0];
  Debug.Log(FirstProfile.name);
```
## Note
#### Basic
```csharp
   var notes = await RSS3.Note.AsyncGetRquest(Address);
```
#### Collections
```csharp
  string[] tags = { "collectible" };
  var collections = await RSS3.Note.AsyncGetRquest(Address,false,false,false,true,100,null, tags);
```
## Platform
```csharp
  string[] networks = { "ethereum" };
  var platforms = await RSS3.Platform.AsyncGetRquest("all", networks);
```

#### Demos are in the scenes folder
#### Screenshot from real android phone showing profile
![Screenshot_2022-11-07-22-19-31-277_com DefaultCompany rss3unity](https://user-images.githubusercontent.com/52639395/200464399-36421bcb-d5e8-4cc9-b008-db0f62a6d392.jpg)

#### Demo showing NFT
https://user-images.githubusercontent.com/52639395/200463675-dd4b9cb6-4158-4c37-b494-f5a3df3dc178.mp4