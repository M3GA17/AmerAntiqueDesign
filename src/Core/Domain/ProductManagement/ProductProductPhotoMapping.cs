using Domain.ProductManagement.ValueObjects;
using Shared.Primitives;

namespace Domain.ProductManagement;

public class ProductProductPhotoMapping : Entity<IdProduct, IdProductPhoto>
{
    public virtual Product Product { get; set; } = null!;
    public virtual ProductPhoto ProductPhoto { get; set; } = null!;
}
