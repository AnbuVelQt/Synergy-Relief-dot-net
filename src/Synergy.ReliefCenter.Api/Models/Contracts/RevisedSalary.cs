using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Synergy.ReliefCenter.Api.Models
{
    public class RevisedSalary
    {
        // TODO: Check whether effective date is required field while adding revised salary
        // Also can effective date in past
        public DateTime? EffectiveFromDate { get; set; }

        public string ReasonForRevision { get; set; }

        public decimal TotalMonthlyWage { get; set; }
    }
}
