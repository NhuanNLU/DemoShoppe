using DemoShoppe.Contract.Abstractions.Message;

namespace DemoShoppe.Contract.Services.Cart.Request
{
    public sealed record ClearCartCommand() : ICommand;
}
