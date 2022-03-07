using Newtonsoft.Json;

namespace Dynamics.Models
{
    public class Contact
    {
        [JsonIgnore]
        public string entityName = "contacts";
        [JsonIgnore]
        public string id { get; set; }
        [JsonProperty("firstname", NullValueHandling = NullValueHandling.Ignore)]
        public string firstName { get; set; }
        [JsonProperty("lastname", NullValueHandling = NullValueHandling.Ignore)]
        public string lastName { get; set; }

        public Contact WithFirstName(string firstName)
        {
            this.firstName = firstName;
            return this;
        }

        public Contact WithLastName(string lastName)
        {
            this.lastName = lastName;
            return this;
        }

        public string Serialize()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
