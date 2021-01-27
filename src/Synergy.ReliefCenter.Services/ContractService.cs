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
        private readonly IContractReviewerRepository _contractReviewerRepository;
        //private readonly IEmailService _emailService;
        private readonly IExternalUserDetailsRepository _externalUserDetailsRepository;
        private readonly IMasterDataRepository _masterDataRepository;

        public ContractService(
            IContractRepository contractRepository,
            IContractFormRepository contractFormRepository,
            IVesselDataRepository vesselRepository,
            ISeafarerDataRepository seafarerRepository,
            IMapper mapper,
            IExternalSalaryMatrixRepository externalSalaryMatrixRepository,
            IContractReviewerRepository contractReviewerRepository,
            //IEmailService emailService,
            IExternalUserDetailsRepository externalUserDetailsRepository,
            IMasterDataRepository masterDataRepository)
        {
            _contractRepository = contractRepository;
            _contractFormRepository = contractFormRepository;
            _vesselDataRepository = vesselRepository;
            _seafarerDataRepository = seafarerRepository;
            _mapper = mapper;
            _externalSalaryMatrixRepository = externalSalaryMatrixRepository;
            _contractReviewerRepository = contractReviewerRepository;
            //_emailService = emailService;
            _externalUserDetailsRepository = externalUserDetailsRepository;
            _masterDataRepository = masterDataRepository;
        }


        public async Task<ContractDto> CreateContract(string vesselImoNumber, string seafarerCdcNumber,string AuthToken, string crewWageApiBaseUrl)
        {
            var vesselDetails =await _vesselDataRepository.GetVesselByIdAsync(vesselImoNumber);
            var seafarerDetails = await _seafarerDataRepository.GetSeafarerByIdAsync(seafarerCdcNumber);
            var seafarerAllDetails = await _seafarerDataRepository.GetSeafarerContactDetailsByIdAsync(seafarerDetails.Id);
            var salarymatrix =await _externalSalaryMatrixRepository.GetSalaryMatrix(vesselDetails.Id, seafarerDetails.Id,AuthToken, crewWageApiBaseUrl);
            
            var contractDto = new ContractDto()
            {
                SeafarerId = seafarerAllDetails.SeafarerId,
                VesselId = vesselDetails.Id,
                Status = ContractStatus.InDraft,
                Salary = salarymatrix.TotalMonthlyWages,
                CdcNumber = seafarerDetails.CdcNumber,
                ImoNumber = vesselDetails.ImoNumber.ToString()
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
                PassportNumber =  _seafarerDataRepository.GetSeafarerDocumentsByIdAsync(seafarerDetails.Id, _masterDataRepository.GetDocumentCategoryByIdAsync("Passport")).Result.Number,
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

        public async Task<ContractDto> GetConracts(string vesselImoNumber, string seafarerCdcNumber, string apiKey, string userDetailsApiBaseUrl)
        {
            var contracts = new ContractDto();
            var contract = await _contractRepository.GetAllIncluding().AsNoTracking().Where(x => x.ImoNumber == vesselImoNumber && x.CdcNumber == seafarerCdcNumber && ((x.EndDate >= DateTime.UtcNow && x.StartDate < DateTime.UtcNow) || (x.StartDate ==null && x.EndDate == null))).OrderByDescending(x=>x.Id).FirstOrDefaultAsync();
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
