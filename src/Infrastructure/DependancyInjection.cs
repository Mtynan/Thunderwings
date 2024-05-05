using Domain.Interfaces;
using Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class DependancyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddSingleton<ILocalMemoryJetRepository, LocalMemoryJetRepository>();
            services.AddSingleton<ILocalMemoryBasketRepository, LocalMemoryBasketRepository>();

            return services;
        }
    }
}
