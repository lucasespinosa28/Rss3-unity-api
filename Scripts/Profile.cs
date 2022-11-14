using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace RSS3
{
    public class Profiles
    {
        internal string address;
        internal string[] networks = null;
        internal string[] platforms = null;
        public async Task<Models.Profile> AsyncGetRquest()
        {

            string url = $"https://pregod.rss3.dev/v1/profiles/{address}?{Utils.ArrayToNetwork(networks)}&{Utils.ArrayToPlatform(platforms)}";
            Debug.Log(url);
            UnityWebRequest request = UnityWebRequest.Get(url);
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
                var json = request.downloadHandler.text;
                Debug.Log(json);
                return JsonUtility.FromJson<RSS3.Models.Profile>(json);
            }
        }

    }
}