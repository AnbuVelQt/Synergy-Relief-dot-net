using System.ComponentModel.DataAnnotations;

namespace Synergy.ReliefCenter.Data.Entities.Master
{
    public class AccessPolicy
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Identifier { get; set; }
        public PolicyRole PolicyRoles { get; set; }
        public PolicyUser PolicyUsers { get; set; }
    }
}
