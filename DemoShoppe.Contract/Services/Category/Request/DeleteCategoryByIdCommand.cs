using DemoShoppe.Contract.Abstractions.Message;

namespace DemoShoppe.Contract.Services.Category.Request
{
    public sealed record DeleteCategoryByIdCommand
        (Guid id) : ICommand;
}
