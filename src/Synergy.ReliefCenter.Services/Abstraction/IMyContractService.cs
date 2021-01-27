﻿using Synergy.ReliefCenter.Core.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synergy.ReliefCenter.Services.Abstraction
{
    public interface IMyContractService
    {
        Task<ContractDto> GetSeafarerConract(string imoNumber, string userId, string apiKey,string userDetailsApiBaseUrl);
        Task<IList<MyContractsDto>> GetSeafarerConracts(string imoNumber, string userId);
    }
}