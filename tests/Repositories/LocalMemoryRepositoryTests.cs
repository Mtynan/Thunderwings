using Domain.Common;
using Domain.Entities;
using Infrastructure.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Thunderwings.UnitTests.Repositories
{
    public class LocalMemoryRepositoryTests
    {
        private LocalMemoryRepository _repo;
        [Fact]
        public async Task GetJets_ReturnsCorrectlyFilteredData()
        {
            // arrange
            var data = new List<MilitaryJet>
            {
                new MilitaryJet { Id = 1, Name = "matt", Manufacturer = "mt", Country = "USA", Role = "Fighter", TopSpeed = 100, Price = 150 },
                new MilitaryJet { Id = 2, Name = "matt1", Manufacturer = "mt1", Country = "UK", Role = "Fighter", TopSpeed = 100, Price = 120 },
                new MilitaryJet { Id = 3, Name = "matt2", Manufacturer = "mt2", Country = "France", Role = "Fighter", TopSpeed = 100, Price = 115 }
            };
            _repo = new LocalMemoryRepository(data);
            var filter = new MilitaryJetFilter { Country = "USA" };

            // act
            var (filteredJets, totalCount) = await _repo.GetJets(filter, 1, 10);

            // assert
            Assert.Single(filteredJets);
            Assert.Equal(1, totalCount);
            Assert.Equal(1, filteredJets[0].Id);
            Assert.Equal("matt", filteredJets[0].Name);
        }

        [Fact]
        public async Task GetJets_ApplyPaginationCorrectly()
        {
            // arrange
            var data = new List<MilitaryJet>
            {
                new MilitaryJet { Id = 1, Name = "matt", Manufacturer = "mt", Country = "USA", Role = "Fighter", TopSpeed = 100, Price = 150 },
                new MilitaryJet { Id = 2, Name = "matt1", Manufacturer = "mt1", Country = "UK", Role = "Fighter", TopSpeed = 100, Price = 120 },
                new MilitaryJet { Id = 3, Name = "matt2", Manufacturer = "mt2", Country = "France", Role = "Fighter", TopSpeed = 100, Price = 115 }
            };
            _repo = new LocalMemoryRepository(data);
            var filter = new MilitaryJetFilter(); 

            // act
            var (filteredJetsPage1, totalCount) = await _repo.GetJets(filter, 1, 2);
            var (filteredJetsPage2, _) = await _repo.GetJets(filter, 2, 2);

            // assert
            Assert.Equal(2, filteredJetsPage1.Count);
            Assert.Single(filteredJetsPage2); 
            Assert.Equal(3, totalCount); 
        }
    }
}
