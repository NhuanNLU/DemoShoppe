using DemoShoppe.Domain.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DemoShoppe.Persistence.Repositories
{
    public class RepositoryBase<TEntity, TKey> :
        IRepositoryBase<TEntity, TKey>
        where TEntity : class
    {
        protected readonly AppDbContext _dbContext;
        protected RepositoryBase(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IQueryable<TEntity> FindQuery()
            => _dbContext.Set<TEntity>().AsNoTracking();
        public IQueryable<TEntity> FindCommand()
            => _dbContext.Set<TEntity>();
        public void Add(TEntity entity)
            => _dbContext.Set<TEntity>().Add(entity);
        public void Remove(TEntity entity)
            => _dbContext.Set<TEntity>().Remove(entity);

        public void Update(TEntity entity)
            => _dbContext.Set<TEntity>().Update(entity);

        public async Task<int> TotalItems(CancellationToken cancellationToken = default)
            => await _dbContext.Set<TEntity>().AsNoTracking().CountAsync(cancellationToken);
    }
}
