using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence.Data;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class LocalMemoryRepository : ILocalMemoryRepository
    {
        private List<MilitaryJet> _jets;

        public LocalMemoryRepository(List<MilitaryJet>? seedData = null)
        {
            if (seedData == null)
            {
                _jets = DataUtil.SeedData();
            }
            else
            {
                _jets = seedData;
            }
        }


        public Task<List<MilitaryJet>> GetJets(string? Name, string? Manufacturer, string? Country, string? Role, int? TopSpeed, int? Price)
        {
            var query = _jets.AsQueryable();

            if (!string.IsNullOrWhiteSpace(Name))
                query = query.Where(jet => jet.Name.Contains(Name, StringComparison.CurrentCultureIgnoreCase));
            if (!string.IsNullOrWhiteSpace(Manufacturer))
                query = query.Where(jet => jet.Manufacturer.Contains(Manufacturer, StringComparison.CurrentCultureIgnoreCase));
            if (!string.IsNullOrWhiteSpace(Country))
                query = query.Where(jet => jet.Country.Contains(Country, StringComparison.CurrentCultureIgnoreCase));
            if (!string.IsNullOrWhiteSpace(Role))
                query = query.Where(jet => jet.Role.Contains(Role, StringComparison.CurrentCultureIgnoreCase));
            if (TopSpeed.HasValue)
                query = query.Where(jet => jet.TopSpeed >= TopSpeed.Value);
            if (Price.HasValue)
                query = query.Where(jet => jet.Price >= Price.Value);
            return Task.FromResult(query.ToList());
        }
    }
}
