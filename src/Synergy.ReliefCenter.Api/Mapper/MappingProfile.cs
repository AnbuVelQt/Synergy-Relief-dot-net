using AutoMapper;
using Synergy.ReliefCenter.Api.Models;
using Synergy.ReliefCenter.Core.Models.Dtos;

namespace Synergy.ReliefCenter.Api.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Contract, ContractDto>()
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
            CreateMap<ContractAttachmentDetail, ContractAttachmentDetailDto>().ReverseMap();
            CreateMap<SeafarerDetail, SeafarerDetailDto>().ReverseMap();
            CreateMap<VesselDetail, VesselDetailDto>().ReverseMap();
            CreateMap<ContractWages, ContractWagesDto>().ReverseMap();
            CreateMap<TravelDetail, TravelDetailDto>().ReverseMap();
            CreateMap<OTRateCard, OTRateCardDto>().ReverseMap();
            CreateMap<WageComponent, WageComponentDto>().ReverseMap();
            CreateMap<UpdateContractRequest, UpdateContractDto>().ReverseMap();
            CreateMap<UpdateContractWages, UpdateContractWagesDto>().ReverseMap();
            CreateMap<ContractReviewerSet, ContractReviewerSetDto>().ReverseMap();
            CreateMap<ContractReviewer, ContractReviewerDto>().ReverseMap();
            CreateMap<Reviewers, ReviewersDto>().ReverseMap();
            CreateMap<UserInfo, UserInfoDto>().ReverseMap();
            CreateMap<RevisedSalary, RevisedSalaryDto>().ReverseMap();
            CreateMap<MyContracts, MyContractsDto>().ReverseMap();
        }
    }
}
