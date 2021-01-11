using AutoMapper;
using Synergy.ReliefCenter.Api.Models;
using Synergy.ReliefCenter.Core.Models.Dtos;

namespace Synergy.ReliefCenter.Api.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<ContractDto, Contract>();
            CreateMap<Data.Entities.Contract, ContractDto>();
        }
    }
}
