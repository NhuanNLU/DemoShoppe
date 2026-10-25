using DemoShoppe.Contract.Abstractions.Message;
using DemoShoppe.Contract.Services.Auth.Response;

namespace DemoShoppe.Contract.Services.Auth.Request
{
    public sealed record RegisterCommand
        (string email,
        string username,
        string passwordHash) : ICommand<RegisterResponse>;
}
