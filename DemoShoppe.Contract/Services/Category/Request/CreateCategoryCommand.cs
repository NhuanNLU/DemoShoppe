using DemoShoppe.Contract.Abstractions.Message;
using DemoShoppe.Contract.Services.Category.Response;

namespace DemoShoppe.Contract.Services.Category.Request
{
    public sealed record CreateCategoryCommand
        (string name,
        string? description,
        Guid? parentId,
        string? imageUrl) : ICommand<CreateCategoryResponse>;
}
