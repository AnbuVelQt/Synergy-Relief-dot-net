﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Synergy.ReliefCenter.Data.Models
{
    [Table("ContractReviewers")]
    [Index(nameof(ContractId), Name = "index_contract_reviewer_on_contract_id")]
    public partial class ContractReviewer
    {
        public ContractReviewer()
        {
            Contract = new HashSet<VesselContract>();
        }
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Column("contract_id")]
        public long ContractId { get; set; }

        [Column("reviewer_id")]
        public string ReviewerId { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("role")]
        public string Role { get; set; }

        [Column("approved")]
        public bool Approved { get; set; }

        [Column("approved_on")]
        public DateTime ApprovedOn { get; set; }

        [ForeignKey(nameof(ContractId))]
        [InverseProperty("ContractReviewers")]
        public virtual VesselContract VesselContracts { get; set; }

        [InverseProperty(nameof(VesselContract.Reviewer))]
        public virtual ICollection<VesselContract> Contract { get; set; }
    }
}
