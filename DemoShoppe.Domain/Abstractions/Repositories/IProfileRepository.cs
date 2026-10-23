using DemoShoppe.Domain.Entities;

namespace DemoShoppe.Domain.Abstractions.Repositories
{
    public interface IProfileRepository : IRepositoryBase<Profile, Guid>
    {
    }
}
