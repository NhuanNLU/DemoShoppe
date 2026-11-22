using DemoShoppe.Domain.Entities;

namespace DemoShoppe.Domain.Abstractions.Repositories
{
    public interface ICartRepository : IRepositoryBase<Cart, Guid>
    {
    }
}
