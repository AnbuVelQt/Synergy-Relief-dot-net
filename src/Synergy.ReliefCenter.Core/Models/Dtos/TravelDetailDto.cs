using System;

namespace Synergy.ReliefCenter.Core.Models.Dtos
{
    public class TravelDetailDTO
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string PlaceOfEnagement { get; set; }

        public string ContractTerms { get; set; }
    }
}
