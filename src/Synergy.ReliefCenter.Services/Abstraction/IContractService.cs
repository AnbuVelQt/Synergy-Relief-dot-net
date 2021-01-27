using Synergy.ReliefCenter.Core.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synergy.ReliefCenter.Services.Abstraction
{
    public interface IContractService
    {
        Task<ContractDto> CreateContract(string vesselImoNumber, string seafarerCdcNumber, string AuthToken, string crewWageApiBaseUrl);

        Task<ContractDto> GetConract(long id, string apiKey, string userDetailsApiBaseUrl);

        Task UpdateContract(UpdateContractDto contractDto, long id);

        Task<ContractDto> GetConracts(string vesselImoNumber, string seafarerCdcNumber, string apiKey, string userDetailsApiBaseUrl);

        Task AssignReviewers(long id, ContractReviewerSetDto reviewerSetDto, string apiKey, string userDetailsApiBaseUrl);
    }
}
