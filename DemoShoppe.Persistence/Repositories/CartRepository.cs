using DemoShoppe.Domain.Abstractions.Repositories;
using DemoShoppe.Domain.Entities;

namespace DemoShoppe.Persistence.Repositories
{
    public class CartRepository : RepositoryBase<Cart, Guid>, ICartRepository
    {
        public CartRepository(AppDbContext dbContext) : base(dbContext)
        {

        }
    }
}
