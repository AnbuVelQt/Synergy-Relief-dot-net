namespace Synergy.ReliefCenter.Core.Models
{
    public class AccessPolicyModel
    {

        public string Identifier { get; set; }
        public string Name { get; set; }
        public string[] AllowedRoles { get; set; }
        public string[] AllowedUsers { get; set; }
    }
}
