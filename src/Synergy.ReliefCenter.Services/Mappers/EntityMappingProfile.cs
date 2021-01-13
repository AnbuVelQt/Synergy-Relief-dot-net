using AutoMapper;
using Newtonsoft.Json;
using Synergy.ReliefCenter.Core.Models.Dtos;
using Synergy.ReliefCenter.Data.Entities;

namespace Synergy.ReliefCenter.Services.Mappers
{
    public class EntityMappingProfile : Profile
    {
        public EntityMappingProfile()
        {
            CreateMap<Contract, ContractDto>().ReverseMap();
            CreateMap<ContractForm, ContractFormDto>()
                .ForMember(dest => dest.Data, opt => opt.MapFrom(src => JsonConvert.DeserializeObject<ContractFormDataDto>(src.Data)))
                .ReverseMap()
                .ForMember(dest => dest.Data, opt => opt.MapFrom(src => JsonConvert.SerializeObject(src.Data)));
        }
    }
}
