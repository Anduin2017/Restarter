using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Restarter.Models;

namespace Restarter
{
    public class HealthMonitor
    {
        public int Id { get; set; }
        [Required]
        public string MonitorName { get; set; }
        [Required]
        public string RequestPath { get; set; }

        public bool IsPostMethod { get; set; }
        public bool IsHttpsMethod { get; set; }

        public string Form { get; set; }
        public int Port { get; set; }
        [Required]
        public int ExpectedStatusCode { get; set; }
        [Required]
        public string ExpectedContent { get; set; }

        [InverseProperty(nameof(Server.Monitor))]
        public IEnumerable<Server> Servers { get; set; }
    }
}