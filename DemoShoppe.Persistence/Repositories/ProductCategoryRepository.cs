using DemoShoppe.Domain.Abstractions.Repositories;
using DemoShoppe.Domain.Entities;

namespace DemoShoppe.Persistence.Repositories
{
    public class ProductCategoryRepository : RepositoryBase<ProductCategory, Guid>, IProductCategoryRepository
    {
        public ProductCategoryRepository(AppDbContext dbContext)
            : base(dbContext)
        {

        }
    }
}
