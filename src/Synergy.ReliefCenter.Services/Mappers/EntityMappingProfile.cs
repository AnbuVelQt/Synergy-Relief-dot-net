using AutoMapper;
using Newtonsoft.Json;
using Synergy.ReliefCenter.Core.Models.Dtos;
using Synergy.ReliefCenter.Data.Models;
using Synergy.ReliefCenter.Data.Entities.SalaryMatrix;
using Synergy.ReliefCenter.Data.Entities.Master;

namespace Synergy.ReliefCenter.Services.Mappers
{
    public class EntityMappingProfile : Profile
    {
        public EntityMappingProfile()
        {
            CreateMap<VesselContract, ContractDto>()
                .ReverseMap();
            CreateMap<ContractForm, ContractFormDto>()
                .ForMember(dest => dest.Data, opt => opt.MapFrom(src => JsonConvert.DeserializeObject<ContractFormDataDto>(src.Data)))
                .ReverseMap()
                .ForMember(dest => dest.Data, opt => opt.MapFrom(src => JsonConvert.SerializeObject(src.Data)));
            CreateMap<ContractWagesDto, SalaryMatrix>().ReverseMap();
            CreateMap<WageComponent, WageComponentDto>().ReverseMap();
            CreateMap<OTRateCard, OTRateCardDto>().ReverseMap();
            CreateMap<UpdateContractWagesDto, ContractWagesDto>().ReverseMap();
            CreateMap<ContractReviewer, ContractReviewerSetDto>().ReverseMap();
            CreateMap<ContractReviewer, ReviewersDto>().ReverseMap();
            CreateMap<UserDetails, UserInfoDto>().ReverseMap();
            
        }
    }
}
