using AutoMapper;
using Microsoft.Extensions.Configuration;
using Synergy.AdobeSign.Models;
using Synergy.ReliefCenter.Api.Mapper;
using Synergy.ReliefCenter.Api.Models;
using Synergy.ReliefCenter.Core.Models.Dtos;

namespace Synergy.ReliefCenter.Api.Mappers
{
    public class MappingProfile : Profile
    {
        private readonly IConfiguration _configuration;
        public MappingProfile(IConfiguration configuration)
        {
            _configuration = configuration;
            CreateMap<Contract, ContractDTO>()
                .ReverseMap()                
                .ForMember(dest => dest.SeafarerDetail, opt => opt.MapFrom(src => src.ContractForm.Data.SeafarerDetail))
                .ForMember(dest => dest.TravelInfo, opt => opt.MapFrom(src => src.ContractForm.Data.TravelInfo))
                .ForMember(dest => dest.VesselInfo, opt => opt.MapFrom(src => src.ContractForm.Data.VesselInfo))
                .ForMember(dest => dest.Wages, opt => opt.MapFrom(src => src.ContractForm.Data.Wages))
                //.ForPath(dest => dest.Wages.OTRateCard, opt => opt.MapFrom(src => src.ContractForm.Data.Wages.OTRateCard))
                .ForMember(dest => dest.AttachmentDetail, opt => opt.MapFrom(src => src.ContractForm.Data.AttachmentDetail))
                .ForMember(dest => dest.ContractReviewers, opt => opt.MapFrom(src => src.ContractForm.Data.ContractReviewers))
                .ForMember(dest => dest.NextReviewer, opt => opt.MapFrom(src => src.ContractForm.Data.NextReviewer))
                .ForMember(dest => dest.RevisedSalaries, opt => opt.MapFrom(src => src.ContractForm.Data.RevisedSalaries));
            CreateMap<ContractAttachmentDetail, ContractAttachmentDetailDTO>().ReverseMap();
            CreateMap<SeafarerDetail, SeafarerDetailDTO>().ReverseMap();
            CreateMap<VesselDetail, VesselDetailDTO>().ReverseMap();
            CreateMap<ContractWages, ContractWagesDTO>().ReverseMap();
            CreateMap<TravelDetail, TravelDetailDTO>().ReverseMap();
            CreateMap<OTRateCard, OTRateCardDTO>().ReverseMap();
            CreateMap<WageComponent, WageComponentDTO>().ReverseMap();
            CreateMap<UpdateContractRequest, UpdateContractDTO>().ReverseMap();
            CreateMap<UpdateContractWages, UpdateContractWagesDTO>().ReverseMap();
            CreateMap<ContractReviewerSet, ContractReviewerSetDTO>().ReverseMap();
            CreateMap<ContractReviewer, ContractReviewerDTO>().ReverseMap();
            CreateMap<Reviewers, ReviewersDTO>().ReverseMap();
            CreateMap<UserInfo, UserInfoDTO>().ReverseMap();
            CreateMap<RevisedSalary, RevisedSalaryDTO>().ReverseMap();
            CreateMap<MyContracts, MyContractsDTO>().ReverseMap();

            CreateMap<ContractDTO, AgreementCreationInfo>().ConvertUsing(new ContractToAgreementMapper(_configuration));
        }
    }
}
