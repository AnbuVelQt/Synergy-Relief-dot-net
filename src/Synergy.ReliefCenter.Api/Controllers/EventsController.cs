using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Synergy.ReliefCenter.Api.Models;
using Synergy.ReliefCenter.Api.Models.Events;
using Synergy.ReliefCenter.Api.Validations;
using Synergy.ReliefCenter.Core.Models.Dtos;
using Synergy.ReliefCenter.Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Synergy.ReliefCenter.Api.Controllers
{
    public class EventsController : ApiControllerBase
    {
        public EventsController(IContractService contractService, IMapper mapper, IConfiguration configuration)
        {

        }

        [HttpGet]
        [Route("adobesign")]
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status200OK)]
        public ActionResult AdobeVerifyWebhookUrl()
        {
            string clientId = Request.Headers["x-adobesign-clientid"];
            Response.Headers.Add("X-AdobeSign-ClientId", clientId);
            return Ok();
        }

        [HttpPost]
        [Route("adobesign")]
        [ProducesResponseType(typeof(AdobeSignWebhookEventData), StatusCodes.Status200OK)]
        public ActionResult AdobeWebhookEvents(AdobeSignWebhookEventData eventData)
        {
            string clientId = Request.Headers["x-adobesign-clientid"];
            Response.Headers.Add("X-AdobeSign-ClientId", clientId);
            return Ok();
        }

    }
}
