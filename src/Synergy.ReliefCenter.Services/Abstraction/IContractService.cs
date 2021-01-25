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
        Task<ContractDto> CreateContract(long vesselId, long seafarerId, string AuthToken);

        Task ApproveAsync(long contractId);

        Task<ContractDto> GetConract(long id);

        Task UpdateContract(UpdateContractDto contractDto, long id);

        Task<ContractDto> GetConracts(long vesselId,long seafarerId);
    }
}
