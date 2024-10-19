using Mc2.CrudTest.Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;
using Mc2.CrudTest.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Mc2.CrudTest.Infrastructure.Repositories;

namespace Mc2.CrudTest.Infrastructure
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
           

            var conn = configuration.GetConnectionString("DefaultConnection");

            // Add MSSQL DB Context
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(conn));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();




            return services;

        }

        public static void ApplyDatabaseMigrations(this IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                db.Database.Migrate();
            }
        }

    }

 
}
