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
        public async Task<ContractDTO> GetSeafarerConract(string imoNumber, string userId, string apiKey, string userDetailsApiBaseUrl)
        {
            var seafarerDetails = _seafarerDataRepository.GetSeafarerByIdentityAsync(userId);
            var contracts = new ContractDTO();

            var contract = await _contractRepository.GetAllIncluding().AsNoTracking().Where(x => x.SeafarerId == seafarerDetails.Result.Id && ((x.EndDate >= DateTime.UtcNow) || (x.StartDate == null && x.EndDate == null)) && x.Status != ContractStatus.Cancelled.ToString() && x.Status != ContractStatus.InDraft.ToString()).OrderByDescending(x => x.Id).FirstOrDefaultAsync();
            if (imoNumber != null)
            {
                contract.ImoNumber = imoNumber;
            }
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
                    UserInfo = new UserInfoDTO()
                    {
                        Id = userInfo is null ? data.ReviewerId : userInfo.Id,
                        Email = userInfo is null ? data.Email : userInfo.Email,
                        Name = userInfo is null ? data.Name : userInfo.Name
                    }
                });
            }

            contracts = _mapper.Map<ContractDTO>(contract);
            contracts.ContractForm = _mapper.Map<ContractFormDTO>(contractForm);
            contracts.ContractForm.Data.ContractReviewers = _mapper.Map<List<ReviewersDTO>>(reviewer);
            contracts.ContractForm.Data.NextReviewer = _mapper.Map<ReviewersDTO>(reviewer.Where(x => x.ReviewerId == reviewers.Where(x => x.Id == contract.NextReviewer).FirstOrDefault().ReviewerId).FirstOrDefault());
            return contracts;
        }

        public async Task<IList<MyContractsDTO>> GetSeafarerConracts(string imoNumber, string userId)
        {
            var seafarerDetails = _seafarerDataRepository.GetSeafarerByIdentityAsync(userId);
           
            var contract = await _contractRepository.GetAllIncluding().AsNoTracking()
                .Where(x => x.SeafarerId == seafarerDetails.Result.Id && 
                ((x.EndDate >= DateTime.UtcNow) || (x.StartDate == null && x.EndDate == null)) && x.Status != ContractStatus.Cancelled.ToString() && x.Status != ContractStatus.InDraft.ToString())
                .OrderByDescending(x => x.Id).ToListAsync();
            if (imoNumber != null)
            {
                contract.Where(s => s.ImoNumber == imoNumber);
            }
            if (contract is null)
            {
                return null;
            }
            var contractDetails = new List<MyContractsDTO>();
            foreach(var data in contract)
            {
                var contractForm = _contractFormRepository.GetAllIncluding().AsNoTracking().Where(s => s.ContractId == data.Id).FirstOrDefaultAsync();
                var mapToDto = _mapper.Map<ContractFormDTO>(contractForm.Result);
                contractDetails.Add(new MyContractsDTO()
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
            
            var contracts = _mapper.Map<List<MyContractsDTO>>(contractDetails);
            return contracts;
        }
    }
}
