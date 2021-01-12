using Synergy.ReliefCenter.Data.Contexts;
using Synergy.ReliefCenter.Data.Entities.Master;
using Synergy.ReliefCenter.Data.Repositories.Abstraction.MasterRepository;

namespace Synergy.ReliefCenter.Data.Repositories.MasterRepository
{
    public class RankRepository : BaseMasterRepository<Rank>, IRankRepository
    {
        protected MasterContext masterContext;

        public RankRepository(MasterContext MasterContext) : base(MasterContext)
        {
            masterContext = MasterContext;
        }
    }
}
