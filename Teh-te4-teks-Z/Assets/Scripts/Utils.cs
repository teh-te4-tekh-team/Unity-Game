namespace Assets.Scripts
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Security.Policy;
    using System.Text;
    using UnityEngine;

    public class Utils
    {
        public static IEnumerator Post(string url, string data, Action<string> action)
        {
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Content-Type", "application/json");

            yield return Request(url, data, headers, action);
        }

        public static IEnumerator Update(string url, string data, Action<string> action)
        {
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Content-Type", "application/json");
            headers.Add("X-HTTP-Method-Override", "PUT");

            yield return Request(url, data, headers, action);
        }

        private static IEnumerator Request(string url, string data, Dictionary<string, string> headers, Action<string> action)
        {
            byte[] pData = Encoding.ASCII.GetBytes(data.ToCharArray());
            WWW request = new WWW(url, pData, headers);

            yield return request;

            if (request.isDone)
            {
                action.Invoke(request.text);
            }
        }
    }
}
