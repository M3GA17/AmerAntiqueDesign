using Shared.ValueObjects;
namespace Domain.ProductManagement.ValueObjects;
public class IdProductPhoto : BaseId<Guid>
{
    public IdProductPhoto()
        : base(Guid.NewGuid())
    {
        Value = Guid.NewGuid();
    }
    public IdProductPhoto(Guid value)
        : base(value)
    {
    }
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
