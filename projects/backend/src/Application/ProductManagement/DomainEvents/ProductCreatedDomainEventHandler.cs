using Domain.ProductManagement.DomainEvents;
using MediatR;

namespace Application.ProductManagement.DomainEvents;

public class ProductCreatedDomainEventHandler : INotificationHandler<ProductCreatedDomainEvent>
{
    public Task Handle(ProductCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Nuovo prodotto creato con ID: {notification.IdProduct}");
        return Task.CompletedTask;
    }
}
