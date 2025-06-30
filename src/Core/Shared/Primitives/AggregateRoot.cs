namespace Shared.Primitives;

public abstract class AggregateRoot<TId> : Entity<TId>
    where TId : ValueObject
{
    private readonly List<IDomainEvent> domainEvents = [];
    public virtual int DatabaseVersion { get; protected set; }

    protected AggregateRoot() : base()
    {
        DatabaseVersion = 0;
    }

    protected AggregateRoot(TId id) : base(id)
    {
        Id = id;
        DatabaseVersion = 0;
    }

    // Incrementa versione per optimistic concurrency
    public void IncrementVersion()
    {
        DatabaseVersion++;
    }


    // Domain events
    protected void RaiseDomainEvent(IDomainEvent domainEvent)
    {
        domainEvents.Add(domainEvent);
    }

    public void ClearDomainEvents()
    {
        domainEvents.Clear();
    }





    //// Metodo template per validazione
    //public virtual void Validate()
    //{
    //    var validationResult = ValidateInternal();
    //    if (!validationResult.IsValid)
    //    {
    //        throw new DomainValidationException(validationResult.Errors);
    //    }
    //}

    //protected abstract ValidationResult ValidateInternal();
}