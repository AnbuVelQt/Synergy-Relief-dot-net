using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synergy.ReliefCenter.Core.Domain.Models
{
    public class TravelDetails
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string PlaceOfEnagement { get; set; }

        public string ContractTerms { get; set; }
    }
}
