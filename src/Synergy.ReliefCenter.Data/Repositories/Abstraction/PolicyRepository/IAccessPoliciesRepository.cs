using Synergy.ReliefCenter.Core.Models;

namespace Synergy.ReliefCenter.Data.Repositories.Abstraction.PolicyRepository
{
    public interface IAccessPoliciesRepository 
    {
        public AccessPolicyModel GetAccessPolicy(string policyIdentifier);
    }
}
