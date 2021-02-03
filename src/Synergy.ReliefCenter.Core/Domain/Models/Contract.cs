using Synergy.ReliefCenter.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synergy.ReliefCenter.Core.Domain.Models
{
    public class Contract
    {
        // this is for automapper or contract entity to contract domain
        public Contract()
        {
        }
        private Contract(ContractInformation information)
        {
            Information = information;
            Reviewers = new List<ContractReviewers>();
        }
        public List<ContractReviewers> Reviewers { get; private set; }
        // If there is not reviewer return null
        // If both reviewer has approved then return null
        // return first review which hasn't approved contract (If contract is inverification status)

        public ContractReviewers NextReviewer { get { return Reviewers is null ? null : Reviewers?.FirstOrDefault(_ => _.Approved == false); } private set { NextReviewer = NextReviewer; } }

        public ContractInformation Information { get; set; }
        public ContractStatus Status { get;private set; }

        public static Contract CreateInstance(ContractInformation information)
        {
            return new Contract(information);
        }
        public void AssignReviewers(ContractReviewers[] reviewers)
        {
            // TODO: Add domain validation
            if(Reviewers is null)
            {
                List<ContractReviewers> reviewers1 = new List<ContractReviewers>();
                reviewers1.AddRange(reviewers.ToList());
                Reviewers = reviewers1;
            }            
            Status = ContractStatus.InVerification;
        }
        public void Verify()
        {
            if(Status == ContractStatus.InVerification)
            {
                if(NextReviewer != null && NextReviewer.Role == ReviewerRole.SourcingExecutive.ToString())
                {
                    NextReviewer.ApprovedOn = DateTime.UtcNow;
                    Status = ContractStatus.InApprove;
                    NextReviewer.Approved = true;
                }
            }
            
        }
        public void Approve()
        {
            if (Status == ContractStatus.InApprove)
            {
                if (NextReviewer != null && NextReviewer.Role == ReviewerRole.FleetHead.ToString())
                {
                    NextReviewer.ApprovedOn = DateTime.UtcNow;
                    NextReviewer.Approved = true;
                }

                if (NextReviewer is null)
                {
                    Status = ContractStatus.SignaturePending;
                }
            }
        }

        public void Reject(string comment)
        {
            NextReviewer.Approved = false;
            NextReviewer.ApprovedOn = DateTime.UtcNow;
            Status = ContractStatus.InDraft;
        }

    }
}
