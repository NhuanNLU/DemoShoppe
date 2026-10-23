using DemoShoppe.Domain.Abstractions.Repositories;
using DemoShoppe.Domain.Entities;

namespace DemoShoppe.Persistence.Repositories
{
    public class ProfileRepository : RepositoryBase<Profile, Guid>, IProfileRepository
    {
        public ProfileRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
