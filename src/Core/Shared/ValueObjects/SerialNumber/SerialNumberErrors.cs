using Shared.Base.Validation;

namespace Shared.ValueObjects;
public static class SerialNumberErrors
{
    public static readonly Error Empty = new("SerialNumberErrors.Empty", "Serial Number can't be empty");
    public static readonly Error MaxLength = new("SerialNumberErrors.MaxLength", "Serial number exceeds maximum length");
}
