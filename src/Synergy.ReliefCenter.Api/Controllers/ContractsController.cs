using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Synergy.AdobeSign;
using Synergy.AdobeSign.Models;
using Synergy.ReliefCenter.Api.Helper;
using Synergy.ReliefCenter.Api.Models;
using Synergy.ReliefCenter.Api.Validations;
using Synergy.ReliefCenter.Core.Models.Dtos;
using Synergy.ReliefCenter.Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Synergy.ReliefCenter.Api.Controllers
{
    public class ContractsController : ApiControllerBase
    {
        private readonly IContractService _contractService;
        private readonly IMapper _mapper;
        private readonly IAdobeSignRestClient _adobeSignRestClient;
        private readonly IConfiguration _configuration;
        private const string CONTRACT_DOC_ID_SECTION = "AbodeSign:ContractDocumentId";

        public ContractsController(IContractService contractService,IMapper mapper, IAdobeSignRestClient adobeSignRestClient, IConfiguration configuration)
        {
            _contractService = contractService;
            _mapper = mapper;
            _adobeSignRestClient = adobeSignRestClient;
            _configuration = configuration;
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
            var fileInfosList = new List<FileInformation>();
            string contractDocumentId = _configuration.GetSection(CONTRACT_DOC_ID_SECTION).Value;
            fileInfosList.Add(new FileInformation { libraryDocumentId = contractDocumentId });
            var participantSetsInfoList = new List<ParticipantInfo>();
            var memberInfoList = new List<MemberInfo>();
            memberInfoList.Add(new MemberInfo { email = "pentagram@synergyship.com" });
            participantSetsInfoList.Add(new ParticipantInfo { memberInfos = memberInfoList, order = 1, role = Enum.GetName<AdobeRoleEnum>(AdobeRoleEnum.FORM_FILLER) });
            var mergeFieldInfoList = new List<MergeFieldInfo>();
            mergeFieldInfoList.Add(new MergeFieldInfo() { fieldName = "seafarerName", defaultValue = "Jhon" });
            var agreementCreationInfo = new AgreementCreationInfo
            {
                fileInfos = fileInfosList,
                name = "Demo Check 197",
                participantSetsInfo = participantSetsInfoList,
                signatureType = Enum.GetName<AdobeSignatureTypeEnum>(AdobeSignatureTypeEnum.ESIGN),
                state = Enum.GetName<AdobeStateEnum>(AdobeStateEnum.DRAFT),
                mergeFieldInfo = mergeFieldInfoList
            };
            var adobeCreateAgreementResponse = await _adobeSignRestClient.CreateAgreementAsync(agreementCreationInfo);
            


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
