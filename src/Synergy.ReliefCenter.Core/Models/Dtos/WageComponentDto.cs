using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Synergy.ReliefCenter.Core.Models.Dtos
{
    public class WageComponentDTO
    {  
        public string Name { get; set; }

        public string AccountCode { get; set; }

        public string Frequency { get; set; }

        public string Type { get; set; }

        public decimal Amount { get; set; }

        public DateTime EffectiveDate { get; set; }
    }
}
