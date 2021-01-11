using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synergy.ReliefCenter.Data.Entities.Seafarer
{
    public class SeafarerContactDetails
    {
        public long Id { get; set; }
        public long SeafarerId { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public long StateId { get; set; }
        public long CountryId { get; set; }
        public string PinCode { get; set; }
        public string City { get; set; }
    }
}
