using System.Collections.Generic;

namespace Synergy.ReliefCenter.Core.Models.Dtos
{
    public class ContractWagesDTO
    {
        public decimal BasicAmount { get; set; }

        public decimal SpecialAllowance { get; set; }

        public decimal TotalMonthlyAmount { get; set; }

        public IList<WageComponentDTO> CBAEarningComponents { get; set; }

        public IList<WageComponentDTO> OtherEarningComponents { get; set; }

        public IList<WageComponentDTO> DeductionComponents { get; set; }

        public OTRateCardDTO OTRateCard {get;set;}
    }
}
