using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Synergy.ReliefCenter.Data.Entities.SalaryMatrix
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum WageComponentType
    {
        Earning,
        Deduction
    }
}
