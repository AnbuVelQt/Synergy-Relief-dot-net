using Synergy.ReliefCenter.Data.Contexts;
using Synergy.ReliefCenter.Data.Repositories.Abstraction;

namespace Synergy.ReliefCenter.Data.Repositories
{
    public class SeafarerDataRepository : ISeafarerDataRepository
    {
        private readonly SeafarerDbContext _context;

        public SeafarerDataRepository(SeafarerDbContext context)
        {
            _context = context;
        }
    }
}
