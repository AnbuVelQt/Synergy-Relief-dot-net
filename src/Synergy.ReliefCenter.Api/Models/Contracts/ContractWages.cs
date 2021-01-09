using System.Collections.Generic;

namespace Synergy.ReliefCenter.Api.Models
{
    public class ContractWages
    {
        public decimal BasicAmount { get; set; }

        public decimal SpecialAllownce { get; set; }

        public decimal TotalMonthlyAmount { get; set; }

        public IList<WageComponent> CBAEarningComponents { get; set; }

        public IList<WageComponent> OtherEarningComponents { get; set; }

        public IList<WageComponent> DeductionComponents { get; set; }

        public OTRateCard OTRateCard {get;set;}
    }
}
