using Newtonsoft.Json;
using Synergy.ReliefCenter.Data.Entities.Master;
using Synergy.ReliefCenter.Data.Repositories.Abstraction;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Synergy.ReliefCenter.Data.Repositories
{
    public class ExternalUserDetailsRepository : IExternalUserDetailsRepository
    {
        public async Task<UserDetails> GetUserDetails(string userId, string apiKey, string userDetailsApiBaseUrl)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(userDetailsApiBaseUrl);
                client.DefaultRequestHeaders.Add("apikey", apiKey);
                //HTTP GET
                var response = await client.GetAsync("User/UserRole/"+ userId);
                if (response.IsSuccessStatusCode)
                {
                    var responseAsString = await response.Content.ReadAsStringAsync();
                    var responseAsConcreteType = JsonConvert.DeserializeObject<UserDetails>(responseAsString);
                    return responseAsConcreteType;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
