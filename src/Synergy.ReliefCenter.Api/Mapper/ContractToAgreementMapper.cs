using AutoMapper;
using Synergy.AdobeSign.Models;
using Synergy.ReliefCenter.Core.Models.Dtos;
using Synergy.ReliefCenter.Services.Enums;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace Synergy.ReliefCenter.Api.Mapper
{
	public class ContractToAgreementMapper : ITypeConverter<ContractDTO, AgreementCreationInfo>
	{
        #region configuration
        private const string CONTRACT_DOC_ID_SECTION_LESS_ROWS = "AbodeSign:ContractDocumentId_v1.1";
        private const string CONTRACT_DOC_ID_SECTION_MORE_ROWS = "AbodeSign:ContractDocumentId_v1.2";
        private readonly IConfiguration _configuration;
        #endregion

        #region Adobe Merge Fields Name
        private const string ADOBE_FIELD_SEAFARER_NAME = "seafarerName";
        private const string ADOBE_FIELD_CREW_CODE = "crewCode";
        private const string ADOBE_FIELD_AGE = "age";
        private const string ADOBE_FIELD_DATE_OF_BIRTH = "dateOfBirth";
        private const string ADOBE_FIELD_PLACE_OF_BIRTH = "placeOfBirth";
        private const string ADOBE_FIELD_NATIONALITY = "nationality";
        private const string ADOBE_FIELD_PP_NO = "ppNo";
        private const string ADOBE_FIELD_CDC_NO = "cdcNo";
        private const string ADOBE_FIELD_RANK = "rank";
        private const string ADOBE_FIELD_SEAFARER_ADDRESS = "seafarerAddress";
        private const string ADOBE_FIELD_VESSEL_OWNER = "vesselOwner";
        private const string ADOBE_FIELD_EMPLOYMENT_AGENT = "employerAgent";
        private const string ADOBE_FIELD_DOC_HOLDER = "docHolder";
        private const string ADOBE_FIELD_VESSEL_NAME = "vesselName";
        private const string ADOBE_FIELD_IMO_NO = "imoNo";
        private const string ADOBE_FIELD_CBA = "cba";
        private const string ADOBE_FIELD_CURRENT_CBA_NO = "currentCBANo";
        private const string ADOBE_FIELD_PORT_OF_REGISTRY = "portOfRegistry";
        private const string ADOBE_FIELD_PLACE_OF_ENGAGEMENT = "placeOfEngagement";
        private const string ADOBE_FIELD_CONTRACT_TERM = "contractTerm";
        private const string ADOBE_FIELD_CONTRACT_START_DATE = "contractStartDate";
        private const string ADOBE_FIELD_CONTRACT_EXPIRY_DATE = "contractExpiryDate";
        private const string ADOBE_FIELD_IS_KIN_DETAILS_PROVIDED = "isKinDetailsProvided";
        private const string ADOBE_FIELD_IS_MEDICAL_CERTIFICATE_ISSUED = "isMedicalCertificateIssued";
        private const string ADOBE_FIELD_HEADER_CONTRACT_TITLE = "headerContractTitle";
        private const string ADOBE_FIELD_HEADER_EFFECTIVE_FROM = "headerEffectiveFrom";
        private const string ADOBE_FIELD_HEADER_LICENSE_NO = "headerLicenseNo";
        private const string ADOBE_FIELD_HEADER_COMPANY_NAME = "headerCompanyName";
        private const string ADOBE_FIELD_VERIFIED_BY = "verifiedBy";
        private const string ADOBE_FIELD_VERIFIED_ON = "verifiedOn";
        private const string ADOBE_FIELD_MONTHLY_WAGES_HEADER_1 = "monthlyWagesHeader1";
        private const string ADOBE_FIELD_MONTHLY_WAGES_VALUE_1 = "monthlyWagesValue1";
        private const string ADOBE_FIELD_MONTHLY_WAGES_HEADER_2 = "monthlyWagesHeader2";
        private const string ADOBE_FIELD_MONTHLY_WAGES_VALUE_2 = "monthlyWagesValue2";
        private const string ADOBE_FIELD_TOTAL_MONTHLY_WAGES = "totalMonthlyWages";
        private const string ADOBE_FIELD_MONTHLY_WAGES_HEADER = "monthlyWagesHeader";
        private const string ADOBE_FIELD_MONTHLY_WAGES_VALUE = "monthlyWagesValue";
        private const string ADOBE_FIELD_OTHER_EARNINGS_SNO = "otherEarningsSNo";
        private const string ADOBE_FIELD_OTHER_EARNINGS_TITLE = "otherEarningsTitle";
        private const string ADOBE_FIELD_OTHER_EARNINGS_AMOUNT = "otherEarningsAmount";
        private const string ADOBE_FIELD_OTHER_EARNINGS_DATE = "otherEarningsDate";
        private const string ADOBE_FIELD_DEDUCTIONS_SNO = "deductionsSNo";
        private const string ADOBE_FIELD_DEDUCTIONS_TITLE = "deductionsTitle";
        private const string ADOBE_FIELD_DEDUCTIONS_AMOUNT = "deductionsAmount";
        private const string ADOBE_FIELD_DEDUCTIONS_DATE = "deductionsDate";
        private const string ADOBE_FIELD_REVISED_SALARY_SNO = "revisedSalarySNo";
        private const string ADOBE_FIELD_REVISED_SALARY_EFF_FROM = "revisedSalaryEffectiveFrom";
        private const string ADOBE_FIELD_REVISED_SALARY_AMOUNT = "revisedSalaryAmount";
        private const string ADOBE_FIELD_REVISED_SALARY_REMARKS = "revisedSalaryRemarks";
        private const string ADOBE_VALUE_AGREEMENT_NAME = "Seaman's Employment Contract";
        private const string ADOBE_VALUE_PARTICIPANT_1 = "Participant 1";
        private const string ADOBE_VALUE_PARTICIPANT_2 = "Participant 2";
        private const string ADOBE_VALUE_HEADER_CONTRACT_TITLE = "Seaman's Employment Contract";
        private const string ADOBE_VALUE_HEADER_COMPANY_NAME = "Synergy Maritime Recruitment Services Pvt Ltd, Delhi.";
        private const string ADOBE_VALUE_MONTHLY_WAGES_HEADER_1 = "Basic Wages";
        private const string ADOBE_VALUE_MONTHLY_WAGES_HEADER_2 = "Special Allowance";
        #endregion

        public ContractToAgreementMapper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public AgreementCreationInfo Convert(ContractDTO contractData, AgreementCreationInfo agreementCreationInfo, ResolutionContext context)
		{
            var FormData = contractData.ContractForm.Data;
            var fileInfosList = new List<FileInformation>();

            var participantSetsInfoList = new List<ParticipantInfo>();
            var memberInfoListFleetHead = new List<MemberInfo>();
            memberInfoListFleetHead.Add(new MemberInfo { email = contractData.VerifierEmail });
            participantSetsInfoList.Add(new ParticipantInfo { memberInfos = memberInfoListFleetHead, order = 1, role = Enum.GetName<AdobeRoleEnum>(AdobeRoleEnum.SIGNER), label = ADOBE_VALUE_PARTICIPANT_1 });
            var memberInfoListSeafarer = new List<MemberInfo>();
            memberInfoListSeafarer.Add(new MemberInfo { email = FormData.SeafarerDetail.Email });
            participantSetsInfoList.Add(new ParticipantInfo { memberInfos = memberInfoListSeafarer, order = 1, role = Enum.GetName<AdobeRoleEnum>(AdobeRoleEnum.SIGNER), label = ADOBE_VALUE_PARTICIPANT_2 });
            var mergeFieldInfoList = new List<MergeFieldInfo>();

            mergeFieldInfoList.Add(new MergeFieldInfo() { fieldName = ADOBE_FIELD_SEAFARER_NAME, defaultValue = FormData.SeafarerDetail.Name });
            mergeFieldInfoList.Add(new MergeFieldInfo() { fieldName = ADOBE_FIELD_CREW_CODE, defaultValue = FormData.SeafarerDetail.CrewCode });
            mergeFieldInfoList.Add(new MergeFieldInfo() { fieldName = ADOBE_FIELD_AGE, defaultValue = FormData.SeafarerDetail.Age.ToString() });
            mergeFieldInfoList.Add(new MergeFieldInfo() { fieldName = ADOBE_FIELD_DATE_OF_BIRTH, defaultValue = convertDateString(FormData.SeafarerDetail.DateOfBirth) });
            mergeFieldInfoList.Add(new MergeFieldInfo() { fieldName = ADOBE_FIELD_PLACE_OF_BIRTH, defaultValue = FormData.SeafarerDetail.PlaceOfBirth });
            mergeFieldInfoList.Add(new MergeFieldInfo() { fieldName = ADOBE_FIELD_NATIONALITY, defaultValue = FormData.SeafarerDetail.Nationality });
            mergeFieldInfoList.Add(new MergeFieldInfo() { fieldName = ADOBE_FIELD_PP_NO, defaultValue = FormData.SeafarerDetail.PassportNumber });
            mergeFieldInfoList.Add(new MergeFieldInfo() { fieldName = ADOBE_FIELD_CDC_NO, defaultValue = FormData.SeafarerDetail.CDCNumber });
            mergeFieldInfoList.Add(new MergeFieldInfo() { fieldName = ADOBE_FIELD_RANK, defaultValue = FormData.SeafarerDetail.Rank });
            mergeFieldInfoList.Add(new MergeFieldInfo() { fieldName = ADOBE_FIELD_SEAFARER_ADDRESS, defaultValue = FormData.SeafarerDetail.Address });
            mergeFieldInfoList.Add(new MergeFieldInfo() { fieldName = ADOBE_FIELD_VESSEL_OWNER, defaultValue = FormData.VesselInfo.Owner });
            mergeFieldInfoList.Add(new MergeFieldInfo() { fieldName = ADOBE_FIELD_EMPLOYMENT_AGENT, defaultValue = FormData.VesselInfo.EmployerAgent });
            mergeFieldInfoList.Add(new MergeFieldInfo() { fieldName = ADOBE_FIELD_DOC_HOLDER, defaultValue = FormData.VesselInfo.MLCHolder });
            mergeFieldInfoList.Add(new MergeFieldInfo() { fieldName = ADOBE_FIELD_VESSEL_NAME, defaultValue = FormData.VesselInfo.Name });
            mergeFieldInfoList.Add(new MergeFieldInfo() { fieldName = ADOBE_FIELD_IMO_NO, defaultValue = FormData.VesselInfo.IMONumber.ToString() });
            mergeFieldInfoList.Add(new MergeFieldInfo() { fieldName = ADOBE_FIELD_CBA, defaultValue = FormData.VesselInfo.CBA });
            mergeFieldInfoList.Add(new MergeFieldInfo() { fieldName = ADOBE_FIELD_CURRENT_CBA_NO, defaultValue = FormData.VesselInfo.CBA });
            mergeFieldInfoList.Add(new MergeFieldInfo() { fieldName = ADOBE_FIELD_PORT_OF_REGISTRY, defaultValue = FormData.VesselInfo.PortOfRegistry });
            mergeFieldInfoList.Add(new MergeFieldInfo() { fieldName = ADOBE_FIELD_PLACE_OF_ENGAGEMENT, defaultValue = FormData.TravelInfo.PlaceOfEnagement });
            mergeFieldInfoList.Add(new MergeFieldInfo() { fieldName = ADOBE_FIELD_CONTRACT_TERM, defaultValue = FormData.TravelInfo.ContractTerms });
            mergeFieldInfoList.Add(new MergeFieldInfo() { fieldName = ADOBE_FIELD_CONTRACT_START_DATE, defaultValue = convertDateString(FormData.TravelInfo.StartDate) });
            mergeFieldInfoList.Add(new MergeFieldInfo() { fieldName = ADOBE_FIELD_CONTRACT_EXPIRY_DATE, defaultValue = convertDateString(FormData.TravelInfo.EndDate) });
            mergeFieldInfoList.Add(new MergeFieldInfo() { fieldName = ADOBE_FIELD_IS_KIN_DETAILS_PROVIDED, defaultValue = FormData.AttachmentDetail.NextOfKinFormAttached.ToString() });
            mergeFieldInfoList.Add(new MergeFieldInfo() { fieldName = ADOBE_FIELD_IS_MEDICAL_CERTIFICATE_ISSUED, defaultValue = FormData.AttachmentDetail.MedicalCertificateAttached.ToString() });

            //Need set these as dynamic after the fields ready
            mergeFieldInfoList.Add(new MergeFieldInfo() { fieldName = ADOBE_FIELD_HEADER_CONTRACT_TITLE, defaultValue = ADOBE_VALUE_HEADER_CONTRACT_TITLE });
            mergeFieldInfoList.Add(new MergeFieldInfo() { fieldName = ADOBE_FIELD_HEADER_EFFECTIVE_FROM, defaultValue = "Effective From: " });
            mergeFieldInfoList.Add(new MergeFieldInfo() { fieldName = ADOBE_FIELD_HEADER_LICENSE_NO, defaultValue = "RPS-License No: " });
            mergeFieldInfoList.Add(new MergeFieldInfo() { fieldName = ADOBE_FIELD_HEADER_COMPANY_NAME, defaultValue = ADOBE_VALUE_HEADER_COMPANY_NAME });
            mergeFieldInfoList.Add(new MergeFieldInfo() { fieldName = ADOBE_FIELD_VERIFIED_BY, defaultValue = contractData.VerifierName });
            if (contractData.VerifyDate != null)
                mergeFieldInfoList.Add(new MergeFieldInfo() { fieldName = ADOBE_FIELD_VERIFIED_ON, defaultValue = convertDateTimeString((DateTime)contractData.VerifyDate) });

            //Wages component table section
            mergeFieldInfoList.Add(new MergeFieldInfo() { fieldName = ADOBE_FIELD_MONTHLY_WAGES_HEADER_1, defaultValue = ADOBE_VALUE_MONTHLY_WAGES_HEADER_1 });
            mergeFieldInfoList.Add(new MergeFieldInfo() { fieldName = ADOBE_FIELD_MONTHLY_WAGES_VALUE_1, defaultValue = convertAmountString(FormData.Wages.BasicAmount) });
            mergeFieldInfoList.Add(new MergeFieldInfo() { fieldName = ADOBE_FIELD_MONTHLY_WAGES_HEADER_2, defaultValue = ADOBE_VALUE_MONTHLY_WAGES_HEADER_2 });
            mergeFieldInfoList.Add(new MergeFieldInfo() { fieldName = ADOBE_FIELD_MONTHLY_WAGES_VALUE_2, defaultValue = convertAmountString(FormData.Wages.SpecialAllownce) });
            mergeFieldInfoList.Add(new MergeFieldInfo() { fieldName = ADOBE_FIELD_TOTAL_MONTHLY_WAGES, defaultValue = convertAmountString(FormData.Wages.TotalMonthlyAmount) });

            int wageLastStaticRowNo = 2;
            if (FormData.Wages.OTRateCard != null)
            {
                wageLastStaticRowNo++;
                mergeFieldInfoList.Add(new MergeFieldInfo() { fieldName = ADOBE_FIELD_MONTHLY_WAGES_HEADER + wageLastStaticRowNo.ToString(), defaultValue = $"Fixed Overtime ({FormData.Wages.OTRateCard.MinHours} Hrs)" });
                mergeFieldInfoList.Add(new MergeFieldInfo() { fieldName = ADOBE_FIELD_MONTHLY_WAGES_VALUE + wageLastStaticRowNo.ToString(), defaultValue = convertAmountString(FormData.Wages.OTRateCard.TotalAmount) });
                wageLastStaticRowNo++;
                mergeFieldInfoList.Add(new MergeFieldInfo() { fieldName = ADOBE_FIELD_MONTHLY_WAGES_HEADER + wageLastStaticRowNo.ToString(), defaultValue = "Overtime Rate per Hour" });
                mergeFieldInfoList.Add(new MergeFieldInfo() { fieldName = ADOBE_FIELD_MONTHLY_WAGES_VALUE + wageLastStaticRowNo.ToString(), defaultValue = convertAmountString(FormData.Wages.OTRateCard.PerHourRate) });
            }

            foreach (var CBAEarning in FormData.Wages.CBAEarningComponents)
            {
                wageLastStaticRowNo++;
                mergeFieldInfoList.Add(new MergeFieldInfo() { fieldName = ADOBE_FIELD_MONTHLY_WAGES_HEADER + wageLastStaticRowNo.ToString(), defaultValue = CBAEarning.Name });
                mergeFieldInfoList.Add(new MergeFieldInfo() { fieldName = ADOBE_FIELD_MONTHLY_WAGES_VALUE + wageLastStaticRowNo.ToString(), defaultValue = convertAmountString(CBAEarning.Amount) });
            }

            int otherEarningsSNo = 0;
            foreach (var data in FormData.Wages.OtherEarningComponents)
            {
                otherEarningsSNo++;
                mergeFieldInfoList.Add(new MergeFieldInfo() { fieldName = ADOBE_FIELD_OTHER_EARNINGS_SNO + otherEarningsSNo.ToString(), defaultValue = otherEarningsSNo.ToString() });
                mergeFieldInfoList.Add(new MergeFieldInfo() { fieldName = ADOBE_FIELD_OTHER_EARNINGS_TITLE + otherEarningsSNo.ToString(), defaultValue = data.Name });
                mergeFieldInfoList.Add(new MergeFieldInfo() { fieldName = ADOBE_FIELD_OTHER_EARNINGS_AMOUNT + otherEarningsSNo.ToString(), defaultValue = convertAmountString(data.Amount) });
                mergeFieldInfoList.Add(new MergeFieldInfo() { fieldName = ADOBE_FIELD_OTHER_EARNINGS_DATE + otherEarningsSNo.ToString(), defaultValue = convertDateString(data.EffectiveDate) });
            }

            int deductionsSNo = 0;
            foreach (var data in FormData.Wages.DeductionComponents)
            {
                deductionsSNo++;
                mergeFieldInfoList.Add(new MergeFieldInfo() { fieldName = ADOBE_FIELD_DEDUCTIONS_SNO + deductionsSNo.ToString(), defaultValue = deductionsSNo.ToString() });
                mergeFieldInfoList.Add(new MergeFieldInfo() { fieldName = ADOBE_FIELD_DEDUCTIONS_TITLE + deductionsSNo.ToString(), defaultValue = data.Name });
                mergeFieldInfoList.Add(new MergeFieldInfo() { fieldName = ADOBE_FIELD_DEDUCTIONS_AMOUNT + deductionsSNo.ToString(), defaultValue = convertAmountString(data.Amount) });
                mergeFieldInfoList.Add(new MergeFieldInfo() { fieldName = ADOBE_FIELD_DEDUCTIONS_DATE + deductionsSNo.ToString(), defaultValue = convertDateString(data.EffectiveDate) });
            }

            int revisedSalarySNo = 0;
            foreach (var data in FormData.RevisedSalaries)
            {
                revisedSalarySNo++;
                mergeFieldInfoList.Add(new MergeFieldInfo() { fieldName = ADOBE_FIELD_REVISED_SALARY_SNO + revisedSalarySNo.ToString(), defaultValue = revisedSalarySNo.ToString() });
                mergeFieldInfoList.Add(new MergeFieldInfo() { fieldName = ADOBE_FIELD_REVISED_SALARY_EFF_FROM + revisedSalarySNo.ToString(), defaultValue = convertDateString(data.EffectiveFromDate) });
                mergeFieldInfoList.Add(new MergeFieldInfo() { fieldName = ADOBE_FIELD_REVISED_SALARY_AMOUNT + revisedSalarySNo.ToString(), defaultValue = convertAmountString(data.TotalMonthlyWage) });
                mergeFieldInfoList.Add(new MergeFieldInfo() { fieldName = ADOBE_FIELD_REVISED_SALARY_REMARKS + revisedSalarySNo.ToString(), defaultValue = data.ReasonForRevision });
            }

            string contractDocumentId = _configuration.GetSection(CONTRACT_DOC_ID_SECTION_LESS_ROWS).Value;
            if (FormData.Wages.OtherEarningComponents.Count > 5 || FormData.Wages.DeductionComponents.Count > 5 || wageLastStaticRowNo > 8)
            {   // default template v1.1 have only limited rows, so use this to have upto 7 rows.
                contractDocumentId = _configuration.GetSection(CONTRACT_DOC_ID_SECTION_MORE_ROWS).Value;
            }
            fileInfosList.Add(new FileInformation { libraryDocumentId = contractDocumentId });

            agreementCreationInfo = new AgreementCreationInfo
            {
                fileInfos = fileInfosList,
                name = ADOBE_FIELD_AGREEMENT_NAME,
                participantSetsInfo = participantSetsInfoList,
                signatureType = Enum.GetName<AdobeSignatureTypeEnum>(AdobeSignatureTypeEnum.ESIGN),
                state = Enum.GetName<AdobeStateEnum>(AdobeStateEnum.IN_PROCESS),
                mergeFieldInfo = mergeFieldInfoList
            };

            return agreementCreationInfo;
        }

        public static string convertDateString(DateTime dateToConvert)
        {
            string dateString = dateToConvert.ToString(), convertedDateString = "";
            if (dateString != "01-01-0001 00:00:00")
            {
                convertedDateString = dateToConvert.ToString("dd'/'MMM'/'yyyy");
            }
            return convertedDateString;
        }

        public static string convertDateTimeString(DateTime dateToConvert)
        {
            string dateString = dateToConvert.ToString(), convertedDateString = "";
            if (dateString != "01-01-0001 00:00:00")
            {
                convertedDateString = dateToConvert.ToString("dd'/'MMM'/'yyyy h:mm:ss tt K");
            }
            return convertedDateString;
        }

        public static string convertAmountString(decimal Amount)
        {
            return (Amount > 0 ? Amount.ToString("F") : "0.00");
        }

    }
}
