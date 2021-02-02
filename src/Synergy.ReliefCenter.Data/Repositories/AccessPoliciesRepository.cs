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
        private object policyRoles;

        public AccessPoliciesRepository(MasterDbContext MasterDbContext) : base()
        {
            masterDbContext = MasterDbContext;
        }

        public AccessPolicyModel GetAccessPolicy(string policyIdentifier)
        {
           
            var AccessPolicy = (from ap in masterDbContext.AccessPolicies
                                join pr in masterDbContext.PolicyRoles on ap.Id equals pr.AccessPolicyId
                                join pu in masterDbContext.PolicyUsers on ap.Id equals pu.AccessPolicyId
                                where ap.Identifier == policyIdentifier 
                                select new AccessPolicyModel
                                {
                                    Name= ap.Name,
                                    Identifier=ap.Identifier,
                                    AllowedRoles=  new string[] {pr.Role },
                                    AllowedUsers = new string[] {pu.UserId }
                                }).FirstOrDefault();
           
            return AccessPolicy;
        }

    }
}
