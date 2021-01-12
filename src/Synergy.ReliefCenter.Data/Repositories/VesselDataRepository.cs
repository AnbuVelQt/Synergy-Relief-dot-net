using Synergy.ReliefCenter.Data.Contexts;
using Synergy.ReliefCenter.Data.Entities.Vessel;
using Synergy.ReliefCenter.Data.Repositories.Abstraction;
using System.Threading.Tasks;

namespace Synergy.ReliefCenter.Data.Repositories
{
    public class VesselDataRepository : IVesselDataRepository
    {
        private readonly VesselDbContext _context;

        public VesselDataRepository(VesselDbContext context)
        {
            _context = context;
        }

        public ValueTask<Vessel> GetVesselByIdAsync(long id)
        {
            throw new System.NotImplementedException();
        }
    }
}
