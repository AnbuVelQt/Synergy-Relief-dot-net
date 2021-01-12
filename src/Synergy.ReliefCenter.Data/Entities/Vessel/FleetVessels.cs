using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Synergy.ReliefCenter.Data.Entities.Vessel
{
    public class FleetVessels
    {
        
        public long Id { get; set; }

        public long FleetId { get; set; }

        public long VesselId { get; set; }

        public DateTime? DeletedAt { get; set; }

        public Vessel VesselDetails { get; set; }
}
}
