using Synergy.ReliefCenter.Core.Models.Dtos;
using System.Collections.Generic;

namespace Synergy.ReliefCenter.Core.Models.Dtos
{
    public class UpdateContractWagesDTO
    {
        public decimal SpecialAllowance { get; set; }

        public IList<WageComponentDTO> OtherEarningComponents { get; set; }

        public IList<WageComponentDTO> DeductionComponents { get; set; }
    }
}
