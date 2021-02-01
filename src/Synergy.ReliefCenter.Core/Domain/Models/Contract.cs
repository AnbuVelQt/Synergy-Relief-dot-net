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
        public List<ContractReviewers> Reviewers { get; }
        // If there is not reviewer return null
        // If both reviewer has approved then return null
        // return first review which hasn't approved contract (If contract is inverification status)
        public ContractReviewers NextReviewer { get; set; }

        public ContractInformation Information { get; set; }
        public ContractStatus Status { get; set; }

        public static Contract CreateInstance(ContractInformation information)
        {
            return new Contract(information);
        }
        public void AssignReviewers(ContractReviewers[] reviewers)
        {
            // TODO: Add domain validation
            Reviewers.AddRange(reviewers);
            Status = ContractStatus.InVerification;
        }
        public void Approve(string comment)
        {
            // If contract status is InVerification
            if(Status == ContractStatus.InVerification)
            {
                NextReviewer.Approved = true;
                NextReviewer.ApprovedOn = DateTime.UtcNow;
            }
            
            // Check if all reviewer has approved the contract then change status to
            if(NextReviewer is null)
            {
                Status = ContractStatus.SignaturePending;
            }
            else
            {
                Status = ContractStatus.InVerification;
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
