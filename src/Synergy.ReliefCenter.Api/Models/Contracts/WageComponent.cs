using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Synergy.ReliefCenter.Api.Models
{
    public class WageComponent
    {
        public string Name { get; set; }

        public string AccountCode { get; set; }

        public string Frequency { get; set; }

        public string Type { get; set; }

        public decimal Amount { get; set; }
    }
}
