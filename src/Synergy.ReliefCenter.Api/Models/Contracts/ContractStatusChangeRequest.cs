using Synergy.ReliefCenter.Core.Models;

namespace Synergy.ReliefCenter.Api.Models
{
    public class ContractStatusChangeRequest
    {
        public ContractStatus Status { get; set; }

        public string Reason { get; set; }
    }
}
