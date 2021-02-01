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
    public class AccessPoliciesRepository : BasePoliciesRepository<AccessPolicies>, IAccessPoliciesRepository
    {
        protected MasterDbContext masterDbContext;

        public AccessPoliciesRepository(MasterDbContext MasterDbContext) : base(MasterDbContext)
        {
            masterDbContext = MasterDbContext;
        }
    
    }
}
