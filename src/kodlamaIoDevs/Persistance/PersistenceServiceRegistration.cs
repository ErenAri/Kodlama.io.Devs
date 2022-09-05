using Application.Services.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistance.Contexts;
using Persistance.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration congfiguration)
        {
            services.AddDbContext<BaseDbContexts>(options => options.UseSqlServer(
                congfiguration.GetConnectionString("KodlamaIoDevsConnectionString")));
            services.AddScoped<ILanguageRepository, LanguageRepository>();
            
            return services;
        }
    }
}
