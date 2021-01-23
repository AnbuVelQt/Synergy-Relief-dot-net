using Synergy.ReliefCenter.Core.Models;
using System;

namespace Synergy.ReliefCenter.Data.Entities.SeafarerCenter
{
    public class Seafarer : EntityBase
    {
        public long NationalityId { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string CrewCode { get; set; }

        public string CdcNumber { get; set; }

        public string PlaceOfBirth { get; set; }

        public DateTime DateOfBirth { get; set; }

        public long? StateId { get; set; }

        public long RankId { get; set; }

        public string CdcWithoutCode { get; set; }

        public string IdentityUserId { get; set; }
    }
}
