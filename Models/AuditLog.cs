using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Restarter.Models
{
    public class AuditLog
    {
        [Key]
        public int Id { get; set; }

        public string Operator { get; set; }
        public DateTime EventTime { get; set; } = DateTime.Now;
        public string Action { get; set; }
    }
}
