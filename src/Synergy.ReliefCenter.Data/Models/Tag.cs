using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Synergy.ReliefCenter.Data.Models
{
    [Table("tags")]
    [Index(nameof(Name), Name = "index_tags_on_name", IsUnique = true)]
    public partial class Tag
    {
        public Tag()
        {
            Taggings = new HashSet<Tagging>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("name", TypeName = "character varying")]
        public string Name { get; set; }
        [Column("created_at")]
        public DateTime? CreatedAt { get; set; }
        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; }
        [Column("taggings_count")]
        public int? TaggingsCount { get; set; }

        [InverseProperty(nameof(Tagging.Tag))]
        public virtual ICollection<Tagging> Taggings { get; set; }
    }
}
