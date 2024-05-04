using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class MilitaryJet : EntityBase
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("manufacturer")]
        public string Manufacturer { get; set; }
        [JsonPropertyName("country")]
        public string Country { get; set; }
        [JsonPropertyName("role")]
        public string Role { get; set; }
        [JsonPropertyName("topSpeed")]
        public int TopSpeed { get; set; }
        [JsonPropertyName("price")]
        public int Price { get; set; }
    }
}
