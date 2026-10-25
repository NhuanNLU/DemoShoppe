using DemoShoppe.Contract.Abstractions.Message;
using DemoShoppe.Contract.Abstractions.Shared;
using DemoShoppe.Contract.Services.Category.Response;

namespace DemoShoppe.Contract.Services.Category.Request
{
    public sealed record GetAllCategoryCommand(int pageIndex, int pageSize) :
        IQuery<PageResult<GetAllCategoryResponse>>;
}
