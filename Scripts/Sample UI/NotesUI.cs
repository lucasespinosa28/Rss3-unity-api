using System.Net.Sockets;
using System.Threading.Tasks;
using System.Transactions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UIElements;

public class NotesUI : MonoBehaviour
{
    public UIDocument m_UIDocument;
    [SerializeField]
    public string Address;

    private DropdownField inputTags;
    private Button buttonSearch;
    private TextField inputAddress;
    private ScrollView scrollView;
    private VisualElement container;
    void Start()
    {
        VisualElement root = m_UIDocument.rootVisualElement;
        inputTags = root.Q<DropdownField>("InputTags");
        inputAddress = root.Q<TextField>("InputAddress");
        buttonSearch = root.Q<Button>("ButtonSearch");
        buttonSearch.RegisterCallback<ClickEvent>(LoadUI);
        scrollView = root.Q<ScrollView>();
        container = new VisualElement();
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
    private async void LoadUI(ClickEvent evt)
    {
        container.Clear();
        container = new VisualElement();
        container.AddToClassList("all-container");
        container.AddToClassList("bold-text");
        scrollView.Add(container);

        string[] tags = { $"{inputTags.value}" };
        var Notes = new RSS3.Note()
        {
            instance = inputAddress.text,
            tags = tags,
            limit = 500,
        };
        var notes = await Notes.AsyncGetRquest();
        if(inputTags.value == "transaction")
        {
            displayTransactions(notes);
        }
        if (inputTags.value == "collectible")
        {
            displayCollectibles(notes);
        }
       
    }
    private async void displayCollectibles(RSS3.Models.Note notes)
    {
        foreach (var collection in notes.result)
        {
            foreach (var action in collection.actions)
            {
                if (action.metadata.image != null)
                {
                    var texture = await asyncGetTexture(action.metadata.image);
                    if (texture != null)
                    {
                        container.AddToClassList("scroll-container");
                        VisualElement nft = new VisualElement();
                        nft.AddToClassList("nft-container");
                        var title = action.metadata.name;
                        if (action.metadata.name.Length > 15)
                        {
                            title = $"{title.Remove(15)}...";
                        }
                        VisualElement Name = new Label(title);
                        Name.AddToClassList("nft-text");
                        nft.Add(Name);
                        VisualElement UrlImage = new ImageFromUrl(texture);
                        UrlImage.AddToClassList("pointer");
                        nft.Add(UrlImage);
                        UrlImage.RegisterCallback<ClickEvent>(ev => Application.OpenURL(action.metadata.image));
                        container.Add(nft);
                    }

                }
            }
        }
    }
    private async void displayTransactions(RSS3.Models.Note notes)
    {
        foreach (var transactions in notes.result)
        {
            foreach (var action in transactions.actions)
            {
                var transcationContainer = new VisualElement();
                var texture = await asyncGetTexture(action.metadata.image);
                var symbolHeader = new VisualElement();
                if (texture != null)
                {
                    VisualElement symbolImage = new ImageFromUrl(texture);
                    symbolImage.AddToClassList("symbol-image");
                    symbolHeader.Add(symbolImage);
                }
                string[] labels = { action.metadata.symbol, $"value: {action.metadata.value_display}", $"from {action.address_from.Remove(7)}...", $"to: {action.address_to.Remove(7)}..." };
                foreach (var text in labels)
                {
                    symbolHeader.Add(new Label(text));
                }
                string[] classes = { "symbol-container", "pointer" };
                foreach (var classId in classes)
                {
                    symbolHeader.AddToClassList(classId);
                }
                symbolHeader.RegisterCallback<ClickEvent>(ev => Application.OpenURL(action.related_urls[0]));
                transcationContainer.Add(symbolHeader);
                container.Add(transcationContainer);
            }
        }
    }
}
