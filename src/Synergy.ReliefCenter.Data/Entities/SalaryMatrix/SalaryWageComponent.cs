using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synergy.ReliefCenter.Data.Entities.SalaryMatrix
{
    public class SalaryWageComponent
    {
        public string Name { get; set; }

        public string AccountCode { get; set; }

        public WageFrequency Frequency { get; set; }

        public WageComponentType Type { get; set; }

        public decimal Amount { get; set; }

        public DateTime EffectiveDate { get; set; }

        public int? MinExperience { get; set; }

        public int? MaxExperience { get; set; }
    }
}
