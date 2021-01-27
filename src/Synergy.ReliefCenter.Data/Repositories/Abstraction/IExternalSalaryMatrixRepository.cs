using Synergy.ReliefCenter.Data.Entities.SalaryMatrix;
using System.Threading.Tasks;

namespace Synergy.ReliefCenter.Data.Repositories.Abstraction
{
    public interface IExternalSalaryMatrixRepository
    {
        Task<SalaryMatrix> GetSalaryMatrix(string vesselImoNumber, string seafarerCdcNumer, string AuthToken, string crewWageApiBaseUrl);
    }
}
