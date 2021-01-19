using Synergy.ReliefCenter.Data.Models;
using Synergy.ReliefCenter.Data.Repositories.Abstraction.ReliefRepository;
using ContractForm = Synergy.ReliefCenter.Data.Models.ContractForm;

namespace Synergy.ReliefCenter.Data.Repositories.ReliefRepository
{
    public class ContractFormRepository : BaseReliefRepository<ContractForm>, IContractFormRepository
    {
        protected synergy_manningContext reliefContext;

        public ContractFormRepository(synergy_manningContext ReliefContext) : base(ReliefContext)
        {
            reliefContext = ReliefContext;
        }
    }
}
