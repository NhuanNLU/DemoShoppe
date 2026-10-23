using DemoShoppe.Domain.Abstractions;

namespace DemoShoppe.Domain.Entities
{
    public class ProductImage : EntityAuditSortDeleteBase<Guid>
    {
        public Guid ProductId { get; set; }
        public required string ImageUrl { get; set; }
        public bool IsPrimary { get; set; }
        public Product Product { get; set; }
    }
}
