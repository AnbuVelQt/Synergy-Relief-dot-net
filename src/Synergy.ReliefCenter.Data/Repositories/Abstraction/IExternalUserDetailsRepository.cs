using Synergy.ReliefCenter.Data.Entities.Master;
using Synergy.ReliefCenter.Data.Entities.SalaryMatrix;
using System.Threading.Tasks;

namespace Synergy.ReliefCenter.Data.Repositories.Abstraction
{
    public interface IExternalUserDetailsRepository
    {
        Task<UserDetails> GetUserDetails(string userId, string apiKey, string userDetailsApiBaseUrl);
    }
}
