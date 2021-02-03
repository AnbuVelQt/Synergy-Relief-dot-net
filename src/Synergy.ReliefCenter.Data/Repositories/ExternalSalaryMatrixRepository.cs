using Newtonsoft.Json;
using Synergy.ReliefCenter.Data.Entities.SalaryMatrix;
using Synergy.ReliefCenter.Data.Repositories.Abstraction;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Synergy.ReliefCenter.Data.Repositories
{
    public class ExternalSalaryMatrixRepository : IExternalSalaryMatrixRepository
    {
        public async Task<SalaryMatrix> GetSalaryMatrix(string vesselImoNumber, string seafarerCdcNumer, string AuthToken, string crewWageApiBaseUrl)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(crewWageApiBaseUrl);
                client.DefaultRequestHeaders.Add("Authorization", AuthToken.Replace("Bearer ", ""));
                //HTTP GET
                var response = await client.GetAsync("SalaryMatrix?vesselIMONumber=" + vesselImoNumber + "&seafarerCDCNumber=" + seafarerCdcNumer );
                if (response.IsSuccessStatusCode)
                {
                    var responseAsString = await response.Content.ReadAsStringAsync();
                    var responseAsConcreteType = JsonConvert.DeserializeObject<SalaryMatrix>(responseAsString);
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
