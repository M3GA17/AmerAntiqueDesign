using Domain.ProductManagement.ValueObjects;
using Shared.Base.Validation;
using Shared.Primitives;
using Shared.ValueObjects;

namespace Domain.ProductManagement;

public class Product : AggregateRoot<IdProduct>
{
    public virtual SerialNumber SerialNumber { get; private set; } = null!;
    public virtual string Name { get; private set; } = null!;
    public virtual string? Description { get; private set; }
    public virtual Dimension Dimension { get; private set; } = null!;

    protected Product() : base(new IdProduct())
    { }

    public static Product Create(SerialNumber serialNumber, string name, string? description, Dimension dimension)
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
            Dimension = dimension,
        };

        return product;
    }

    public virtual void Update(SerialNumber serialNumber, string name, string description, Dimension dimension, bool isEnabled)
    {
        SerialNumber = serialNumber;
        Name = name;
        Description = description;
        Dimension = dimension;
    }
}

