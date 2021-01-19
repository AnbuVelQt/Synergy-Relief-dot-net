using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Synergy.ReliefCenter.Data.Models
{
    [Table("travel_ticket_requests")]
    [Index(nameof(ReliefId), Name = "index_travel_ticket_requests_on_relief_id")]
    [Index(nameof(SeafarerId), Name = "index_travel_ticket_requests_on_seafarer_id")]
    public partial class TravelTicketRequest
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Column("seafarer_id")]
        public long SeafarerId { get; set; }
        [Column("relief_id")]
        public long ReliefId { get; set; }
        [Column("from_city", TypeName = "character varying")]
        public string FromCity { get; set; }
        [Column("to_city", TypeName = "character varying")]
        public string ToCity { get; set; }
        [Column("travel_date", TypeName = "date")]
        public DateTime? TravelDate { get; set; }
        [Column("travel_time")]
        public int? TravelTime { get; set; }
        [Column("email_status")]
        public int? EmailStatus { get; set; }
        [Column("created_by_id")]
        public DateTime? CreatedById { get; set; }
        [Column("created_by_name", TypeName = "character varying")]
        public string CreatedByName { get; set; }
        [Column("updated_by_id")]
        public DateTime? UpdatedById { get; set; }
        [Column("updated_by_name", TypeName = "character varying")]
        public string UpdatedByName { get; set; }
        [Column("created_at", TypeName = "timestamp(6) without time zone")]
        public DateTime CreatedAt { get; set; }
        [Column("updated_at", TypeName = "timestamp(6) without time zone")]
        public DateTime UpdatedAt { get; set; }
        [Column("email_failed_reason", TypeName = "character varying")]
        public string EmailFailedReason { get; set; }
        [Column("travel_mode")]
        public int? TravelMode { get; set; }
        [Column("pnr", TypeName = "character varying")]
        public string Pnr { get; set; }
        [Column("travel_duration", TypeName = "character varying")]
        public string TravelDuration { get; set; }
        [Column("number_of_stops")]
        public int? NumberOfStops { get; set; }
        [Column("flight_number", TypeName = "character varying")]
        public string FlightNumber { get; set; }
        [Column("departure_time")]
        public DateTime? DepartureTime { get; set; }
        [Column("email_sent_at")]
        public DateTime? EmailSentAt { get; set; }
        [Column("from_airport", TypeName = "character varying")]
        public string FromAirport { get; set; }
        [Column("to_airport", TypeName = "character varying")]
        public string ToAirport { get; set; }

        [ForeignKey(nameof(ReliefId))]
        [InverseProperty("TravelTicketRequests")]
        public virtual Relief Relief { get; set; }
    }
}
