using System;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UIElements;

namespace Assets.Scripts.Sample_UI
{
    public class ProfileUI : MonoBehaviour
    {
        [SerializeField]
        public string m_Address;
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
            var profile = new RSS3.Profiles();
            profile.networks = networks;
            profile.address = m_Address;
            var objects = await profile.AsyncGetRquest();
            RSS3.Models.Profile.Result FirstProfile = objects.result[0];

            Debug.Log(FirstProfile.profile_uri[0]);
            Name.text += $" {FirstProfile.name}";
            Handle.text += $" {FirstProfile.handle}";
            Bio.text += $" {FirstProfile.bio}";
            Url.text += $" {FirstProfile.url}";
            Address.text += $" {FirstProfile.address.Remove(7)}...";
            Network.text += $" {FirstProfile.network}";
            Platform.text += $" {FirstProfile.platform}";
            Source.text += $" {FirstProfile.source}";
            var imageUrl = parseImageUrl(FirstProfile);
            var result = await asyncGetTexture(imageUrl);
            VisualElement UrlImage = new ImageFromUrl(result);
            UrlImage.AddToClassList("round-avatar");
            avatar.Add(UrlImage);

            Url.RegisterCallback<ClickEvent>(ev => Application.OpenURL(FirstProfile.url));
        }

        public static Func<RSS3.Models.Profile.Result, string> parseImageUrl = imageUrl => {
            if (imageUrl.profile_uri[0].Contains("eip155:1/erc1155"))
            {
                return $"https://cdn.stamp.fyi/avatar/{imageUrl.address}?s=300";
            }
            if (imageUrl.profile_uri[0].Contains("ipfs"))
            {
                return $"https://ipfs.rss3.page/ipfs/{imageUrl.profile_uri[0].Split("//")[1]}";
            }
            return imageUrl.profile_uri[0];
        };
        private async Task<Texture> asyncGetTexture(string imageUrl)
        {
            // UnityWebRequest webRequest = UnityWebRequest.Get(uri)
            Debug.Log(imageUrl);
            var request = UnityWebRequestTexture.GetTexture(imageUrl);
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