using DemoShoppe.Domain.Abstractions;

namespace DemoShoppe.Domain.Entities
{
    public class Product : EntityAuditSortDeleteBase<int>
    {
        public int UserId { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public required decimal Price { get; set; }
        public required int Quantity { get; set; }

        public User User { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
        public ICollection<ProductCategory> ProductCategories { get; set; } = new List<ProductCategory>();
        public ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>();
        public ICollection<Cart> Carts { get; set; } = new List<Cart>();
    }
}
