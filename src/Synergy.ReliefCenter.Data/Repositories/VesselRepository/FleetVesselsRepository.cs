using Synergy.ReliefCenter.Data.Contexts;
using Synergy.ReliefCenter.Data.Entities.Vessel;
using Synergy.ReliefCenter.Data.Repositories.Abstraction.VesselRepository;

namespace Synergy.ReliefCenter.Data.Repositories.VesselRepository
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
