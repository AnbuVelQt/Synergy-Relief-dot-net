using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Synergy.ReliefCenter.Data.Entities.SalaryMatrix
{
	public class SalaryMatrix
	{
		public decimal BasicAmount { get; set; }

		public decimal SpecialAllowance { get; set; }

		public decimal TotalMonthlyWages { get; set; }

		public IList<SalaryWageComponent> CBAEarningComponents { get; set; }

		public IList<SalaryWageComponent> OtherEarningComponents { get; set; }

		public IList<SalaryWageComponent> DeductionComponents { get; set; }

		public OTRateCard OTRate { get; set; }
	}
}
