using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Synergy.ReliefCenter.Data.Models
{
    [Table("travel_documents")]
    [Index(nameof(ReliefId), Name = "index_travel_documents_on_relief_id")]
    [Index(nameof(SeafarerId), Name = "index_travel_documents_on_seafarer_id")]
    [Index(nameof(TravelDocumentListId), Name = "index_travel_documents_on_travel_document_list_id")]
    public partial class TravelDocument
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Column("seafarer_id")]
        public long SeafarerId { get; set; }
        [Column("relief_id")]
        public long ReliefId { get; set; }
        [Column("attachment_name", TypeName = "character varying")]
        public string AttachmentName { get; set; }
        [Column("attachment_url", TypeName = "character varying")]
        public string AttachmentUrl { get; set; }
        [Column("attachment_size", TypeName = "character varying")]
        public string AttachmentSize { get; set; }
        [Column("attachment_content_type", TypeName = "character varying")]
        public string AttachmentContentType { get; set; }
        [Column("travel_document_list_id")]
        public long TravelDocumentListId { get; set; }
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

        [ForeignKey(nameof(ReliefId))]
        [InverseProperty("TravelDocuments")]
        public virtual Relief Relief { get; set; }
        [ForeignKey(nameof(TravelDocumentListId))]
        [InverseProperty("TravelDocuments")]
        public virtual TravelDocumentList TravelDocumentList { get; set; }
    }
}
