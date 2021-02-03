using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synergy.ReliefCenter.Core.Domain.Models
{
    public class ContractWages
    {
        public decimal BasicAmount { get; set; }

        public decimal SpecialAllownce { get; set; }

        public decimal TotalMonthlyAmount { get; set; }

        public IList<WageComponents> CBAEarningComponents { get; set; }

        public IList<WageComponents> OtherEarningComponents { get; set; }

        public IList<WageComponents> DeductionComponents { get; set; }

        public OTRate OTRateCard { get; set; }
    }
}
