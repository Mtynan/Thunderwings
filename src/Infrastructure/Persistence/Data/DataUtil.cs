using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Data
{
    internal static class DataUtil
    {
        internal static List<MilitaryJet> SeedData()
        {
            try
            {
                var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                var fullPath = Path.Combine(baseDirectory, "Persistence", "Data", "Aircraft.json");
                string json = File.ReadAllText(fullPath);
                var jets = JsonSerializer.Deserialize<List<MilitaryJet>>(json) ?? [];
                int id = 1;
                foreach (var jet in jets)
                {
                    jet.Id = id++;
                }

                return jets;
            }
            catch (Exception ex)
            {
                return [];
            }       
        }
    }
}
