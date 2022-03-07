using Dynamics.Basic;
using Dynamics.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;
using Test.Basic;

namespace Test.Methods
{
    [TestClass]
    public class Post : PostDeleteBase
    {
        [TestMethod]
        public async Task get_id_from_created_record()
        {
            // Arrange
            var contact = new Contact().WithFirstName(firstName);
            var request = new Request()
                .EntityName(entityName)
                .Body(contact.Serialize());

            var response = await crm.Post(request);
            // Act 
            string contactId = response.GetCreatedId();

            // Assert
            var isGuid = Guid.TryParse(contactId, out var guid);
            Assert.IsTrue(isGuid);

        }

        [TestMethod]
        public async Task post_contact()
        {
            // Arrange
            var contact = new Contact().WithFirstName(firstName);
            var request = new Request()
                .EntityName(entityName)
                .Body(contact.Serialize());

            // Act
            var response = await crm.Post(request);
            var statusCode = (int)response.StatusCode;

            // Assert
            Assert.AreEqual(204, statusCode);
        }
    }
}
