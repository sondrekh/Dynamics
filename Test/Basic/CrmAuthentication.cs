using Dynamics.Basic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Test.Basic
{
    [TestClass]
    public class WhoAmI : TestBase
    {
        [TestMethod]
        public void get_access_token_setup_function()
        {
            // Assert
            Assert.IsFalse(string.IsNullOrEmpty(crm.accessToken));
        }

        [TestMethod]
        public async Task is_whoAmI_working()
        {
            // Arrange
            var url = $"{devUrl}/{api}/WhoAmI";
            message = Message.CreateMessage(HttpMethod.Get, url, crm.accessToken);

            // Act
            var response = await Message.CrmRequest(message);
            var statusCode = (int)response.StatusCode;

            // Assert 
            Assert.AreEqual(200, statusCode);
        }


        [TestMethod]
        public async Task get_function_whoami()
        {
            // Arrange
            var request = new Request().EntityName("WhoAmI");

            // Act 
            var response = await crm.GetList(request);
            var statusCode = (int)response.StatusCode;

            // Assert 
            Assert.AreEqual(200, statusCode);
        }

        [TestMethod]
        public async Task create_and_send_message()
        {
            // Arrange
            var url = $"https://pplabsondre.crm4.dynamics.com/api/data/v9.1/contacts";

            // Act 
            var response = await Message.CreateAndSendMessage(url, HttpMethod.Get, crm.accessToken);
            var statusCode = (int)response.StatusCode;

            // Assert
            Assert.AreEqual(200, statusCode);
        }
    }
}
