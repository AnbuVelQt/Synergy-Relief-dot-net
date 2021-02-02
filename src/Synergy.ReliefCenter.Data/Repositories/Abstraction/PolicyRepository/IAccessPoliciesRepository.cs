using Synergy.ReliefCenter.Data.Entities.Master;

namespace Synergy.ReliefCenter.Data.Repositories.Abstraction.PolicyRepository
{
    public interface IAccessPoliciesRepository 
    {
        public AccessPolicies GetAllowedRoles(string policyName, string UserId);
    }
}
