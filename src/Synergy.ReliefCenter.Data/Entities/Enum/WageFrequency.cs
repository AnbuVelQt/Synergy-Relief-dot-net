using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
namespace Synergy.ReliefCenter.Data.Entities.Enum
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum WageFrequency
    {
        one_time,
        monthly
    }
}
