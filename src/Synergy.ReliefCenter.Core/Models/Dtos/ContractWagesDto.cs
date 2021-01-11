using System.Collections.Generic;

namespace Synergy.ReliefCenter.Core.Models.Dtos
{
    public class ContractWagesDto
    {
        public decimal BasicAmount { get; set; }

        public decimal SpecialAllownce { get; set; }

        public decimal TotalMonthlyAmount { get; set; }

        public IList<WageComponentDto> CBAEarningComponents { get; set; }

        public IList<WageComponentDto> OtherEarningComponents { get; set; }

        public IList<WageComponentDto> DeductionComponents { get; set; }

        public OTRateCardDto OTRateCard {get;set;}
    }
}
