using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Synergy.AdobeSign.Model;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Synergy.AdobeSign
{
    public class AdobeSignRestClient
    {
        public readonly string _adobeSignRestClient = string.Empty, _adobeSignRestAgreementsPath = string.Empty;
        public AdobeSignRestClient()
        {
            var configurationBuilder = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            configurationBuilder.AddJsonFile(path, false);

            var root = configurationBuilder.Build();
            _adobeSignRestClient = root.GetSection("AdobeSignConfigs").GetSection("RestBaseUrl").Value;
            _adobeSignRestAgreementsPath = root.GetSection("AdobeSignConfigs").GetSection("RestAgreementsPath").Value;
        }
        public async Task<AgreementCreationResponse> CreateAgreement(string authorization, AgreementCreationInfo agreementInfo)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_adobeSignRestClient);

                
                var json = JsonConvert.SerializeObject(agreementInfo, Formatting.Indented);
                var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", authorization);

                //HTTP POST
                var response = await client.PostAsync(_adobeSignRestAgreementsPath, stringContent);
                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    var responseJson = JsonConvert.DeserializeObject<AgreementCreationResponse>(responseString);
                    return responseJson;
                }
                else
                {
                    return null;
                }
            }
            
        }

        //public object GetAgreements(string authorization)
        //{
        //    var client = new RestClient("https://api.in1.echosign.com:443");
        //    var request = new RestRequest("api/rest/v6/agreements", Method.GET);
        //    request.AddHeader("Authorization", authorization);
        //    request.RequestFormat = DataFormat.Json;
        //    var queryResult = client.Execute<List<object>>(request).Data;
        //    return queryResult;
        //}

        //public async Task<object> RestSharpPost(string authorization)
        //{
        //    var client = new RestClient(_adobeSignRestClient);
        //    var request = new RestRequest(_adobeSignRestAgreementsPath, Method.POST);
        //    request.RequestFormat = DataFormat.Json;
        //    request.AddHeader("Authorization", authorization);
        //    var fileInfosList = new List<FileInformation>();
        //    fileInfosList.Add(new FileInformation { libraryDocumentId = "CBJCHBCAABAA6n2lxqPkvqZzRzIph8fZ85m_hYzMntqf" });
        //    var participantSetsInfoList = new List<ParticipantInfo>();
        //    var memberInfoList = new List<MemberInfo>();
        //    memberInfoList.Add(new MemberInfo { email = "pentagram@synergyship.com" });
        //    participantSetsInfoList.Add(new ParticipantInfo { memberInfos = memberInfoList, order = 1, role = "FORM_FILLER" });
        //    request.AddJsonBody(new AgreementCreationInfo
        //    {
        //        fileInfos = fileInfosList,
        //        name = "Demo Check 195",
        //        participantSetsInfo = participantSetsInfoList,
        //        signatureType = "ESIGN",
        //        state = "DRAFT"
        //    });
        //    IRestResponse response = await client.ExecuteAsync(request);
        //    var responseJson = JsonConvert.DeserializeObject<AgreementCreationResponse>(response.Content);
        //    return responseJson;
        //}

        
    }
}
