using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Synergy.ReliefCenter.Api.Models
{
    public class RevisedSalary
    {
        public DateTime EffectiveFromDate { get; set; }

        public string ReasonForRevision { get; set; }

        public decimal TotalMonthlyWage { get; set; }
    }
}
