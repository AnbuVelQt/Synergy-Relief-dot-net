namespace Synergy.ReliefCenter.Data.Entities.SeafarerCenter
{
    public class SeafarerContactDetails
    {
       
        public long Id { get; set; }
        public long SeafarerId { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public long? StateId { get; set; }
        public long? CountryId { get; set; }
        public string PinCode { get; set; }
        public string City { get; set; }

       // public Seafarer SeafarerDetails { get; set; }
    }
}
