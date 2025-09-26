using Shared.ValueObjects;
namespace Domain.ProductManagement.ValueObjects;
public class IdProduct : BaseId<Guid>
{
    public IdProduct()
        : base(Guid.NewGuid())
    {
        Value = Guid.NewGuid();
    }
    public IdProduct(Guid value)
        : base(value)
    {
    }
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
