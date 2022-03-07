using Dynamics.Basic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test.Basic
{
    [TestClass]
    public class TestBuildUrls
    {
        private string _entityName = "entities";
        private string _recordId = "someguid";
        private BuildUrl _buildUrl = new BuildUrl("url.com");
        public Request request;

        [TestInitialize]
        public void setup()
        {
            request = new Request()
                .EntityName(_entityName)
                .RecordId(_recordId);
        }


        [TestMethod]
        public void build_url()
        {
            // Arrange
            var select = "$select=name";
            var filter = "&$filter=name eq 'name'";

            // Act 
            var url = _buildUrl.GetList(request
                                            .Select(select)
                                            .Filter(filter));

            // Assert
            var expectedResult = "url.com/api/data/v9.1/entities?$select=name&$filter=name eq 'name'";
            Assert.AreEqual(expectedResult, url);
        }

        [TestMethod]
        public void build_single_url()
        {
            // Act 
            var url = _buildUrl.GetRecord(request);

            // Assert
            var expectedResult = "url.com/api/data/v9.1/entities(someguid)?";
            Assert.AreEqual(expectedResult, url);
        }


        [TestMethod]
        public void build_post_url()
        {
            // Act
            var url = _buildUrl.Post(request);

            // Assert
            var expectedResult = "url.com/api/data/v9.1/entities";
            Assert.AreEqual(expectedResult, url);
        }

        [TestMethod]
        public void build_patch_url()
        {
            // Act 
            var url = _buildUrl.Patch(request);

            // Assert
            var expectedResult = "url.com/api/data/v9.1/entities(someguid)";
            Assert.AreEqual(expectedResult, url);
        }

        [TestMethod]
        public void build_delete_url()
        {
            // Act
            var url = _buildUrl.Delete(request);

            // Assert
            var expected = "url.com/api/data/v9.1/entities(someguid)";
            Assert.AreEqual(expected, url);
        }

        [TestMethod]
        public void build_put_url()
        {
            // Arrange
            var fieldName = "fieldName";

            // Act
            var url = _buildUrl.Put(request.FieldName(fieldName));

            // Assert
            var expected = $"url.com/api/data/v9.1/entities(someguid)/{fieldName}";
            Assert.AreEqual(expected, url);
        }
    }
}
