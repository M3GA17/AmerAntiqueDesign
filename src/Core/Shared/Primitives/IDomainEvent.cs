namespace Shared.Primitives;

public interface IDomainEvent
{
    Guid EventId { get; }
    DateTime OccurredOn { get; }
}




//MemberCreateDomainEvent -- al passoto, è appena successo
//GathertinmgCreateDomainEvent