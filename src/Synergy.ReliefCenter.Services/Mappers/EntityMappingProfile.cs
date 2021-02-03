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
            CreateMap<VesselContract, ContractDTO>()
                .ReverseMap();
            CreateMap<ContractForm, ContractFormDTO>()
                .ForMember(dest => dest.Data, opt => opt.MapFrom(src => JsonConvert.DeserializeObject<ContractFormDataDTO>(src.Data)))
                .ReverseMap()
                .ForMember(dest => dest.Data, opt => opt.MapFrom(src => JsonConvert.SerializeObject(src.Data)));
            CreateMap<ContractWagesDTO, SalaryMatrix>().ReverseMap();
            CreateMap<WageComponent, WageComponentDTO>().ReverseMap();
            CreateMap<OTRateCard, OTRateCardDTO>().ReverseMap();
            CreateMap<UpdateContractWagesDTO, ContractWagesDTO>().ReverseMap();
            CreateMap<ContractReviewer, ContractReviewerSetDTO>().ReverseMap();
            CreateMap<ContractReviewer, ReviewersDTO>().ReverseMap();
            CreateMap<UserDetails, UserInfoDTO>().ReverseMap();
            
        }
    }
}
