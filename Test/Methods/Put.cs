using Dynamics.Basic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Test.Basic;

namespace Test.Methods
{
    [TestClass]
    public class Put : TestBase
    {
        [TestMethod]
        public async Task put_field_on_record()
        {
            // Arrange
            var request = new Request()
                .EntityName(entityName)
                .FieldName("firstname")
                .RecordId(contactId)
                .Body("{\"value\" : \"FirstNameValue\"");

            // Act
            var response = await crm.Put(request);

            // Assert
            Assert.AreEqual(204, (int)response.StatusCode);
        }
    }
}
