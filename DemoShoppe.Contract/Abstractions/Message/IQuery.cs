using DemoShoppe.Contract.Abstractions.Shared;
using MediatR;

namespace DemoShoppe.Contract.Abstractions.Message
{
    public interface IQuery<TResponse>: IRequest<Result<TResponse>>
    {
    }
}
