using System.Text.Json.Serialization;

namespace WebApi.Requests.MilitaryJets
{
    public sealed record GetMilitaryJetsRequest
    {
        public string? Name { get; set; }
        public string? Manufacturer { get; set; }
        public string? Country { get; set; }
        public string? Role { get; set; }
        public int? TopSpeed { get; set; }
        public int? Price { get; set; }
    }
}
