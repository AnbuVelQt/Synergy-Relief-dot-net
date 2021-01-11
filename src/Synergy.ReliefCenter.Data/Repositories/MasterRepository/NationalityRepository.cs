using Synergy.ReliefCenter.Data.Contexts;
using Synergy.ReliefCenter.Data.Entities.Master;
using Synergy.ReliefCenter.Data.Interfaces.MasterRepository;

namespace Synergy.CrewWage.Data.Repositories.MasterRepository
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
