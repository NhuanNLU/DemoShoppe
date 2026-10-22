using DemoShoppe.Domain.Abstractions;

namespace DemoShoppe.Domain.Entities
{
    public class Cart : EntityAuditSortDeleteBase<int>
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public required int Quantity { get; set; }
        public User User { get; set; }
        public Product Product { get; set; }
    }
}
