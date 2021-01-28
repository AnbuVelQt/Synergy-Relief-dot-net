using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Synergy.ReliefCenter.Data.Models
{
    [Table("ContractForm")]
    [Index(nameof(ContractId), Name = "index_contract_form_on_contract_id")]
    public class ContractForm
    {
        public long Id { get; set; }

        [Column("vessel_contract_id")]
        public long ContractId { get; set; }

        [Column(TypeName = "jsonb")]
        public string Data { get; set; }

        [Column("created_at", TypeName = "timestamp(6) without time zone")]
        public DateTime CreatedAt { get; set; }

        [Column("updated_at", TypeName = "timestamp(6) without time zone")]
        public DateTime UpdatedAt { get; set; }

        [Column("created_by", TypeName = "character varying")]
        public string CreatedBy { get; set; }

        [Column("updated_by", TypeName = "character varying")]
        public string UpdatedBy { get; set; }

        [ForeignKey(nameof(ContractId))]
        [InverseProperty("ContractForms")]
        public virtual VesselContract VesselContract { get; set; }
    }
}
