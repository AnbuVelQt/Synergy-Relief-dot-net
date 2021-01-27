using Synergy.ReliefCenter.Data.Entities.Master;
using System.Threading.Tasks;

namespace Synergy.ReliefCenter.Data.Repositories.Abstraction
{
    public interface IMasterDataRepository
    {
        ValueTask<Rank> GetRankByIdAsync(long id);

        long GetDocumentCategoryByIdAsync(string name);
    }
}
