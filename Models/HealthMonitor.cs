using System;

namespace Restarter
{
    public class HealthMonitor
    {
        public int Id { get; set; }
        public string MonitorName { get; set; }
        // Format: "/home/test"
        public string RequestPath { get; set; }

        public bool IsPostMethod { get; set; }

        public string Form { get; set; }

        public int ExpectedStatusCode { get; set; }
        public string ExpectedContent { get; set; }
    }
}