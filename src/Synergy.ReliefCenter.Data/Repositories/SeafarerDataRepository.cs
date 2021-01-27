using Microsoft.EntityFrameworkCore;
using Synergy.ReliefCenter.Data.Contexts;
using Synergy.ReliefCenter.Data.Entities.SeafarerCenter;
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

        public async ValueTask<Seafarer> GetSeafarerByIdAsync(string id)
        {
            return await _context.Seafarers.FirstOrDefaultAsync(x => x.CdcNumber == id);
        }

        public async ValueTask<SeafarerContactDetails> GetSeafarerContactDetailsByIdAsync(long id)
        {
            return await _context.ContactDetails.FirstOrDefaultAsync(x => x.SeafarerId == id);
        }

        public async ValueTask<Seafarer> GetSeafarerByIdentityAsync(string userId)
        {
            return await _context.Seafarers.Where(x => x.IdentityUserId == userId).FirstOrDefaultAsync();
        }

        public async ValueTask<SeafarerDocuments> GetSeafarerDocumentsByIdAsync(long seafarerId, long id)
        {
            return await _context.SeafarerDocuments.Where(x => x.SeafarerId == seafarerId && x.DocumentSubCategoryId == id).FirstOrDefaultAsync();
        }
    }
}
