using Domain.Common;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ILocalMemoryJetRepository
    {
        Task<(List<MilitaryJet> filteredJets, int totalCount)> GetJets(MilitaryJetFilter filter, int pageNumber, int pageSize);
        Task<int> CalculateTotalPrice(IEnumerable<int> jetIds);
        bool Exists(int jetId);
    }
}
