using Synergy.ReliefCenter.Data.Contexts;
using Synergy.ReliefCenter.Data.Entities;
using Synergy.ReliefCenter.Data.Repositories.Abstraction.ReliefRepository;
using Synergy.ReliefCenter.Data.Repositories.ReliefRepository;

namespace Synergy.ReliefCenter.Data.Repositories.ReliefRepository
{
    public class ContractRepository: BaseReliefRepository<Contract>, IContractRepository
    {
        protected ReliefContext reliefContext;

        public ContractRepository(ReliefContext ReliefContext) : base(ReliefContext)
        {
            reliefContext = ReliefContext;
        }
    }
}
