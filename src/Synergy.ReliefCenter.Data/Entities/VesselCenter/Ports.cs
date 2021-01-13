using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synergy.ReliefCenter.Data.Entities.VesselCenter
{
    public class Ports
    {
        public Ports()
        {
            Vessele = new HashSet<Vessel>();
        }
        public long Id { get; set; }
        public string Name { get; set; }

        [ForeignKey("PortId")]
        public ICollection<Vessel> Vessele { get; set; }

    }
}
