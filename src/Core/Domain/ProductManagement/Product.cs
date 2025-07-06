using Domain.ProductManagement.ValueObjects;
using Domain.UserManagement;
using Shared.Primitives;
using Shared.ValueObjects;

namespace Domain.ProductManagement;

public class Product : AggregateRoot<IdProduct>
{
    public virtual SerialNumber SerialNumber { get; set; } = null!;
    public virtual string Name { get; set; } = null!;
    public virtual string? Description { get; set; }
    public virtual IdCategory IdCategory { get; set; } = null!;
    //public virtual Category Category { get; set; } = null!;
    public virtual ProductStatus ProductStatus { get; set; } = null!;
    public virtual Dimension Dimension { get; set; } = null!;
    public virtual IdUser IdUserCreate { get; set; } = null!;
    public virtual IdUser? IdUserUpdate { get; set; }

    public Product() : base(new IdProduct())
    {
    }

    public static Product Create(SerialNumber serialNumber, string name, string? description, IdCategory idCategory,
                          ProductStatus productStatus, Dimension dimension, IdUser idUserCreate, DateTimeOffset dateCreate)
    {
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
}

