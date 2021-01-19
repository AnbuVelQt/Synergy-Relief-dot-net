using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Synergy.ReliefCenter.Data.Models
{
    [Table("shore_employee_device_tokens")]
    [Index(nameof(ShoreEmployeeId), Name = "index_shore_employee_device_tokens_on_shore_employee_id")]
    public partial class ShoreEmployeeDeviceToken
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Column("device_token", TypeName = "character varying")]
        public string DeviceToken { get; set; }
        [Column("shore_employee_id", TypeName = "character varying")]
        public string ShoreEmployeeId { get; set; }
        [Column("created_at", TypeName = "timestamp(6) without time zone")]
        public DateTime CreatedAt { get; set; }
        [Column("updated_at", TypeName = "timestamp(6) without time zone")]
        public DateTime UpdatedAt { get; set; }
    }
}
