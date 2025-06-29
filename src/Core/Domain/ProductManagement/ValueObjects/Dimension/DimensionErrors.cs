using Shared.Base;

namespace Domain.ProductManagement.ValueObjects;
public static class DimensionErrors
{
    public static readonly Error DimensionCannotBeZero =
        new("DimensionErrors.DimensionCannotBeZero", "The dimension value cannot be zero");
}
