using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace RSS3
{
    public class Note
    {
        internal string instance;
        internal bool count_only = false;
        internal bool include_poap = true;
        internal bool query_status = true;
        internal bool refresh = false;
        internal int limit = 500;
        internal string hash = null;
        internal string[] tags = null;
        internal string[] types = null;
        internal string[] networks = null;
        internal string[] platforms = null;
        internal string timestamp = null;
        public async Task<Models.Note> AsyncGetRquest()
        {
            string url = $"https://pregod.rss3.dev/v1/notes/" +
                $"{instance}?" +
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