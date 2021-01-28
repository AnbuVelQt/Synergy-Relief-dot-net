using Microsoft.EntityFrameworkCore;
using Synergy.AdobeSign.Models;
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
//using Synergy.Core.EmailService;
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
        private const string USER_DETAILS_APIURL_SECTION = "UserDetails:ApiUrl";
        private const string USER_DETAILS_APIKEY_SECTION = "UserDetails:ApiKey";
        private readonly IContractReviewerRepository _contractReviewerRepository;
        //private readonly IEmailService _emailService;
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
            //IEmailService emailService,
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
            //_emailService = emailService;
            _externalUserDetailsRepository = externalUserDetailsRepository;
        }


        public async Task<ContractDto> CreateContract(long vesselId, long seafarerId,string AuthToken, string crewWageApiBaseUrl)
        {
            var vesselDetails =await _vesselDataRepository.GetVesselByIdAsync(vesselId);
            var seafarerDetails = await _seafarerDataRepository.GetSeafarerByIdAsync(seafarerId);
            var seafarerAllDetails = await _seafarerDataRepository.GetSeafarerContactDetailsByIdAsync(seafarerId);
            var salarymatrix =await _externalSalaryMatrixRepository.GetSalaryMatrix(vesselId, seafarerId,AuthToken, crewWageApiBaseUrl);
            
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
                Name = seafarerDetails.FirstName + " " + seafarerDetails.LastName,
                Nationality = seafarerDetails.NationalityId.ToString(),
                PlaceOfBirth = seafarerDetails.PlaceOfBirth,
                Rank = seafarerDetails.RankId.ToString(),
                PassportNumber =null,
                Age = DateTime.Now.Year-seafarerDetails.DateOfBirth.Year,
                Email = seafarerAllDetails.Email,
                Phone = seafarerAllDetails.Phone
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
            memberInfoListFleetHead.Add(new MemberInfo { email = "anbu.vel@qantler.com" });
            participantSetsInfoList.Add(new ParticipantInfo { memberInfos = memberInfoListFleetHead, order = 1, role = Enum.GetName<AdobeRoleEnum>(AdobeRoleEnum.SIGNER), label = "Participant 1" });

            string userDetailsApiBaseUrl = _configuration.GetSection(USER_DETAILS_APIURL_SECTION).Value;
            string userDetailsApiKey = _configuration.GetSection(USER_DETAILS_APIKEY_SECTION).Value;
            var contractData = await GetConract(contractId,userDetailsApiKey, userDetailsApiBaseUrl);

            var FormData = contractData.ContractForm.Data;
            var memberInfoListSeafarer = new List<MemberInfo>();
            memberInfoListSeafarer.Add(new MemberInfo { email = FormData.SeafarerDetail.Email });
            participantSetsInfoList.Add(new ParticipantInfo { memberInfos = memberInfoListSeafarer, order = 1, role = Enum.GetName<AdobeRoleEnum>(AdobeRoleEnum.SIGNER), label = "Participant 2" });
            var mergeFieldInfoList = new List<MergeFieldInfo>();

            mergeFieldInfoList.Add(new MergeFieldInfo() { fieldName = "seafarerName", defaultValue = FormData.SeafarerDetail.Name });
            mergeFieldInfoList.Add(new MergeFieldInfo() { fieldName = "crewCode", defaultValue = FormData.SeafarerDetail.CrewCode });
            mergeFieldInfoList.Add(new MergeFieldInfo() { fieldName = "age", defaultValue = FormData.SeafarerDetail.Age.ToString() });
            mergeFieldInfoList.Add(new MergeFieldInfo() { fieldName = "dateOfBirth", defaultValue = convertDateString(FormData.SeafarerDetail.DateOfBirth) });
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
            mergeFieldInfoList.Add(new MergeFieldInfo() { fieldName = "currentCBANo", defaultValue = FormData.VesselInfo.CBA });
            mergeFieldInfoList.Add(new MergeFieldInfo() { fieldName = "portOfRegistry", defaultValue = FormData.VesselInfo.PortOfRegistry });
            mergeFieldInfoList.Add(new MergeFieldInfo() { fieldName = "placeOfEngagement", defaultValue = FormData.TravelInfo.PlaceOfEnagement });
            mergeFieldInfoList.Add(new MergeFieldInfo() { fieldName = "contractTerm", defaultValue = FormData.TravelInfo.ContractTerms });
            mergeFieldInfoList.Add(new MergeFieldInfo() { fieldName = "contractStartDate", defaultValue = convertDateString(FormData.TravelInfo.StartDate) });
            mergeFieldInfoList.Add(new MergeFieldInfo() { fieldName = "contractExpiryDate", defaultValue = convertDateString(FormData.TravelInfo.EndDate) });
            mergeFieldInfoList.Add(new MergeFieldInfo() { fieldName = "isKinDetailsProvided", defaultValue = FormData.AttachmentDetail.NextOfKinFormAttached.ToString() });
            mergeFieldInfoList.Add(new MergeFieldInfo() { fieldName = "isMedicalCertificateIssued", defaultValue = FormData.AttachmentDetail.MedicalCertificateAttached.ToString() });
            
            //Need set these as dynamic after the fields ready
            mergeFieldInfoList.Add(new MergeFieldInfo() { fieldName = "headerContractTitle", defaultValue = "Seaman's Employment Contract - " });
            mergeFieldInfoList.Add(new MergeFieldInfo() { fieldName = "headerEffectiveFrom", defaultValue = "Effective From: " });
            mergeFieldInfoList.Add(new MergeFieldInfo() { fieldName = "headerLicenseNo", defaultValue = "RPS-License No: " });
            mergeFieldInfoList.Add(new MergeFieldInfo() { fieldName = "headerCompanyName", defaultValue = "Synergy Maritime Recruitment Services Pvt Ltd, Delhi." });
            mergeFieldInfoList.Add(new MergeFieldInfo() { fieldName = "verifiedBy", defaultValue = "" });
            mergeFieldInfoList.Add(new MergeFieldInfo() { fieldName = "verifiedOn", defaultValue = "" });

            //Wages component table section
            mergeFieldInfoList.Add(new MergeFieldInfo() { fieldName = "monthlyWagesHeader1", defaultValue = "Basic Wages" });
            mergeFieldInfoList.Add(new MergeFieldInfo() { fieldName = "monthlyWagesValue1", defaultValue = FormData.Wages.BasicAmount.ToString() });
            mergeFieldInfoList.Add(new MergeFieldInfo() { fieldName = "monthlyWagesHeader2", defaultValue = "Special Allownce" });
            mergeFieldInfoList.Add(new MergeFieldInfo() { fieldName = "monthlyWagesValue2", defaultValue = FormData.Wages.SpecialAllownce.ToString() });
            mergeFieldInfoList.Add(new MergeFieldInfo() { fieldName = "totalMonthlyWages", defaultValue = FormData.Wages.TotalMonthlyAmount.ToString() });

            int otherEarningsSNo = 0;
            foreach (var data in FormData.Wages.OtherEarningComponents)
            {
                otherEarningsSNo++;
                mergeFieldInfoList.Add(new MergeFieldInfo() { fieldName = "otherEarningsSNo"+ otherEarningsSNo.ToString(), defaultValue = otherEarningsSNo.ToString() });
                mergeFieldInfoList.Add(new MergeFieldInfo() { fieldName = "otherEarningsTitle"+ otherEarningsSNo.ToString(), defaultValue = data.Name });
                mergeFieldInfoList.Add(new MergeFieldInfo() { fieldName = "otherEarningsAmount"+ otherEarningsSNo.ToString(), defaultValue = data.Amount.ToString() });
                mergeFieldInfoList.Add(new MergeFieldInfo() { fieldName = "otherEarningsDate" + otherEarningsSNo.ToString(), defaultValue = "Not Available" });
            }

            int deductionsSNo = 0;
            foreach (var data in FormData.Wages.DeductionComponents)
            {
                deductionsSNo++;
                mergeFieldInfoList.Add(new MergeFieldInfo() { fieldName = "deductionsSNo" + deductionsSNo.ToString(), defaultValue = deductionsSNo.ToString() });
                mergeFieldInfoList.Add(new MergeFieldInfo() { fieldName = "deductionsTitle" + deductionsSNo.ToString(), defaultValue = data.Name });
                mergeFieldInfoList.Add(new MergeFieldInfo() { fieldName = "deductionsAmount" + deductionsSNo.ToString(), defaultValue = data.Amount.ToString() });
                mergeFieldInfoList.Add(new MergeFieldInfo() { fieldName = "deductionsDate" + deductionsSNo.ToString(), defaultValue = "Not Available" });
            }

            int revisedSalarySNo = 0;
            foreach (var data in FormData.RevisedSalaries)
            {
                revisedSalarySNo++;
                mergeFieldInfoList.Add(new MergeFieldInfo() { fieldName = "revisedSalarySNo" + revisedSalarySNo.ToString(), defaultValue = revisedSalarySNo.ToString() });
                mergeFieldInfoList.Add(new MergeFieldInfo() { fieldName = "revisedSalaryEffectiveFrom" + revisedSalarySNo.ToString(), defaultValue = convertDateString(data.EffectiveFromDate) });
                mergeFieldInfoList.Add(new MergeFieldInfo() { fieldName = "revisedSalaryAmount" + revisedSalarySNo.ToString(), defaultValue = data.TotalMonthlyWage.ToString() });
                mergeFieldInfoList.Add(new MergeFieldInfo() { fieldName = "revisedSalaryRemarks" + revisedSalarySNo.ToString(), defaultValue = data.ReasonForRevision });
            }

            var agreementCreationInfo = new AgreementCreationInfo
            {
                fileInfos = fileInfosList,
                name = "Demo Check 201",
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

        public static string convertDateString(DateTime dateToConvert)
        {
            string dateString = dateToConvert.ToString(), convertedDateString = "";
            if (dateString != "01-01-0001 00:00:00") {
                convertedDateString = dateToConvert.ToString("dd'/'MMM'/'yyyy");
            }
            return convertedDateString;
        }

        public async Task<ContractDto> GetConract(long id, string apiKey, string userDetailsApiBaseUrl)
        {
            var ContractDetails = new ContractDto();
            var contract =await _contractRepository.GetAllIncluding().AsNoTracking().Where(x => x.Id == id).FirstOrDefaultAsync();
            var contractForm = await _contractFormRepository.GetAllIncluding().AsNoTracking().Where(x => x.ContractId == id).FirstOrDefaultAsync();
            var reviewers = await _contractReviewerRepository.GetAllIncluding().AsNoTracking().Where(x => x.ContractId == id).ToListAsync();

            var reviewer = new List<ReviewersDto>();
            var userInfo = new UserDetails();
            foreach (var data in reviewers)
            {
                userInfo = await _externalUserDetailsRepository.GetUserDetails(data.ReviewerId, apiKey,userDetailsApiBaseUrl);
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

        public async Task<ContractDto> GetConracts(long vesselId, long seafarerId, string apiKey, string userDetailsApiBaseUrl)
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
                userInfo = await _externalUserDetailsRepository.GetUserDetails(data.ReviewerId, apiKey,userDetailsApiBaseUrl);
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

            contracts = _mapper.Map <ContractDto>(contract);
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

        public async Task AssignReviewers(long id, ContractReviewerSetDto reviewerSetDto, string apiKey, string userDetailsApiBaseUrl)
        {
            var contract = _contractRepository.Get(id);
            var contractForm =await _contractFormRepository.GetAllIncluding().Where(x => x.ContractId == id).FirstOrDefaultAsync();
            var reviewer = new List<ContractReviewer>();
            var userInfo = new UserDetails();
            foreach (var data in reviewerSetDto.Reviewers)
            {
                userInfo = await _externalUserDetailsRepository.GetUserDetails(data.Id, apiKey,userDetailsApiBaseUrl);
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

            await SendEmail(reviewerToBeAdded.Select(x=>x.Email).FirstOrDefault(),_mapper.Map<ContractFormDto>(contractForm));
            return;
        }

        private async Task SendEmail(string email,ContractFormDto contract)
        {
            //SendingMailInfo sendingMailInfo = new SendingMailInfo();
            //var path = Path.Combine(Directory.GetCurrentDirectory(),"Templates" ,"TravelDetails.html");
            //var reader = new StreamReader(path);
            //var mailBody = reader.ReadToEnd();
            //reader.Dispose();
            //mailBody = mailBody.Replace("{NAME}", contract.Data.SeafarerDetail.Name);
            //mailBody = mailBody.Replace("{EMAIL}", contract.Data.SeafarerDetail.Email);
            //mailBody = mailBody.Replace("{AGE}", contract.Data.SeafarerDetail.Age.ToString());
            //sendingMailInfo.Body = mailBody;
            //sendingMailInfo.To.Add("abhishek.p@solutelabs.com");
            //sendingMailInfo.Subject = "Seafarer Profile Assigned for Approval";
            //sendingMailInfo.Name = "Synergy Marine";
            //sendingMailInfo.From = "support@synergymarinetest.com";
            //sendingMailInfo.IsBodyHtml = true;
            //await _emailService.SendEmailAsync(sendingMailInfo);
        }

        
    }
}
