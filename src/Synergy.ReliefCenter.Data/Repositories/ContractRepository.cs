using Synergy.ReliefCenter.Data.Contexts;
using Synergy.ReliefCenter.Data.Entities;
using Synergy.ReliefCenter.Data.Repositories.Abstraction.ReliefRepository;

namespace Synergy.ReliefCenter.Data.Repositories.ReliefRepository
{
    public class ContractRepository: BaseReliefRepository<Contract>, IContractRepository
    {
        protected ReliefDbContext reliefContext;

        public ContractRepository(ReliefDbContext ReliefContext) : base(ReliefContext)
        {
            reliefContext = ReliefContext;
        }
    }
}
