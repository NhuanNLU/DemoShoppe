using DemoShoppe.Domain.Abstractions;

namespace DemoShoppe.Domain.Entities
{
    public class Cart : EntityAuditSortDeleteBase<Guid>
    {
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }
        public required int Quantity { get; set; }
        public User User { get; set; }
        public Product Product { get; set; }
    }
}
