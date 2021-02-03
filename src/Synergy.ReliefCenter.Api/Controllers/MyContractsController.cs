using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Synergy.ReliefCenter.Api.Configuration;
using Synergy.ReliefCenter.Api.Filter;
using Synergy.ReliefCenter.Api.Models;
using Synergy.ReliefCenter.Core.Constants;
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
        private readonly IConfiguration _configuration;
        private const string USER_DETAILS_APIURL_SECTION = "UserDetails:ApiUrl";
        private const string USER_DETAILS_APIKEY_SECTION = "UserDetails:ApiKey";

        public MyContractsController(IMyContractService contractService, IMapper mapper, IConfiguration configuration)
        {
            _contractService = contractService;
            _mapper = mapper;
            _configuration = configuration;
        }

        [HasPolicyAccess(PolicyNames.AccessContract)]
        [HttpGet()]
        [Route("mycontracts")]
        [ProducesResponseType(typeof(List<MyContracts>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetSeafarerConracts([FromQuery] string imoNumber)
        {
            var userId = Request.HttpContext.User.Claims.FirstOrDefault(s => s.Type.Equals("user_id", StringComparison.OrdinalIgnoreCase))?.Value;
            var contractDetails = await _contractService.GetSeafarerConracts(imoNumber, userId);
            var getContractDetails = _mapper.Map<List<MyContracts>>(contractDetails);
            if (contractDetails == null)
            {
                return NotFound();
            }
            return Ok(getContractDetails);
        }

        [HasPolicyAccess(PolicyNames.AccessContract)]
        [HttpGet()]
        [Route("mycontract/active")]
        [ProducesResponseType(typeof(Contract), StatusCodes.Status200OK)]
        public async Task<ActionResult<Contract>> GetSeafarerConract([FromQuery] string imoNumber)
        {
            var userId = Request.HttpContext.User.Claims.FirstOrDefault(s => s.Type.Equals("user_id", StringComparison.OrdinalIgnoreCase))?.Value;
            string userDetailsApiBaseUrl = _configuration.GetSection(USER_DETAILS_APIURL_SECTION).Value;
            string userDetailsApiKey = _configuration.GetSection(USER_DETAILS_APIKEY_SECTION).Value;
            var contractDetails = await _contractService.GetSeafarerConract(imoNumber, userId,userDetailsApiKey,userDetailsApiBaseUrl);
            var getContractDetails = _mapper.Map<Contract>(contractDetails);
            if (contractDetails == null)
            {
                return NotFound();
            }
            return Ok(getContractDetails);
        }
    }
}
