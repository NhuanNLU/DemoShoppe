using DemoShoppe.Contract.Abstractions.Message;
using DemoShoppe.Contract.Services.Category.Response;

namespace DemoShoppe.Contract.Services.Category.Request
{
    public class GetCategoryByIdRequest : IQuery<GetCategoryByIdResponse>
    {
        public Guid Id { get; set; }
    }
}
