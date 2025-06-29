using Domain.ProductManagement.ValueObjects;
using Shared.Primitives;

namespace Domain.ProductManagement;


public class ProductStatus : WellKnown<string, IdProductStatus, ProductStatus>
{
    public virtual string StatusName { get; protected set; }

    public ProductStatus(IdProductStatus idProductStatus, string statusName) : base(idProductStatus)
    {
        StatusName = statusName;
    }

    public static readonly ProductStatus Null = new(new IdProductStatus("PsNll"), "Null");
    public static readonly ProductStatus Draft = new(new IdProductStatus("PsDrf"), "Draft");
    public static readonly ProductStatus Repair = new(new IdProductStatus("PsRpr"), "Repair");
    public static readonly ProductStatus Inserted = new(new IdProductStatus("PsIns"), "Inserted");
    public static readonly ProductStatus Available = new(new IdProductStatus("PsAvl"), "Available");
    public static readonly ProductStatus Unavailable = new(new IdProductStatus("PsUnv"), "Unavailable");
    public static readonly ProductStatus Sold = new(new IdProductStatus("PsSld"), "Sold");
    public static readonly ProductStatus Archived = new(new IdProductStatus("PsAvl"), "Available");
}