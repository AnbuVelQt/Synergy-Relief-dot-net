using System.Collections.Generic;

namespace Synergy.AdobeSign.Models
{
    public class AgreementCreationResponse
    {
        public string Id { get; set; }
        public IList<SigningUrl> SigningUrls { get; set; }
    }

    public class SigningUrlsResponse
    {
        public IList<signingUrlSetInfo> signingUrlSetInfos { get; set; }
    }

    public class signingUrlSetInfo
    {
        public IList<SigningUrl> SigningUrls { get; set; }
    }

    public class SigningUrl
    {
        public string email { get; set; }
        public string esignUrl { get; set; }
    }

}
