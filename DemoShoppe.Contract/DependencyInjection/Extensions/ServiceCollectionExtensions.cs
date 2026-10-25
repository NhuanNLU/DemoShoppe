using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace DemoShoppe.Contract.DependencyInjection.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddFluentValidation(this IServiceCollection services)
            => services.AddValidatorsFromAssembly(typeof(ServiceCollectionExtensions).Assembly);
        }

}
