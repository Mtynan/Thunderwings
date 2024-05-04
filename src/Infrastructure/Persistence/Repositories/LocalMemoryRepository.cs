using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence.Data;
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


        public Task<List<MilitaryJet>> GetJets()
        {
            return Task.FromResult(_jets);
        }
    }
}
