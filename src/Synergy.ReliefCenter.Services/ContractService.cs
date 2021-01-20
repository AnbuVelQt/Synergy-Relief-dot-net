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
using Newtonsoft.Json;
using System.Collections.Generic;
using Synergy.ReliefCenter.Data.Entities.SalaryMatrix;
using Synergy.ReliefCenter.Data.Models;
using ContractForm = Synergy.ReliefCenter.Data.Models.ContractForm;
using Synergy.Core.EmailService;
using System.IO;

namespace Synergy.ReliefCenter.Services
{
    public class ContractService : IContractService
    {
        private readonly IContractRepository _contractRepository;
        private readonly IContractFormRepository _contractFormRepository;
        private readonly IVesselDataRepository _vesselDataRepository;
        private readonly ISeafarerDataRepository _seafarerDataRepository;
        private readonly IMapper _mapper;
        private readonly IExternalSalaryMatrixRepository _externalSalaryMatrixRepository;
        private readonly IContractReviewerRepository _contractReviewerRepository;
        private readonly IEmailService _emailService;

        public ContractService(
            IContractRepository contractRepository,
            IContractFormRepository contractFormRepository,
            IVesselDataRepository vesselRepository,
            ISeafarerDataRepository seafarerRepository,
            IMapper mapper,
            IExternalSalaryMatrixRepository externalSalaryMatrixRepository,
            IContractReviewerRepository contractReviewerRepository,
            IEmailService emailService)
        {
            _contractRepository = contractRepository;
            _contractFormRepository = contractFormRepository;
            _vesselDataRepository = vesselRepository;
            _seafarerDataRepository = seafarerRepository;
            _mapper = mapper;
            _externalSalaryMatrixRepository = externalSalaryMatrixRepository;
            _contractReviewerRepository = contractReviewerRepository;
            _emailService = emailService;
        }


        public async Task<ContractDto> CreateContract(long vesselId, long seafarerId,string AuthToken)
        {
            var vesselDetails =await _vesselDataRepository.GetVesselByIdAsync(vesselId);
            var seafarerDetails = await _seafarerDataRepository.GetSeafarerByIdAsync(seafarerId);
            var seafarerAllDetails = await _seafarerDataRepository.GetSeafarerContactDetailsByIdAsync(seafarerId);
            var salarymatrix =await _externalSalaryMatrixRepository.GetSalaryMatrix(vesselId, seafarerId,AuthToken);
            
            var contractDto = new ContractDto()
            {
                SeafarerId = seafarerAllDetails.SeafarerId,
                VesselId = vesselDetails.Id,
                Status = ContractStatus.InDraft,
                Salary = salarymatrix.TotalMonthlyWages
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
            
            contractDto.ContractForm.Data.Wages = new ContractWagesDto()
            {
                BasicAmount = salarymatrix.BasicAmount,
                CBAEarningComponents = _mapper.Map<List<WageComponentDto>>(salarymatrix.CBAWageComponents.Where(x => x.Type.Equals(WageComponentType.Earning.ToString()))),
                OtherEarningComponents = _mapper.Map<List<WageComponentDto>>(salarymatrix.CompanyWageComponents.Where(x => x.Type.Equals(WageComponentType.Earning.ToString()))),
                DeductionComponents = _mapper.Map<List<WageComponentDto>>(salarymatrix.CBAWageComponents.Where(x => x.Type.Equals(WageComponentType.Deduction.ToString()))),
                SpecialAllownce = salarymatrix.SpecialAllowance,
                OTRateCard = _mapper.Map<OTRateCardDto>(salarymatrix.OTRate),
                TotalMonthlyAmount = salarymatrix.TotalMonthlyWages
            };
            
            var contractToCreate = _mapper.Map<VesselContract>(contractDto);
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
            var reviewers = await _contractReviewerRepository.GetAllIncluding().AsNoTracking().Where(x => x.ContractId == id).ToListAsync();
            var nextReviewer = reviewers.Where(x => x.Id == contract.NextReviewer).FirstOrDefault();
            ContractDetails = _mapper.Map<ContractDto>(contract);
            ContractDetails.ContractForm = _mapper.Map<ContractFormDto>(contractForm);
            ContractDetails.ContractForm.Data.ContractReviewers = _mapper.Map<List<ReviewersDto>>(reviewers);
            ContractDetails.ContractForm.Data.NextReviewer = _mapper.Map<ReviewersDto>(nextReviewer);
            return ContractDetails;
        }

        public async Task<ContractDto> GetConracts(long vesselId, long seafarerId)
        {
            var contracts = new ContractDto();
            var contract = await _contractRepository.GetAllIncluding().AsNoTracking().Where(x => x.VesselId == vesselId && x.SeafarerId == seafarerId && ((x.EndDate >= DateTime.UtcNow && x.StartDate < DateTime.UtcNow) || (x.StartDate ==null && x.EndDate == null))).OrderByDescending(x=>x.Id).FirstOrDefaultAsync();
            if (contract is null)
            {
                return null;
            }
            var contractForm = await _contractFormRepository.GetAllIncluding().AsNoTracking().Where(x => x.ContractId == contract.Id).FirstOrDefaultAsync();
            var reviewers = await _contractReviewerRepository.GetAllIncluding().AsNoTracking().Where(x => x.ContractId == contract.Id).ToListAsync();
            var nextReviewer = reviewers.Where(x => x.Id == contract.NextReviewer).FirstOrDefault();

            contracts = _mapper.Map <ContractDto>(contracts);
            contracts.ContractForm = _mapper.Map<ContractFormDto>(contractForm);
            contracts.ContractForm.Data.ContractReviewers = _mapper.Map<List<ReviewersDto>>(reviewers);
            contracts.ContractForm.Data.NextReviewer = _mapper.Map<ReviewersDto>(nextReviewer);
            return contracts;
        }

        public async Task UpdateContract(UpdateContractDto contractDto,long id)
        {
            var contract = _contractRepository.Get(id);
            var contractForm = await _contractFormRepository.GetAllIncluding().AsNoTracking().Where(x => x.ContractId == id).FirstOrDefaultAsync();
            
            var convertToDto = JsonConvert.DeserializeObject<ContractFormDataDto>(contractForm.Data);
            contract.StartDate = contractDto.TravelInfo.StartDate;
            contract.EndDate = contractDto.TravelInfo.EndDate;
            convertToDto.AttachmentDetail = _mapper.Map(contractDto.AttachmentDetail, convertToDto.AttachmentDetail);
            convertToDto.TravelInfo = _mapper.Map(contractDto.TravelInfo, convertToDto.TravelInfo);
            ContractWagesDto wage= new ContractWagesDto();
            List<WageComponentDto> otherEarnings = new List<WageComponentDto>();
            otherEarnings.AddRange(convertToDto.Wages.OtherEarningComponents.ToList());
            otherEarnings.AddRange(contractDto.Wages.OtherEarningComponents.ToList());
            wage.OtherEarningComponents = otherEarnings;
            List<WageComponentDto> deduction = new List<WageComponentDto>();
            deduction.AddRange(convertToDto.Wages.DeductionComponents.ToList());
            deduction.AddRange(contractDto.Wages.DeductionComponents.ToList());
            wage.DeductionComponents = deduction;
            wage.SpecialAllownce = convertToDto.Wages.SpecialAllownce + contractDto.Wages.SpecialAllowance;
            
            convertToDto.Wages.OtherEarningComponents = _mapper.Map(wage.OtherEarningComponents,convertToDto.Wages.OtherEarningComponents);
            convertToDto.Wages.DeductionComponents = _mapper.Map(wage.DeductionComponents, convertToDto.Wages.DeductionComponents);
            convertToDto.Wages.SpecialAllownce = _mapper.Map(wage.SpecialAllownce, convertToDto.Wages.SpecialAllownce);
            convertToDto.Wages = _mapper.Map<ContractWagesDto>(convertToDto.Wages);

            var contractToUpdate = _mapper.Map(_mapper.Map<VesselContract>(contract), contract);
            await _contractRepository.UpdateAsync(contractToUpdate);

            var contactDataDto = _mapper.Map<ContractFormDataDto>(convertToDto);
            contractForm.Data = JsonConvert.SerializeObject(contactDataDto);
            await _contractFormRepository.UpdateAsync(_mapper.Map<ContractForm>(contractForm));
            
            return;
        }

        public async Task AssignReviewers(long id, ContractReviewerSetDto reviewerSetDto)
        {
            var contract = _contractRepository.Get(id);
            var reviewer = new List<ContractReviewer>();
            foreach (var data in reviewerSetDto.Reviewers)
            {
                reviewer.Add(new ContractReviewer()
                {
                    ReviewerId = data.Id,
                    Role = data.Role.ToString(),
                    ContractId = id
                });

            }
            
            var reviewerToBeAdded = _mapper.Map<List<ContractReviewer>>(reviewer);
            foreach (var aa in reviewerToBeAdded)
            {
                await _contractReviewerRepository.InsertAsync(aa);

            }            
            await _contractReviewerRepository.SaveAsync();
            contract.Status = ContractStatus.InVerification.ToString();
            contract.NextReviewer = await _contractReviewerRepository.GetAllIncluding().Where(x => x.ContractId == id).OrderBy(z => z.Id).Select(x => x.Id).FirstOrDefaultAsync();
            var mapContract = _mapper.Map<VesselContract>(contract);
            await _contractRepository.UpdateAsync(mapContract);

            await SendEmail("abhishek.p@solutelabs.com");
            return;
        }

        private async Task SendEmail(string email)
        {
            SendingMailInfo sendingMailInfo = new SendingMailInfo();
            string[] To = { email };
            var path = Path.Combine(Directory.GetCurrentDirectory(), "OnBoarding.html");
            var reader = new StreamReader(path);
            var mailBody = reader.ReadToEnd();
            reader.Dispose();
            //mailBody = mailBody.Replace("{Logs}", htmlStr.ToString());
            sendingMailInfo.Body = mailBody;
            //sendingMailInfo.To = To.ToList();
            sendingMailInfo.To.Add(email);
            sendingMailInfo.Subject = "You have a contract to verify";
            sendingMailInfo.Name = "Abhishek Pandey";
            sendingMailInfo.From = "abhishek.p@solutelabs.com";
            sendingMailInfo.IsBodyHtml = true;            
            await _emailService.SendEmailAsync(sendingMailInfo);
        }
    }
}
