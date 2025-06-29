using Shared.Primitives;
using Shared.ValueObjects;
namespace Domain.ProductManagement.ValueObjects;
public class IdCategory : BaseId<Guid>
{
    public IdCategory()
        : base(Guid.NewGuid())
    {
        Value = Guid.NewGuid();
    }
    public IdCategory(Guid value)
        : base(value)
    {
    }
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
