using Synergy.ReliefCenter.Data.Models;
using Synergy.ReliefCenter.Data.Repositories.Abstraction;

namespace Synergy.ReliefCenter.Data.Repositories
{
    public class ManningDataRepository : IManningDataRepository
    {
        private readonly synergy_manningContext _context;

        public ManningDataRepository(synergy_manningContext context)
        {
            _context = context;
        }
    }
}
