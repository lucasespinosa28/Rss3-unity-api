using System;
using System.Collections;
using System.Text;
using UnityEngine;

namespace RSS3
{
    public class Utils 
    {

        public static string ArrayToNetwork(string[] networks)
        {
            if (networks == null) return "";
            string result = "";
            for (int i = 0; i < networks.Length; i++)
            {
                result += $"network={networks[i]}";
                if (i != networks.Length-1) result += "&";
            }
            return result;
        }
        public static string ArrayToPlatform(string[] platforms)
        {
            if (platforms == null) return "";
            string result = "";
            for (int i = 0; i < platforms.Length; i++)
            {
                result += $"platform={platforms[i]}";
                if (i != platforms.Length - 1) result += "&";
            }
            return result;
        }
        public static string ArrayToTag(string[] tags)
        {
            if (tags == null) return "";
            string result = "";
            for (int i = 0; i < tags.Length; i++)
            {
                result += $"tag={tags[i]}";
                if (i != tags.Length - 1) result += "&";
            }
            return result;
        }
        public static string ArrayToType(string[] types)
        {
            if (types == null) return "";
            string result = "";
            for (int i = 0; i < types.Length; i++)
            {
                result += $"tag={types[i]}";
                if (i != types.Length - 1) result += "&";
            }
            return result;
        }
        public static string HasTimestamp(string timestamp)
        {
            if (timestamp == null) return "";
            return $"&timestamp={timestamp}";


        }
    }
}