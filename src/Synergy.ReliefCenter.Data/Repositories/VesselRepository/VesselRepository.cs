using Synergy.ReliefCenter.Data.Contexts;
using Synergy.ReliefCenter.Data.Entities.Vessel;
using Synergy.ReliefCenter.Data.Repositories.Abstraction.VesselRepository;

namespace Synergy.ReliefCenter.Data.Repositories.VesselRepository
{
    public class VesselRepository : BaseVesselRepository<Vessel>, IVesselRepository
    {
        protected VesselContext vesselContext;

        public VesselRepository(VesselContext VesselContext) : base(VesselContext)
        {
            vesselContext = VesselContext;
        }
    }
}
