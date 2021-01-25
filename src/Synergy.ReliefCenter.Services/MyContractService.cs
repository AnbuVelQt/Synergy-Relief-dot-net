using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Synergy.ReliefCenter.Core.Models;
using Synergy.ReliefCenter.Core.Models.Dtos;
using Synergy.ReliefCenter.Data.Entities.Master;
using Synergy.ReliefCenter.Data.Repositories.Abstraction;
using Synergy.ReliefCenter.Data.Repositories.Abstraction.ReliefRepository;
using Synergy.ReliefCenter.Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synergy.ReliefCenter.Services
{
    public class MyContractService:IMyContractService
    {
        private readonly IContractRepository _contractRepository;
        private readonly IContractFormRepository _contractFormRepository;
        private readonly IVesselDataRepository _vesselDataRepository;
        private readonly ISeafarerDataRepository _seafarerDataRepository;
        private readonly IMapper _mapper;
        private readonly IExternalSalaryMatrixRepository _externalSalaryMatrixRepository;
        private readonly IContractReviewerRepository _contractReviewerRepository;
        private readonly IExternalUserDetailsRepository _externalUserDetailsRepository;

        public MyContractService(
            IContractRepository contractRepository,
            IContractFormRepository contractFormRepository,
            IVesselDataRepository vesselRepository,
            ISeafarerDataRepository seafarerRepository,
            IMapper mapper,
            IContractReviewerRepository contractReviewerRepository,
            IExternalSalaryMatrixRepository externalSalaryMatrixRepository,
            IExternalUserDetailsRepository externalUserDetailsRepository)
        {
            _contractRepository = contractRepository;
            _contractFormRepository = contractFormRepository;
            _vesselDataRepository = vesselRepository;
            _seafarerDataRepository = seafarerRepository;
            _mapper = mapper;
            _contractReviewerRepository = contractReviewerRepository;
            _externalSalaryMatrixRepository = externalSalaryMatrixRepository;
            _externalUserDetailsRepository = externalUserDetailsRepository;
        }
        public async Task<ContractDto> GetSeafarerConract(long vesselId, string userId, string apiKey, string userDetailsApiBaseUrl)
        {
            var seafarerDetails = _seafarerDataRepository.GetSeafarerByIdentityAsync(userId);
            var contracts = new ContractDto();

            var contract = await _contractRepository.GetAllIncluding().AsNoTracking().Where(x => x.SeafarerId == seafarerDetails.Result.Id && ((x.EndDate >= DateTime.UtcNow && x.StartDate < DateTime.UtcNow) || (x.StartDate == null && x.EndDate == null))).OrderByDescending(x => x.Id).FirstOrDefaultAsync();
            if (vesselId > 0)
            {
                contract.VesselId = vesselId;
            }
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
                    UserInfo = new UserInfoDto()
                    {
                        Id = userInfo is null ? data.ReviewerId : userInfo.Id,
                        Email = userInfo is null ? data.Email : userInfo.Email,
                        Name = userInfo is null ? data.Name : userInfo.Name
                    }
                });
            }

            contracts = _mapper.Map<ContractDto>(contract);
            contracts.ContractForm = _mapper.Map<ContractFormDto>(contractForm);
            contracts.ContractForm.Data.ContractReviewers = _mapper.Map<List<ReviewersDto>>(reviewer);
            contracts.ContractForm.Data.NextReviewer = _mapper.Map<ReviewersDto>(reviewer.Where(x => x.ReviewerId == reviewers.Where(x => x.Id == contract.NextReviewer).FirstOrDefault().ReviewerId).FirstOrDefault());
            return contracts;
        }

        public async Task<IList<MyContractsDto>> GetSeafarerConracts(long vesselId, string userId)
        {
            var seafarerDetails = _seafarerDataRepository.GetSeafarerByIdentityAsync(userId);
           
            var contract = await _contractRepository.GetAllIncluding().AsNoTracking()
                .Where(x => x.SeafarerId == seafarerDetails.Result.Id && 
                ((x.EndDate >= DateTime.UtcNow && x.StartDate < DateTime.UtcNow) || (x.StartDate == null && x.EndDate == null)))
                .OrderByDescending(x => x.Id).ToListAsync();
            if (vesselId > 0)
            {
                contract.Where(s => s.VesselId == vesselId);
            }
            if (contract is null)
            {
                return null;
            }
            var contractDetails = new List<MyContractsDto>();
            foreach(var data in contract)
            {
                var contractForm = _contractFormRepository.GetAllIncluding().AsNoTracking().Where(s => s.ContractId == data.Id).FirstOrDefaultAsync();
                var mapToDto = _mapper.Map<ContractFormDto>(contractForm.Result);
                contractDetails.Add(new MyContractsDto()
                {
                    Id = data.Id,
                    Salary = Convert.ToDecimal(data.Salary),
                    SeafarerName = mapToDto.Data.SeafarerDetail.Name,
                    SeafarerEmail = mapToDto.Data.SeafarerDetail.Email,
                    SeafarerPhone = mapToDto.Data.SeafarerDetail.Phone,
                    SeafarerAddress = mapToDto.Data.SeafarerDetail.Address,
                    SeafarerCrewCode = mapToDto.Data.SeafarerDetail.CrewCode,
                    DateOfBirth = mapToDto.Data.SeafarerDetail.DateOfBirth,
                    SeafarerCDCNumber = mapToDto.Data.SeafarerDetail.CDCNumber,
                    SeafarerNationality = mapToDto.Data.SeafarerDetail.Nationality,
                    VesselName = mapToDto.Data.VesselInfo.Name,
                    VesselOwner = mapToDto.Data.VesselInfo.Owner,
                    TravelEndDate = mapToDto.Data.TravelInfo.EndDate,
                    TravelStartDate = mapToDto.Data.TravelInfo.StartDate,
                    PlaceOfEnagement = mapToDto.Data.TravelInfo.PlaceOfEnagement,
                    ContractTerms = mapToDto.Data.TravelInfo.ContractTerms,
                    Status = (ContractStatus)Enum.Parse(typeof(ContractStatus), data.Status)                    
                });
            }            
            
            var contracts = _mapper.Map<List<MyContractsDto>>(contractDetails);
            return contracts;
        }
    }
}
