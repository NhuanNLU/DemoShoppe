namespace DemoShoppe.Domain.Abstractions.Entities
{
    public interface IEntityBase<TKey>
    {
        TKey Id { get; set; }
    }
}
