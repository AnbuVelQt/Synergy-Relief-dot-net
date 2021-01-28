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
        Task<ContractDTO> CreateContract(string vesselImoNumber, string seafarerCdcNumber, string AuthToken, string crewWageApiBaseUrl);

        Task<ContractDTO> GetConract(long id, string apiKey, string userDetailsApiBaseUrl);

        Task UpdateContract(UpdateContractDTO contractDto, long id);

        Task<ContractDTO> GetConracts(string vesselImoNumber, string seafarerCdcNumber, string apiKey, string userDetailsApiBaseUrl);

        Task AssignReviewers(long id, ContractReviewerSetDTO reviewerSetDto, string apiKey, string userDetailsApiBaseUrl);
    }
}
