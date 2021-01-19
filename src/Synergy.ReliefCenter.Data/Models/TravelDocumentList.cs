using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Synergy.ReliefCenter.Data.Models
{
    [Table("travel_document_lists")]
    public partial class TravelDocumentList
    {
        public TravelDocumentList()
        {
            TravelDocuments = new HashSet<TravelDocument>();
        }

        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Column("name", TypeName = "character varying")]
        public string Name { get; set; }
        [Column("created_at", TypeName = "timestamp(6) without time zone")]
        public DateTime CreatedAt { get; set; }
        [Column("updated_at", TypeName = "timestamp(6) without time zone")]
        public DateTime UpdatedAt { get; set; }
        [Column("identifier", TypeName = "character varying")]
        public string Identifier { get; set; }

        [InverseProperty(nameof(TravelDocument.TravelDocumentList))]
        public virtual ICollection<TravelDocument> TravelDocuments { get; set; }
    }
}
