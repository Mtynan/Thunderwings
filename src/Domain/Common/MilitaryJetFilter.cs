using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common
{
    public sealed record MilitaryJetFilter(
    string? Name = null,
    string? Manufacturer = null,
    string? Country = null,
    string? Role = null,
    int? TopSpeed = null,
    int? Price = null);
}
