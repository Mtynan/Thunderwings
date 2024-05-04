using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.Features.MilitaryJets
{
    public sealed record MilitaryJetDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public string Country { get; set; }
        public string Role { get; set; }  
        public int TopSpeed { get; set; }   
        public int Price { get; set; }
    }
}
