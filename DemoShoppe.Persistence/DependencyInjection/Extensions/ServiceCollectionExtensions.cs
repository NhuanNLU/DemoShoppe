using DemoShoppe.Domain.Abstractions;
using DemoShoppe.Domain.Abstractions.Repositories;
using DemoShoppe.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DemoShoppe.Persistence.DependencyInjection.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddUnitOfWork(this IServiceCollection services)
            => services.AddScoped<IUnitOfWork, UnitOfWork>();
        public static IServiceCollection AddRepositoryBase(this IServiceCollection services)
            => services.AddScoped(typeof(IRepositoryBase<,>), typeof(RepositoryBase<,>));
        public static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
            => services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
    }
}
