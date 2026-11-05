using DemoShoppe.Contract.Abstractions.Message;
using DemoShoppe.Contract.Services.Profile.Response;

namespace DemoShoppe.Contract.Services.Profile.Request
{
    public sealed record GetMeQuery() : IQuery<GetMeResponse>;
}
