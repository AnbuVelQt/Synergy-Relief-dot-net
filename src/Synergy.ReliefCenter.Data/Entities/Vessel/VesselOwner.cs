using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synergy.ReliefCenter.Data.Entities.Vessel
{
    public class VesselOwner
    {
        public VesselOwner()
        {
            Vessel = new HashSet<Vessel>();
        }
        public long Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        [ForeignKey("OwnerId")]
        public ICollection<Vessel> Vessel { get; set; }
    }
}
