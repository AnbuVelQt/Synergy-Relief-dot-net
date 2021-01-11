using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Synergy.ReliefCenter.Core.Models;
using Synergy.ReliefCenter.Core.Models.Dtos;
using Synergy.ReliefCenter.Data.Interfaces.SeafarerRepository;
using Synergy.ReliefCenter.Data.Interfaces.VesselRepository;
using Synergy.ReliefCenter.Data.Repositories.Abstraction.ReliefRepository;
using Synergy.ReliefCenter.Services.Abstraction;
using System;
using System.Text.Json;
using System.Linq;
using System.Threading.Tasks;

namespace Synergy.ReliefCenter.Services.Services
{
    public class ContractService : IContractService
    {
        private readonly IContractRepository _contractRepository;
        private readonly IContractFormRepository _contractFormRepository;
        private readonly IVesselRepository _vesselRepository;
        private readonly ISeafarerRepository _seafarerRepository;

        public ContractService(
            IContractRepository contractRepository,
            IContractFormRepository contractFormRepository,
            IVesselRepository vesselRepository,
            ISeafarerRepository seafarerRepository
            )
        {
            _contractRepository = contractRepository;
            _contractFormRepository = contractFormRepository;
            _vesselRepository = vesselRepository;
            _seafarerRepository = seafarerRepository;
        }
        public async Task<ContractDto> CreateContract(long vesselId, long seafarerId)
        {
            var response = new ContractDto();
            var vesselDetails =await _vesselRepository.GetAllIncluding().Where(x => x.Id == vesselId).FirstOrDefaultAsync();
            var seafarerDetails =await _seafarerRepository.GetAllIncluding().Where(x => x.Id == seafarerId).FirstOrDefaultAsync();
            return null;
        }

        public async Task<ContractDto> GetConract(long id)
        {
            var ContractDetails = new ContractDto();
            var contract =await _contractRepository.GetAllIncluding().AsNoTracking().Where(x => x.Id == id).FirstOrDefaultAsync();
            var contractForm = await _contractFormRepository.GetAllIncluding().AsNoTracking().Where(x => x.ContractId == id).FirstOrDefaultAsync();
            var forecastData = System.Text.Json.JsonSerializer.Deserialize<ContractFormDataDto>(contractForm.Data, new System.Text.Json.JsonSerializerOptions { IgnoreNullValues = true, PropertyNameCaseInsensitive = true });
            //TODO:[Abhishek] implement changes for WagesComponent from CrewWage
            ContractDetails.ContractForm = new ContractFormDto();
            ContractDetails.ContractForm.Data = forecastData;
            ContractDetails.SeafarerId = contract.SeafarerId;
            ContractDetails.VesselId = contract.VesselId;
            ContractDetails.StartDate = contract.StartDate;
            ContractDetails.EndDate = contract.EndDate;
            ContractDetails.CreatedBy = contract.CreatedBy;
            ContractDetails.UpdatedBy = contract.UpdatedBy;
            ContractDetails.Status = (ContractStatus)Enum.Parse(typeof(ContractStatus), contract.Status);
            
            return ContractDetails;
        }
    }
}
