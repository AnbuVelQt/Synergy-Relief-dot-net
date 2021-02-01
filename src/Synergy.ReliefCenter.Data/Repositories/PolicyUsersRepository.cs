using Synergy.ReliefCenter.Data.Contexts;
using Synergy.ReliefCenter.Data.Entities.Master;
using Synergy.ReliefCenter.Data.Repositories.Abstraction.PolicyRepository;

namespace Synergy.ReliefCenter.Data.Repositories
{
    public class PolicyUsersRepository : BasePoliciesRepository<PolicyUsers>, IPolicyUsersRepository
    {
        protected MasterDbContext masterDbContext;

        public PolicyUsersRepository(MasterDbContext MasterDbContext) : base(MasterDbContext)
        {
            masterDbContext = MasterDbContext;
        }
    }
}
