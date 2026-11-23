using DemoShoppe.Contract.Abstractions.Message;
using DemoShoppe.Contract.Services.Cart.Response;

namespace DemoShoppe.Contract.Services.Cart.Request
{
    public sealed record GetMyCartQuery() : IQuery<GetMyCartResponse>;
}
