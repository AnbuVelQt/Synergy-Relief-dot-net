using Newtonsoft.Json;
using Synergy.ReliefCenter.Data.Entities.Master;
using Synergy.ReliefCenter.Data.Repositories.Abstraction;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Synergy.ReliefCenter.Data.Repositories
{
    public class ExternalUserDetailsRepository : IExternalUserDetailsRepository
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly ExternalApiConfiguration.UserInfoApi _configuration;
        public ExternalUserDetailsRepository(IHttpClientFactory clientFactory, ExternalApiConfiguration.UserInfoApi configuration)
        {
            _clientFactory = clientFactory;
            _configuration = configuration;
        }
        public async Task<UserDetails> GetUserDetails(string userId)
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
            new Uri(_configuration.ApiUrl+"User/UserRole/"+userId));
            request.Headers.Add("apikey", _configuration.ApiKey);

            var client = _clientFactory.CreateClient();

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var responseStream = await response.Content.ReadAsStringAsync();
                var responseAsConcreteType = JsonConvert.DeserializeObject<UserDetails>(responseStream);
                return responseAsConcreteType;
            }
            else
            {
                return null;
            }
        }
    }
}
