using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Synergy.ReliefCenter.Data.Models
{
    [Table("vessel_contracts")]
    [Index(nameof(EndDate), Name = "index_vessel_contracts_on_end_date")]
    [Index(nameof(RankId), Name = "index_vessel_contracts_on_rank_id")]
    [Index(nameof(SeafarerId), Name = "index_vessel_contracts_on_seafarer_id")]
    [Index(nameof(StartDate), Name = "index_vessel_contracts_on_start_date")]
    [Index(nameof(VesselId), Name = "index_vessel_contracts_on_vessel_id")]
    [Index(nameof(NextReviewer), Name = "index_vessel_contracts_on_next_reviewer")]
    [Index(nameof(CdcNumber), Name = "index_vessel_contracts_on_cdc_number")]
    [Index(nameof(ImoNumber), Name = "index_vessel_contracts_on_imo_number")]
    public partial class VesselContract
    {
        public VesselContract()
        {
            Reliefs = new HashSet<Relief>();
            ContractForms = new HashSet<ContractForm>();
            ContractReviewers = new HashSet<ContractReviewer>();
        }

        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Column("seafarer_id")]
        public long? SeafarerId { get; set; }
        [Column("vessel_id")]
        public long? VesselId { get; set; }
        [Column("salary")]
        public double? Salary { get; set; }
        [Column("start_date")]
        public DateTime? StartDate { get; set; }
        [Column("end_date")]
        public DateTime? EndDate { get; set; }
        [Column("created_at", TypeName = "timestamp(6) without time zone")]
        public DateTime CreatedAt { get; set; }
        [Column("updated_at", TypeName = "timestamp(6) without time zone")]
        public DateTime UpdatedAt { get; set; }
        [Column("created_by_id", TypeName = "character varying")]
        public string CreatedById { get; set; }
        [Column("created_by_name", TypeName = "character varying")]
        public string CreatedByName { get; set; }
        [Column("updated_by_id", TypeName = "character varying")]
        public string UpdatedById { get; set; }
        [Column("updated_by_name", TypeName = "character varying")]
        public string UpdatedByName { get; set; }
        [Column("rank_id")]
        public long? RankId { get; set; }
        [Column("deleted_at")]
        public DateTime? DeletedAt { get; set; }

        [Column("next_reviewer")]
        public long? NextReviewer { get; set; }

        [Column("status")]
        public string Status { get; set; }

        [Column("imo_number")]
        public string ImoNumber { get; set; }

        [Column("cdc_number")]
        public string CdcNumber { get; set; }


        [InverseProperty(nameof(Relief.VesselContract))]
        public virtual ICollection<Relief> Reliefs { get; set; }

        
        [InverseProperty(nameof(ContractForm.VesselContract))]
        public virtual ICollection<ContractForm> ContractForms { get; set; }

        [Column("ref_agreement_id", TypeName = "character varying")]
        public string ReferenceAgreementId { get; set; }

        [InverseProperty(nameof(ContractReviewer.VesselContracts))]
        public virtual ICollection<ContractReviewer> ContractReviewers { get; set; }

        [ForeignKey(nameof(NextReviewer))]
        [InverseProperty("Contract")]
        public virtual ContractReviewer Reviewer { get; set; }
    }
}
