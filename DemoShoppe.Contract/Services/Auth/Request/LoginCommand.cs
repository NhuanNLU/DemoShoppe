using DemoShoppe.Contract.Abstractions.Message;
using DemoShoppe.Contract.Services.Auth.Response;

namespace DemoShoppe.Contract.Services.Auth.Request
{
    public sealed record LoginCommand
    (string email, string password) : ICommand<LoginResponse>;
}
