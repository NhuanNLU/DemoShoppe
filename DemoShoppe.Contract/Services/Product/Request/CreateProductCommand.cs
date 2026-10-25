using DemoShoppe.Contract.Abstractions.Message;
using DemoShoppe.Contract.Services.Product.Response;

namespace DemoShoppe.Contract.Services.Product.Request
{
    public sealed record CreateProductCommand(
        Guid userId,
        Guid categoryId,
        string name,
        string? description,
        decimal price,
        int quantity) : ICommand<CreateProductResponse>;
}