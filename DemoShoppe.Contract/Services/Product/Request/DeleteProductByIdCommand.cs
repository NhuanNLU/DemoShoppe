using DemoShoppe.Contract.Abstractions.Message;

namespace DemoShoppe.Contract.Services.Product.Request
{
    public sealed record DeleteProductByIdCommand
    (Guid id) : ICommand;
}
