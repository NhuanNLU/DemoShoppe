using DemoShoppe.Domain.Abstractions.Repositories;
using DemoShoppe.Domain.Entities;

namespace DemoShoppe.Persistence.Repositories
{
    public class ProductImageRepository : RepositoryBase<ProductImage, Guid>, IProductImageRepository
    {
        public ProductImageRepository(AppDbContext dbContext) : base(dbContext)
        {

        }
    }
}
