using DemoShoppe.Domain.Abstractions.Repositories;
using DemoShoppe.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DemoShoppe.Persistence.Repositories
{
    public class UserRepository : RepositoryBase<User, Guid>, IUserRepository
    {
        public UserRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
        public async Task<bool> CheckEmailExistAsync(string email, CancellationToken cancellationToken = default)
            => await _dbContext.Users.AnyAsync(x => x.Email == email, cancellationToken);

        public async Task<bool> CheckUsernameExistAsync(string username, CancellationToken cancellationToken = default)
            => await _dbContext.Users.AnyAsync(x => x.Username == username, cancellationToken);
    }
}
