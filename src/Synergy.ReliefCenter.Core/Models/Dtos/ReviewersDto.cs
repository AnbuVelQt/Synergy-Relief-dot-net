using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synergy.ReliefCenter.Core.Models.Dtos
{
    public class ReviewersDTO
    {
        public string ReviewerId { get; set; }

        public string Role { get; set; }

        public bool Approved { get; set; }

        public DateTime ApprovedOn { get; set; }

        public UserInfoDTO UserInfo { get; set; }
    }
}
