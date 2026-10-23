namespace DemoShoppe.Domain.Entities
{
    public class OrderDetail
    {
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public required int Quantity { get; set; }
        public string? Note { get; set; }
        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}
