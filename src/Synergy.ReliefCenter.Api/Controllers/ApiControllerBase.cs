using Microsoft.AspNetCore.Mvc;

namespace Synergy.ReliefCenter.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public abstract class ApiControllerBase : ControllerBase
    {
    }
}
