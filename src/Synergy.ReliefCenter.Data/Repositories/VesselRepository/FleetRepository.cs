using Synergy.ReliefCenter.Data.Contexts;
using Synergy.ReliefCenter.Data.Entities.Vessel;
using Synergy.ReliefCenter.Data.Interfaces.VesselRepository;

namespace Synergy.CrewWage.Data.Repositories.VesselRepository
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
