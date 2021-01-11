using Synergy.CrewWage.Data.Repositories.SeafarerRepository;
using Synergy.ReliefCenter.Data.Contexts;
using Synergy.ReliefCenter.Data.Entities.Seafarer;
using Synergy.ReliefCenter.Data.Repositories.Abstraction.SeafarerRepository;

namespace Synergy.ReliefCenter.Data.Repositories.SeafarerRepository
{
    public class SeafarerContactDetailRepository : BaseSeafarerRepository<SeafarerContactDetails>, ISeafarerContactDetailRepository
    {
        protected SeafarerContext seafarerContext;

        public SeafarerContactDetailRepository(SeafarerContext SeafarerContext) : base(SeafarerContext)
        {
            seafarerContext = SeafarerContext;
        }
    }
}
