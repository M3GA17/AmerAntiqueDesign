using Domain.ProductManagement.ValueObjects;
using Domain.UserManagement;
using Shared.Base.Validation;
using Shared.Primitives;
using Shared.ValueObjects;

namespace Domain.ProductManagement;

public class Product : AggregateRoot<IdProduct>
{
    public virtual SerialNumber SerialNumber { get; private set; } = null!;
    public virtual string Name { get; private set; } = null!;
    public virtual string? Description { get; private set; }
    public virtual IdCategory IdCategory { get; private set; } = null!;
    public virtual ProductStatus ProductStatus { get; private set; } = null!;
    public virtual Dimension Dimension { get; private set; } = null!;
    public virtual IdUser IdUserCreate { get; private set; } = null!;
    public virtual IdUser? IdUserUpdate { get; private set; }
    public virtual ICollection<ProductPhoto> ProductPhotos { get; private set; } = [];

    protected Product() : base(new IdProduct())
    { }

    //public virtual void Validate()
    //{
    //    var validationResult = ValidateInternal();
    //    if (!validationResult.IsValid)
    //    {
    //        throw new DomainValidationException(validationResult.Errors);
    //    }
    //}


    public static Product Create(SerialNumber serialNumber, string name, string? description, IdCategory idCategory,
                          ProductStatus productStatus, Dimension dimension, IdUser idUserCreate, DateTimeOffset dateCreate)
    {
        var validation = new ValidationExceptionCollection();

        //if ()
        //{

        //}

        //ValidationError validation = Validate()


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

    public void UpdatePhoto(IdProductPhoto idProductPhoto, string name)
    {
        var productPhoto = ProductPhotos.FirstOrDefault(pp => pp.Id == idProductPhoto);
        productPhoto?.UpdateName(name);
    }
}

