using Mc2.CrudTest.Application.Common.Interfaces;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Mc2.CrudTest.Application;
using Mc2.CrudTest.Infrastructure;

namespace Mc2.CrudTest.AcceptanceTests.Drivers
{
    public class Driver 
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IConfiguration _configuration;

        public Driver()
        {

            var services = new ServiceCollection();
            _configuration = new ConfigurationBuilder()
           .AddJsonFile("appsettings.json")
           .Build();

           

            services.AddApplicationServices();
            services.AddInfrastructureServices(_configuration);
            _serviceProvider = services.BuildServiceProvider();
        }

        public IMediator GetMediator() => _serviceProvider.GetRequiredService<IMediator>();
        public IUnitOfWork GetDbContext() => _serviceProvider.GetRequiredService<IUnitOfWork>();

    }
}