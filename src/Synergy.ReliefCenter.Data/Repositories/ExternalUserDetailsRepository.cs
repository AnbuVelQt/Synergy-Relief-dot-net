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
        public async Task<UserDetails> GetUserDetails(string userId, string AuthToken)
        {
            using (var client = new HttpClient())
            {
                AuthToken = "E2d9shgYnvFqmA4tJJFuduXGvJtJbvvHKcT8WX5NnIBOwPZ1";
                client.BaseAddress = new Uri("https://login-shore.synergymarinetest.com/");
                client.DefaultRequestHeaders.Add("apikey", AuthToken);
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
