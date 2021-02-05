using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synergy.ReliefCenter.Core.Domain.Models
{
    public class VesselDetails
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
