using Synergy.ReliefCenter.Data.Contexts;
using Synergy.ReliefCenter.Data.Entities.Seafarer;
using Synergy.ReliefCenter.Data.Repositories.Abstraction.SeafarerRepository;
using System.Linq;

namespace Synergy.ReliefCenter.Data.Repositories.SeafarerRepository
{
    public class SeafarerRepository : BaseSeafarerRepository<Seafarer>, ISeafarerRepository
    {
        protected SeafarerContext seafarerContext;

        public SeafarerRepository(SeafarerContext SeafarerContext) : base(SeafarerContext)
        {
            seafarerContext = SeafarerContext;
        }
        public Seafarer GetSeafarerRankById(int seafarerId)
        {
            return seafarerContext.Seafarers.SingleOrDefault(x => x.Id == seafarerId);
            
        }
    }
}
