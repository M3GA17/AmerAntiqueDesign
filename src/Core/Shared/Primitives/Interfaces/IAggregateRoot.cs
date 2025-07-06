namespace Shared.Primitives.Interfaces;

public interface IAggregateRoot
{
    public void IncrementVersion();
    public int DatabaseVersion { get; protected set; }
}
