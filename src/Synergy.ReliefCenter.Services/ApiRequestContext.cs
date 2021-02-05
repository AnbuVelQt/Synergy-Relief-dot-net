using Microsoft.AspNetCore.Http;
using Synergy.ReliefCenter.Services.Abstraction;
using System.Collections.Generic;
using System.Linq;

namespace Synergy.ReliefCenter.Services
{
    public class ApiRequestContext : IApiRequestContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
      
        public ApiRequestContext(IHttpContextAccessor httpRequestProcessor)
        {
            _httpContextAccessor = httpRequestProcessor;
        }
        public string UserId
        {
            get
            {
                return _httpContextAccessor.HttpContext.User.Claims.First(_ => _.Type == "user_id").Value;
            }
        }
        public List<string> AllowedRoles
        {
            
            get
            {
                return  _httpContextAccessor.HttpContext.User.Claims.Where(claim => claim.Type.Equals("http://schemas.microsoft.com/ws/2008/06/identity/claims/role")).Select(c => c.Value).ToList() ;

            }
        }
    }


}
