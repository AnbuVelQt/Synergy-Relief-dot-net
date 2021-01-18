using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Synergy.ReliefCenter.Data.Models
{
    [Table("reliefs")]
    [Index(nameof(RelieverSeafarerId), Name = "index_reliefs_on_reliever_seafarer_id")]
    [Index(nameof(RelieverSfStatusCode), Name = "index_reliefs_on_reliever_sf_status_code")]
    [Index(nameof(RelievingSfStatusCode), Name = "index_reliefs_on_relieving_sf_status_code")]
    [Index(nameof(VesselContractId), Name = "index_reliefs_on_vessel_contract_id")]
    public partial class Relief
    {
        public Relief()
        {
            AgentLetters = new HashSet<AgentLetter>();
            RecommendationLists = new HashSet<RecommendationList>();
            ShortlistedSeafarers = new HashSet<ShortlistedSeafarer>();
            TravelDocuments = new HashSet<TravelDocument>();
            TravelTicketRequests = new HashSet<TravelTicketRequest>();
        }

        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Column("vessel_contract_id")]
        public long? VesselContractId { get; set; }
        [Column("reliever_seafarer_id")]
        public long? RelieverSeafarerId { get; set; }
        [Column("relieving_sf_status_code")]
        public long? RelievingSfStatusCode { get; set; }
        [Column("reliever_sf_status_code")]
        public long? RelieverSfStatusCode { get; set; }
        [Column("created_at", TypeName = "timestamp(6) without time zone")]
        public DateTime CreatedAt { get; set; }
        [Column("updated_at", TypeName = "timestamp(6) without time zone")]
        public DateTime UpdatedAt { get; set; }
        [Column("created_by_id", TypeName = "character varying")]
        public string CreatedById { get; set; }
        [Column("created_by_name", TypeName = "character varying")]
        public string CreatedByName { get; set; }
        [Column("updated_by_id", TypeName = "character varying")]
        public string UpdatedById { get; set; }
        [Column("updated_by_name", TypeName = "character varying")]
        public string UpdatedByName { get; set; }
        [Column("relief_state", TypeName = "character varying")]
        public string ReliefState { get; set; }
        [Column("reliever_travel_state", TypeName = "character varying")]
        public string RelieverTravelState { get; set; }
        [Column("documentation_state", TypeName = "character varying")]
        public string DocumentationState { get; set; }
        [Column("relieving_travel_state", TypeName = "character varying")]
        public string RelievingTravelState { get; set; }
        [Column("reason", TypeName = "character varying")]
        public string Reason { get; set; }
        [Column("deleted_at")]
        public DateTime? DeletedAt { get; set; }

        [ForeignKey(nameof(VesselContractId))]
        [InverseProperty("Reliefs")]
        public virtual VesselContract VesselContract { get; set; }
        [InverseProperty(nameof(AgentLetter.Relief))]
        public virtual ICollection<AgentLetter> AgentLetters { get; set; }
        [InverseProperty(nameof(RecommendationList.Relief))]
        public virtual ICollection<RecommendationList> RecommendationLists { get; set; }
        [InverseProperty(nameof(ShortlistedSeafarer.Relief))]
        public virtual ICollection<ShortlistedSeafarer> ShortlistedSeafarers { get; set; }
        [InverseProperty(nameof(TravelDocument.Relief))]
        public virtual ICollection<TravelDocument> TravelDocuments { get; set; }
        [InverseProperty(nameof(TravelTicketRequest.Relief))]
        public virtual ICollection<TravelTicketRequest> TravelTicketRequests { get; set; }
    }
}
