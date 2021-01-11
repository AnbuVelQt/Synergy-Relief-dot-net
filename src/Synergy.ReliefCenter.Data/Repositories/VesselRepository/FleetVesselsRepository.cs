using Synergy.ReliefCenter.Data.Contexts;
using Synergy.ReliefCenter.Data.Entities.Vessel;
using Synergy.ReliefCenter.Data.Interfaces.VesselRepository;

namespace Synergy.CrewWage.Data.Repositories.VesselRepository
{
    public class FleetVesselsRepository : BaseVesselRepository<FleetVessels>, IFleetVesselsRepository
    {
        protected VesselContext vesselContext;

        public FleetVesselsRepository(VesselContext VesselContext) : base(VesselContext)
        {
            vesselContext = VesselContext;
        }
    }
}
