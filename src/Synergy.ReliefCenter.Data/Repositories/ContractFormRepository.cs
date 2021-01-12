using Synergy.ReliefCenter.Data.Contexts;
using Synergy.ReliefCenter.Data.Entities;
using Synergy.ReliefCenter.Data.Repositories.Abstraction.ReliefRepository;

namespace Synergy.ReliefCenter.Data.Repositories.ReliefRepository
{
    public class ContractFormRepository : BaseReliefRepository<ContractForm>, IContractFormRepository
    {
        protected ReliefDbContext reliefContext;

        public ContractFormRepository(ReliefDbContext ReliefContext) : base(ReliefContext)
        {
            reliefContext = ReliefContext;
        }
    }
}
