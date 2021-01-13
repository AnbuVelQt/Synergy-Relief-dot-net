using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Synergy.ReliefCenter.Data.Entities.VesselCenter
{
    public class Vessel
    {
        public Vessel()
        {
            FleetVesselsDetails = new HashSet<FleetVessels>();
        }
        public long Id { get; set; }

        public string Name { get; set; }

        public long? OwnerId { get; set; }

        public long? BeneficiaryOwnerId { get; set; }

        public long? ImoNumber { get; set; }

        public string ShipYard { get; set; }
        public long? PortId { get; set; }

        public VesselOwner OwnerDetails { get; set; }
        public Ports PortDetails { get; set; }

        [ForeignKey("VesselId")]
        public ICollection<FleetVessels> FleetVesselsDetails { get; set; }
    }
}
