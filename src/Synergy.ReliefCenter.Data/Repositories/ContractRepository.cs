using Synergy.ReliefCenter.Data.Models;
using Synergy.ReliefCenter.Data.Repositories.Abstraction.ReliefRepository;

namespace Synergy.ReliefCenter.Data.Repositories.ReliefRepository
{
    public class ContractRepository: BaseReliefRepository<VesselContract>, IContractRepository
    {
        protected synergy_manningContext reliefContext;

        public ContractRepository(synergy_manningContext ReliefContext) : base(ReliefContext)
        {
            reliefContext = ReliefContext;
        }
    }
}
