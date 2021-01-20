using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Synergy.AdobeSign;
using Synergy.AdobeSign.Models;
using Synergy.ReliefCenter.Api.Models;
using Synergy.ReliefCenter.Api.Validations;
using Synergy.ReliefCenter.Core.Models.Dtos;
using Synergy.ReliefCenter.Services.Abstraction;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Synergy.ReliefCenter.Api.Controllers
{
    public class ContractsController : ApiControllerBase
    {
        private readonly IContractService _contractService;
        private readonly IMapper _mapper;
        private readonly IAdobeSignRestClient _adobeSignRestClient;

        public ContractsController(IContractService contractService,IMapper mapper, IAdobeSignRestClient adobeSignRestClient)
        {
            _contractService = contractService;
            _mapper = mapper;
            _adobeSignRestClient = adobeSignRestClient;
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
        public async Task<ActionResult<Contract>> ApproveContract([FromBody] CreateContractRequest model)
        {
            var fileInfosList = new List<FileInformation>();
            fileInfosList.Add(new FileInformation { libraryDocumentId = "CBJCHBCAABAA6n2lxqPkvqZzRzIph8fZ85m_hYzMntqf" });
            var participantSetsInfoList = new List<ParticipantInfo>();
            var memberInfoList = new List<MemberInfo>();
            memberInfoList.Add(new MemberInfo { email = "pentagram@synergyship.com" });
            participantSetsInfoList.Add(new ParticipantInfo { memberInfos = memberInfoList, order = 1, role = "FORM_FILLER" });
            var agreementCreationInfo = new AgreementCreationInfo
            {
                fileInfos = fileInfosList,
                name = "Demo Check 197",
                participantSetsInfo = participantSetsInfoList,
                signatureType = "ESIGN",
                state = "DRAFT"
            };
            var adobeAgreementResponse = await _adobeSignRestClient.CreateAgreementAsync(agreementCreationInfo);

            var validator = new CreateContractRequestValidation();
            var result = validator.Validate(model);
            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }
            var AuthToken = Request.Headers["Authorization"];
            var contract = await _contractService.CreateContract(model.VesselId, model.SeafarerId, AuthToken);
            var createContractDetails = _mapper.Map<Contract>(contract);
            return Created("", createContractDetails);
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
            var contract = await _contractService.CreateContract(model.VesselId,model.SeafarerId, AuthToken);
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

        [HttpGet()]
        [Route("active")]
        [ProducesResponseType(typeof(Contract), StatusCodes.Status200OK)]
        public async Task<ActionResult<Contract>> GetConracts([FromQuery] long vesselId,[FromQuery] long seafarerId)
        {
            var contractDetails = await _contractService.GetConracts(vesselId,seafarerId);
            var getContractDetails = _mapper.Map<Contract>(contractDetails);
            if (contractDetails == null)
            {
                return NotFound("No Data Found for the query");
            }
            return Ok(getContractDetails);
        }
    }
}
