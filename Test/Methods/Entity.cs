using Dynamics.Basic;
using Dynamics.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Test.Basic;

namespace Test.Models
{
    [TestClass]
    public class TestEntity : TestBase
    {
        private const string _contactId = "f237894f-fdfb-ea11-a813-000d3a4aaef5";
        [TestMethod]
        public void string_to_object()
        {
            // Arrange
            var data = "{\"tulleattribute\":\"value\",\"firstname\":\"Sondre\"}";

            // Act
            var obj = Entity.ToObj<Contact>(data);

            // Assert
            Assert.AreEqual(obj.firstName, "Sondre");
        }

        [TestMethod]
        public async Task get_single_contact_response_to_object()
        {
            // Arrange
            var request = new Request()
                .EntityName(entityName)
                .RecordId(_contactId);
            var response = await crm.GetRecord(request);

            // Act 
            var data = Entity.ResponseToString(response);
            var result = Entity.ToObj<Contact>(data);

            // Assert
            Assert.IsTrue(result.firstName != null);
        }

        [TestMethod]
        public async Task get_list_of_contacts_response_to_object_list()
        {
            // Arrange
            var request = new Request()
                .EntityName(entityName)
                .Select(contactSelect)
                .Filter(contactFilter);

            var response = await crm.GetList(request);

            // Act
            var data = Entity.ResponseToString(response);
            List<Contact> contacts = Entity.ToList<Contact>(data);

            // Assert
            var result = contacts[0];
            var expected = new Contact().WithFirstName(firstName);
            Assert.AreEqual(expected.firstName, result.firstName);
        }

        [TestMethod]
        public void serialize_object()
        {
            // Arrange
            var contact = new Contact().WithFirstName(firstName);

            // Act
            var json = JsonConvert.SerializeObject(contact);

            // Assert
            var expected = $"{{\"firstname\":\"{firstName}\"}}";
            Assert.AreEqual(expected, json);
        }

        [TestMethod]
        public void serialize_object_from_entity_ignore_null_values()
        {
            // Arrange
            var contact = new Contact().WithFirstName(firstName);

            // Act
            var json = contact.Serialize();

            // Assert
            var expected = $"{{\"firstname\":\"{firstName}\"}}";
            Assert.AreEqual(expected, json);
        }
    }
}
