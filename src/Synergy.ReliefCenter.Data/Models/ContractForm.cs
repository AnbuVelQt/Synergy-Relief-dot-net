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

        [ForeignKey(nameof(ContractId))]
        [InverseProperty("ContractForms")]
        public virtual VesselContract VesselContract { get; set; }
    }
}
