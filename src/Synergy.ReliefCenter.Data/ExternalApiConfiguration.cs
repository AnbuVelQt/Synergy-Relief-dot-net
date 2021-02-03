using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Synergy.ReliefCenter.Data
{
    public class ExternalApiConfiguration
    {        
        public class CrewWageApi
        {
            public string ApiUrl { get; set; }
        }

        public class UserInfoApi
        {
            public string ApiKey { get; set; }

            public string ApiUrl { get; set; }
        }
    }
}
