using Application.Common;
using Application.Features.MilitaryJets;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Thunderwings.IntegrationTests
{
    public class MilitaryJetsControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly WebApplicationFactory<Program> _factory;

        public MilitaryJetsControllerTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.AddSingleton<ILocalMemoryJetRepository>(new LocalMemoryJetRepository(GetTestData()));
                });
            });

            _client = _factory.CreateClient();
        }

        private List<MilitaryJet> GetTestData()
        {
            return new List<MilitaryJet>
        {
            new MilitaryJet { Id=1, Name = "Matt", Manufacturer = "mt", Country = "uk", Role = "Fighter", TopSpeed = 10, Price = 10000000 },
            new MilitaryJet {Id=2, Name = "LES", Manufacturer = "mt", Country = "uk", Role = "Fighter", TopSpeed = 10, Price = 10000000 }
        };
        }

        [Fact]
        public async Task GetJets_WithValidQuery_ReturnsOk()
        {
            // arrange
            var url = "/api/militaryjets?name=Matt";

            // act
            var response = await _client.GetAsync(url);

            // assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var content = await response.Content.ReadAsStringAsync();
            var jets = JsonConvert.DeserializeObject<PagedResult<MilitaryJetDto>>(content);
            Assert.NotNull(jets?.Items);
            Assert.NotEmpty(jets.Items);
            Assert.Single(jets.Items);
            Assert.Equal(1,jets.Items[0].Id);
        }

        [Fact]
        public async Task GetJets_NoMatchingJets_Returns204()
        {
            // arrange
            var url = "/api/militaryjets?name=asdasdasd";

            // act
            var response = await _client.GetAsync(url);

            // assert
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

        }
        [Fact]
        public async Task GetJets_WithValidationError_Returns400()
        {
            // arrange
            var url = "/api/militaryjets?name=Matt&topspeed=0";

            // act
            var response = await _client.GetAsync(url);

            // assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            var content = await response.Content.ReadAsStringAsync();
            Assert.Contains("Top speed must be greater than 0", content);
        }
    }
}
