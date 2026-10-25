using DemoShoppe.Contract.Abstractions.Message;
using DemoShoppe.Contract.Services.Category.Response;

namespace DemoShoppe.Contract.Services.Category.Request
{
    public sealed record GetCategoryByIdCommand(Guid id)
        : IQuery<GetCategoryByIdResponse>;
}
