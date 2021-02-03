using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synergy.ReliefCenter.Data.Entities.Master
{
    public class DocumentSubCategory
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public long DocumentGroupsCategoryId { get; set; }
    }
}
