using Domain.ProductManagement.ValueObjects;
using Shared.Base.Interfaces;

namespace Domain.ProductManagement.DomainEvents;

public sealed record ProductCreatedDomainEvent(IdProduct IdProduct) : IDomainEvent
{
}
