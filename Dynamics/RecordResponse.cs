using Dynamics.Basic;
using System;
using System.Net.Http;

namespace PluginRegistration
{
    public class RecordResponse
    {
        public int statusCode { get; set; }
        public string recordId { get; set; }

        public RecordResponse(HttpResponseMessage response, Type T)
        {
            if (response == null)
                return;

            statusCode = (int)response.StatusCode;

            var method = response.RequestMessage.Method;

            if (method == HttpMethod.Post)
                recordId = response.GetCreatedId();

            else if (method == HttpMethod.Delete || method == new HttpMethod("PATCH"))
                recordId = Message.ExtractIdFromResponse(response.RequestMessage.RequestUri.ToString());

            Console.WriteLine($"Status: {statusCode}, type: {T}, method: {response.RequestMessage.Method}");
        }
    }
}
