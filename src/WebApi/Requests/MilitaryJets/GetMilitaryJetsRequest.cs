using System.Text.Json.Serialization;

namespace WebApi.Requests.MilitaryJets
{
    /// <summary>
    /// The request to fetch Military Jets.
    /// </summary>
    public sealed record GetMilitaryJetsRequest
    {
        /// <summary>
        /// The Name of the Jet.
        /// </summary>
        /// <example>F-22 Raptor</example>
        public string? Name { get; set; }
        /// <summary>
        /// The Manufacturer of the Jet.
        /// </summary>
        /// <example>Lockheed Martin</example>
        public string? Manufacturer { get; set; }
        /// <summary>
        /// The Country Where the Jet was made.
        /// </summary>
        /// <example>Russia</example>
        public string? Country { get; set; }
        /// <summary>
        /// The Role of the Jet.
        /// </summary>
        /// <example>Multirole fighter</example>
        public string? Role { get; set; }
        /// <summary>
        /// The minimum speed you want to filter by.
        /// </summary>
        /// <example>1520</example>
        public int? TopSpeed { get; set; }
        /// <summary>
        /// The minimum price you want to filter by.
        /// </summary>
        /// <example>70000000</example>
        public int? Price { get; set; }
        /// <summary>
        /// The page number of the results to fetch. If page number is not supplied 1 is default.
        /// </summary>
        /// <example>1</example>
        public int PageNumber { get; set; } = 1;
        /// <summary>
        /// The number of items to return per page. If not supplied, default is 10.
        /// </summary>
        /// <example>10</example>
        public int PageSize { get; set; } = 10;
    }
}
