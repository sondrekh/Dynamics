using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using System.IO;


namespace Dynamics.Basic
{
    public class CrmAuthentication
    {
        public string clientId { get; set; }
        public string clientSecret { get; set; }
        public string tenantId { get; set; }
        public string resourceUrl { get; set; }

        public static CrmAuthentication GetIdVariablesFromFile(string pathToIds)
        {
            var json = File.ReadAllText(pathToIds);
            return MapJsonToAuthenticationVariables(json);
        }

        private static CrmAuthentication MapJsonToAuthenticationVariables(string json)
        {
            return JObject.Parse(json).ToObject<CrmAuthentication>();
        }
        public static CrmAuthentication Get(string pathToIds, TestContext TestContext)
        {
            if (File.Exists(pathToIds))
                return GetIdVariablesFromFile(pathToIds);

            var auth = new CrmAuthentication()
            {
                clientId = (string)TestContext.Properties["clientId"],
                clientSecret = (string)TestContext.Properties["clientSecret"],
                tenantId = (string)TestContext.Properties["tenantId"],
                resourceUrl = (string)TestContext.Properties["resourceUrl"],
            };

            return auth;
        }
    }
}
