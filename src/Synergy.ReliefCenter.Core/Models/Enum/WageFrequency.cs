using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
namespace Synergy.ReliefCenter.Core.Models.Enum
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum WageFrequency
    {
        one_time,
        monthly
    }
}
