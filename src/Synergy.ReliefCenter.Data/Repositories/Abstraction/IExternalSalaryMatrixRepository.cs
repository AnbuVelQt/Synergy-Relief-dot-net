using Synergy.ReliefCenter.Data.Entities.SalaryMatrix;
using System.Threading.Tasks;

namespace Synergy.ReliefCenter.Data.Repositories.Abstraction
{
    public interface IExternalSalaryMatrixRepository
    {
        Task<SalaryMatrix> GetSalaryMatrix(long vesselId, long seafarerId, string authToken);
    }
}
