using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Dynamics.Models;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace Dynamics.Basic
{
    public class Crm
    {
        public string accessToken { get; set; }
        public CrmAuthentication authentication { get; set; }
        public BuildUrl buildUrl { get; set; }

        public Crm(CrmAuthentication authentication)
        {
            this.authentication = authentication;
            buildUrl = new BuildUrl(authentication.resourceUrl);
        }

        public async Task<HttpResponseMessage> Put(IRequest request)
        {
            string url = buildUrl.Put(request);
            return await Message.CreateAndSendMessage(url, HttpMethod.Put, accessToken, request.body);
        }

        public async Task<HttpResponseMessage> Post(IRequest request)
        {
            string url = buildUrl.Post(request);
            return await Message.CreateAndSendMessage(url, HttpMethod.Post, accessToken, request.body);
        }

        public async Task<HttpResponseMessage> Patch(IRequest request)
        {
            var url = buildUrl.Patch(request);
            return await Message.CreateAndSendMessage(url, new HttpMethod("PATCH"), accessToken, request.body);
        }

        public async Task<T> GetRecord<T>(IRequest request)
        {
            var response = await GetRecord(request);
            var data = response.ResponseToString();
            return Entity.ToObj<T>(data);
        }

        public async Task<List<T>> GetList<T>(IRequest request)
        {
            var response = await GetList(request);
            var data = Entity.ResponseToString(response);
            return Entity.ToList<T>(data);
        }

        public async Task<T> GetFirst<T>(IRequest request)
        {
            List<T> list = await GetList<T>(request);
            return list.First();
        }

        public async Task<HttpResponseMessage> GetList(IRequest request)
        {
            var url = buildUrl.GetList(request);
            return await Message.CreateAndSendMessage(url, HttpMethod.Get, accessToken);
        }

        public async Task<HttpResponseMessage> GetRecord(IRequest request)
        {
            var url = buildUrl.GetRecord(request);
            return await Message.CreateAndSendMessage(url, HttpMethod.Get, accessToken);
        }

        public async Task GetAccessToken()
        {
            var authenticationContext = new AuthenticationContext($"https://login.microsoftonline.com/{authentication.tenantId}");
            var result = await authenticationContext.AcquireTokenAsync(authentication.resourceUrl, new ClientCredential(authentication.clientId, authentication.clientSecret));
            this.accessToken = result.AccessToken;
        }

        public async Task<HttpResponseMessage> Delete(IRequest request)
        {
            var url = buildUrl.Delete(request);
            return await Message.CreateAndSendMessage(url, HttpMethod.Delete, accessToken);
        }
    }
}
