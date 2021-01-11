namespace Synergy.ReliefCenter.Data.Entities.Vessel
{
    public class Vessel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public long OwnerId { get; set; }

        public long? BeneficiaryOwnerId { get; set; }

        public long? ImoNumber { get; set; }
        public string ShipYard { get; set; }
        public long PortId { get; set; }
    }
}
