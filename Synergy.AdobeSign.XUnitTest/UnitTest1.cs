using Newtonsoft.Json.Linq;
using Synergy.AdobeSign.Configurations;
using Synergy.AdobeSign.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Synergy.AdobeSign.XUnitTest
{
    public class UnitTest1
    {
        private AgreementCreationInfo agreementCreationInfo;
        private AdobeSignRestClient adobeSignRestClient;
        private readonly IHttpClientFactory _clientFactory;
        public UnitTest1()
        {
            AdobeSignConfiguration _configuration = new AdobeSignConfiguration()
            {
                AccessKey = "3AAABLblqZhDUcRVexDXgOSEd0TWOxz-XYfWgPxBQhF1eYu5osWZoWZ-2ZPX3wRkLBYpBKExgOWXsNhQyUa4EykXbZUPqWFaA",
                ApiUrl = "https://api.in1.echosign.com:443",
                ApiVersion = "v6"
            };
            adobeSignRestClient = new AdobeSignRestClient(_configuration,_clientFactory);
            var fileInfosList = new List<FileInformation>();
            string contractDocumentId = "CBJCHBCAABAAuIj3UtlJc9u7_PKAdBAu3UqrGBXJWqrT";
            fileInfosList.Add(new FileInformation { libraryDocumentId = contractDocumentId });
            var participantSetsInfoList = new List<ParticipantInfo>();
            var memberInfoList1 = new List<MemberInfo>();
            memberInfoList1.Add(new MemberInfo { email = "pentagram@synergyship.com" });
            participantSetsInfoList.Add(new ParticipantInfo { memberInfos = memberInfoList1, order = 1, role = "SIGNER" });
            var memberInfoList2 = new List<MemberInfo>();
            memberInfoList2.Add(new MemberInfo { email = "anbu.vel@qantler.com" });
            participantSetsInfoList.Add(new ParticipantInfo { memberInfos = memberInfoList2, order = 2, role = "SIGNER" });
            var mergeFieldInfoList = new List<MergeFieldInfo>();

            mergeFieldInfoList.Add(new MergeFieldInfo() { fieldName = "seafarerName", defaultValue = "Anbu" });
            mergeFieldInfoList.Add(new MergeFieldInfo() { fieldName = "crewCode", defaultValue = "CR-128933" });

            agreementCreationInfo = new AgreementCreationInfo
            {
                fileInfos = fileInfosList,
                name = "Demo Contract Sign 001",
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
            AdobeSignRestClient adobeSignRestClientInvalid = new AdobeSignRestClient(new AdobeSignConfiguration(),_clientFactory);
            Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => adobeSignRestClientInvalid.CreateAgreementAsync(agreementCreationInfo));
        }

        [Fact]
        public async Task TestAsync_InValidCase_AgreementInfo()
        {
            var fileInfosList = new List<FileInformation>();
            string contractDocumentId = "CBJCHBCAABAAuIj3UtlJc9u7_PKAdBAu3UqrGBXJWqrT";
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
