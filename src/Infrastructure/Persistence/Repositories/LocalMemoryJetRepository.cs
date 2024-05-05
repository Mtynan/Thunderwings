using Domain.Common;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence.Data;
using MediatR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class LocalMemoryJetRepository : ILocalMemoryJetRepository
    {
        private List<MilitaryJet> _jets;

        public LocalMemoryJetRepository(List<MilitaryJet>? seedData = null)
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


        public Task<(List<MilitaryJet> filteredJets, int totalCount)> GetJets(MilitaryJetFilter filter, int pageNumber, int pageSize)
        {
            var query = _jets.AsQueryable();
            query = ApplyFilter(query, filter);
            var totalCount = query.Count();
       
            var filterJets = query.Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
           
            return Task.FromResult((filterJets, totalCount));
        }

        private IQueryable<MilitaryJet> ApplyFilter(IQueryable<MilitaryJet> query, MilitaryJetFilter filter)
        {
            if (!string.IsNullOrWhiteSpace(filter.Name))
                query = query.Where(jet => jet.Name.Contains(filter.Name, StringComparison.CurrentCultureIgnoreCase));
            if (!string.IsNullOrWhiteSpace(filter.Manufacturer))
                query = query.Where(jet => jet.Manufacturer.Contains(filter.Manufacturer, StringComparison.CurrentCultureIgnoreCase));
            if (!string.IsNullOrWhiteSpace(filter.Country))
                query = query.Where(jet => jet.Country.Contains(filter.Country, StringComparison.CurrentCultureIgnoreCase));
            if (!string.IsNullOrWhiteSpace(filter.Role))
                query = query.Where(jet => jet.Role.Contains(filter.Role, StringComparison.CurrentCultureIgnoreCase));
            if (filter.TopSpeed.HasValue)
                query = query.Where(jet => jet.TopSpeed >= filter.TopSpeed.Value);
            if (filter.Price.HasValue)
                query = query.Where(jet => jet.Price >= filter.Price.Value);

            return query;
        }
    }
}
