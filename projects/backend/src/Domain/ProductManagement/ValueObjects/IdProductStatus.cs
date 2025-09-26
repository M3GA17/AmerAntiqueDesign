using Shared.ValueObjects;
namespace Domain.ProductManagement.ValueObjects;
public class IdProductStatus(string value) : BaseId<string>(value)
{
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
