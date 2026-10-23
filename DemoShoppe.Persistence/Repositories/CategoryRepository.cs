using DemoShoppe.Domain.Abstractions.Repositories;
using DemoShoppe.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DemoShoppe.Persistence.Repositories
{
    public class CategoryRepository : RepositoryBase<Category, Guid>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext dbContext) : base(dbContext)
        {

        }
        public async Task<bool> CategoryNameExists(string name)
            => await _dbContext.Categories.AnyAsync(x => x.Name == name);

        public async Task<int> CountCategory()
            => await _dbContext.Categories.CountAsync();
    }
}
