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
using Microsoft.Extensions.Configuration;
using Synergy.AdobeSign;
using System.IO;
using Synergy.ReliefCenter.Data.Entities.Master;
using Synergy.Core.EmailService;
using Synergy.ReliefCenter.Core.Domain.Models;

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
        private readonly IContractReviewerRepository _contractReviewerRepository;
        private readonly IEmailService _emailService;
        private readonly IExternalUserDetailsRepository _externalUserDetailsRepository;
        private readonly IMasterDataRepository _masterDataRepository;
        private readonly IApiRequestContext _apiRequestContext;

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
            IExternalUserDetailsRepository externalUserDetailsRepository,
            IMasterDataRepository masterDataRepository,
            IApiRequestContext apiRequestContext)
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
            _masterDataRepository = masterDataRepository;
            _apiRequestContext = apiRequestContext;
        }


        public async Task<ContractDTO> CreateContract(string vesselImoNumber, string seafarerCdcNumber,string AuthToken)
        {
            var contract = await _contractRepository.GetAllIncluding().AsNoTracking().Where(x => x.ImoNumber == vesselImoNumber && x.CdcNumber == seafarerCdcNumber && ((x.EndDate >= DateTime.UtcNow) || (x.StartDate == null && x.EndDate == null)) && x.Status != ContractStatus.Cancelled.ToString()).OrderByDescending(x => x.Id).FirstOrDefaultAsync();
            if (contract != null)
            {
                //return null;
            }
            var vesselDetails =await _vesselDataRepository.GetVesselByIdAsync(vesselImoNumber);
            var seafarerDetails = await _seafarerDataRepository.GetSeafarerByIdAsync(seafarerCdcNumber);
            var seafarerAllDetails = await _seafarerDataRepository.GetSeafarerContactDetailsByIdAsync(seafarerDetails.Id);
            var salarymatrix =await _externalSalaryMatrixRepository.GetSalaryMatrix(vesselImoNumber, seafarerCdcNumber,AuthToken);
            
            var contractDto = new ContractDTO()
            {
                SeafarerId = seafarerAllDetails.SeafarerId,
                VesselId = vesselDetails.Id,
                Status = ContractStatus.InDraft,
                Salary = salarymatrix.TotalMonthlyWages,
                CdcNumber = seafarerDetails.CdcNumber,
                ImoNumber = vesselDetails.ImoNumber.ToString()
            };
           
            var seafarer = new SeafarerDetailDTO()
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

            var vessels = new VesselDetailDTO()
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

            contractDto.ContractForm = new ContractFormDTO();
            contractDto.ContractForm.Data = new ContractFormDataDTO();
            contractDto.ContractForm.Data.SeafarerDetail = seafarer;
            contractDto.ContractForm.Data.VesselInfo = vessels;
            contractDto.ContractForm.Data.TravelInfo = new TravelDetailDTO();
            contractDto.ContractForm.Data.AttachmentDetail = new ContractAttachmentDetailDTO();
            contractDto.ContractForm.Data.RevisedSalaries = new List<RevisedSalaryDTO>();
            
            contractDto.ContractForm.Data.Wages = new ContractWagesDTO()
            {
                BasicAmount = salarymatrix.BasicAmount,
                CBAEarningComponents = _mapper.Map<List<WageComponentDTO>>(salarymatrix.CBAWageComponents.Where(x => x.Type.Equals(WageComponentType.Earning.ToString()))),
                OtherEarningComponents = _mapper.Map<List<WageComponentDTO>>(salarymatrix.CompanyWageComponents.Where(x => x.Type.Equals(WageComponentType.Earning.ToString()))),
                DeductionComponents = _mapper.Map<List<WageComponentDTO>>(salarymatrix.CBAWageComponents.Where(x => x.Type.Equals(WageComponentType.Deduction.ToString()))),
                SpecialAllownce = salarymatrix.SpecialAllowance,
                OTRateCard = _mapper.Map<OTRateCardDTO>(salarymatrix.OTRate),
                TotalMonthlyAmount = salarymatrix.TotalMonthlyWages
            };
            
            var contractToCreate = _mapper.Map<VesselContract>(contractDto);
            contractToCreate.CreatedAt = DateTime.UtcNow;
            contractToCreate.CreatedById = _apiRequestContext.UserId;
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
            var contractData = await GetConract(contractId);
            AgreementCreationInfo agreementCreationInfo = _mapper.Map<AgreementCreationInfo>(contractData);
            var adobeCreateAgreementResponse = await _adobeSignRestClient.CreateAgreementAsync(agreementCreationInfo);
            string agreementId = adobeCreateAgreementResponse.Id;
            var contract = _contractRepository.Get(contractId);
            contract.ReferenceAgreementId = agreementId;
            var contractToUpdate = _mapper.Map(_mapper.Map<VesselContract>(contract), contract);
            await _contractRepository.UpdateAsync(contractToUpdate);

            return;
        }

        public async Task<ContractDTO> GetConract(long id)
        {
            var ContractDetails = new ContractDTO();
            var contract =await _contractRepository.GetAllIncluding().AsNoTracking().Where(x => x.Id == id).FirstOrDefaultAsync();
            var contractForm = await _contractFormRepository.GetAllIncluding().AsNoTracking().Where(x => x.ContractId == id).FirstOrDefaultAsync();
            var reviewers = await _contractReviewerRepository.GetAllIncluding().AsNoTracking().Where(x => x.ContractId == id).ToListAsync();

            var reviewer = new List<ReviewersDTO>();
            var userInfo = new UserDetails();
            foreach (var data in reviewers)
            {
                userInfo = await _externalUserDetailsRepository.GetUserDetails(data.ReviewerId);
                reviewer.Add(new ReviewersDTO()
                {
                    ReviewerId = userInfo.Id is null ? data.ReviewerId : userInfo.Id,
                    Role = data.Role.ToString(),
                    Approved = data.Approved,
                    UserInfo = new UserInfoDTO()
                    {
                        Id = userInfo.Id is null ? data.ReviewerId : userInfo.Id,
                        Email = userInfo.Email is null ? data.Email : userInfo.Email,
                        Name = userInfo.Name is null ? data.Name : userInfo.Name
                    }
                });
            }
            
            ContractDetails = _mapper.Map<ContractDTO>(contract);
            if (reviewers.Count > 0)
            {
                ContractDetails.VerifierEmail = reviewers.Where(x => x.Role.Equals(ReviewerRole.FleetHead.ToString())).FirstOrDefault().Email;
                ContractDetails.VerifierName = reviewers.Where(x => x.Role.Equals(ReviewerRole.FleetHead.ToString())).FirstOrDefault().Name;
                ContractDetails.VerifyDate = reviewers.Where(x => x.Role.Equals(ReviewerRole.FleetHead.ToString())).FirstOrDefault().ApprovedOn;

            }
            ContractDetails.ContractForm = _mapper.Map<ContractFormDTO>(contractForm);
            ContractDetails.ContractForm.Data.ContractReviewers = _mapper.Map<List<ReviewersDTO>>(reviewer);
            ContractDetails.ContractForm.Data.NextReviewer = _mapper.Map<ReviewersDTO>(reviewer.Where(x => x.ReviewerId == reviewers.Where(x => x.Id == contract.NextReviewer).FirstOrDefault().ReviewerId).FirstOrDefault());
            return ContractDetails;
        }

        public async Task<ContractDTO> GetConracts(string vesselImoNumber, string seafarerCdcNumber)
        {
            var contracts = new ContractDTO();
            var contract = await _contractRepository.GetAllIncluding().AsNoTracking().Where(x => x.ImoNumber == vesselImoNumber && x.CdcNumber == seafarerCdcNumber && ((x.EndDate >= DateTime.UtcNow) || (x.StartDate ==null && x.EndDate == null)) && x.Status != ContractStatus.Cancelled.ToString()).OrderByDescending(x=>x.Id).FirstOrDefaultAsync();
            if (contract is null)
            {
                return null;
            }
            var contractForm = await _contractFormRepository.GetAllIncluding().AsNoTracking().Where(x => x.ContractId == contract.Id).FirstOrDefaultAsync();
            var reviewers = await _contractReviewerRepository.GetAllIncluding().AsNoTracking().Where(x => x.ContractId == contract.Id).ToListAsync();
            
            var reviewer = new List<ReviewersDTO>();
            var userInfo = new UserDetails();
            foreach (var data in reviewers)
            {
                userInfo = await _externalUserDetailsRepository.GetUserDetails(data.ReviewerId);
                reviewer.Add(new ReviewersDTO()
                {
                    ReviewerId = userInfo is null ? data.ReviewerId : userInfo.Id,
                    Role = data.Role.ToString(),
                    Approved = data.Approved,
                    UserInfo = new UserInfoDTO(){
                        Id = userInfo is null ? data.ReviewerId : userInfo.Id,
                        Email = userInfo is null ? data.Email :userInfo.Email,
                        Name = userInfo is null ? data.Name : userInfo.Name
                    }                    
                });
            }
                        
            contracts = _mapper.Map <ContractDTO>(contract);
            if (reviewers.Count > 0)
            {
                contracts.VerifierEmail = reviewers.Where(x => x.Role.Equals(ReviewerRole.FleetHead.ToString())).FirstOrDefault().Email;
                contracts.VerifierName = reviewers.Where(x => x.Role.Equals(ReviewerRole.FleetHead.ToString())).FirstOrDefault().Name;
                contracts.VerifyDate = reviewers.Where(x => x.Role.Equals(ReviewerRole.FleetHead.ToString())).FirstOrDefault().ApprovedOn;
            }

            contracts.ContractForm = _mapper.Map<ContractFormDTO>(contractForm);
            contracts.ContractForm.Data.ContractReviewers = _mapper.Map<List<ReviewersDTO>>(reviewer);
            contracts.ContractForm.Data.NextReviewer = _mapper.Map<ReviewersDTO>(reviewer.Where(x => x.ReviewerId == reviewers.Where(x => x.Id == contract.NextReviewer).FirstOrDefault().ReviewerId).FirstOrDefault());
            return contracts;
        }

        public async Task UpdateContract(UpdateContractDTO contractDto,long id)
        {
            var contract = _contractRepository.Get(id);
            var contractForm = await _contractFormRepository.GetAllIncluding().AsNoTracking().Where(x => x.ContractId == id).FirstOrDefaultAsync();
            
            var convertToDto = JsonConvert.DeserializeObject<ContractFormDataDTO>(contractForm.Data);
            contract.StartDate = contractDto.TravelInfo.StartDate;
            contract.EndDate = contractDto.TravelInfo.EndDate;
            convertToDto.AttachmentDetail = _mapper.Map(contractDto.AttachmentDetail, convertToDto.AttachmentDetail);
            convertToDto.TravelInfo = _mapper.Map(contractDto.TravelInfo, convertToDto.TravelInfo);

            ContractWagesDTO wage= new ContractWagesDTO();
            List<WageComponentDTO> otherEarnings = new List<WageComponentDTO>();
            otherEarnings.AddRange(contractDto.Wages.OtherEarningComponents.ToList());
            wage.OtherEarningComponents = otherEarnings;
            List<WageComponentDTO> deduction = new List<WageComponentDTO>();
            deduction.AddRange(contractDto.Wages.DeductionComponents.ToList());
            wage.DeductionComponents = deduction;
            wage.SpecialAllownce = contractDto.Wages.SpecialAllowance;

            List<RevisedSalaryDTO> revisedSalaries = new List<RevisedSalaryDTO>();
            revisedSalaries.AddRange(contractDto.RevisedSalaries);
            
            convertToDto.Wages.OtherEarningComponents = _mapper.Map(wage.OtherEarningComponents,convertToDto.Wages.OtherEarningComponents);
            convertToDto.Wages.DeductionComponents = _mapper.Map(wage.DeductionComponents, convertToDto.Wages.DeductionComponents);
            convertToDto.Wages.SpecialAllownce = _mapper.Map(wage.SpecialAllownce, convertToDto.Wages.SpecialAllownce);
            convertToDto.Wages = _mapper.Map<ContractWagesDTO>(convertToDto.Wages);
            convertToDto.RevisedSalaries = _mapper.Map<List<RevisedSalaryDTO>>(revisedSalaries);

            contract.UpdatedAt = DateTime.UtcNow;
            contract.UpdatedById = _apiRequestContext.UserId;
            var contractToUpdate = _mapper.Map(_mapper.Map<VesselContract>(contract), contract);
            await _contractRepository.UpdateAsync(contractToUpdate);

            var contactDataDto = _mapper.Map<ContractFormDataDTO>(convertToDto);
            contractForm.Data = JsonConvert.SerializeObject(contactDataDto);
            await _contractFormRepository.UpdateAsync(_mapper.Map<ContractForm>(contractForm));
            
            return;
        }

        public async Task AssignReviewers(long id, ContractReviewerSetDTO reviewerSetDto)
        {
            var contract = _contractRepository.Get(id);
            var contractForm =await _contractFormRepository.GetAllIncluding().Where(x => x.ContractId == id).FirstOrDefaultAsync();
            var reviewer = new List<ContractReviewer>();
            var userInfo = new UserDetails();
            foreach (var data in reviewerSetDto.Reviewers)
            {
                userInfo = await _externalUserDetailsRepository.GetUserDetails(data.Id);
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
            contract.UpdatedAt = DateTime.UtcNow;
            contract.UpdatedById = _apiRequestContext.UserId;
            var mapContract = _mapper.Map<VesselContract>(contract);
            await _contractRepository.UpdateAsync(mapContract);

            await SendEmail(reviewerToBeAdded.Select(x=>x.Email).FirstOrDefault(),_mapper.Map<ContractFormDTO>(contractForm));
            return;
        }

        private async Task SendEmail(string email,ContractFormDTO contract)
        {
            SendingMailInfo sendingMailInfo = new SendingMailInfo();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "Templates", "TravelDetails.html");
            var reader = new StreamReader(path);
            var mailBody = reader.ReadToEnd();
            reader.Dispose();
            mailBody = mailBody.Replace("{NAME}", contract.Data.SeafarerDetail.Name);
            mailBody = mailBody.Replace("{EMAIL}", contract.Data.SeafarerDetail.Email);
            mailBody = mailBody.Replace("{AGE}", contract.Data.SeafarerDetail.Age.ToString());
            sendingMailInfo.Body = mailBody;
            sendingMailInfo.To.Add("abhishek.p@solutelabs.com");
            sendingMailInfo.Subject = "Seafarer Profile Assigned for Approval";
            sendingMailInfo.Name = "Synergy Marine";
            sendingMailInfo.From = "support@synergymarinetest.com";
            sendingMailInfo.IsBodyHtml = true;
            await _emailService.SendEmailAsync(sendingMailInfo);
        }

        public async Task<object> ApproveContract(long id, string userId)
        {
            var contract = await _contractRepository.GetAllIncluding().AsNoTracking().Where(x => x.Id == id).FirstOrDefaultAsync();
            if (contract is null)
            {
                return null;
            }
            var reviewers = await _contractReviewerRepository.GetAllIncluding().AsNoTracking().Where(x => x.ContractId == id).ToListAsync();
            var approver = reviewers.Where(x => x.ReviewerId == userId && x.Role.Equals(ReviewerRole.FleetHead.ToString())).FirstOrDefault();
            if (approver is null)
            {
                return null;
            }

            Contract contractDomainModel = new Contract();
            var information = new ContractInformation();
            contractDomainModel.AssignReviewers(_mapper.Map<List<ContractReviewers>>(reviewers).ToArray());
            contractDomainModel.Approve();
            await _contractReviewerRepository.UpdateAsync(_mapper.Map<ContractReviewer>(contractDomainModel.Reviewers.FirstOrDefault(_ => _.Role == ReviewerRole.FleetHead.ToString())));
            contract.Status = contractDomainModel.Information.Status.ToString();
            contract.NextReviewer = contractDomainModel.NextReviewer.Id;
            contract.UpdatedAt = DateTime.UtcNow;
            contract.UpdatedById = _apiRequestContext.UserId;
            await _contractRepository.UpdateAsync(contract);
            return 0;
        }

        public async Task<object> VerifyContract(long id, string userId)
        {
            var contract = await _contractRepository.GetAllIncluding().AsNoTracking().Where(x => x.Id == id).FirstOrDefaultAsync();
            if (contract is null)
            {
                return null;
            }
            var reviewers = await _contractReviewerRepository.GetAllIncluding().AsNoTracking().Where(x => x.ContractId == id).ToArrayAsync();
            var approver = reviewers.Where(x => x.ReviewerId == userId && x.Role.Equals(ReviewerRole.SourcingExecutive.ToString())).FirstOrDefault();
            if (approver is null)
            {
                return null;
            }
            Contract contractDomainModel = new Contract();
            contractDomainModel.AssignReviewers(_mapper.Map<List<ContractReviewers>>(reviewers).ToArray());
            contractDomainModel.Verify();
            await _contractReviewerRepository.UpdateAsync(_mapper.Map<ContractReviewer>(contractDomainModel.Reviewers.FirstOrDefault(_=>_.Role == ReviewerRole.SourcingExecutive.ToString())));
            contract.Status = contractDomainModel.Status.ToString();
            contract.NextReviewer = contractDomainModel.NextReviewer.Id;
            contract.UpdatedAt = DateTime.UtcNow;
            contract.UpdatedById = _apiRequestContext.UserId;
            await _contractRepository.UpdateAsync(contract);
            return 0;
        }

        public async Task RejectContract(long id, string userId, string comment)
        {
            var contract = await _contractRepository.GetAllIncluding().AsNoTracking().Where(x => x.Id == id).FirstOrDefaultAsync();
            var reviewers = await _contractReviewerRepository.GetAllIncluding().AsNoTracking().Where(x => x.ContractId == id).ToArrayAsync();

            var reviewer = await _contractReviewerRepository.GetAllIncluding().AsNoTracking().Where(x => x.ContractId == id && x.ReviewerId == userId).FirstOrDefaultAsync();
            
            Contract contractDomainModel = new Contract();
            contractDomainModel.AssignReviewers(_mapper.Map<List<ContractReviewers>>(reviewers).ToArray());
            contractDomainModel.Reject(comment);
            await _contractReviewerRepository.UpdateAsync(_mapper.Map<ContractReviewer>(contractDomainModel.Reviewers.FirstOrDefault(_=>_.ReviewerId == userId)));
            contract.Status = contractDomainModel.Status.ToString();
            contract.UpdatedAt = DateTime.UtcNow;
            contract.UpdatedById = _apiRequestContext.UserId;
            await _contractRepository.UpdateAsync(contract);
            return;
        }  
    }
}
