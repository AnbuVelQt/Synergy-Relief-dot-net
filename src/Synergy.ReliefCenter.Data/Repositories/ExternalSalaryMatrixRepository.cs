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
        private readonly IHttpClientFactory _clientFactory;
        private readonly ExternalApiConfiguration.CrewWageApi _configuration;
        public ExternalSalaryMatrixRepository(IHttpClientFactory clientFactory, ExternalApiConfiguration.CrewWageApi configuration)
        {
            _clientFactory = clientFactory;
            _configuration = configuration;
        }
        public async Task<SalaryMatrix> GetSalaryMatrix(string vesselImoNumber, string seafarerCdcNumer, string AuthToken)
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
            new Uri(_configuration.ApiUrl + "SalaryMatrix?vesselIMONumber=" + vesselImoNumber + "&seafarerCDCNumber=" + seafarerCdcNumer));
            request.Headers.Add("Authorization", AuthToken.Replace("Bearer ", ""));

            var client = _clientFactory.CreateClient();

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var responseStream = await response.Content.ReadAsStringAsync();
                var responseAsConcreteType = JsonConvert.DeserializeObject<SalaryMatrix>(responseStream);
                return responseAsConcreteType;
            }
            else
            {
                return null;
            }
        }
    }
}
