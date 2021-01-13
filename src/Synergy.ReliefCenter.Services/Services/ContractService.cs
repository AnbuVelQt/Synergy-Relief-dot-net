using Microsoft.EntityFrameworkCore;
using Synergy.ReliefCenter.Core.Models;
using Synergy.ReliefCenter.Core.Models.Dtos;
using Synergy.ReliefCenter.Data.Repositories.Abstraction.ReliefRepository;
using Synergy.ReliefCenter.Services.Abstraction;
using System;
using System.Linq;
using System.Threading.Tasks;
using Synergy.ReliefCenter.Data.Repositories.Abstraction;
using AutoMapper;
using Synergy.ReliefCenter.Data.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Synergy.ReliefCenter.Services.Services
{
    public class ContractService : IContractService
    {
        private readonly IContractRepository _contractRepository;
        private readonly IContractFormRepository _contractFormRepository;
        private readonly IVesselDataRepository _vesselDataRepository;
        private readonly ISeafarerDataRepository _seafarerDataRepository;
        private readonly IMapper _mapper;

        public ContractService(
            IContractRepository contractRepository,
            IContractFormRepository contractFormRepository,
            IVesselDataRepository vesselRepository,
            ISeafarerDataRepository seafarerRepository,
            IMapper mapper
            )
        {
            _contractRepository = contractRepository;
            _contractFormRepository = contractFormRepository;
            _vesselDataRepository = vesselRepository;
            _seafarerDataRepository = seafarerRepository;
            _mapper = mapper;
        }
        public async Task<ContractDto> CreateContract(long vesselId, long seafarerId)
        {
            var response = new ContractDto();
            var vesselDetails =await _vesselDataRepository.GetVesselByIdAsync(vesselId);
            var seafarerDetails = await _seafarerDataRepository.GetSeafarerByIdAsync(seafarerId);
            var seafarerAllDetails = await _seafarerDataRepository.GetSeafarerContactDetailsByIdAsync(seafarerId);
            
            var cc = new ContractDto()
            {
                SeafarerId = seafarerAllDetails.SeafarerId,
                VesselId = vesselDetails.Id,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(90),
                Status = ContractStatus.InDraft,
                CreatedBy =1,
                CreatedOn = DateTime.UtcNow,
                Id = (_contractRepository.GetAllIncluding().OrderByDescending(x=>x.Id).FirstOrDefault().Id) +1
            };
            
            var saveContract = _mapper.Map<Contract>(cc);
            await _contractRepository.InsertAsync(saveContract);
            await _contractRepository.SaveAsync();
            
            
            var seafarer = new SeafarerDetailDto()
            {
                Id = seafarerDetails.Id,
                Address = seafarerAllDetails.Address,
                CDCNumber = seafarerDetails.CdcNumber,
                CrewCode = seafarerDetails.CrewCode,
                DateOfBirth = seafarerDetails.DateOfBirth,
                Name = seafarerDetails.FirstName + "" + seafarerDetails.LastName,
                Nationality = seafarerDetails.NationalityId.ToString(),
                PlaceOfBirth = seafarerDetails.PlaceOfBirth,
                Rank = seafarerDetails.RankId.ToString(),
                PassportNumber =null,
                Age = DateTime.Now.Year-seafarerDetails.DateOfBirth.Year
            };
            var vessels = new VesselDetailDto()
            {
                Id = vesselDetails.Id,
                Name = vesselDetails.Name,
                CBA = null,
                EmployerAgent = null,
                IMONumber = vesselDetails.ImoNumber,
                MLCHolder = null,
                Owner = vesselDetails.OwnerDetails.Address,
                PortOfRegistry = vesselDetails.PortDetails.Name
            };
            
            response.ContractForm = new ContractFormDto();
            response.ContractForm.Data = new ContractFormDataDto();
            response.ContractForm.Data.SeafarerDetail = seafarer;
            response.ContractForm.Data.VesselInfo = vessels;
            response.ContractForm.Data.TravelInfo = null;
            response.ContractForm.Data.AttachmentDetail = null;
            response.ContractForm.Data.Wages = null;
            
            var check = new ContractForm()
            {
                Id = (_contractFormRepository.GetAllIncluding().OrderByDescending(x => x.Id).FirstOrDefault().Id) + 1,
                ContractId = saveContract.Id,
                Data = JsonConvert.SerializeObject(response.ContractForm.Data)
            };
            
            var saveContractForm = _mapper.Map<ContractForm>(check);
            await _contractFormRepository.InsertAsync(saveContractForm);
            await _contractFormRepository.SaveAsync();
            
            return response;
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
