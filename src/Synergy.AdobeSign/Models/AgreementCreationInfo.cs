using System;
using System.Collections.Generic;
using System.Text;

namespace Synergy.AdobeSign.Models
{
    public class AgreementCreationInfo
    {
        public List<FileInformation> fileInfos { get; set; }
        public string name { get; set; }
        public List<ParticipantInfo> participantSetsInfo { get; set; }
        public string signatureType { get; set; }
        public string state { get; set; }
        public List<MergeFieldInfo> mergeFieldInfo { get; set; }
    }

    public class FileInformation
    {
        public string libraryDocumentId { get; set; }
    }

    public class ParticipantInfo
    {
        public List<MemberInfo> memberInfos { get; set; }
        public int order { get; set; }
        public string role { get; set; }
        public string label { get; set; }
        public string name{ get; set; }

    }

    public class MemberInfo
    {
        public string email { get; set; }
    }

    public class MergeFieldInfo
    {
        public string fieldName { get; set; }
        public string defaultValue { get; set; }
    }
}
