using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Synergy.ReliefCenter.Api.Models
{
    public class VesselDetail
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Owner { get; set; }

        public string EmployerAgent { get; set; }

        public string MLCHolder { get; set; }

        public string IMONumber { get; set; }

        public string PortOfRegistry { get; set; }

        public string CBA { get; set; }
    }
}
