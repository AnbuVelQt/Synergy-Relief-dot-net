using Microsoft.EntityFrameworkCore;
using Synergy.ReliefCenter.Data.Contexts;
using Synergy.ReliefCenter.Data.Entities.Seafarer;
using Synergy.ReliefCenter.Data.Repositories.Abstraction;
using System.Linq;
using System.Threading.Tasks;

namespace Synergy.ReliefCenter.Data.Repositories
{
    public class SeafarerDataRepository : ISeafarerDataRepository
    {
        private readonly SeafarerDbContext _context;

        public SeafarerDataRepository(SeafarerDbContext context)
        {
            _context = context;
        }

        public async ValueTask<Seafarer> GetSeafarerByIdAsync(long id)
        {
            return await _context.Seafarers.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async ValueTask<SeafarerContactDetails> GetSeafarerContactDetailsByIdAsync(long id)
        {
            return await _context.ContactDetails.FirstOrDefaultAsync(x => x.SeafarerId == id);
        }
    }
}
