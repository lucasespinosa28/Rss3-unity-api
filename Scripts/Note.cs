using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace RSS3
{
    public class Note
    {
        public static async Task<Models.Note> AsyncGetRquest(
            string address,
            bool count_only = false,
            bool include_poap = true,
            bool query_status = true,
            bool refresh = false,
            int limit = 500,
            string hash = null,
            string[] tags = null,
            string[] types = null,
            string[] networks = null,
            string[] platforms = null,
            string timestamp = null

            )
        {
            string url = $"https://pregod.rss3.dev/v1/notes/" +
                $"{address}?" +
                $"refresh={refresh}" +
                $"&limit={limit}" +
                $"&hash={hash}" +
                $"&{Utils.ArrayToTag(tags)}" +
                $"&{Utils.ArrayToType(types)}" +
                $"&{Utils.ArrayToNetwork(networks)}" +
                $"&{Utils.ArrayToPlatform(platforms)}" +
                $"{Utils.HasTimestamp(timestamp)}" +
                $"&include_poap={include_poap}" +
                $"&count_only={count_only}" +
                $"&query_status={query_status}";
            UnityWebRequest request = UnityWebRequest.Get(url);
            Debug.Log(url);
            _ = request.SendWebRequest();
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
                return JsonUtility.FromJson<Models.Note>(json);
            }
        }
    }
}