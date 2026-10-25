using DemoShoppe.Contract.Abstractions.Message;
using DemoShoppe.Contract.Services.Category.Response;

namespace DemoShoppe.Contract.Services.Category.Request
{
    public sealed record UpdateCategoryByIdCommand(
        Guid id,
        string name,
        string? description,
        string? imageUrl) : ICommand<UpdateCategoryByIdResponse>;
}
