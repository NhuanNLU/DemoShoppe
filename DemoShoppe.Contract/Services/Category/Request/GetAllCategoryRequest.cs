using DemoShoppe.Contract.Abstractions.Message;
using DemoShoppe.Contract.Abstractions.Shared;
using DemoShoppe.Contract.Services.Category.Response;

namespace DemoShoppe.Contract.Services.Category.Request
{
    public class GetAllCategoryRequest : IQuery<PageResult<GetAllCategoryResponse>>
    {
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
