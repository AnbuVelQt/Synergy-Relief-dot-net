using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Synergy.ReliefCenter.Core.Models.Enum
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum CrewWageImportContentType
    {
        Union,
        UnionVessel,
        CompanyWageChart,
        CBAWageChart,
        CompanyWageComponent,
        WageComponent
    }
}
