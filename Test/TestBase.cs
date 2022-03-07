using Dynamics.Basic;
using Dynamics.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using System.Threading.Tasks;

namespace Test.Basic
{
    [TestClass]
    public class TestBase
    {
        public static string pathToIds = @"C:\Users\SondreKværneHansen\Documents\GitHub\LAB_Oppsett for test\ids.json";
        public const string devUrl = "https://pplabsondre.crm4.dynamics.com";
        public const string api = "api/data/v9.1";
        public static string firstName = "TestContactWillBeDeleted";
        public string entityName = "contacts";
        public string contactSelect = "$select=firstname";
        public string contactFilter = $"&$filter=firstname eq '{firstName}'";
        public string contactId = "f237894f-fdfb-ea11-a813-000d3a4aaef5";
        public Contact contact { get; set; }
        public HttpRequestMessage message = new HttpRequestMessage();
        public Crm crm;
        public TestContext TestContext { get; set; }

        [TestInitialize]
        public async Task SetUp()
        {
            var auth = CrmAuthentication.Get(pathToIds, TestContext);
            crm = new Crm(auth);
            await crm.GetAccessToken();
            contact = new Contact().WithFirstName(firstName);
        }
    }

    [TestClass]
    public class PostDeleteBase : TestBase
    {
        [TestInitialize]
        public async Task create_contact_for_tests()
        {
            var contact = new Contact().WithFirstName(firstName);
            var request = new Request()
                .EntityName(entityName)
                .Body(contact.Serialize());

            var response = await crm.Post(request);
            contactId = Message.GetCreatedId(response);
        }

        [TestCleanup]
        public async Task teardown_delete_created_test_contacts()
        {
            var request = new Request()
                .EntityName(entityName)
                .Select(contactSelect)
                .Filter(contactFilter);

            var response = await crm.GetList(request);
            var data = Entity.ResponseToString(response);
            var contacts = Entity.ToList<Contact>(data);

            foreach (Contact contact in contacts)
                await crm.Delete(request.RecordId(contact.id));
        }
    }
}
