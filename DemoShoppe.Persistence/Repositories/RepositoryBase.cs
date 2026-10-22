using DemoShoppe.Domain.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DemoShoppe.Persistence.Repositories
{
    public class RepositoryBase<TEntity, TKey> :
        IRepositoryBase<TEntity, TKey>
        where TEntity : class
    {
        private readonly AppDbContext _dbContext;
        public RepositoryBase(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<TEntity>> FindAll(CancellationToken cancellationToken = default)
            => await _dbContext.Set<TEntity>().ToListAsync(cancellationToken);

        public async Task<TEntity> FindById(TKey id, CancellationToken cancellationToken = default)
            => await _dbContext.Set<TEntity>().FindAsync(id, cancellationToken);
        public void Add(TEntity entity)
            => _dbContext.Set<TEntity>().Add(entity);
        public void Remove(TEntity entity)
            => _dbContext.Set<TEntity>().Remove(entity);

        public void Update(TEntity entity)
            => _dbContext.Set<TEntity>().Update(entity);
    }
}
