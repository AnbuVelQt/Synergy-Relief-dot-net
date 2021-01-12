using Synergy.ReliefCenter.Data.Contexts;
using Synergy.ReliefCenter.Data.Entities.Vessel;
using Synergy.ReliefCenter.Data.Repositories.Abstraction.VesselRepository;

namespace Synergy.ReliefCenter.Data.Repositories.VesselRepository
{
    public class FleetRepository : BaseVesselRepository<Fleets>, IFleetRepository
    {
        protected VesselContext vesselContext;

        public FleetRepository(VesselContext VesselContext) : base(VesselContext)
        {
            vesselContext = VesselContext;
        }
    }
}
