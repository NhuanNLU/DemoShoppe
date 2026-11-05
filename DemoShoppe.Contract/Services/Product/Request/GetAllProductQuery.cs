using DemoShoppe.Contract.Abstractions.Message;
using DemoShoppe.Contract.Abstractions.Shared;
using DemoShoppe.Contract.Services.Product.Response;

namespace DemoShoppe.Contract.Services.Product.Request
{
    public sealed record GetAllProductQuery(
        int pageIndex,
        int pageSize) : IQuery<PageResult<GetAllProductResponse>>;
}
