using Dynamics.Basic;
using Dynamics.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using System.Threading.Tasks;

namespace Test.Basic
{
    [TestClass]
    public class MessageTests 
    {
        public CrmAuthentication authentication;
        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void setup()
        {
            authentication = CrmAuthentication.Get(TestBase.pathToIds, TestContext);
        }

        public HttpRequestMessage message = new HttpRequestMessage();
        public const string devUrl = "https://pplabsondre.crm4.dynamics.com";

        [TestMethod]
        public void test_add_content_to_httprequestmessage()
        {
            // Arrange
            var body = "Content";

            // Act 
            message.Content = Message.AddContent(body);

            // Assert 
            Assert.IsTrue(message.Content != null);
        }

        [TestMethod]
        public void no_headers_added_to_httprequestmessage()
        {
            // Assert
            Assert.IsTrue(message.Headers.Authorization == null);
        }

        [TestMethod]
        public async Task add_headers_to_httprequestmessage()
        {
            // Arrange 
            var crm = new Crm(authentication);
            await crm.GetAccessToken();

            // Act
            message = Message.AddHeaders(crm.accessToken, message);

            // Assert 
            Assert.IsFalse(message.Headers == null);
        }

        [TestMethod]
        public async Task create_httprequestmessage_no_body()
        {
            // Arrange
            var crm = new Crm(authentication);
            await crm.GetAccessToken();
            var requestUri = devUrl;

            // Act
            var message = Message.CreateMessage(HttpMethod.Get, requestUri, crm.accessToken);

            // Assert
            Assert.IsFalse(message.Headers.Authorization == null);
            Assert.IsTrue(message.Content == null);
        }

        [TestMethod]
        public async Task create_httprequestmessage_with_body()
        {
            // Arrange
            var crm = new Crm(authentication);
            await crm.GetAccessToken();
            var requestUri = devUrl;
            var body = "Content";

            // Act
            var message = Message.CreateMessage(HttpMethod.Get, requestUri, crm.accessToken, body);

            // Assert
            Assert.IsFalse(message.Headers.Authorization == null);
            Assert.IsFalse(message.Content == null);
        }

        [TestMethod]
        public async Task response_object_to_string()
        {
            // Arrange
            var crm = new Crm(authentication);
            var response = await crm.GetList(new Request().EntityName("WhoAmI"));

            // Act
            string data = Entity.ResponseToString(response);

            // Assert
            Assert.IsTrue(data != null);
        }

        [TestMethod]
        public void extract_id_from_response()
        {
            // Arrange
            var guid = "43cadc42-2aa8-eb11-b1ac-00224881bc3f";
            var headersLocation = $"https://pplabsondre.crm4.dynamics.com/api/data/v9.1/contacts({guid})";

            // Act
            var id = Message.ExtractIdFromResponse(headersLocation);

            // Assert
            Assert.AreEqual(guid, id);
        }
    }
}
