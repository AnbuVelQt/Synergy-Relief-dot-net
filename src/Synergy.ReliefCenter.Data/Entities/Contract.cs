using System;

namespace Synergy.ReliefCenter.Data.Entities
{
    public class Contract
    {
        public long Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public long VesselId { get; set; }
        public long SeafarerId { get; set; }
        public string Status { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}
