using System;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Dynamics.Basic
{
    public static class Message
    {
        public static HttpContent AddContent(string body)
        {
            return new StringContent(body, Encoding.UTF8, "application/json");
        }

        public static HttpRequestMessage AddHeaders(string accessToken, HttpRequestMessage msg)
        {
            msg.Headers.Add("Authorization", $"Bearer {accessToken}");
            return msg;
        }

        public static HttpRequestMessage CreateMessage(HttpMethod httpMethod, string requestUri, string accessToken, string body = null)
        {
            var message = new HttpRequestMessage(httpMethod, requestUri);
            message = AddHeaders(accessToken, message);

            if (body != null)
                message.Content = AddContent(body);

            return message;
        }
        public static async Task<HttpResponseMessage> CrmRequest(HttpRequestMessage message)
        {
            return await new HttpClient().SendAsync(message);
        }

        public static async Task<HttpResponseMessage> CreateAndSendMessage(string url, HttpMethod httpMethod, string accessToken, string body = null)
        {
            var message = CreateMessage(httpMethod, url, accessToken, body);
            return await CrmRequest(message);
        }

        public static string GetCreatedId(this HttpResponseMessage response)
        {
            var responseString = response.Headers.Location.ToString();
            return ExtractIdFromResponse(responseString);
        }

        public static string ExtractIdFromResponse(string responseString)
        {
            Regex rx = new Regex(@"\(([^)]+)\)");
            Match match = rx.Match(responseString);
            return match.Value.Replace("(", "").Replace(")", "");
        }
    }
}
