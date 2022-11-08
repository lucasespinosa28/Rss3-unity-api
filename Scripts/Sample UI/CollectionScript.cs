using System.Net.Sockets;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UIElements;

public class CollectionScript : MonoBehaviour
{
    public UIDocument m_UIDocument;
    [SerializeField]
    public string Address;
    async void Start()
    {
        var root = m_UIDocument.rootVisualElement;
        string[] tags = { "collectible" };
        var collections = await RSS3.Note.AsyncGetRquest(Address,false,false,false,true,100,null, tags);
        var container = root.Q<ScrollView>("Container");
        var header = new Label(Address);
        header.AddToClassList("nft-text");
        container.Add(header);
        foreach (var collection in collections.result)
        {
            foreach (var action in collection.actions)
            {
                if(action.metadata.image != null)
                {
                    var texture = await asyncGetTexture(action.metadata.image);
                    if(texture != null)
                    {
                        VisualElement Name = new Label(action.metadata.name);
                        Name.AddToClassList("nft-text");
                        container.Add(Name);
                        VisualElement UrlImage = new ImageFromUrl(texture);
                        container.Add(UrlImage);
                    }
                  
                }
            }
        }
    }

    private async Task<Texture> asyncGetTexture(string url)
    {
        // UnityWebRequest webRequest = UnityWebRequest.Get(uri)
        var request = UnityWebRequestTexture.GetTexture($"{url}");
        request.SendWebRequest();
        while (!request.isDone)
        {
            await Task.Yield();
        }

        if (request.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log($"Error while Sending: {request.error}");
            return null;
        }
        else
        {
            Texture myTexture = DownloadHandlerTexture.GetContent(request);
            return myTexture;
        }
    }
}
