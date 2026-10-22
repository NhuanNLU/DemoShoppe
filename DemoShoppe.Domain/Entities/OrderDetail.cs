namespace DemoShoppe.Domain.Entities
{
    public class OrderDetail
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public required int Quantity { get; set; }
        public string? Note { get; set; }
        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}
