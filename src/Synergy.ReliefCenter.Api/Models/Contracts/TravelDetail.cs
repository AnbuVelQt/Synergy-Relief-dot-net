using System;

namespace Synergy.ReliefCenter.Api.Models
{
    public class TravelDetail
    {
        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string PlaceOfEnagement { get; set; }

        public string ContractTerms { get; set; }
    }
}
