namespace DemoShoppe.Domain.Abstractions.Repositories
{
    public interface IRepositoryBase<TEntity, TKey> where TEntity : class
    {
        IQueryable<TEntity> FindQuery();
        IQueryable<TEntity> FindCommand();
        Task<int> TotalItems(CancellationToken cancellationToken = default);
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Remove(TEntity entity);
    }
}
