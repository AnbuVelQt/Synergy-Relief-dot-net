using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synergy.ReliefCenter.Core.Models.Dtos
{
    public class RevisedSalaryDto
    {
        public DateTime EffectiveFromDate { get; set; }

        public string ReasonForRevision { get; set; }

        public decimal TotalMonthlyWage { get; set; }
    }
}
