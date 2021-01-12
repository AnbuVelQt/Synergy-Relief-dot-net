using Synergy.ReliefCenter.Data.Contexts;
using Synergy.ReliefCenter.Data.Entities.Master;
using Synergy.ReliefCenter.Data.Repositories.Abstraction.MasterRepository;

namespace Synergy.ReliefCenter.Data.Repositories.MasterRepository
{
    public class NationalityRepository : BaseMasterRepository<Nationality>, INationalityRepository
    {
        protected MasterContext masterContext;

        public NationalityRepository(MasterContext MasterContext) : base(MasterContext)
        {
            masterContext = MasterContext;
        }
    }
}
