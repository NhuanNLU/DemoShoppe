namespace DemoShoppe.Domain.Abstractions.Repositories
{
    public interface IRepositoryBase<TEntity, TKey> where TEntity : class
    {
        Task<TEntity> FindById(TKey id, CancellationToken cancellationToken = default);
        IQueryable<TEntity> FindAll();
        Task<int> TotalItems(CancellationToken cancellationToken = default);
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Remove(TEntity entity);
    }
}
