using Domain.ProductManagement.ValueObjects;
using Shared.Base;

namespace Domain.ProductManagement.DomainEvents;

public sealed record ProductCreatedDomainEvent(IdProduct IdProduct) : IDomainEvent
{
}
