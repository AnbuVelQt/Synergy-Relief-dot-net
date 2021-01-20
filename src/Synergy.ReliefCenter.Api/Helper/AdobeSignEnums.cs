using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Synergy.ReliefCenter.Api.Helper
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum AdobeStateEnum
    {
        DRAFT,
        AUTHORING,
        IN_PROCESS
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum AdobeSignatureTypeEnum
    {
        ESIGN,
        WRITTEN
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum AdobeRoleEnum
    {
        SIGNER,
        APPROVER,
        ACCEPTOR,
        CERTIFIED_RECIPIENT,
        FORM_FILLER,
        DELEGATE_TO_SIGNER,
        DELEGATE_TO_APPROVER,
        DELEGATE_TO_ACCEPTOR,
        DELEGATE_TO_CERTIFIED_RECIPIENT,
        DELEGATE_TO_FORM_FILLER,
        SHARE
    }
}
