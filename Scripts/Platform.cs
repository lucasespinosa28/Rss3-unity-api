using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace RSS3
{
    public class Platform
    {
        public static async Task<Models.Platform> AsyncGetRquest(string tag = "all", string[] networks = null)
        {
            string url = $"https://pregod.rss3.dev/v1/platforms/{tag}?{Utils.ArrayToNetwork(networks)}";
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
                return JsonUtility.FromJson<Models.Platform>(json);
            }
        }
    }
}
