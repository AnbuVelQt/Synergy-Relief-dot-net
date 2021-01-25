using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Synergy.ReliefCenter.Api.Models;
using Synergy.ReliefCenter.Api.Validations;
using Synergy.ReliefCenter.Core.Models.Dtos;
using Synergy.ReliefCenter.Services.Abstraction;
using System.Threading.Tasks;

namespace Synergy.ReliefCenter.Api.Controllers
{
    public class ContractsController : ApiControllerBase
    {
        private readonly IContractService _contractService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private const string CREW_WAGE_APIURL_SECTION = "CrewWage:ApiUrl";
        private const string USER_DETAILS_APIURL_SECTION = "UserDetails:ApiUrl";
        private const string USER_DETAILS_APIKEY_SECTION = "UserDetails:ApiKey";

        public ContractsController(IContractService contractService,IMapper mapper, IConfiguration configuration)
        {
            _contractService = contractService;
            _mapper = mapper;
            _configuration = configuration;
        }
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(Contract), StatusCodes.Status200OK)]
        public async Task<ActionResult<Contract>> GetConract([FromRoute] long id)
        {
            string userDetailsApiBaseUrl = _configuration.GetSection(USER_DETAILS_APIURL_SECTION).Value;
            string userDetailsApiKey = _configuration.GetSection(USER_DETAILS_APIKEY_SECTION).Value;
            var contractDetails =await _contractService.GetConract(id,userDetailsApiKey,userDetailsApiBaseUrl);
            var getContractDetails = _mapper.Map<Contract>(contractDetails);
            return Ok(getContractDetails);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Contract), StatusCodes.Status201Created)]
        public async Task<ActionResult<Contract>> CreateContract([FromBody] CreateContractRequest model)
        {
            var validator = new CreateContractRequestValidation();
            var result = validator.Validate(model);
            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }
            string crewWageApiBaseUrl = _configuration.GetSection(CREW_WAGE_APIURL_SECTION).Value;
            var AuthToken = Request.Headers["Authorization"];
            var contract =await _contractService.CreateContract(model.VesselId,model.SeafarerId, AuthToken, crewWageApiBaseUrl);
            var createContractDetails = _mapper.Map<Contract>(contract);
            return Created("", createContractDetails);
        }

        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateContract([FromBody] UpdateContractRequest model,long id)
        {
            var requestModel = _mapper.Map<UpdateContractDto>(model);
            await _contractService.UpdateContract(requestModel, id);
            return NoContent();
        }

        [HttpPut]
        [Route("{id}/reviewers")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> AssignReviewers([FromRoute] long id,[FromBody] ContractReviewerSet model)
        {
            string userDetailsApiBaseUrl = _configuration.GetSection(USER_DETAILS_APIURL_SECTION).Value;
            string userDetailsApiKey = _configuration.GetSection(USER_DETAILS_APIKEY_SECTION).Value;
            var requestModel = _mapper.Map<ContractReviewerSetDto>(model);
            await _contractService.AssignReviewers(id,requestModel, userDetailsApiKey, userDetailsApiBaseUrl);
            return NoContent();
        }

        [HttpGet()]
        [Route("active")]
        [ProducesResponseType(typeof(Contract), StatusCodes.Status200OK)]
        public async Task<ActionResult<Contract>> GetConracts([FromQuery] long vesselId,[FromQuery] long seafarerId)
        {
            string userDetailsApiBaseUrl = _configuration.GetSection(USER_DETAILS_APIURL_SECTION).Value;
            string userDetailsApiKey = _configuration.GetSection(USER_DETAILS_APIKEY_SECTION).Value;
            var contractDetails = await _contractService.GetConracts(vesselId,seafarerId,userDetailsApiKey,userDetailsApiBaseUrl);
            var getContractDetails = _mapper.Map<Contract>(contractDetails);
            if (contractDetails == null)
            {
                return NotFound();
            }
            return Ok(getContractDetails);
        }
        
    }
}
