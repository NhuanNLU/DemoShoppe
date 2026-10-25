using DemoShoppe.Domain.Abstractions.Repositories;
using DemoShoppe.Domain.Entities;

namespace DemoShoppe.Persistence.Repositories
{
    public class ProductRepository : RepositoryBase<Product, Guid>, IProductRepository
    {
        public ProductRepository(AppDbContext dbContext) : base(dbContext)
        {

        }
    }
}
