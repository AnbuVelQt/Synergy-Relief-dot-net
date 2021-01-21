using Synergy.AdobeSign.Configurations;
using Synergy.AdobeSign.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Synergy.AdobeSign.XUnitTest
{
    public class UnitTest1
    {
        private AgreementCreationInfo agreementCreationInfo;
        private AdobeSignRestClient adobeSignRestClient;
        public UnitTest1()
        {
            AdobeSignConfiguration _configuration = new AdobeSignConfiguration()
            {
                AccessKey = "3AAABLblqZhDUcRVexDXgOSEd0TWOxz-XYfWgPxBQhF1eYu5osWZoWZ-2ZPX3wRkLBYpBKExgOWXsNhQyUa4EykXbZUPqWFaA",
                ApiUrl = "https://api.in1.echosign.com:443",
                ContractDocumentId = "CBJCHBCAABAA6n2lxqPkvqZzRzIph8fZ85m_hYzMntqf",
                ApiVersion = "v6"
            };
            adobeSignRestClient = new AdobeSignRestClient(_configuration);
            var fileInfosList = new List<FileInformation>();
            string contractDocumentId = "CBJCHBCAABAA6n2lxqPkvqZzRzIph8fZ85m_hYzMntqf";
            fileInfosList.Add(new FileInformation { libraryDocumentId = contractDocumentId });
            var participantSetsInfoList = new List<ParticipantInfo>();
            var memberInfoList = new List<MemberInfo>();
            memberInfoList.Add(new MemberInfo { email = "pentagram@synergyship.com" });
            participantSetsInfoList.Add(new ParticipantInfo { memberInfos = memberInfoList, order = 1, role = "FORM_FILLER" });
            var mergeFieldInfoList = new List<MergeFieldInfo>();
            mergeFieldInfoList.Add(new MergeFieldInfo() { fieldName = "seafarerName", defaultValue = "Jhon" });
            agreementCreationInfo = new AgreementCreationInfo
            {
                fileInfos = fileInfosList,
                name = "Demo Check 197",
                participantSetsInfo = participantSetsInfoList,
                signatureType = "ESIGN",
                state = "IN_PROCESS",
                mergeFieldInfo = mergeFieldInfoList
            };
        }


        [Fact]
        public async Task TestAsync_ValidCase()
        {
            await adobeSignRestClient.CreateAgreementAsync(agreementCreationInfo);
        }

        [Fact]
        public void TestAsync_InValidCase_Configs()
        {
            AdobeSignRestClient adobeSignRestClientInvalid = new AdobeSignRestClient(new AdobeSignConfiguration());
            Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => adobeSignRestClientInvalid.CreateAgreementAsync(agreementCreationInfo));
        }

        [Fact]
        public async Task TestAsync_InValidCase_AgreementInfo()
        {
            var fileInfosList = new List<FileInformation>();
            string contractDocumentId = "CBJCHBCAABAA6n2lxqPkvqZzRzIph8fZ85m_hYzMntqf";
            fileInfosList.Add(new FileInformation { libraryDocumentId = contractDocumentId });
            var participantSetsInfoList = new List<ParticipantInfo>();
            var memberInfoList = new List<MemberInfo>();
            memberInfoList.Add(new MemberInfo { email = "pentagram@synergyship.com" });
            participantSetsInfoList.Add(new ParticipantInfo { memberInfos = memberInfoList, order = 1, role = "FORM_FILLER" });
            var mergeFieldInfoList = new List<MergeFieldInfo>();
            mergeFieldInfoList.Add(new MergeFieldInfo() { fieldName = "", defaultValue = "" });
            var agreementCreationInfoInvalid = new AgreementCreationInfo
            {
                fileInfos = fileInfosList,
                name = "",
                participantSetsInfo = participantSetsInfoList,
                signatureType = "",
                state = "",
                mergeFieldInfo = mergeFieldInfoList
            };
            var response = await Assert.ThrowsAsync<Exception>(() => adobeSignRestClient.CreateAgreementAsync(agreementCreationInfoInvalid));
        }
    }
}
