using DemoShoppe.Contract.Abstractions.Message;
using DemoShoppe.Contract.Abstractions.Shared;
using DemoShoppe.Contract.Services.Product.Response;

namespace DemoShoppe.Contract.Services.Product.Request
{
    public sealed record GetMyProductsQuery
    (int pageSize, int pageIndex) : IQuery<PageResult<GetMyProductsResponse>>;
}
