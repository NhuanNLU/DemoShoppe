namespace DemoShoppe.Contract.Services.Product.Response
{
    public sealed record CreateProductResponse(
        Guid id,
        Guid userId,
        string name,
        string? description,
        decimal price,
        int quantity);
    
}
