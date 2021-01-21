using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Synergy.AdobeSign.Models;
using System.Threading;
using Synergy.AdobeSign.Configurations;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Synergy.AdobeSign
{
    public class AdobeSignRestClient : IAdobeSignRestClient
    {
        private readonly AdobeSignConfiguration _configuration;
        
        public AdobeSignRestClient(AdobeSignConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<AgreementCreationResponse> CreateAgreementAsync(AgreementCreationInfo agreementInfo, CancellationToken cancellationToken = default)
        {
            if (_configuration.AccessKey == null || _configuration.ApiUrl == null || _configuration.ContractDocumentId == null || _configuration.ApiVersion == null)
            {
                throw new ArgumentOutOfRangeException("Invalid configuration values passed.");
            }
            var agrrementPath = $"/api/rest/{_configuration.ApiVersion}/agreements";

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_configuration.ApiUrl);


                var json = JsonConvert.SerializeObject(agreementInfo, Formatting.Indented);
                var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_configuration.AccessKey}");

                //HTTP POST
                var response = await client.PostAsync(agrrementPath, stringContent);
                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    var responseJson = JsonConvert.DeserializeObject<AgreementCreationResponse>(responseString);
                    var agreementId = responseJson.Id;
                    responseJson.SigningUrls = await GetAgreementSigningUrlsAsync(agreementId);
                    return responseJson;
                }
                else
                {
                    throw new Exception(await response.Content.ReadAsStringAsync());
                }
            }
            
        }

        private async Task<IList<SigningUrl>> GetAgreementSigningUrlsAsync(string agreementId, CancellationToken cancellationToken = default)
        {
            if (_configuration.AccessKey == null || _configuration.ApiUrl == null || _configuration.ContractDocumentId == null || _configuration.ApiVersion == null)
            {
                throw new ArgumentOutOfRangeException("Invalid configuration values passed.");
            }
            var getSigningUrlsPath = $"/api/rest/{_configuration.ApiVersion}/agreements/{agreementId}/signingUrls";

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_configuration.ApiUrl);

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_configuration.AccessKey}");

                //HTTP GET
                var response = await client.GetAsync(getSigningUrlsPath);
                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();

                    var signingUrlsResponse = JsonConvert.DeserializeObject<SigningUrlsResponse>(responseString);
                    return signingUrlsResponse.signingUrlSetInfos[0].SigningUrls;
                }
                else
                {
                    throw new Exception(await response.Content.ReadAsStringAsync());
                }
            }

        }

    }
}
