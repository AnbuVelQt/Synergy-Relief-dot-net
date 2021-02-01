using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synergy.ReliefCenter.Core.Domain.Models
{
    public class ContractInformation
    {
        public TravelDetails TravelInfo { get; set; }

        public VesselDetails VesselInfo { get; set; }

        public SeafarerDetails SeafarerDetail { get; set; }

        public ContractWages Wages { get; set; }

        public IList<ContractReviewers> ContractReviewers { get; set; }

        public ContractReviewers NextReviewer { get; set; }

        public IList<RevisedSalary> RevisedSalaries { get; set; }
    }
}
