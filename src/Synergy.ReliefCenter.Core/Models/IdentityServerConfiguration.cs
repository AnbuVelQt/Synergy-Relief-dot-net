namespace Synergy.ReliefCenter.Core.Models
{
    public class IdentityServerConfiguration
    {
        public string ShoreAuthorityUrl { get; set; }
        public string ShoreApiKey { get; set; }
        public string SeafarerAuthorityUrl { get; set; }
    }

    public static class AuthenticationSchemas
    {
        public const string ShoreIdp = "ShoreIdp";
        public const string SeafarerIdp = "SeafarerIdp";

    }
}
