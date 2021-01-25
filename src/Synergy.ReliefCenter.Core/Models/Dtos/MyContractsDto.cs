using Synergy.ReliefCenter.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Synergy.ReliefCenter.Core.Models.Dtos
{
    public class MyContractsDto
    {
        public long Id { get; set; }
        public decimal? Salary { get; set; }

        public ContractStatus Status { get; set; }

        public string VesselName { get; set; }

        public string VesselOwner { get; set; }

        public string SeafarerName { get; set; }

        public string SeafarerCrewCode { get; set; }

        public string SeafarerCDCNumber { get; set; }

        public string SeafarerNationality { get; set; }

        public string SeafarerAddress { get; set; }

        public string SeafarerEmail { get; set; }

        public string SeafarerPhone { get; set; }

        public DateTime DateOfBirth { get; set; }

        public DateTime TravelStartDate { get; set; }

        public DateTime TravelEndDate { get; set; }

        public string PlaceOfEnagement { get; set; }

        public string ContractTerms { get; set; }
    }
}
