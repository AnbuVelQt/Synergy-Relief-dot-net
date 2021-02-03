using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synergy.ReliefCenter.Data.Entities.SeafarerCenter
{
    public class SeafarerDocuments
    {
        public long Id { get; set; }
        public DateTime IssueDate { get; set; }

        public DateTime ExpiryDate { get; set; }
        public long SeafarerId { get; set; }
        public long DocumentGroupId { get; set; }
        public long DocumentCategoryId { get; set; }
        public long DocumentSubCategoryId { get; set; }
        public string Number { get; set; }
    }
}
