using DemoShoppe.Domain.Entities;

namespace DemoShoppe.Domain.Abstractions.Repositories
{
    public interface IUserRepository : IRepositoryBase<User, Guid>
    {
        Task<bool> CheckEmailExistAsync(string email, CancellationToken cancellationToken = default);
        Task<bool> CheckUsernameExistAsync(string username, CancellationToken cancellationToken = default);
    }
}
