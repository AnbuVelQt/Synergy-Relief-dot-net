using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Synergy.AdobeSign.Models;

namespace Synergy.ReliefCenter.Api.Models.Events
{
    public class AdobeSignWebhookEventData
    {
        public string actingUserEmail { get; set; }
        public string actingUserId { get; set; }
        public string actingUserIpAddress { get; set; }
        public string actionType { get; set; }
        public Agreement agreement { get; set; }
        public string Event { get; set; }   //
        public DateTime eventDate { get; set; }
        public string eventResourceType { get; set; }
        public string participantRole { get; set; }
        public string participantUserEmail { get; set; }
        public string participantUserId { get; set; }
        public string webhookId { get; set; }
        public string webhookName { get; set; }
        public IList<WebhookNotificationApplicableUsers> webhookNotificationApplicableUsers { get; set; }
        public string webhookNotificationId { get; set; }
        public string webhookScope { get; set; }
        public WebhookUrlInfo webhookUrlInfo { get; set; }


    }

    public class Agreement
    {
        public string id { get; set; }
        public DateTime createdDate { get; set; }
        public bool documentVisibilityEnabled { get; set; }
        public string locale { get; set; }
        public string senderEmail { get; set; }
        public string signatureType { get; set; }
        public ParticipantSetsInfo participantSetsInfo { get; set; }
        public string name { get; set; }
        public string status { get; set; }
    }

    public class ParticipantSetsInfo
    {
        public IList<ParticipantInfo> participantSets { get; set; }
    }

    public class WebhookNotificationApplicableUsers
    {
        public string email { get; set; }
        public string id { get; set; }
        public bool payloadApplicable { get; set; }
        public string role { get; set; }
    }

    public class WebhookUrlInfo
    {
        public string url { get; set; }
    }

}
