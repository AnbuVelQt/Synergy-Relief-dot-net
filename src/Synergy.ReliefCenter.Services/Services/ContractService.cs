﻿using Microsoft.EntityFrameworkCore;
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
            IMapper mapper)
        {
            _contractRepository = contractRepository;
            _contractFormRepository = contractFormRepository;
            _vesselDataRepository = vesselRepository;
            _seafarerDataRepository = seafarerRepository;
            _mapper = mapper;
        }
        public async Task<ContractDto> CreateContract(long vesselId, long seafarerId)
        {
            var vesselDetails =await _vesselDataRepository.GetVesselByIdAsync(vesselId);
            var seafarerDetails = await _seafarerDataRepository.GetSeafarerByIdAsync(seafarerId);
            var seafarerAllDetails = await _seafarerDataRepository.GetSeafarerContactDetailsByIdAsync(seafarerId);
            
            var contractDto = new ContractDto()
            {
                SeafarerId = seafarerAllDetails.SeafarerId,
                VesselId = vesselDetails.Id,
                Status = ContractStatus.InDraft,
            };
            
           
            
            
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

            contractDto.ContractForm = new ContractFormDto();
            contractDto.ContractForm.Data = new ContractFormDataDto();
            contractDto.ContractForm.Data.SeafarerDetail = seafarer;
            contractDto.ContractForm.Data.VesselInfo = vessels;
            contractDto.ContractForm.Data.TravelInfo = new TravelDetailDto();
            contractDto.ContractForm.Data.AttachmentDetail = new ContractAttachmentDetailDto();
            contractDto.ContractForm.Data.Wages = new ContractWagesDto();
            
            var contractToCreate = _mapper.Map<Contract>(contractDto);
            await _contractRepository.InsertAsync(contractToCreate);
            await _contractRepository.SaveAsync();
            long ContractId = contractToCreate.Id;
            contractDto.ContractForm.ContractId = ContractId;
            
            var contractFormToCreate = _mapper.Map<ContractForm>(contractDto.ContractForm);
            await _contractFormRepository.InsertAsync(contractFormToCreate);
            await _contractFormRepository.SaveAsync();
            
            return contractDto;
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
            ContractDetails.EndDate = contract.EndDate;
            ContractDetails.Status = contract.Status;
            
            return ContractDetails;
        }
    }
}
