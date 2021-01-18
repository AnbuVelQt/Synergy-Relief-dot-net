using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        public ContractsController(IContractService contractService,IMapper mapper)
        {
            _contractService = contractService;
            _mapper = mapper;
        }
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(Contract), StatusCodes.Status200OK)]
        public async Task<ActionResult<Contract>> GetConract([FromRoute] long id)
        {
            var contractDetails =await _contractService.GetConract(id);
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
            var AuthToken = Request.Headers["Authorization"];
            var contract =await _contractService.CreateContract(model.VesselId,model.SeafarerId, AuthToken);
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
        public async Task<IActionResult> AssignReviewers([FromRoute] long id)
        {
            return NoContent();
        }

    }
}
