using Synergy.ReliefCenter.Data.Contexts;
using Synergy.ReliefCenter.Data.Entities.Master;
using Synergy.ReliefCenter.Data.Repositories.Abstraction.PolicyRepository;

namespace Synergy.ReliefCenter.Data.Repositories
{
    public class PolicyRolesRepository : BasePoliciesRepository<PolicyRoles>, IPolicyRolesRepository
    {
        protected MasterDbContext masterDbContext;

        public PolicyRolesRepository(MasterDbContext MasterDbContext) : base(MasterDbContext)
        {
                masterDbContext = MasterDbContext;
        }

    }
}
