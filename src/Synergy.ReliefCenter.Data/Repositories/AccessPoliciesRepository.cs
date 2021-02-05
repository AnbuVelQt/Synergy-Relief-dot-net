using Synergy.ReliefCenter.Core.Models;
using Synergy.ReliefCenter.Data.Contexts;
using Synergy.ReliefCenter.Data.Repositories.Abstraction.PolicyRepository;
using System.Collections.Generic;
using System.Linq;

namespace Synergy.ReliefCenter.Data.Repositories
{
    public class AccessPoliciesRepository :  IAccessPoliciesRepository
    {
        protected MasterDbContext masterDbContext;

        public AccessPoliciesRepository(MasterDbContext MasterDbContext) : base()
        {
            masterDbContext = MasterDbContext;
        }

        public AccessPolicyModel GetAccessPolicy(string policyIdentifier)
        {
            List<string> Roles = (from ap in masterDbContext.AccessPolicies
                                  join pr in masterDbContext.PolicyRoles on ap.Id equals pr.AccessPolicyId
                                  where ap.Identifier == policyIdentifier
                                  select pr.Role).ToList();
            List<string> Users = (from ap in masterDbContext.AccessPolicies
                                  join pu in masterDbContext.PolicyUsers on ap.Id equals pu.AccessPolicyId
                                  where ap.Identifier == policyIdentifier
                                  select pu.UserId).ToList();
            var AccessPolicy = (from ap in masterDbContext.AccessPolicies
                                where ap.Identifier == policyIdentifier
                                select new AccessPolicyModel
                                {
                                    Name = ap.Name,
                                    Identifier = ap.Identifier,
                                    AllowedRoles = Roles,
                                    AllowedUsers = Users
                                }).FirstOrDefault();
            return AccessPolicy;
        }

    }
}
