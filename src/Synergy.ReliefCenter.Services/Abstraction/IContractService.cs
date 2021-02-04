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
        Task ApproveAsync(long contractId);
        Task<ContractDTO> CreateContract(string vesselImoNumber, string seafarerCdcNumber, string AuthToken);

        Task<ContractDTO> GetConract(long id);

        Task UpdateContract(UpdateContractDTO contractDto, long id);

        Task<ContractDTO> GetConracts(string vesselImoNumber, string seafarerCdcNumber);

        Task AssignReviewers(long id, ContractReviewerSetDTO reviewerSetDto);

        Task<object> ApproveContract(long id,string userId);

        Task<object> VerifyContract(long id, string userId);

        Task RejectContract(long id, string userId, string comment);
    }
}
