using Domain.ProductManagement.ValueObjects;
using Domain.UserManagement;
using Shared.Base.Validation;
using Shared.Primitives;
using Shared.ValueObjects;

namespace Domain.ProductManagement;

public class Product : AggregateRoot<IdProduct, IdUser>
{
    public virtual SerialNumber SerialNumber { get; private set; } = null!;
    public virtual string Name { get; private set; } = null!;
    public virtual string? Description { get; private set; }
    public virtual IdCategory IdCategory { get; private set; } = null!;
    public virtual ProductStatus ProductStatus { get; private set; } = null!;
    public virtual Dimension Dimension { get; private set; } = null!;
    public virtual ICollection<ProductPhoto> ProductPhotos { get; private set; } = [];

    protected Product() : base(new IdProduct())
    { }

    public static Product Create(SerialNumber serialNumber, string name, string? description, IdCategory idCategory,
                          ProductStatus productStatus, Dimension dimension, IdUser idUserCreate, DateTimeOffset dateCreate)
    {
        var validate = new ValidationExceptionCollection();

        if (string.IsNullOrWhiteSpace(name))
            validate.AddError(nameof(name), ValidationExceptionCode.ErrorProductNameCannotBeNull);
        if (name.Length > 200)
            validate.AddError(nameof(name), ValidationExceptionCode.ErrorProductNameTooLong);

        validate.TryThrow();

        var product = new Product
        {
            SerialNumber = serialNumber,
            Name = name,
            Description = description,
            IdCategory = idCategory,
            ProductStatus = productStatus,
            Dimension = dimension,
            IdUserCreate = idUserCreate,
            DateCreate = dateCreate
        };

        return product;
    }

    public virtual void Update(SerialNumber serialNumber, string name, string description, IdCategory idCategory,
                        ProductStatus productStatus, Dimension dimension, bool isEnabled, IdUser idUserUpdate, DateTimeOffset dateUpdate)
    {
        SerialNumber = serialNumber;
        Name = name;
        Description = description;
        IdCategory = idCategory;
        ProductStatus = productStatus;
        Dimension = dimension;
        IdUserUpdate = idUserUpdate;
        DateUpdate = dateUpdate;
    }

    public virtual void AddProductPhoto(string name, string url, bool isMain, int displayOrder, IdUser idUserUpdate, DateTimeOffset dateUpdate)
    {
        var productPhoto = ProductPhoto.Create(this, name, url, isMain, displayOrder, idUserUpdate, dateUpdate);




        ProductPhotos.Add(productPhoto);
        IdUserUpdate = idUserUpdate;
        DateUpdate = dateUpdate;
    }

    public virtual void UpdateProductPhoto(IdProductPhoto idProductPhoto, string name, string url, bool isMain, int displayOrder, IdUser idUserUpdate, DateTimeOffset dateUpdate)
    {
        var productPhoto = ProductPhotos.FirstOrDefault(p => p.Id.Equals(idProductPhoto));
        productPhoto.Update(name, url, isMain, displayOrder, idUserUpdate, dateUpdate);
        IdUserUpdate = idUserUpdate;
        DateUpdate = dateUpdate;
    }
}

