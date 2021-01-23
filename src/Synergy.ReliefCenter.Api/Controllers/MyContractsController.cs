using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Synergy.ReliefCenter.Api.Models;
using Synergy.ReliefCenter.Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Synergy.ReliefCenter.Api.Controllers
{
    
    public class MyContractsController : ApiControllerBase
    {
        private readonly IMyContractService _contractService;
        private readonly IMapper _mapper;

        public MyContractsController(IMyContractService contractService, IMapper mapper)
        {
            _contractService = contractService;
            _mapper = mapper;
        }

        [HttpGet()]
        [Route("mycontracts")]
        [ProducesResponseType(typeof(List<MyContracts>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetSeafarerConracts([FromQuery] long vesselId, [FromQuery] string auth)
        {
            var contractDetails = await _contractService.GetSeafarerConracts(vesselId, auth);
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
        public async Task<ActionResult<Contract>> GetSeafarerConract([FromQuery] long vesselId, [FromQuery] string auth)
        {
            var contractDetails = await _contractService.GetSeafarerConract(vesselId, auth);
            var getContractDetails = _mapper.Map<Contract>(contractDetails);
            if (contractDetails == null)
            {
                return NotFound();
            }
            return Ok(getContractDetails);
        }
    }
}
