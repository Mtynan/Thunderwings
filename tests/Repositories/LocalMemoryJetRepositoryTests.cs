using Domain.Common;
using Domain.Entities;
using Infrastructure.Persistence.Repositories;

namespace Thunderwings.UnitTests.Repositories
{
    public class LocalMemoryJetRepositoryTests
    {
        private LocalMemoryJetRepository _repo;
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
            _repo = new LocalMemoryJetRepository(data);
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
            _repo = new LocalMemoryJetRepository(data);
            var filter = new MilitaryJetFilter(); 

            // act
            var (filteredJetsPage1, totalCount) = await _repo.GetJets(filter, 1, 2);
            var (filteredJetsPage2, _) = await _repo.GetJets(filter, 2, 2);

            // assert
            Assert.Equal(2, filteredJetsPage1.Count);
            Assert.Single(filteredJetsPage2); 
            Assert.Equal(3, totalCount); 
        }

        [Fact]
        public async Task CalculateTotalPrice_ValidIds_ReturnsCorrectTotal()
        {
            // arrange
            var jetIds = new List<int> { 1, 2 };
            var data = new List<MilitaryJet>
            {
                new MilitaryJet { Id = 1, Name = "matt", Manufacturer = "mt", Country = "USA", Role = "Fighter", TopSpeed = 100, Price = 100 },
                new MilitaryJet { Id = 2, Name = "matt1", Manufacturer = "mt1", Country = "UK", Role = "Fighter", TopSpeed = 100, Price = 150 },
                new MilitaryJet { Id = 3, Name = "matt2", Manufacturer = "mt2", Country = "France", Role = "Fighter", TopSpeed = 100, Price = 115 }
            };
            _repo = new LocalMemoryJetRepository(data);

            // act
            var total = await _repo.CalculateTotalPrice(jetIds);

            // assert
            Assert.Equal(250, total);  
        }
    }
}
