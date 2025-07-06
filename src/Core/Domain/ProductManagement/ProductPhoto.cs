using Domain.ProductManagement.ValueObjects;
using Domain.UserManagement;
using Shared.Primitives;

namespace Domain.ProductManagement;

public class ProductPhoto : Entity<IdProductPhoto>
{
    public Product Product { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Url { get; set; } = null!;
    public bool IsMain { get; set; }
    public int DisplayOrder { get; set; }
    public virtual IdUser IdUserCreate { get; set; } = null!;
    public virtual IdUser? IdUserUpdate { get; set; }
    public ProductPhoto() : base(new IdProductPhoto())
    {
    }

    public static ProductPhoto Create(Product product, string name, string url, bool isMain, int displayOrder, IdUser isUserCreate, DateTimeOffset dateTimeOffset)
    {
        var productPhoto = new ProductPhoto
        {
            Product = product,
            Name = name,
            Url = url,
            IsMain = isMain,
            DisplayOrder = displayOrder,
            IdUserCreate = isUserCreate,
            DateCreate = dateTimeOffset
        };

        return productPhoto;
    }
}
