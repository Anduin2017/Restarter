namespace Restarter.Models
{
    public class Server
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Architect { get; set; }
        public string VCPU { get; set; }
        public string Memory { get; set; }
        public string DiskSize { get; set; }
        public string InnerIP { get; set; }
        public string OutterIP { get; set; }
        public string Owner { get; set; }
        public string OS { get; set; }
        public string UsageA { get; set; }
        public string UsageB { get; set; }
        public string VMArchitect { get; set; }
        public string InDomainName { get; set; }
    }
}