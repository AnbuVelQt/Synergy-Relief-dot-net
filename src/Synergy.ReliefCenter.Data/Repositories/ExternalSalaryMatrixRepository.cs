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
        public async Task<SalaryMatrix> GetSalaryMatrix(long vesselId, long seafarerId, string authToken, string crewWageApiBaseUrl)
        {
            using (var client = new HttpClient())
            {
                //AuthToken = "eyJhbGciOiJSUzI1NiIsImtpZCI6ImFYWXZyRFpMdGtNN0hoNTBCRW45UUtjdW9ROCIsInR5cCI6IkpXVCIsIng1dCI6ImFYWXZyRFpMdGtNN0hoNTBCRW45UUtjdW9ROCJ9.eyJuYmYiOjE2MDQzODU5NDUsImV4cCI6MTkwNDM4NTk0NSwiaXNzIjoiaHR0cHM6Ly9sb2dpbi1zaG9yZS5zeW5lcmd5bWFyaW5ldGVzdC5jb20iLCJhdWQiOlsiaHR0cHM6Ly9sb2dpbi1zaG9yZS5zeW5lcmd5bWFyaW5ldGVzdC5jb20vcmVzb3VyY2VzIiwiSWRlbnRpdHlTZXJ2ZXJBcGkiLCJ3YXZlYXBpIl0sImNsaWVudF9pZCI6ImxlYXJuaW5nX3NlYWZhcmVyX2FwcCIsInN1YiI6ImVjOWY5YjljLTk2MzUtNDMwOC1iY2E0LWI5NjhjYjgwYmIzNSIsImF1dGhfdGltZSI6MTYwNDM4NTkxMywiaWRwIjoibG9jYWwiLCJyb2xlIjpbIlNvdXJjaW5nIEV4ZWN1dGl2ZSIsIkdyb3VwIEhlYWQiLCJTb3VyY2luZyBNYW5hZ2VyIl0sInVzZXJfaWQiOiJlYzlmOWI5Yy05NjM1LTQzMDgtYmNhNC1iOTY4Y2I4MGJiMzUiLCJmaXJzdF9uYW1lIjoiUHJhdGhhIiwibGFzdF9uYW1lIjoiQSIsImlzX2FjdGl2ZSI6IjAiLCJlbWFpbCI6InByYXRoYS5hdmFzaGlhQHNvbHV0ZWxhYnMuY29tIiwiZW1haWxfdmVyaWZpZWQiOiJUcnVlIiwidGVuYW50IjoiU3luZXJneSIsInJhbmsiOiJNYXN0ZXIiLCJyYW5rX2lkIjoiMSIsImRlcGFydG1lbnQiOiJNYW5uaW5nIiwiZGVwYXJ0bWVudF9pZCI6IjIiLCJjb21wYW55IjoiU3luZXJneSBNYXJpbmUiLCJjb21wYW55X2lkIjoiMSIsInVzZXJfdHlwZSI6IlNIT1JFIEVNUExPWUVFIiwic2NvcGUiOlsib3BlbmlkIiwicHJvZmlsZSIsImVtYWlsIiwiSWRlbnRpdHlTZXJ2ZXJBcGkiLCJ3YXZlYXBpIl0sImFtciI6WyJwd2QiXX0.QVOfYuvlJWGTt-P-5VpBl26PHn_EWaf20Vx8q6DIndXJO9DMAzflLsjWH8dsdkmUPbq-ezf_-X4T3w56rAvTPeVtpM5wHPMkbXrcpMjld6lwg6D5uf7DVYQOVd0Si6MA42hTUgg3rglhHH0I5-aNi4-1C12lriyCmqb_sMt-XRoN0z2H8SJqgyGZ3mwLMl9BhOb5YiNeTY98TxuQiMQPn-HflMROpoH9dYA5Ti2XGYw9dqTviuStU3FKsiq22vxbow7Tq6KEZcGb1i8TIUNIoYz6o-19WhvpJPE1Wbz-FdnysRjXAKZhG6kIk8_kSXeXEDwK9gm-QwRwwTfsyiA5iA";
                client.BaseAddress = new Uri(crewWageApiBaseUrl);
                client.DefaultRequestHeaders.Add("Authorization", authToken);
                //HTTP GET
                var response = await client.GetAsync("SalaryMatrix?vesselId="+ vesselId + "&seafarerId="+ seafarerId );
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
