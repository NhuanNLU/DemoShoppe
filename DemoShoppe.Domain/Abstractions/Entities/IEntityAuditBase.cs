namespace DemoShoppe.Domain.Abstractions.Entities
{
    public interface IEntityAuditBase<TKey>: 
        IEntityBase<TKey>, IAuditBase
    {
    }
}
