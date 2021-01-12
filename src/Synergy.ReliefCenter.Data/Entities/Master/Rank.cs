using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Synergy.ReliefCenter.Data.Entities.Seafarer;

namespace Synergy.ReliefCenter.Data.Entities.Master
{
    public class Rank
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public long RankCategory { get; set; }
    }
}
