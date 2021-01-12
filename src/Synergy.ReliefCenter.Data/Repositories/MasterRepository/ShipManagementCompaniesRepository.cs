using Synergy.ReliefCenter.Data.Contexts;
using Synergy.ReliefCenter.Data.Entities.Master;
using Synergy.ReliefCenter.Data.Repositories.Abstraction.MasterRepository;

namespace Synergy.ReliefCenter.Data.Repositories.MasterRepository
{
    public class ShipManagementCompaniesRepository : BaseMasterRepository<ShipManagementCompanies>, IShipManagementCompaniesRepository
    {
        protected MasterContext masterContext;

        public ShipManagementCompaniesRepository(MasterContext MasterContext) : base(MasterContext)
        {
            masterContext = MasterContext;
        }
    }
}
