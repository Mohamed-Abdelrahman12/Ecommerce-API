using Ecom.Application.Features.Products.Dto;
using Ecom.Application.Features.Products.Queries.GetAllProducts;
using Ecom.Application.ValidationBehavior;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
namespace Ecom.Application.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
            });
            services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            return services;
        }
    }
}
