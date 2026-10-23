using MediatR;

namespace DemoShoppe.Contract.Abstractions.Message
{
    public interface IDomainEventHandler: INotificationHandler<IDomainEvent>
    {
    }
}
