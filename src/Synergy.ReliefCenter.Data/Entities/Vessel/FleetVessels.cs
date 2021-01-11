using System;

namespace Synergy.ReliefCenter.Data.Entities.Vessel
{
    public class FleetVessels
    {
        public long id { get; set; }

        public long fleet_id { get; set; }

        public long vessel_id { get; set; }

        public DateTime? deleted_at { get; set; }
    }
}
