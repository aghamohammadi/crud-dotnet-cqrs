using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using FluentValidation;
using IbanNet.DependencyInjection.ServiceProvider;
using Mc2.CrudTest.Application.Common.Behaviors;
using MediatR;

namespace Mc2.CrudTest.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddIbanNet();
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                config.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));
            });

            return services;
        }
    }
}
