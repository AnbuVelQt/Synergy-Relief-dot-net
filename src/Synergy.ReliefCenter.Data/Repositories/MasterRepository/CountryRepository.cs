using Synergy.ReliefCenter.Data.Contexts;
using Synergy.ReliefCenter.Data.Entities.Master;
using Synergy.ReliefCenter.Data.Repositories.Abstraction.MasterRepository;

namespace Synergy.ReliefCenter.Data.Repositories.MasterRepository
{
    public class CountryRepository: BaseMasterRepository<Country>, ICountryRepository
    {
        protected MasterContext masterContext;

        public CountryRepository(MasterContext MasterContext) : base(MasterContext)
        {
            masterContext = MasterContext;
        }
    }
}
