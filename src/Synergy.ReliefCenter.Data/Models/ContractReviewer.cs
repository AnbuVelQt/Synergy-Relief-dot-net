using Microsoft.EntityFrameworkCore;
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

        [Column("created_at", TypeName = "timestamp(6) without time zone")]
        public DateTime CreatedAt { get; set; }

        [Column("updated_at", TypeName = "timestamp(6) without time zone")]
        public DateTime UpdatedAt { get; set; }

        [Column("created_by", TypeName = "character varying")]
        public string CreatedBy { get; set; }

        [Column("updated_by", TypeName = "character varying")]
        public string UpdatedBy { get; set; }

        [ForeignKey(nameof(ContractId))]
        [InverseProperty("ContractReviewers")]
        public virtual VesselContract VesselContracts { get; set; }

        [InverseProperty(nameof(VesselContract.Reviewer))]
        public virtual ICollection<VesselContract> Contract { get; set; }
    }
}
