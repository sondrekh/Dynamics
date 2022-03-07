using Dynamics.Basic;
using Dynamics.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;
using Test.Basic;

namespace Test.Methods
{
    [TestClass]
    public class Patch : TestBase
    {
        [TestMethod]
        public async Task patch_contact()
        {
            // Arrange
            var contact = new Contact().WithFirstName(firstName);
            var request = new Request()
                .EntityName(entityName)
                .RecordId(contactId)
                .Body(contact.Serialize());

            // Act
            var response = await crm.Patch(request);
            var statusCode = (int)response.StatusCode;

            // Assert
            Assert.AreEqual(204, statusCode);
        }
    }
}
