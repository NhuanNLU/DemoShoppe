using DemoShoppe.Application.Abstractions;
using DemoShoppe.Infrastructure.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace DemoShoppe.Infrastructure.DependencyInjection.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddJwtTokenService(this IServiceCollection services)
            => services.AddTransient<IJwtTokenService, JwtTokenService>();
    }
}
