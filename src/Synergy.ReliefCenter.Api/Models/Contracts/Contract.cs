using Synergy.ReliefCenter.Core.Models;
using System;
using System.Collections.Generic;

namespace Synergy.ReliefCenter.Api.Models
{
    public class Contract
    {
        public long Id { get; set; }

        public decimal? Salary { get; set; }

        public DateTime? VerifyDate { get; set; }

        public string VerifierName { get; set; }

        public string VerifierEmail { get; set; }

        public ContractStatus Status { get; set; }

        public TravelDetail TravelInfo { get; set; }

        public VesselDetail  VesselInfo { get; set; }

        public SeafarerDetail SeafarerDetail { get; set; }

        public ContractAttachmentDetail AttachmentDetail { get; set; }

        public ContractWages Wages { get; set; }

        public IList<Reviewers> ContractReviewers { get; set; }

        public Reviewers NextReviewer { get; set; }

        public IList<RevisedSalary> RevisedSalaries { get; set; }
    }
}
