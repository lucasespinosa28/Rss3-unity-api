# Rss3-unity-api
this plugin for unity game engine that helps to get information from various blockchain networks using [RSS3 API](https://docs.rss3.io/reference/getnotesbyinstance),
to install use the [unitypackage](https://github.com/lucasespinosa28/Rss3-unity-api/releases/tag/beta)

## [Website Demo](https://lucasespinosa28.github.io/Rss3-unity-api/) (some images may not load because of CORS)
# Code examples
## Profile
```csharp
  var Profile = new RSS3.Profiles(){
      address = "diygod.csb"
  };
  var data =  await Profile.AsyncGetRquest();
  foreach (var item in data.result){
      Debug.Log($"Name: {item.name} Bio: {item.bio}");
  }
  // Debug.Log(JsonUtility.ToJson(data));
```
### Demo from scene Profile
![Captura de tela 2022-11-14 033510](https://user-images.githubusercontent.com/52639395/201592836-5e84da5f-5041-4a7b-adfe-f2c9377b69c0.png)
## Note
#### Basic
All Web3 Feed of any user.
```csharp
    var Notes = new RSS3.Note(){
        instance = "0xf77a535eCDEb2F065F92e076Cb5572E2E96644da",
    };
    var notes = await Notes.AsyncGetRquest();
```
#### Collections
```csharp
  string[] tags = { "collectible" };
  var Notes = new RSS3.Note(){
      instance = "vitalik.eth",
      tags = tags,
  };
  var data  = await Notes.AsyncGetRquest();
  foreach (var note in data.result){
      foreach (var action in note.actions){
          Debug.Log(action.metadata.name);
      }
  }
  // Debug.Log(JsonUtility.ToJson(data));
```
##### Demo from scene Notes
![Captura de tela 2022-11-14 033130](https://user-images.githubusercontent.com/52639395/201592840-68b12270-677f-4821-8426-3da3d6c46072.png)
#### Transaction
```csharp
  string[] tags = { "transaction" };
  var Notes = new RSS3.Note(){
    instance = "vitalik.eth",
    tags = tags,
    limit = 5
  };
  var data = await Notes.AsyncGetRquest();
  foreach (var note in data.result){
    Debug.Log($"{note.tag}: {note.hash}");
  }
  // Debug.Log(JsonUtility.ToJson(data));
```
##### Demo from scene Notes
![Captura de tela 2022-11-14 033048](https://user-images.githubusercontent.com/52639395/201592843-62e56c3d-b84e-42a3-a275-90ef356cf55d.png)
## Platform
```csharp
  string[] network = { "polygon", "binance_smart_chain" };
  var platform = new RSS3.Platform(){
      networks = network,
      tag = "all" //default is all
  };
  var data = await platform.AsyncGetRquest();
  foreach (var item in data.result){
      Debug.Log(item.name);
  }
  // Debug.Log(JsonUtility.ToJson(data));
```







