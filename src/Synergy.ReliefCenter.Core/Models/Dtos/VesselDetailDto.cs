namespace Synergy.ReliefCenter.Core.Models.Dtos
{
    public class VesselDetailDto
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Owner { get; set; }

        public string EmployerAgent { get; set; }

        public string MLCHolder { get; set; }

        public long? IMONumber { get; set; }

        public string PortOfRegistry { get; set; }

        public string CBA { get; set; }
    }
}
