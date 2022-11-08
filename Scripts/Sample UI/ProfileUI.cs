using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UIElements;

namespace Assets.Scripts.Sample_UI
{
    public class ProfileUI : MonoBehaviour
    {
        public UIDocument m_UIDocument;
        async void Start()
        {
            var root= m_UIDocument.rootVisualElement;
            var Name = root.Q<Label>("Name");
            var Handle = root.Q<Label>("Handle");
            var avatar = root.Q<VisualElement>("Avatar");
            var Bio = root.Q<Label>("Bio");
            var Url = root.Q<Label>("Url");
            var Address = root.Q<Label>("Address");
            var Network = root.Q<Label>("Network");
            var Platform = root.Q<Label>("Platform");
            var Source = root.Q<Label>("Source");

            string[] networks = { "ethereum" };
            var profile = await RSS3.Profiles.AsyncGetRquest("0xc8b960d09c0078c18dcbe7eb9ab9d816bcca8944", networks);
            var FirstProfile = profile.result[0];
            Name.text += $" {FirstProfile.name}";
            Handle.text += $" {FirstProfile.handle}";
            Bio.text += $" {FirstProfile.bio}";
            Url.text += $" {FirstProfile.url}";
            Address.text += $" {FirstProfile.address.Remove(7)}...";
            Network.text += $" {FirstProfile.network}";
            Platform.text += $" {FirstProfile.platform}";
            Source.text += $" {FirstProfile.source}";
            var ipfsUrl = FirstProfile.profile_uri[0].Split("//")[1];
            var result = await asyncGetTexture(ipfsUrl);
            VisualElement UrlImage = new ImageFromUrl(result);
            avatar.Add(UrlImage);

            Url.RegisterCallback<ClickEvent>(ev => Application.OpenURL(FirstProfile.url));
        }


        private async Task<Texture> asyncGetTexture(string ipfs)
        {
            // UnityWebRequest webRequest = UnityWebRequest.Get(uri)
            var request = UnityWebRequestTexture.GetTexture($"https://ipfs.rss3.page/ipfs/{ipfs}");
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
}