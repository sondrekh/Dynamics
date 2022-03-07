using Dynamics.Basic;
using Dynamics.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Test.Basic;

namespace Test.Methods
{
    [TestClass]
    public class Get : TestBase
    {
        public Request request;

        [TestInitialize]
        public void setup()
        {
            request = new Request()
                .EntityName(entityName)
                .RecordId(contactId)
                .Select(contactSelect)
                .Filter(contactFilter);
        }


        [TestMethod]
        public async Task get_single_record()
        {
            // Act
            var response = await crm.GetRecord(request);

            // Assert
            Assert.AreEqual(200, (int)response.StatusCode);
        }

        [TestMethod]
        public async Task get_contact_response_object()
        {
            // Act 
            var response = await crm.GetList(request);

            // Assert
            Assert.AreEqual(200, (int)response.StatusCode);
        }

        [TestMethod]
        public async Task get_contacts_response_list()
        {
            // Act 
            HttpResponseMessage response = await crm.GetList(request);

            // Assert
            Assert.AreEqual(200, (int)response.StatusCode);
        }

        [TestMethod]
        public async Task get_contact_object_directly()
        {
            // Arrange
            string emptyString = null;
            var request = new Request()
                .EntityName(entityName)
                .RecordId(contactId);

            // Act
            Contact contact = await crm.GetRecord<Contact>(request);

            // Assert
            Assert.IsFalse(string.IsNullOrEmpty(contact.firstName));
            Assert.IsTrue(string.IsNullOrEmpty(emptyString));
        }

        [TestMethod]
        public async Task get_contact_object_list()
        {
            // Arrange
            var request = new Request().EntityName(entityName);

            // Act
            List<Contact> contacts = await crm.GetList<Contact>(request);

            // Assert 
            Assert.IsFalse(string.IsNullOrEmpty(contacts.First().firstName));
        }

        [TestMethod]
        public async Task get_first_in_list()
        {
            // Arrange
            var request = new Request().EntityName(entityName);

            // Act 
            Contact contact = await crm.GetFirst<Contact>(request);

            // Assert
            Assert.IsFalse(string.IsNullOrEmpty(contact.firstName));
        }
    }
}
