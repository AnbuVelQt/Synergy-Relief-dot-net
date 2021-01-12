using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Synergy.ReliefCenter.Api.Models;
using System;
using System.Threading.Tasks;

namespace Synergy.ReliefCenter.Api.Controllers
{
    public class ContractsController : ApiControllerBase
    {
        private static Contract _mockContract => GetMockContract();
        

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(Contract), StatusCodes.Status200OK)]
        public async Task<ActionResult<Contract>> GetConract([FromRoute] long id)
        {
            return Ok(_mockContract);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Contract), StatusCodes.Status201Created)]
        public async Task<ActionResult<Contract>> CreateContract([FromBody] CreateContractRequest requestModel)
        {
            return Created("", _mockContract); ;
        }

        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateContract([FromBody] UpdateContractRequest requestModel)
        {
            return NoContent();
        }

        [HttpPut]
        [Route("{id}/reviewers")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> AssignReviewers([FromRoute] long id)
        {
            return NoContent();
        }

        private static Contract GetMockContract()
        {
            return new Contract()
            {
                Id = 1,
                TravelInfo = new TravelDetail()
                {
                    StartDate = DateTime.Now.AddDays(10),
                    EndDate = DateTime.Now.AddDays(40),
                    PlaceOfEnagement = "SMRSPL - Chennai",
                    ContractTerms = "9 Month(S) (+/-1 muatual consent of both parties)"
                },
                VesselInfo = new VesselDetail()
                {
                    CBA = "IBF JSU/NUSI/MUI-IMMAJ CA",
                    EmployerAgent = "Synergy Marine Pte Ltd",
                    Id = 1,
                    IMONuber = "9735062",
                    MLCHolder = "Synergy Marine Pte Ltd",
                    Name = "BW MESSINA",
                    Owner = "Sothern Route Maritime",
                    PortOfRegistry = "PANAMA | PANAMA"
                },
                SeafarerDetail = new SeafarerDetail()
                {
                    Address = "VPO Kalanaur PO",
                    CDCNumber = "MUM162653",
                    CrewCode = "04291",
                    PassportNumber = "R78223898",
                    Age = 30,
                    DateOfBirth = DateTime.Now.AddYears(-30),
                    Id = 1,
                    Name = "Malkeet Singh",
                    Nationality = "INDIA",
                    PlaceOfBirth = "Kalanaur",
                    Rank = "Able Bodied Seamen"
                },
                AttachmentDetail = new ContractAttachmentDetail()
                {
                    MedicalCertificateAttached = true,
                    NextOfKinFormAttached = true
                }
            };
        }
    }
}
