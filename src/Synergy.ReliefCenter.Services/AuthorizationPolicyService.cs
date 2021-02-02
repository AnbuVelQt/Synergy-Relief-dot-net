using Synergy.ReliefCenter.Services.Abstraction;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Synergy.ReliefCenter.Data.Repositories.Abstraction.PolicyRepository;

namespace Synergy.ReliefCenter.Services
{
    public class AuthorizationPolicyService : IAuthorizationPolicyService
    {
        private readonly IAccessPoliciesRepository _accessPoliciesRepository;
        private readonly IApiRequestContext _apiRequestContext;
        public AuthorizationPolicyService(IAccessPoliciesRepository accessPoliciesRepository,  IApiRequestContext apiRequestContext)
        {
            _accessPoliciesRepository = accessPoliciesRepository;
            _apiRequestContext = apiRequestContext;
        }

        public bool Validate(string policy)
        {
                //var handler = new JwtSecurityTokenHandler();
                //var jwttoken = token.Replace("bearer ", string.Empty);
                //var tokenClaims = handler.ReadToken(jwttoken) as JwtSecurityToken;
                var tokenRoles = _apiRequestContext.AllowedRoles;
                 var tokenUserId = _apiRequestContext.UserId;
                var accessPolicy = _accessPoliciesRepository.GetAccessPolicy(policy);
                return (accessPolicy.AllowedRoles.Count() > 0  ? accessPolicy.AllowedRoles.Any(role => tokenRoles.Contains(role)) : false) || (accessPolicy.AllowedUsers.Count() > 0 ? accessPolicy.AllowedUsers.Any(user => user == tokenUserId) : false);
        }
    }
}
