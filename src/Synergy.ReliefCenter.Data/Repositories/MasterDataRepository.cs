using Synergy.ReliefCenter.Data.Contexts;
using Synergy.ReliefCenter.Data.Entities.Master;
using Synergy.ReliefCenter.Data.Repositories.Abstraction;
using System.Threading.Tasks;

namespace Synergy.ReliefCenter.Data.Repositories
{
    public class MasterDataRepository : IMasterDataRepository
    {
        private readonly MasterDbContext _context;

        public MasterDataRepository(MasterDbContext context)
        {
            _context = context;
        }

        public ValueTask<Rank> GetRankByIdAsync(long id)
        {
            return _context.Ranks.FindAsync(id);
        }
    }
}
