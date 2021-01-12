using Synergy.ReliefCenter.Data.Entities.Seafarer;

namespace Synergy.ReliefCenter.Data.Repositories.Abstraction.SeafarerRepository
{
    public interface ISeafarerRepository : IBaseRepository<Seafarer>
    {
        Seafarer GetSeafarerRankById(int seafarerId);
    }
}
