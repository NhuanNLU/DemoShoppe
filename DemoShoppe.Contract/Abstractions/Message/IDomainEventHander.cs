using MediatR;

namespace DemoShoppe.Contract.Abstractions.Message
{
    public interface IDomainEventHander: INotificationHandler<IDomainEvent>
    {
    }
}
