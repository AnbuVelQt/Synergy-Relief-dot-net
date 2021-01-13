using Microsoft.EntityFrameworkCore;
using Synergy.ReliefCenter.Data.Contexts;
using Synergy.ReliefCenter.Data.Entities.VesselCenter;
using Synergy.ReliefCenter.Data.Repositories.Abstraction;
using System.Linq;
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

        public async ValueTask<Vessel> GetVesselByIdAsync(long id)
        {
            return await _context.Vessels.Include(x=>x.OwnerDetails).Include(x => x.PortDetails).Include(x => x.FleetVesselsDetails).FirstOrDefaultAsync(x=>x.Id == id);
        }
    }
}
