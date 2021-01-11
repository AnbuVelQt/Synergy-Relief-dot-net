using Synergy.ReliefCenter.Data.Contexts;
using Synergy.ReliefCenter.Data.Entities.Vessel;
using Synergy.ReliefCenter.Data.Interfaces.VesselRepository;

namespace Synergy.CrewWage.Data.Repositories.VesselRepository
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
