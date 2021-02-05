using Synergy.AdobeSign.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Synergy.AdobeSign
{
    public interface IAdobeSignRestClient
    {
        Task<AgreementCreationResponse> CreateAgreementAsync(AgreementCreationInfo agreementInfo, CancellationToken cancellationToken = default);
    }
}
