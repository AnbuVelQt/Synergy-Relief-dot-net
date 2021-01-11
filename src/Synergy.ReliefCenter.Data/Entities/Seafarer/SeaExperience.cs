using System;

namespace Synergy.ReliefCenter.Data.Entities.Seafarer
{
    public class SeaExperience
    {
        public long Id { get; set; }

        public long SeafarerId { get; set; }

        public long ExperienceInDays { get; set; }

        public long? ShipManagementCompanyId { get; set; }

        public long RankId { get; set; }

        public long VesselId { get; set; }

        public DateTime ToDate { get; set; }
    }
}
