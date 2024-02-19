using Cine.Infrastructure.Persistence.Interfaces;
using Cine.Infrastructure.Persistence.Repositories;
using Cine.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cine.Infrastructure.Extensions
{
    public static class InjectionExtensions
    {
        public static IServiceCollection AddInjectionInfrastructure(this IServiceCollection services, IConfiguration configuration) 
        {
            var assembly = typeof(CineDbContext).Assembly.FullName;

            services.AddDbContext<CineDbContext>(
                    options => options.UseSqlServer(configuration.GetConnectionString("defaultConnection"), b=> b.MigrationsAssembly(assembly)
                ), ServiceLifetime.Transient);

            services.AddTransient<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}
