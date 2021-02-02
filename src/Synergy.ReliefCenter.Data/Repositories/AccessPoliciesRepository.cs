using Synergy.ReliefCenter.Data.Contexts;
using Synergy.ReliefCenter.Data.Entities.Master;
using Synergy.ReliefCenter.Data.Repositories.Abstraction.PolicyRepository;
using Synergy.ReliefCenter.Data.Repositories.ReliefRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public AccessPolicies GetAllowedRoles(string policyName, string UserId)
        {
            
            var AllowedRoles = (from ap in masterDbContext.AccessPolicies
                                join pr in masterDbContext.PolicyRoles on ap.Id equals pr.AccessPolicyId
                                join pu in masterDbContext.PolicyUsers on ap.Id equals pu.AccessPolicyId
                                where pr.Role == policyName || pu.UserId == UserId
                                select new AccessPolicies
                                {
                                    Name= ap.Name
                                }).FirstOrDefault();
            return AllowedRoles;
        }

    }
}
