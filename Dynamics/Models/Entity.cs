using Dynamics.Basic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace Dynamics.Models
{
    public static class Entity
    {
        public static T ToObj<T>(string data)
        {
            return JsonConvert.DeserializeObject<T>(data);
        }

        public static List<T> ToList<T>(string data)
        {
            JObject json = JObject.Parse(data);
            JToken elements = json["value"];
            List<T> objects = new List<T>();

            foreach (JToken element in elements)
                objects.Add(ToObj<T>(element.ToString()));

            return objects;
        }

        public static string ResponseToString(this HttpResponseMessage response)
        {
            return response.Content.ReadAsStringAsync().Result;
        }
    }
}
