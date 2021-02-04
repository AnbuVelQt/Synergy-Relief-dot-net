using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Synergy.ReliefCenter.Api.Models;
using Synergy.ReliefCenter.Core.Models;
using Synergy.ReliefCenter.Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Synergy.ReliefCenter.Api.Controllers
{
    [Authorize(AuthenticationSchemes = AuthenticationSchemas.SeafarerIdp)]
    public class MyContractsController : ApiControllerBase
    {
        private readonly IMyContractService _contractService;
        private readonly IMapper _mapper;
        private readonly IApiRequestContext _apiRequestContext;

        public MyContractsController(IMyContractService contractService, IMapper mapper, IApiRequestContext apiRequestContext)
        {
            _contractService = contractService;
            _mapper = mapper;
            _apiRequestContext = apiRequestContext;
        }

        [HttpGet()]
        [Route("mycontracts")]
        [ProducesResponseType(typeof(List<MyContracts>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetSeafarerConracts([FromQuery] string imoNumber)
        {
            var contractDetails = await _contractService.GetSeafarerConracts(imoNumber, _apiRequestContext.UserId);
            var getContractDetails = _mapper.Map<List<MyContracts>>(contractDetails);
            if (contractDetails == null)
            {
                return NotFound();
            }
            return Ok(getContractDetails);
        }

        [HttpGet()]
        [Route("mycontract/active")]
        [ProducesResponseType(typeof(Contract), StatusCodes.Status200OK)]
        public async Task<ActionResult<Contract>> GetSeafarerConract([FromQuery] string imoNumber)
        {  
            var contractDetails = await _contractService.GetSeafarerConract(imoNumber, _apiRequestContext.UserId);
            var getContractDetails = _mapper.Map<Contract>(contractDetails);
            if (contractDetails == null)
            {
                return NotFound();
            }
            return Ok(getContractDetails);
        }
    }
}
