using core.application.interfaces;
using infrastructure.persistance.contexts;
using infrastructure.persistance.repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace infrastructure.persistance
{
    public static class ServiceRegistration
    {
        public static void AddPersistanceInfrastructure(this IServiceCollection svc, IConfiguration config) 
        {
            svc.AddDbContext<AppDbContext>( opt => 
                opt.UseSqlServer(config.GetConnectionString("DefaultConnection"),
                m => m.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));

            svc.AddTransient<IUserRepository, UserRepository>();
        }
    }
}
