using DemoShoppe.Contract.Abstractions.Message;
using DemoShoppe.Contract.Services.Product.Response;

namespace DemoShoppe.Contract.Services.Product.Request
{
    public sealed record GetProductByIdQuery(Guid id) : 
        IQuery<GetProductByIdResponse>;
    
}
