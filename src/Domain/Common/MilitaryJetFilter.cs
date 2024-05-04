using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common
{
    public sealed record MilitaryJetFilter(string? Name, string? Manufacturer, string? Country, string? Role, int? TopSpeed, int? Price);
}
