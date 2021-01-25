﻿using Synergy.AdobeSign.Models;
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
using Newtonsoft.Json;
using System.Collections.Generic;
using Synergy.ReliefCenter.Data.Entities.SalaryMatrix;
using Synergy.ReliefCenter.Data.Models;
using ContractForm = Synergy.ReliefCenter.Data.Models.ContractForm;
using Synergy.ReliefCenter.Services.Enums;
using Microsoft.Extensions.Configuration;
using Synergy.AdobeSign;
using Synergy.Core.EmailService;
using System.IO;
using Synergy.ReliefCenter.Data.Entities;
using Synergy.ReliefCenter.Data.Entities.Master;

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
        private readonly IConfiguration _configuration;
        private readonly IAdobeSignRestClient _adobeSignRestClient;
        private const string CONTRACT_DOC_ID_SECTION = "AbodeSign:ContractDocumentId";
        private readonly IContractReviewerRepository _contractReviewerRepository;
        private readonly IEmailService _emailService;
        private readonly IExternalUserDetailsRepository _externalUserDetailsRepository;

        public ContractService(
            IContractRepository contractRepository,
            IContractFormRepository contractFormRepository,
            IVesselDataRepository vesselRepository,
            ISeafarerDataRepository seafarerRepository,
            IMapper mapper,
            IExternalSalaryMatrixRepository externalSalaryMatrixRepository,
            IConfiguration configuration,
            IAdobeSignRestClient adobeSignRestClient,
            IContractReviewerRepository contractReviewerRepository,
            IEmailService emailService,
            IExternalUserDetailsRepository externalUserDetailsRepository)
        {
            _contractRepository = contractRepository;
            _contractFormRepository = contractFormRepository;
            _vesselDataRepository = vesselRepository;
            _seafarerDataRepository = seafarerRepository;
            _mapper = mapper;
            _externalSalaryMatrixRepository = externalSalaryMatrixRepository;
            _configuration = configuration;
            _adobeSignRestClient = adobeSignRestClient;
            _contractReviewerRepository = contractReviewerRepository;
            _emailService = emailService;
            _externalUserDetailsRepository = externalUserDetailsRepository;
        }

        public async Task<ContractDto> CreateContract(long vesselId, long seafarerId,string AuthToken)
        {
            var vesselDetails =await _vesselDataRepository.GetVesselByIdAsync(vesselId);
            var seafarerDetails = await _seafarerDataRepository.GetSeafarerByIdAsync(seafarerId);
            var seafarerAllDetails = await _seafarerDataRepository.GetSeafarerContactDetailsByIdAsync(seafarerId);
            var salarymatrix =await _externalSalaryMatrixRepository.GetSalaryMatrix(vesselId, seafarerId, authToken);
            
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
            contractDto.ContractForm.Data.RevisedSalaries = new List<RevisedSalaryDto>();
            
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

        public async Task ApproveAsync(long contractId)
        {
            var fileInfosList = new List<FileInformation>();
            string contractDocumentId = _configuration.GetSection(CONTRACT_DOC_ID_SECTION).Value;
            fileInfosList.Add(new FileInformation { libraryDocumentId = contractDocumentId });
            var participantSetsInfoList = new List<ParticipantInfo>();
            var memberInfoListFleetHead = new List<MemberInfo>();
            memberInfoListFleetHead.Add(new MemberInfo { email = "pentagram@synergyship.com" });
            participantSetsInfoList.Add(new ParticipantInfo { memberInfos = memberInfoListFleetHead, order = 1, role = Enum.GetName<AdobeRoleEnum>(AdobeRoleEnum.SIGNER), label = "Participant 1" });

            var memberInfoListSeafarer = new List<MemberInfo>();
            memberInfoListSeafarer.Add(new MemberInfo { email = "anbu.vel@qantler.com" });
            participantSetsInfoList.Add(new ParticipantInfo { memberInfos = memberInfoListSeafarer, order = 1, role = Enum.GetName<AdobeRoleEnum>(AdobeRoleEnum.SIGNER), label = "Participant 2" });
            var mergeFieldInfoList = new List<MergeFieldInfo>();

            var contractData = await GetConract(contractId);

            var FormData = contractData.ContractForm.Data;
            mergeFieldInfoList.Add(new MergeFieldInfo() { fieldName = "seafarerName", defaultValue = FormData.SeafarerDetail.Name });
            mergeFieldInfoList.Add(new MergeFieldInfo() { fieldName = "crewCode", defaultValue = FormData.SeafarerDetail.CrewCode });
            mergeFieldInfoList.Add(new MergeFieldInfo() { fieldName = "age", defaultValue = FormData.SeafarerDetail.Age.ToString() });
            mergeFieldInfoList.Add(new MergeFieldInfo() { fieldName = "dateOfBirth", defaultValue = FormData.SeafarerDetail.DateOfBirth.ToString() });
            mergeFieldInfoList.Add(new MergeFieldInfo() { fieldName = "placeOfBirth", defaultValue = FormData.SeafarerDetail.PlaceOfBirth });
            mergeFieldInfoList.Add(new MergeFieldInfo() { fieldName = "nationality", defaultValue = FormData.SeafarerDetail.Nationality });
            mergeFieldInfoList.Add(new MergeFieldInfo() { fieldName = "ppNo", defaultValue = FormData.SeafarerDetail.PassportNumber });
            mergeFieldInfoList.Add(new MergeFieldInfo() { fieldName = "cdcNo", defaultValue = FormData.SeafarerDetail.CDCNumber });
            mergeFieldInfoList.Add(new MergeFieldInfo() { fieldName = "rank", defaultValue = FormData.SeafarerDetail.Rank });
            mergeFieldInfoList.Add(new MergeFieldInfo() { fieldName = "seafarerAddress", defaultValue = FormData.SeafarerDetail.Address });
            mergeFieldInfoList.Add(new MergeFieldInfo() { fieldName = "vesselOwner", defaultValue = FormData.VesselInfo.Owner });
            mergeFieldInfoList.Add(new MergeFieldInfo() { fieldName = "employerAgent", defaultValue = FormData.VesselInfo.EmployerAgent });
            mergeFieldInfoList.Add(new MergeFieldInfo() { fieldName = "docHolder", defaultValue = FormData.VesselInfo.MLCHolder });
            mergeFieldInfoList.Add(new MergeFieldInfo() { fieldName = "vesselName", defaultValue = FormData.VesselInfo.Name });
            mergeFieldInfoList.Add(new MergeFieldInfo() { fieldName = "imoNo", defaultValue = FormData.VesselInfo.IMONumber.ToString() });
            mergeFieldInfoList.Add(new MergeFieldInfo() { fieldName = "cba", defaultValue = FormData.VesselInfo.CBA });
            mergeFieldInfoList.Add(new MergeFieldInfo() { fieldName = "portOfRegistry", defaultValue = FormData.VesselInfo.PortOfRegistry });
            mergeFieldInfoList.Add(new MergeFieldInfo() { fieldName = "placeOfEngagement", defaultValue = FormData.TravelInfo.PlaceOfEnagement });
            mergeFieldInfoList.Add(new MergeFieldInfo() { fieldName = "contractTerm", defaultValue = FormData.TravelInfo.ContractTerms });
            mergeFieldInfoList.Add(new MergeFieldInfo() { fieldName = "contractStartDate", defaultValue = FormData.TravelInfo.StartDate.ToString() });
            mergeFieldInfoList.Add(new MergeFieldInfo() { fieldName = "contractExpiryDate", defaultValue = FormData.TravelInfo.EndDate.ToString() });
            mergeFieldInfoList.Add(new MergeFieldInfo() { fieldName = "isKinDetailsProvided", defaultValue = FormData.AttachmentDetail.NextOfKinFormAttached.ToString() });
            mergeFieldInfoList.Add(new MergeFieldInfo() { fieldName = "isMedicalCertificateIssued", defaultValue = FormData.AttachmentDetail.MedicalCertificateAttached.ToString() });
            mergeFieldInfoList.Add(new MergeFieldInfo() { fieldName = "headerContractTitle", defaultValue = "Seaman's Employment Contract - " });
            mergeFieldInfoList.Add(new MergeFieldInfo() { fieldName = "headerEffectiveFrom", defaultValue = "Effective From: " });
            mergeFieldInfoList.Add(new MergeFieldInfo() { fieldName = "headerLicenseNo", defaultValue = "RPS-License No: " });
            mergeFieldInfoList.Add(new MergeFieldInfo() { fieldName = "headerCompanyName", defaultValue = "Synergy Maritime Recruitment Services Pvt Ltd, Delhi." });
            mergeFieldInfoList.Add(new MergeFieldInfo() { fieldName = "verifiedBy", defaultValue = "" });
            mergeFieldInfoList.Add(new MergeFieldInfo() { fieldName = "verifiedOn", defaultValue = "" });
            mergeFieldInfoList.Add(new MergeFieldInfo() { fieldName = "monthlyWagesHeader1", defaultValue = "monthlyWagesHeader1" });


            var agreementCreationInfo = new AgreementCreationInfo
            {
                fileInfos = fileInfosList,
                name = "Demo Check 199",
                participantSetsInfo = participantSetsInfoList,
                signatureType = Enum.GetName<AdobeSignatureTypeEnum>(AdobeSignatureTypeEnum.ESIGN),
                state = Enum.GetName<AdobeStateEnum>(AdobeStateEnum.IN_PROCESS),
                mergeFieldInfo = mergeFieldInfoList
            };
            var adobeCreateAgreementResponse = await _adobeSignRestClient.CreateAgreementAsync(agreementCreationInfo);
            string agreementId = adobeCreateAgreementResponse.Id;

            var contract = _contractRepository.Get(contractId);
            contract.ReferenceAgreementId = agreementId;
            var contractToUpdate = _mapper.Map(_mapper.Map<VesselContract>(contract), contract);
            await _contractRepository.UpdateAsync(contractToUpdate);

            return;
        }

        public async Task<ContractDto> GetConract(long id)
        {
            var ContractDetails = new ContractDto();
            var contract =await _contractRepository.GetAllIncluding().AsNoTracking().Where(x => x.Id == id).FirstOrDefaultAsync();
            var contractForm = await _contractFormRepository.GetAllIncluding().AsNoTracking().Where(x => x.ContractId == id).FirstOrDefaultAsync();
            var reviewers = await _contractReviewerRepository.GetAllIncluding().AsNoTracking().Where(x => x.ContractId == id).ToListAsync();

            var reviewer = new List<ReviewersDto>();
            var userInfo = new UserDetails();
            foreach (var data in reviewers)
            {
                userInfo = await _externalUserDetailsRepository.GetUserDetails(data.ReviewerId, "");
                reviewer.Add(new ReviewersDto()
                {
                    ReviewerId = userInfo is null ? data.ReviewerId : userInfo.Id,
                    Role = data.Role.ToString(),
                    Approved = data.Approved,
                    UserInfo = new UserInfoDto()
                    {
                        Id = userInfo is null ? data.ReviewerId : userInfo.Id,
                        Email = userInfo is null ? data.Email : userInfo.Email,
                        Name = userInfo is null ? data.Name : userInfo.Name
                    }
                });
            }

            ContractDetails = _mapper.Map<ContractDto>(contract);
            ContractDetails.ContractForm = _mapper.Map<ContractFormDto>(contractForm);
            ContractDetails.ContractForm.Data.ContractReviewers = _mapper.Map<List<ReviewersDto>>(reviewer);
            ContractDetails.ContractForm.Data.NextReviewer = _mapper.Map<ReviewersDto>(reviewer.Where(x => x.ReviewerId == reviewers.Where(x => x.Id == contract.NextReviewer).FirstOrDefault().ReviewerId).FirstOrDefault());
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
            
            var reviewer = new List<ReviewersDto>();
            var userInfo = new UserDetails();
            foreach (var data in reviewers)
            {
                userInfo = await _externalUserDetailsRepository.GetUserDetails(data.ReviewerId, "");
                reviewer.Add(new ReviewersDto()
                {
                    ReviewerId = userInfo is null ? data.ReviewerId : userInfo.Id,
                    Role = data.Role.ToString(),
                    Approved = data.Approved,
                    UserInfo = new UserInfoDto(){
                        Id = userInfo is null ? data.ReviewerId : userInfo.Id,
                        Email = userInfo is null ? data.Email :userInfo.Email,
                        Name = userInfo is null ? data.Name : userInfo.Name
                    }                    
                });
            }

            contracts = _mapper.Map <ContractDto>(contracts);
            contracts.ContractForm = _mapper.Map<ContractFormDto>(contractForm);
            contracts.ContractForm.Data.ContractReviewers = _mapper.Map<List<ReviewersDto>>(reviewer);
            contracts.ContractForm.Data.NextReviewer = _mapper.Map<ReviewersDto>(reviewer.Where(x => x.ReviewerId == reviewers.Where(x => x.Id == contract.NextReviewer).FirstOrDefault().ReviewerId).FirstOrDefault());
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

            List<RevisedSalaryDto> revisedSalaries = new List<RevisedSalaryDto>();
            revisedSalaries.AddRange(contractDto.RevisedSalaries);
            revisedSalaries.AddRange(convertToDto.RevisedSalaries);
            
            convertToDto.Wages.OtherEarningComponents = _mapper.Map(wage.OtherEarningComponents,convertToDto.Wages.OtherEarningComponents);
            convertToDto.Wages.DeductionComponents = _mapper.Map(wage.DeductionComponents, convertToDto.Wages.DeductionComponents);
            convertToDto.Wages.SpecialAllownce = _mapper.Map(wage.SpecialAllownce, convertToDto.Wages.SpecialAllownce);
            convertToDto.Wages = _mapper.Map<ContractWagesDto>(convertToDto.Wages);
            convertToDto.RevisedSalaries = _mapper.Map<List<RevisedSalaryDto>>(revisedSalaries);

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
            var userInfo = new UserDetails();
            foreach (var data in reviewerSetDto.Reviewers)
            {
                userInfo = await _externalUserDetailsRepository.GetUserDetails(data.Id, "");
                reviewer.Add(new ContractReviewer()
                {
                    ReviewerId = data.Id,
                    Role = data.Role.ToString(),
                    ContractId = id,
                    Email = userInfo.Email,
                    Name = userInfo.Name
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

            await SendEmail(reviewerToBeAdded.Select(x=>x.Email).FirstOrDefault(),mapContract);
            return;
        }

        private async Task SendEmail(string email,VesselContract contract)
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
