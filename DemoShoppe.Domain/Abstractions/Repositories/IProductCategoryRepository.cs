using DemoShoppe.Domain.Entities;

namespace DemoShoppe.Domain.Abstractions.Repositories
{
    public interface IProductCategoryRepository : IRepositoryBase<ProductCategory, Guid>
    {
    }
}
