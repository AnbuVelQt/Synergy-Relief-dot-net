using Synergy.ReliefCenter.Data.Entities.Seafarer;
using Synergy.ReliefCenter.Data.Repositories;

namespace Synergy.ReliefCenter.Data.Interfaces.SeafarerRepository
{
    public interface ISeafarerRepository : IBaseRepository<Seafarer>
    {
        Seafarer GetSeafarerRankById(int seafarerId);
    }
}
