using DemoShoppe.Contract.Abstractions.Message;
using DemoShoppe.Contract.Services.Product.Response;

namespace DemoShoppe.Contract.Services.Product.Request
{
    public sealed record UpdateProductByIdCommand(
        Guid id,
        string? name,
        string? description,
        decimal? price,
        int? quantity) : ICommand<UpdateProductByIdResponse>;
}
