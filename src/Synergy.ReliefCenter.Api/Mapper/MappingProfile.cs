using AutoMapper;
using Synergy.ReliefCenter.Api.Models;
using Synergy.ReliefCenter.Core.Models.Dtos;
using Synergy.ReliefCenter.Data.Entities.Seafarer;

namespace Synergy.ReliefCenter.Api.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ContractDto, Contract>();
            CreateMap<Data.Entities.Contract, ContractDto>();
            CreateMap<ContractDto, Data.Entities.Contract>();
            CreateMap<ContractFormDto, Data.Entities.ContractForm>();
            CreateMap<Data.Entities.ContractForm, ContractFormDto>();
        }
    }
}
