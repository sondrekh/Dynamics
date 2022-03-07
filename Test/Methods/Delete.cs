using Dynamics.Basic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;
using Test.Basic;

namespace Test.Methods
{
    [TestClass]
    public class Delete : PostDeleteBase
    {
        [TestMethod]
        public async Task delete_contact()
        {
            // Act
            var response = await crm.Delete(new Request()
                                                .EntityName(entityName)
                                                .RecordId(contactId)
                                                );
            var statusCode = (int)response.StatusCode;

            // Assert
            Assert.AreEqual(204, statusCode);
        }
    }
}
