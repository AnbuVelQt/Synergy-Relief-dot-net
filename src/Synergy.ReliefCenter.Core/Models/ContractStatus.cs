using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Synergy.ReliefCenter.Core.Models
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ContractStatus
    {
        InDraft,
        InVerification,
        SignaturePending,
        Signed,
        Cancelled
    }
}
