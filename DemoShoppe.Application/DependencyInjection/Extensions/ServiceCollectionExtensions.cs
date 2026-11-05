using Microsoft.Extensions.DependencyInjection;

namespace DemoShoppe.Application.DependencyInjection.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMediatR(this IServiceCollection services)
            => services.AddMediatR(options => options.RegisterServicesFromAssembly
                                                        (typeof(ServiceCollectionExtensions).Assembly));
    }
}
