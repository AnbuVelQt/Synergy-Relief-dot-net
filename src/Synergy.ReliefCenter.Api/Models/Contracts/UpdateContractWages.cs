using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Synergy.ReliefCenter.Api.Models
{
    public class UpdateContractWages
    {
        // TODO: SpecialAllowce can't be in minus
        public decimal SpecialAllowance { get; set; }

        public IList<WageComponent> OtherEarningComponents { get; set; }

        public IList<WageComponent> DeductionComponents { get; set; }
    }
}
