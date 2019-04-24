using System.IO;
using System.Net;

namespace CommonLibrary
{
    public class Http
    {
        public static string HttpRequestPost(string url, byte[] data)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);

            if (data == null || data.Length == 0)
            {
                return "ERROR: Data is empty";
            }

            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            var response = (HttpWebResponse)request.GetResponse();

            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            return responseString;
        }

        public static string HttpRequestGet(string url)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);

            request.Method = "GET";

            var response = (HttpWebResponse)request.GetResponse();

            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            return responseString;
        }
    }
}