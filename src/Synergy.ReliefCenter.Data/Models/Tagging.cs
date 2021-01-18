using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Synergy.ReliefCenter.Data.Models
{
    [Table("taggings")]
    [Index(nameof(Context), Name = "index_taggings_on_context")]
    [Index(nameof(TagId), Name = "index_taggings_on_tag_id")]
    [Index(nameof(TaggableId), Name = "index_taggings_on_taggable_id")]
    [Index(nameof(TaggableType), Name = "index_taggings_on_taggable_type")]
    [Index(nameof(TaggerId), Name = "index_taggings_on_tagger_id")]
    [Index(nameof(TaggerId), nameof(TaggerType), Name = "index_taggings_on_tagger_id_and_tagger_type")]
    [Index(nameof(TagId), nameof(TaggableId), nameof(TaggableType), nameof(Context), nameof(TaggerId), nameof(TaggerType), Name = "taggings_idx", IsUnique = true)]
    [Index(nameof(TaggableId), nameof(TaggableType), nameof(TaggerId), nameof(Context), Name = "taggings_idy")]
    [Index(nameof(TaggableId), nameof(TaggableType), nameof(Context), Name = "taggings_taggable_context_idx")]
    public partial class Tagging
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("tag_id")]
        public int? TagId { get; set; }
        [Column("taggable_type", TypeName = "character varying")]
        public string TaggableType { get; set; }
        [Column("taggable_id")]
        public int? TaggableId { get; set; }
        [Column("tagger_type", TypeName = "character varying")]
        public string TaggerType { get; set; }
        [Column("tagger_id")]
        public int? TaggerId { get; set; }
        [Column("context")]
        [StringLength(128)]
        public string Context { get; set; }
        [Column("created_at")]
        public DateTime? CreatedAt { get; set; }

        [ForeignKey(nameof(TagId))]
        [InverseProperty("Taggings")]
        public virtual Tag Tag { get; set; }
    }
}
