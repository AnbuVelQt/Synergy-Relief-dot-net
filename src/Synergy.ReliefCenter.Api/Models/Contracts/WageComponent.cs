using Synergy.ReliefCenter.Core.Models.Enum;
using System;

namespace Synergy.ReliefCenter.Api.Models
{
    public class WageComponent
    {
        public string Name { get; set; }

        public string AccountCode { get; set; }

        // TODO: Use enum for Frequency and Type to avoid adding wrong value in database
        public WageFrequency Frequency { get; set; }

        public string Type { get; set; }

        public decimal Amount { get; set; }

        // TODO: Check whether effective date is mandatory or not
        // Can it be in past
        public DateTime EffectiveDate { get; set; }
    }
}
