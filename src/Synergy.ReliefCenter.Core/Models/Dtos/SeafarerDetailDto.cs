using System;

namespace Synergy.ReliefCenter.Core.Models.Dtos
{
    public class SeafarerDetailDto
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string CrewCode { get; set; }

        public string CDCNumber { get; set; }

        public string PassportNumber { get; set; }

        public string Rank { get; set; }

        public DateTime DateOfBirth { get; set; }

        public int Age { get; set; }

        public string PlaceOfBirth { get; set; }

        public string Nationality { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }
    }
}
