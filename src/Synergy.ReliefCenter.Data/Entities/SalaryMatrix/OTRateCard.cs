namespace Synergy.ReliefCenter.Data.Entities.SalaryMatrix
{
    public class OTRateCard
    {
        public int MinHours { get; set; }

        public int MaxHours { get; set; }

        public decimal PerHourRate { get; set; }
      
        public decimal TotalAmount
        {
            get { return PerHourRate * MinHours; }
        }
    }
}
