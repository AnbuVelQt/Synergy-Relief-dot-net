using System.ComponentModel.DataAnnotations.Schema;

namespace Synergy.ReliefCenter.Data.Entities
{
    public class ContractForm
    {
        public long Id { get; set; }

        public long ContractId { get; set; }

        [Column(TypeName = "jsonb")]
        public string Data { get; set; }
    }
}
