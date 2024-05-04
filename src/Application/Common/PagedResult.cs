using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common
{
    public record PagedResult<T>(List<T> Items, int TotalItems, int PageNumber, int PageSize)
    {
        public int TotalPages => (int)Math.Ceiling(TotalItems / (double)PageSize);
    }
}
