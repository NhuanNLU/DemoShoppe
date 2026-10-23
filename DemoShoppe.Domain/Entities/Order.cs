using DemoShoppe.Domain.Abstractions;
using DemoShoppe.Domain.Enum;

namespace DemoShoppe.Domain.Entities
{
    public class Order : EntityAuditSortDeleteBase<Guid>
    {
        public Guid UserId { get; set; }
        public required string OrderCode { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public required decimal Price { get; set; }
        public User User { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    }
}
