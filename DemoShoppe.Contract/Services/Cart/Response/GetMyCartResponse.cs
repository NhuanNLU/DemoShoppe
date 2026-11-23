namespace DemoShoppe.Contract.Services.Cart.Response
{
    public sealed record GetMyCartResponse(
        Guid id,
        Guid userId,
        Guid productId,
        int quantity,
        decimal price,
        string image);
}
