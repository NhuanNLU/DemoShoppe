using DemoShoppe.Domain.Entities;

namespace DemoShoppe.Domain.Abstractions.Repositories
{
    public interface ICategoryRepository : IRepositoryBase<Category, Guid>
    {
        Task<bool> CategoryNameExists(string name);
        Task<int> CountCategory();
    }
}
