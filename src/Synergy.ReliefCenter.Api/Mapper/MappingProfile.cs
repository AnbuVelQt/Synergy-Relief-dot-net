using AutoMapper;
using Synergy.ReliefCenter.Api.Models;
using Synergy.ReliefCenter.Core.Models.Dtos;
using Synergy.ReliefCenter.Data.Entities.SeafarerCenter;

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
                .ForMember(dest => dest.AttachmentDetail, opt => opt.MapFrom(src => src.ContractForm.Data.AttachmentDetail));
            CreateMap<ContractAttachmentDetail, ContractAttachmentDetailDto>().ReverseMap();
            CreateMap<SeafarerDetail, SeafarerDetailDto>().ReverseMap();
            CreateMap<VesselDetail, VesselDetailDto>().ReverseMap();
            CreateMap<ContractWages, ContractWagesDto>().ReverseMap();
            CreateMap<TravelDetail, TravelDetailDto>().ReverseMap();
        }
    }
}
