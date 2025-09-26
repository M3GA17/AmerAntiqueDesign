using Domain.ProductManagement.ValueObjects;
using Domain.UserManagement;
using Shared.Base.Validation;
using Shared.Primitives;

namespace Domain.ProductManagement;

public class ProductPhoto() : Entity<IdProductPhoto, IdUser>(new IdProductPhoto())
{
    public Product Product { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Url { get; set; } = null!;
    public bool IsMain { get; set; }
    public int DisplayOrder { get; set; }

    public static ProductPhoto Create(Product product, string name, string url, bool isMain, int displayOrder, IdUser isUserCreate, DateTimeOffset dateCreate)
    {
        var validate = new ValidationExceptionCollection();



        var productPhoto = new ProductPhoto
        {
            Product = product,
            Name = name,
            Url = url,
            IsMain = isMain,
            DisplayOrder = displayOrder,
            IdUserCreate = isUserCreate,
            DateCreate = dateCreate
        };

        return productPhoto;
    }

    public void Update(string name, string url, bool isMain, int displayOrder, IdUser idUserUpdate, DateTimeOffset dateUpdate)
    {
        Name = name;
        Url = url;
        IsMain = isMain;
        DisplayOrder = displayOrder;
        IdUserUpdate = idUserUpdate;
        DateUpdate = dateUpdate;
    }
}
