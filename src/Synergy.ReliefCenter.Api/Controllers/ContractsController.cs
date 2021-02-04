using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Synergy.ReliefCenter.Api.Filter;
using Synergy.ReliefCenter.Api.Models;
using Synergy.ReliefCenter.Core.Constants;
using Synergy.ReliefCenter.Core.Models;
using Synergy.ReliefCenter.Core.Models.Dtos;
using Synergy.ReliefCenter.Services.Abstraction;
using System.Threading.Tasks;

namespace Synergy.ReliefCenter.Api.Controllers
{
    [Authorize(AuthenticationSchemes = AuthenticationSchemas.ShoreIdp)]
    public class ContractsController : ApiControllerBase
    {
        private readonly IContractService _contractService;
        private readonly IMapper _mapper;
        private readonly IApiRequestContext _apiRequestContext;

        public ContractsController(IContractService contractService, IMapper mapper, IApiRequestContext apiRequestContext)
        {
            _contractService = contractService;
            _mapper = mapper;
            _apiRequestContext = apiRequestContext;
        }

        [HasPolicyAccess(PolicyNames.AccessContract)]
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(Contract), StatusCodes.Status200OK)]
        public async Task<ActionResult<Contract>> GetConract([FromRoute] long id)
        {
            var contractDetails = await _contractService.GetConract(id);
            var getContractDetails = _mapper.Map<Contract>(contractDetails);
            return Ok(getContractDetails);
        }

        [HasPolicyAccess(PolicyNames.DraftContract)]
        [HttpPost]
        [ProducesResponseType(typeof(Contract), StatusCodes.Status201Created)]
        public async Task<ActionResult<Contract>> CreateContract([FromBody] CreateContractRequest model)
        {
            var AuthToken = Request.Headers["Authorization"];
            var contract = await _contractService.CreateContract(model.VesselImoNumber, model.SeafarerCdcNumber, AuthToken);
            if (contract is null)
            {
                return Conflict();
            }
            var createContractDetails = _mapper.Map<Contract>(contract);
            return Created("", createContractDetails);
        }

        //[HasPolicyAccess(PolicyNames.DraftContract)]
        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateContract([FromBody] UpdateContractRequest model, long id)
        {
            await _contractService.ApproveAsync(id);  //This will be used for on approve contract & send for Adobe sign
            var requestModel = _mapper.Map<UpdateContractDTO>(model);
            await _contractService.UpdateContract(requestModel, id);
            return NoContent();
        }

    
        [HttpPut]
        [Route("{id}/reviewers")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> AssignReviewers([FromRoute] long id, [FromBody] ContractReviewerSet model)
        {
            var requestModel = _mapper.Map<ContractReviewerSetDTO>(model);
            await _contractService.AssignReviewers(id, requestModel);
            return NoContent();
        }

        [HasPolicyAccess(PolicyNames.AccessContract)]
        [HttpGet()]
        [Route("active")]
        [ProducesResponseType(typeof(Contract), StatusCodes.Status200OK)]
        public async Task<ActionResult<Contract>> GetContracts([FromQuery] string vesselImoNumber, [FromQuery] string seafarerCdcNumber)
        {
            var contractDetails = await _contractService.GetConracts(vesselImoNumber, seafarerCdcNumber);
            var getContractDetails = _mapper.Map<Contract>(contractDetails);
            if (contractDetails == null)
            {
                return NotFound();
            }
            return Ok(getContractDetails);
        }

        [HasPolicyAccess(PolicyNames.ApproveContract)]
        [HttpPut]
        [Route("{id}/approve")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> ApproveContract([FromRoute] long id)
        {
            
            var response = await _contractService.ApproveContract(id, _apiRequestContext.UserId);
            if (response is null)
            {
                return BadRequest();
            }
            return NoContent();
        }

        [HasPolicyAccess(PolicyNames.VerifyContract)]
        [HttpPut]
        [Route("{id}/verify")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> VerifyContract([FromRoute] long id)
        {
            var response = await _contractService.VerifyContract(id, _apiRequestContext.UserId);
            if (response is null)
            {
                return BadRequest();
            }
            return NoContent();
        }

        [HttpPut]
        [Route("{id}/reject")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> RejectContract([FromRoute] long id, [FromQuery] string comment)
        {
            await _contractService.RejectContract(id, _apiRequestContext.UserId, comment);
            return NoContent();
        }
    }
}
