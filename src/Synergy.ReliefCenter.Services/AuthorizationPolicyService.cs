using Synergy.ReliefCenter.Services.Abstraction;
using System.Linq;
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
            var tokenRoles = _apiRequestContext.AllowedRoles;
            var tokenUserId = _apiRequestContext.UserId;
            var accessPolicy = _accessPoliciesRepository.GetAccessPolicy(policy);
            if(accessPolicy!=null)
            {
                return (accessPolicy.AllowedRoles.Any(role => tokenRoles.Contains(role))) || (accessPolicy.AllowedUsers.Any(user => user == tokenUserId));
            }
            return false;
        }
    }
}
