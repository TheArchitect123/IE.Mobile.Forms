using System.Net;

namespace IE.Utilities.Extensions
{
    public static class WebExtensions
    {
        public static bool IsResourceAvailable(this string resource)
        {
            HttpWebRequest request = WebRequest.Create($"{resource}") as HttpWebRequest;
            request.Method = "GET";
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            if (response.StatusCode == HttpStatusCode.OK)
                return true;
            else
                return false;
        }
    }
}
