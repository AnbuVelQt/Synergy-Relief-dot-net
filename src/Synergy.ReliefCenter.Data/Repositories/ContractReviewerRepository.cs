using Synergy.ReliefCenter.Data.Models;
using Synergy.ReliefCenter.Data.Repositories.Abstraction.ReliefRepository;

namespace Synergy.ReliefCenter.Data.Repositories.ReliefRepository
{
    public class ContractReviewerRepository : BaseReliefRepository<ContractReviewer>, IContractReviewerRepository
    {
        protected synergy_manningContext reliefContext;

        public ContractReviewerRepository(synergy_manningContext ReliefContext) : base(ReliefContext)
        {
            reliefContext = ReliefContext;
        }
    }
}
