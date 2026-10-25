namespace DemoShoppe.Contract.Services.Product.Response
{
    public sealed record UpdateProductByIdResponse(
        Guid id,
        string name,
        string? description,
        decimal price,
        int quantity);
}
