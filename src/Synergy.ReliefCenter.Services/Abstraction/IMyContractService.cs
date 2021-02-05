using Synergy.ReliefCenter.Core.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synergy.ReliefCenter.Services.Abstraction
{
    public interface IMyContractService
    {
        Task<ContractDTO> GetSeafarerConract(string imoNumber, string userId);
        Task<IList<MyContractsDTO>> GetSeafarerConracts(string imoNumber, string userId);
    }
}
